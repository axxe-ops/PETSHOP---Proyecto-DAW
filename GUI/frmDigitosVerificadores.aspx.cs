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
        SERVICIO.BACKUP_BLL gestorBackup = new SERVICIO.BACKUP_BLL();
        SERVICIO.BITACORA_BLL gestorBitacora = new BITACORA_BLL();

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

                btnVolver.Visible = true;
            }
            else
            {
                pnlEstadoBD.CssClass = "panel-estado estado-error";
                lblEstadoBD.Text = "❌ ¡ALERTA DE SEGURIDAD! Se detectaron alteraciones directas en la Base de Datos.";

                string detalleErrores = string.Join(" | ", errores);
                gestorBitacora.RegistrarBitacora("⚠️ ALERTA DE SEGURIDAD: Se detectaron inconsistencias en los Dígitos Verificadores. Errores: " + detalleErrores, 5);

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

                btnVolver.Visible = false;
            }
        }

        protected void btnRecalcular_Click(object sender, EventArgs e)
        {
            //Recalcular Digitos Verificadores

            try
            {
                gestorDigitos.RecalcularDigitosSistema();

                BE.USUARIO usuarioActual = SESSION_MANAGER.ObtenerInstancia().ObtenerUsuario();
                gestorBitacora.RegistrarBitacora("El Webmaster " + usuarioActual.Nombre + " ejecutó el recálculo masivo de Dígitos Verificadores.", 4);

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
            try
            {
                var listaBackups = gestorBackup.ListarBackups();

                if (listaBackups == null || listaBackups.Count == 0)
                {
                    return;
                }

                string rutaUltimoBackup = listaBackups[0].RutaArchivo;

                gestorBackup.RestaurarBaseDatos(rutaUltimoBackup);
                gestorDigitos.RecalcularDigitosSistema();

                BE.USUARIO usuarioActual = SESSION_MANAGER.ObtenerInstancia().ObtenerUsuario();
                gestorBitacora.RegistrarBitacora("El Webmaster " + usuarioActual.Nombre + " restauró la base de datos automáticamente desde el panel de Dígitos Verificadores usando: " + rutaUltimoBackup, 5);

                lblEstadoBD.Text = "✔️ ¡Base de datos restaurada con éxito utilizando el último backup!";
                pnlEstadoBD.CssClass = "panel-estado alerta-exito"; // Ajusta a tu clase CSS de éxito si la tienes

            }
            catch (Exception ex)
            {
                lblEstadoBD.Text = "❌ Error crítico al restaurar la base de datos: " + ex.Message;
                pnlEstadoBD.CssClass = "panel-estado alerta-error";
            }
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