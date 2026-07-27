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
    public partial class frmBackup : System.Web.UI.Page
    {
        BACKUP_BLL gestorBackup = new BACKUP_BLL();
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
                CargarGrillaBackups();
            }
        }

        private void CargarGrillaBackups()
        {
            try
            {
                List<BE.BACKUP_INFO> lista = gestorBackup.ListarBackups();

                gvBackups.DataSource = lista;
                gvBackups.DataBind();
            }
            catch (Exception ex)
            {
                lblMensajeRestaurar.Text = "Error al cargar los respaldos: " + ex.Message;
                lblMensajeRestaurar.CssClass = "mensaje-error";
            }
        }

        protected void btnHacerBackup_Click(object sender, EventArgs e)
        {
            try
            {
                // Podés definir una ruta física por defecto en el servidor (ej: C:\BackupsPetShop\)
                // o armar un nombre automático con fecha y hora.

                string nombreArchivo = $"PetShop_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
                string directorio = @"C:\BackupsPetShop\";
                string rutaCompleta = directorio + nombreArchivo;

                gestorBackup.RealizarBackup(rutaCompleta);

                var usuarioActual = SESSION_MANAGER.ObtenerInstancia().ObtenerUsuario();
                gestorBitacora.RegistrarBitacora("El Webmaster " + usuarioActual.Nombre + " generó un backup exitoso en: " + rutaCompleta, 3);

                lblMensajeBackup.Text = "✔️ ¡Backup generado con éxito en: " + rutaCompleta + "!";
                lblMensajeBackup.CssClass = "mensaje-info";

                CargarGrillaBackups();
            }
            catch (Exception ex)
            {
                lblMensajeBackup.Text = "❌ Error al generar el backup: " + ex.Message;
                lblMensajeBackup.CssClass = "mensaje-error";
            }
        }

        protected void gvBackups_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RestaurarBD")
            {
                string rutaArchivoBackup = e.CommandArgument.ToString();

                try
                {
                    gestorBackup.RestaurarBaseDatos(rutaArchivoBackup);

                    var usuarioActual = SESSION_MANAGER.ObtenerInstancia().ObtenerUsuario();
                    gestorBitacora.RegistrarBitacora("El Webmaster " + usuarioActual.Nombre + " RESTAURÓ la base de datos desde el archivo: " + rutaArchivoBackup, 5);

                    lblMensajeRestaurar.Text = $"✔️ Base de datos restaurada con éxito desde: {rutaArchivoBackup}";
                    lblMensajeRestaurar.CssClass = "mensaje-info";
                }
                catch (Exception ex)
                {
                    lblMensajeRestaurar.Text = "❌ Error al restaurar la base de datos: " + ex.Message;
                    lblMensajeRestaurar.CssClass = "mensaje-error";
                }
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmMenúPrincipal.aspx");
        }
    }
}