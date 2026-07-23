using SERVICIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GUI
{
    public partial class frmGestionProductos : System.Web.UI.Page
    {
        BLL.PRODUCTO gestorProducto = new BLL.PRODUCTO();
        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = SESSION_MANAGER.ObtenerInstancia().ObtenerUsuario();
            if (usuario == null || usuario.Permiso != "ADMIN")
            {
                Response.Redirect("frmMenúPrincipal.aspx");
            }

            if (!IsPostBack)
            {
                CargarTiposEnum();
                CargarGrillaProductos();
            }
        }

        private void CargarGrillaProductos()
        {
            var listaProductos = gestorProducto.ListarProductos();
            gvProductos.DataSource = listaProductos;
            gvProductos.DataBind();

            // Verificamos si hay al menos un producto con stock crítico para mostrar la alerta general
            bool hayStockBajo = listaProductos.Exists(p => p.StockActual <= p.StockMinimo);

            if (hayStockBajo)
            {
                lblAlertaStock.Text = "⚠️ Atención: Hay productos que se encuentran con stock mínimo o por debajo del límite.";
                lblAlertaStock.Visible = true;
            }
            else
            {
                lblAlertaStock.Visible = false;
            }
        }

        private void CargarTiposEnum()
        {
            ddlTipo.DataSource = Enum.GetValues(typeof(BE.TipoProducto));
            ddlTipo.DataBind();
        }

        protected void gvProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Extraemos el objeto de la fila actual
                BE.PRODUCTO prod = e.Row.DataItem as BE.PRODUCTO;

                if (prod != null)
                {
                    // Comparamos stock actual con el mínimo
                    if (prod.StockActual <= prod.StockMinimo)
                    {
                        // Pintamos toda la fila de naranja claro
                        e.Row.CssClass = "fila-alerta-stock";
                    }
                }
            }
        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int idProducto = Convert.ToInt32(e.CommandArgument);
                BE.PRODUCTO prod = gestorProducto.ObtenerPorId(idProducto);

                if (prod != null)
                {
                    hfIdProducto.Value = prod.Id.ToString();
                    txtNombre.Text = prod.Nombre;

                    // Seleccionar el tipo en el DropDownList de forma segura
                    if (ddlTipo.Items.FindByText(prod.Tipo) != null)
                        ddlTipo.SelectedValue = prod.Tipo;

                    txtPrecio.Text = prod.Precio.ToString("0.00");
                    txtStock.Text = prod.StockActual.ToString();
                    txtStockMinimo.Text = prod.StockMinimo.ToString();

                    // Configuramos vista de Edición
                    lblTituloForm.Text = "Modificar Producto";
                    btnGuardarCambios.Visible = true;      
                    btnGuardarCambios.Enabled = true;
                    btnRegistrarNuevo.Visible = false;     
                    btnCancelarEdicion.Visible = true;
                    lblMensaje.Text = "Editando el producto: " + prod.Nombre;
                    lblMensaje.ForeColor = System.Drawing.Color.Blue;
                }
            }
        }

        protected void btnModoNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            lblTituloForm.Text = "Registrar Nuevo Producto";

            btnGuardarCambios.Visible = false;     
            btnRegistrarNuevo.Visible = true;      
            btnCancelarEdicion.Visible = true;

            lblMensaje.Text = "Complete los datos para dar de alta un nuevo producto.";
            lblMensaje.ForeColor = System.Drawing.Color.DarkGreen;
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hfIdProducto.Value)) return;

                BE.PRODUCTO prod = new BE.PRODUCTO();
                prod.Id = Convert.ToInt32(hfIdProducto.Value);
                prod.Nombre = txtNombre.Text.Trim();
                prod.Tipo = ddlTipo.SelectedValue;
                prod.Precio = Convert.ToDecimal(txtPrecio.Text);
                prod.StockActual = Convert.ToInt32(txtStock.Text);
                prod.StockMinimo = Convert.ToInt32(txtStockMinimo.Text);

                gestorProducto.ActualizarProducto(prod);

                lblMensaje.Text = "¡Producto actualizado correctamente!";
                lblMensaje.ForeColor = System.Drawing.Color.Green;

                CargarGrillaProductos();
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
                BE.PRODUCTO nuevoProd = new BE.PRODUCTO();
                nuevoProd.Nombre = txtNombre.Text.Trim();
                nuevoProd.Tipo = ddlTipo.SelectedValue;
                nuevoProd.Precio = Convert.ToDecimal(txtPrecio.Text);
                nuevoProd.StockActual = Convert.ToInt32(txtStock.Text);
                nuevoProd.StockMinimo = Convert.ToInt32(txtStockMinimo.Text);

                gestorProducto.InsertarProducto(nuevoProd);

                lblMensaje.Text = "¡Nuevo producto registrado con éxito!";
                lblMensaje.ForeColor = System.Drawing.Color.Green;

                CargarGrillaProductos();
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
            hfIdProducto.Value = "";
            txtNombre.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            txtStockMinimo.Text = "";
            if (ddlTipo.Items.Count > 0) ddlTipo.SelectedIndex = 0;

            lblTituloForm.Text = "Gestión de Productos";
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