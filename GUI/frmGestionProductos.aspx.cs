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
                    // Rellenamos los campos de afuera del cuadrito con los datos actuales
                    hfIdProducto.Value = prod.Id.ToString();
                    txtNombre.Text = prod.Nombre;
                    txtTipo.Text = prod.Tipo;
                    txtPrecio.Text = prod.Precio.ToString("0.00");
                    txtStock.Text = prod.StockActual.ToString();

                    // Activamos el botón de guardar y cancelar
                    btnGuardarCambios.Enabled = true;
                    btnCancelarEdicion.Visible = true;
                    lblMensaje.Text = "Editando el producto: " + prod.Nombre;
                    lblMensaje.ForeColor = System.Drawing.Color.Blue;
                }
            }
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(hfIdProducto.Value)) return;

                BE.PRODUCTO prod = new BE.PRODUCTO();
                prod.Id = Convert.ToInt32(hfIdProducto.Value);
                prod.Nombre = txtNombre.Text.Trim();
                prod.Tipo = txtTipo.Text.Trim();
                prod.Precio = Convert.ToDecimal(txtPrecio.Text);
                prod.StockActual = Convert.ToInt32(txtStock.Text);

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

        protected void btnCancelarEdicion_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            lblMensaje.Text = "";
        }

        private void LimpiarFormulario()
        {
            hfIdProducto.Value = "";
            txtNombre.Text = "";
            txtTipo.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            btnGuardarCambios.Enabled = false;
            btnCancelarEdicion.Visible = false;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmMenúPrincipal.aspx");
        }
    }
}