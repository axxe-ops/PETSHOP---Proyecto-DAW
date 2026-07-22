using SERVICIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GUI
{
    public partial class frmGestionPedidos : System.Web.UI.Page
    {
        BLL.PEDIDO gestorPedido = new BLL.PEDIDO();
        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = SESSION_MANAGER.ObtenerInstancia().ObtenerUsuario();
            if (usuario == null || usuario.Permiso != "ADMIN")
            {
                Response.Redirect("frmMenúPrincipal.aspx");
            }
            if (!IsPostBack)
            {
                CargarPedidos();
            }
        }

        protected void gvPedidos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idPedido = Convert.ToInt32(e.CommandArgument);            

            if (e.CommandName == "ActualizarEstado")
            {
                // 1. Buscamos la fila (GridViewRow) donde se hizo clic usando el botón
                Button btn = (Button)e.CommandSource;
                GridViewRow row = (GridViewRow)btn.NamingContainer;

                // 2. Encontramos el DropDownList dentro de esa misma fila
                DropDownList ddlEstado = (DropDownList)row.FindControl("ddlEstado");

                if (ddlEstado != null)
                {
                    string nuevoEstado = ddlEstado.SelectedValue;

                    // 3. Actualizamos en la base de datos
                    gestorPedido.ActualizarEstado(idPedido, nuevoEstado);

                    lblMensaje.Text = "El estado del pedido #" + idPedido + " se actualizó a: " + nuevoEstado;
                    lblMensaje.ForeColor = System.Drawing.Color.Green;

                    CargarPedidos();
                    pnlDetalle.Visible = false;
                }
            }
            else if (e.CommandName == "VerDetalle")
            {
                BE.PEDIDO pedido = gestorPedido.ObtenerPorId(idPedido);
                if (pedido != null)
                {
                    lblIdPedidoDetalle.Text = pedido.Id.ToString();
                    lblClienteDetalle.Text = pedido.Cliente.Nombre + " - Email: " + pedido.Cliente.Email + " - Tel: " + pedido.Cliente.Telefono;
                    lblFechaDetalle.Text = pedido.Fecha.ToString("dd/MM/yyyy HH:mm");

                    gvDetalleItems.DataSource = pedido.Items;
                    gvDetalleItems.DataBind();

                    pnlDetalle.Visible = true;
                }
            }
        }

        private void CargarPedidos()
        {
            List<BE.PEDIDO> lista = gestorPedido.Listar();
            gvPedidos.DataSource = lista;
            gvPedidos.DataBind();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmMenúPrincipal.aspx");
        }

        protected string ObtenerClaseBadge(string estado)
        {
            switch (estado)
            {
                case "Pendiente": return "badge-pendiente";
                case "Aprobado": return "badge-aprobado";
                case "Enviado": return "badge-enviado";
                case "Entregado": return "badge-entregado";
                case "Cancelado": return "badge-cancelado";
                default: return "badge-pendiente";
            }
        }
    }
}