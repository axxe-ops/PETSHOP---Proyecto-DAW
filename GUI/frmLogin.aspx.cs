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

                bool exito = SERVICIO.SESSION_MANAGER.Login(usuario);

                if (exito != false)
                {
                    Response.Redirect("MenuPrincipal.aspx");
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