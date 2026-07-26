using BE;
using SERVICIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GUI
{
    public partial class frmDigitosVerificadores : System.Web.UI.Page
    {
        SERVICIO.DIGITOSVERIFICADORES_BLL gestorDigitos = new DIGITOSVERIFICADORES_BLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            var usuarioActual = SESSION_MANAGER.ObtenerInstancia().ObtenerUsuario();
            if (usuarioActual == null || usuarioActual.Permiso != PERMISO.WEBMASTER)
            {
                Response.Redirect("frmMenúPrincipal.aspx");
            }

            if (!IsPostBack)
            {
                VerificarIntegridadSistema();
            }
        }

        private void VerificarIntegridadSistema()
        {
            // Llamamos al método global de la BLL que barre todo por nosotros
            List<string> errores = gestorDigitos.VerificarIntegridadSistema();

            if (errores.Count == 0)
            {
                pnlEstadoBD.CssClass = "panel-estado estado-ok";
                lblEstadoBD.Text = "✔️ Base de Datos Integra: No se han detectado modificaciones externas.";
                gvAlterados.DataSource = null;
                gvAlterados.DataBind();
            }
            else
            {
                pnlEstadoBD.CssClass = "panel-estado estado-error";
                lblEstadoBD.Text = "❌ ¡ALERTA DE SEGURIDAD! Se detectaron alteraciones directas en la Base de Datos.";

                // Transformamos los errores en una lista de objetos estructurados para la tabla
                var listaErroresFormateada = errores.Select(e => {
                    // Acá podemos separar el string o armar la estructura limpiamente
                    return new
                    {
                        Tabla = "USUARIO",
                        IdFila = e.Contains("ID") ? e.Split(' ')[3].Replace(":", "") : "Global",
                        Incoherencia = e
                    };
                }).ToList();

                gvAlterados.DataSource = listaErroresFormateada;
                gvAlterados.DataBind();
            }
        }

        protected void btnRecalcular_Click(object sender, EventArgs e)
        {
            //Recalcular Digitos Verificadores

            try
            {
                // 1. Ejecutamos el recálculo masivo en la BLL
                gestorDigitos.RecalcularDigitosSistema();

                // 2. Volvemos a verificar el estado para actualizar los colores y limpiar la grilla automáticamente
                VerificarIntegridadSistema();

            }
            catch (Exception ex)
            {
                // Manejo básico de error por si falla la actualización
                pnlEstadoBD.CssClass = "panel-estado estado-error";
                lblEstadoBD.Text = "❌ Error al recalcular los dígitos: " + ex.Message;
            }
        }

        protected void btnRestaurar_Click(object sender, EventArgs e)
        {
            //Restaurar Base de Datos


        }

        protected void btnVerificar_Click(object sender, EventArgs e)
        {
            //Verificar Integridad 

            VerificarIntegridadSistema();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmMenúPrincipal.aspx");
        }



    }
}