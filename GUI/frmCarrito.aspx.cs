using SERVICIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GUI
{
    public partial class frmCarrito : System.Web.UI.Page
    {
        BLL.PEDIDO gestorPedido = new BLL.PEDIDO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SESSION_MANAGER.ObtenerInstancia().ObtenerUsuario() == null)
            {
                Response.Redirect("frmLogin.aspx");
            }

            if (!IsPostBack)
            {
                CargarCarrito();
            }
        }

        private void CargarCarrito()
        {
            BE.CARRITO carrito = Session["Carrito"] as BE.CARRITO;

            if (carrito == null || carrito.Items.Count == 0)
            {
                pnlConProductos.Visible = false;
                pnlCarritoVacio.Visible = true;
                btnConfirmarCompra.Visible = false; // Ocultamos confirmar si no hay nada
                lblTotalGeneral.Text = "0.00";
            }
            else
            {
                pnlConProductos.Visible = true;
                pnlCarritoVacio.Visible = false;
                btnConfirmarCompra.Visible = true;

                //Repeater
                rptCarrito.DataSource = carrito.Items;
                rptCarrito.DataBind();

                // Calculamos el total general sumando los subtotales de cada ítem
                decimal total = 0;
                foreach (var item in carrito.Items)
                {
                    item.CalcularSubTotal();
                    total += item.Subtotal;
                }
                lblTotalGeneral.Text = total.ToString("0.00");
            }
        }

        protected void btnConfirmarCompra_Click(object sender, EventArgs e)
        {
            BE.CARRITO carrito = Session["Carrito"] as BE.CARRITO;
            BE.USUARIO usuarioActual = SESSION_MANAGER.ObtenerInstancia().ObtenerUsuario();

            if (carrito != null && carrito.Items.Count > 0 && usuarioActual != null)
            {
                try
                {
                    //Armamos el objeto PEDIDO
                    BE.PEDIDO nuevoPedido = new BE.PEDIDO();
                    nuevoPedido.Cliente = usuarioActual;
                    nuevoPedido.Fecha = DateTime.Now;
                    nuevoPedido.Estado = "Pendiente"; //Queda pendiente para que el admin lo gestione

                    decimal totalPedido = 0;

                    foreach (var itemCarrito in carrito.Items)
                    {
                        itemCarrito.CalcularSubTotal();
                        totalPedido += itemCarrito.Subtotal;

                        BE.ITEM_CARRITO itemPedido = new BE.ITEM_CARRITO();
                        itemPedido.Producto = itemCarrito.Producto;
                        itemPedido.Cantidad = itemCarrito.Cantidad;
                        itemPedido.Subtotal = itemCarrito.Subtotal;
                        nuevoPedido.Items.Add(itemPedido);
                    }

                    nuevoPedido.MontoTotal = totalPedido;

                    //Persistimos el pedido en la base de datos                    
                    gestorPedido.Insertar(nuevoPedido);

                    //Limpiamos el carrito de la sesión tras la compra exitosa
                    Session["Carrito"] = null;

                    //Mostramos éxito y redirigimos (o bloqueamos botones)
                    pnlConProductos.Visible = false;
                    btnConfirmarCompra.Visible = false;
                    btnSeguirComprando.Text = "⬅️ Volver al Menú Principal";

                    lblMensaje.Text = "¡Compra confirmada con éxito! Tu pedido quedó en estado Pendiente de aprobación.";
                    lblMensaje.CssClass = "mensaje text-success";
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al procesar la compra: " + ex.Message;
                    lblMensaje.CssClass = "mensaje text-danger";
                }
            }
        }

        protected void btnSeguirComprando_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmMenúPrincipal.aspx");
        }

        protected void rptCarrito_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Quitar")
            {
                int idProductoQuitar = Convert.ToInt32(e.CommandArgument);
                BE.CARRITO carrito = Session["Carrito"] as BE.CARRITO;

                if (carrito != null)
                {
                    // Removemos el ítem de la lista del carrito
                    carrito.Items.RemoveAll(i => i.Producto.Id == idProductoQuitar);
                    Session["Carrito"] = carrito;

                    // Refrescamos la vista
                    CargarCarrito();
                    lblMensaje.Text = "Producto eliminado del carrito.";
                    lblMensaje.CssClass = "mensaje text-danger";
                }
            }
        }
    }
}