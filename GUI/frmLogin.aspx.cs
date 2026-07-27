using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GUI
{
    public partial class Default : System.Web.UI.Page
    {
        SERVICIO.DIGITOSVERIFICADORES_BLL gestorDigitos = new SERVICIO.DIGITOSVERIFICADORES_BLL();
        SERVICIO.BITACORA_BLL gestorBitacora = new SERVICIO.BITACORA_BLL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtPassword.Text))
                {
                    lblMensaje.Text = "Por favor, complete todos los campos.";
                    return;
                }

                USUARIO usuario = new USUARIO();
                usuario.Nombre = txtUsuario.Text.Trim();        // .Trim() elimina espacios invisibles
                usuario.Password = txtPassword.Text.Trim();

                //Verificamos primero si la base tiene inconsistencias
                var inconsistencias = gestorDigitos.VerificarIntegridadSistema();

                if (inconsistencias != null && inconsistencias.Count > 0)
                {
                    BE.USUARIO usuarioValidado = SERVICIO.SESSION_MANAGER.ValidarUsuario(usuario);

                    if (usuarioValidado != null && usuarioValidado.Permiso == PERMISO.WEBMASTER)
                    {                        
                        bool credencialesValidas = SERVICIO.SESSION_MANAGER.Login(usuario);

                        if (credencialesValidas)
                        {
                            gestorBitacora.RegistrarBitacora("El Webmaster " + usuario.Nombre + " ingresó al sistema con advertencia de base de datos alterada.", 4);

                            Response.Redirect("frmDigitosVerificadores.aspx", false);
                            Context.ApplicationInstance.CompleteRequest();
                            return;
                        }
                    }

                    // Si no es el Webmaster (o puso mal la contraseña con la base rota).
                    gestorBitacora.RegistrarBitacora("Intento de acceso bloqueado para el usuario '" + usuario.Nombre + "' debido a inconsistencia de datos en la BD.", 3);

                    Session.Clear();
                    lblMensaje.Text = "⚠️ Sistema bloqueado por inconsistencia de datos. Solo el Webmaster puede ingresar.";
                    return;
                }

                bool exito = SERVICIO.SESSION_MANAGER.Login(usuario);

                if (exito != false)
                {
                    Response.Redirect("frmMenúPrincipal.aspx");
                }
                else
                {
                    lblMensaje.Text = "Usuario o contraseña incorrectos.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Ocurrió un error al intentar ingresar. Intente más tarde.";                
            }
        }
    }
}