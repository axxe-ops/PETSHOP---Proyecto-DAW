using SERVICIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GUI
{
    public partial class MenúPrincipal : System.Web.UI.Page
    {
        BLL.PRODUCTO gestorProducto = new BLL.PRODUCTO();
        protected void Page_Load(object sender, EventArgs e)
        {
            BE.USUARIO usuarioActual = SESSION_MANAGER.ObtenerInstancia().ObtenerUsuario();

            // Verificamos si hay sesión (para proteger la página)
            if (usuarioActual == null)
            {
                Response.Redirect("frmLogin.aspx");
            }

            if (!IsPostBack)
            {
                lblUsuarioLogueado.Text = usuarioActual.Nombre;
                CargarCatalogo();
            }
        }
        private void CargarCatalogo()
        {
            List<BE.PRODUCTO> productos = gestorProducto.ListarProductos();

            rptProductos.DataSource = productos;
            rptProductos.DataBind(); // ¡Comando que "pinta" los datos en el HTML!
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            SESSION_MANAGER.ObtenerInstancia().Logout();
            Response.Redirect("frmLogin.aspx");
        }

        protected void btnVerCarrito_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmCarrito.aspx");
        }

        protected void rptProductos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // Verificamos si el comando que se disparó es "Agregar"
            if (e.CommandName == "Agregar")
            {
                // 1. Obtenemos el ID del producto desde el CommandArgument
                int idProducto = Convert.ToInt32(e.CommandArgument);
                TextBox txtCantidad = (TextBox)e.Item.FindControl("txtCantidad");

                int cantidadDeseada = 1;
                if (txtCantidad != null)
                {
                    // Intentamos parsear lo que escribió el usuario; si falla, por defecto toma 1
                    int.TryParse(txtCantidad.Text, out cantidadDeseada);
                    if (cantidadDeseada < 1) cantidadDeseada = 1;
                }
                                                
                BE.PRODUCTO productoSeleccionado = gestorProducto.ObtenerPorId(idProducto);
                if (productoSeleccionado != null)
                {
                    //Buscamos si el producto ya está en el carrito actual
                    BE.CARRITO carrito = Session["Carrito"] as BE.CARRITO;
                    if (carrito == null)
                    {
                        carrito = new BE.CARRITO();
                        Session["Carrito"] = carrito;
                    }

                    int cantidadYaEnCarrito = 0;
                    var itemExistente = carrito.Items.Find(i => i.Producto.Id == idProducto);
                    if (itemExistente != null)
                    {
                        cantidadYaEnCarrito = itemExistente.Cantidad;
                    }

                    //Sumamos lo que ya tiene + lo que quiere agregar ahora
                    int cantidadTotalDeseada = cantidadYaEnCarrito + cantidadDeseada;

                    if (productoSeleccionado.StockActual >= cantidadTotalDeseada)
                    {
                        // Si ya existía, sumamos; si no, lo agregamos como nuevo
                        if (itemExistente != null)
                        {
                            itemExistente.Cantidad = cantidadTotalDeseada; // O sumarle el nuevo, como prefieras
                        }
                        else
                        {
                            BE.ITEM_CARRITO nuevoItem = new BE.ITEM_CARRITO();
                            nuevoItem.Producto = productoSeleccionado;
                            nuevoItem.Cantidad = cantidadDeseada;
                            carrito.Items.Add(nuevoItem);
                        }

                        ActualizarContadorCarrito(carrito);
                        lblMensaje.Text = "¡Producto agregado al carrito con éxito!";
                        lblMensaje.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        // Si no hay stock, mostramos alerta
                        lblMensaje.Text = "No hay stock suficiente para la cantidad solicitada. Stock disponible: " + productoSeleccionado.StockActual;
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        private void ActualizarContadorCarrito(BE.CARRITO carrito)
        {
            int totalItems = 0;
            foreach (var item in carrito.Items)
            {
                totalItems += item.Cantidad;
            }
            btnVerCarrito.Text = "🛒 Ver Carrito (" + totalItems + ")";
        }

        // ------------------------ BOTONES ------------------------
        protected void btnIrGestionPedidos_Click(object sender, EventArgs e)
        {
            //Gestionar pedidos

            Response.Redirect("frmGestionPedidos.aspx");

        }

        protected void btnIrNuevoProducto_Click(object sender, EventArgs e)
        {
            //Nuevo Producto

            Response.Redirect("frmProducto.aspx");
        }

        protected void btnIrGestionProductos_Click(object sender, EventArgs e)
        {
            // Gestion Productos

            Response.Redirect("frmGestionProductos.aspx");
        }
    }
}