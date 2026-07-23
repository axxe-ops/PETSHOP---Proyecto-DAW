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
    public partial class frmGestionUsuarios : System.Web.UI.Page
    {
        BLL.USUARIO gestorUsuario = new BLL.USUARIO();
        protected void Page_Load(object sender, EventArgs e)
        {
            var usuarioActual = SESSION_MANAGER.ObtenerInstancia().ObtenerUsuario();
            if (usuarioActual == null || usuarioActual.Permiso != "ADMIN")
            {
                Response.Redirect("frmMenúPrincipal.aspx");
            }

            if (!IsPostBack)
            {
                CargarGrillaUsuarios();

                ddlPermiso.Items.Clear();
                ddlPermiso.Items.Add(new ListItem("Administrador", PERMISO.ADMIN));
                ddlPermiso.Items.Add(new ListItem("Webmaster", PERMISO.WEBMASTER));
                ddlPermiso.Items.Add(new ListItem("Usuario", PERMISO.USUARIO));
            }
        }

        private void CargarGrillaUsuarios()
        {
            gvUsuarios.DataSource = gestorUsuario.Listar(); //
            gvUsuarios.DataBind();
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idUsuario = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Seleccionar")
            {
                BE.USUARIO usu = gestorUsuario.ObtenerPorId(idUsuario); //

                if (usu != null)
                {
                    hfIdUsuario.Value = usu.Id.ToString();
                    txtNombre.Text = usu.Nombre;
                    txtPassword.Text = usu.Password;

                    if (ddlPermiso.Items.Count == 0)
                    {
                        ddlPermiso.Items.Clear();
                        ddlPermiso.Items.Add(new ListItem("Administrador", PERMISO.ADMIN));
                        ddlPermiso.Items.Add(new ListItem("Webmaster", PERMISO.WEBMASTER));
                        ddlPermiso.Items.Add(new ListItem("Usuario", PERMISO.USUARIO));
                    }

                    // Validamos que el valor exista en la lista antes de asignarlo para evitar el error
                    ListItem item = ddlPermiso.Items.FindByValue(usu.Permiso);
                    if (item != null)
                    {
                        ddlPermiso.SelectedValue = usu.Permiso;
                    }
                    else
                    {
                        ddlPermiso.SelectedIndex = 0; // Si no coincide, selecciona el primero por defecto
                    }


                    txtEmail.Text = usu.Email;
                    txtTelefono.Text = usu.Telefono;

                    lblTituloForm.Text = "Modificar Usuario";
                    btnGuardarCambios.Visible = true;
                    btnRegistrarNuevo.Visible = false;
                    btnCancelarEdicion.Visible = true;
                    lblMensaje.Text = "Editando al usuario: " + usu.Nombre;
                    lblMensaje.ForeColor = System.Drawing.Color.Blue;
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    gestorUsuario.Eliminar(idUsuario);
                    lblMensaje.Text = "¡Usuario eliminado con éxito!";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    CargarGrillaUsuarios();
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al eliminar usuario: " + ex.Message;
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void btnModoNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            lblTituloForm.Text = "Registrar Nuevo Usuario";
            btnGuardarCambios.Visible = false;
            btnRegistrarNuevo.Visible = true;
            btnCancelarEdicion.Visible = true;
            lblMensaje.Text = "Complete los datos para dar de alta un nuevo usuario.";
            lblMensaje.ForeColor = System.Drawing.Color.DarkGreen;
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hfIdUsuario.Value)) return;

                BE.USUARIO usu = new BE.USUARIO();
                usu.Id = Convert.ToInt32(hfIdUsuario.Value);
                usu.Nombre = txtNombre.Text.Trim();

                if (!string.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    usu.Password = SEGURIDAD.ENCRIPTADO.Hashear(txtPassword.Text.Trim());
                }
                else
                {
                    var usuarioActual = gestorUsuario.ObtenerPorId(usu.Id);
                    usu.Password = usuarioActual.Password;
                }

                usu.Permiso = ddlPermiso.SelectedValue;
                usu.Email = txtEmail.Text.Trim();
                usu.Telefono = txtTelefono.Text.Trim();

                gestorUsuario.ActualizarUsuario(usu);

                lblMensaje.Text = "¡Usuario actualizado correctamente!";
                lblMensaje.ForeColor = System.Drawing.Color.Green;

                CargarGrillaUsuarios();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al actualizar: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnRegistrarNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                BE.USUARIO nuevoUsu = new BE.USUARIO();
                nuevoUsu.Nombre = txtNombre.Text.Trim();
                nuevoUsu.Password = SEGURIDAD.ENCRIPTADO.Hashear(txtPassword.Text.Trim());
                nuevoUsu.Permiso = ddlPermiso.SelectedValue;
                nuevoUsu.Email = txtEmail.Text.Trim();
                nuevoUsu.Telefono = txtTelefono.Text.Trim();

                gestorUsuario.Insertar(nuevoUsu);

                lblMensaje.Text = "¡Nuevo usuario registrado con éxito!";
                lblMensaje.ForeColor = System.Drawing.Color.Green;

                CargarGrillaUsuarios();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al registrar: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnCancelarEdicion_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            lblMensaje.Text = "";
        }

        private void LimpiarFormulario()
        {
            hfIdUsuario.Value = "";
            txtNombre.Text = "";
            txtPassword.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
            ddlPermiso.SelectedIndex = 0;

            lblTituloForm.Text = "Gestión de Usuario";
            btnGuardarCambios.Visible = false;
            btnRegistrarNuevo.Visible = false;
            btnCancelarEdicion.Visible = false;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmMenúPrincipal.aspx");
        }


    }
}