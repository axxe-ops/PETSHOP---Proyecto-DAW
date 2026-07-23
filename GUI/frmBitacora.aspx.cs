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
    public partial class frmBitacora : System.Web.UI.Page
    {
        BITACORA_BLL gestorBitacora = new BITACORA_BLL();   
        protected void Page_Load(object sender, EventArgs e)
        {
            var usuarioActual = SESSION_MANAGER.ObtenerInstancia().ObtenerUsuario();
            if (usuarioActual == null || usuarioActual.Permiso != PERMISO.ADMIN)
            {
                Response.Redirect("frmMenúPrincipal.aspx");
            }

            if (!IsPostBack)
            {
                CargarGrillaBitacora();
            }
        }

        private void CargarGrillaBitacora()
        {
            gvBitacora.DataSource = gestorBitacora.Listar();
            gvBitacora.DataBind();
        }

        protected void gvBitacora_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                BE.BITACORA registro = (BE.BITACORA)e.Row.DataItem;
                Label lblCrit = (Label)e.Row.FindControl("lblCriticidad");

                if (lblCrit != null && registro != null)
                {
                    int crit = registro.Criticidad;
                    lblCrit.Text = "Nivel " + crit;

                    // Asignamos una clase CSS según la gravedad del 1 al 5
                    if (crit == 1) lblCrit.CssClass += " crit-1";
                    else if (crit == 2) lblCrit.CssClass += " crit-2";
                    else if (crit == 3) lblCrit.CssClass += " crit-3";
                    else if (crit == 4) lblCrit.CssClass += " crit-4";
                    else if (crit == 5) lblCrit.CssClass += " crit-5";
                }
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmMenúPrincipal.aspx");
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            var listaOriginal = gestorBitacora.Listar();
            var listaFiltrada = listaOriginal.AsEnumerable();

            // 1. Filtro por Nombre de Usuario
            if (!string.IsNullOrEmpty(txtFiltroUsuario.Text.Trim()))
            {
                string usuarioBusqueda = txtFiltroUsuario.Text.Trim().ToLower();
                listaFiltrada = listaFiltrada.Where(x => x.NombreUsuario != null && x.NombreUsuario.ToLower().Contains(usuarioBusqueda));
            }

            // 2. Filtro por Criticidad
            int criticidadSeleccionada = Convert.ToInt32(ddlFiltroCriticidad.SelectedValue);
            if (criticidadSeleccionada > 0)
            {
                listaFiltrada = listaFiltrada.Where(x => x.Criticidad == criticidadSeleccionada);
            }

            // 3. Filtro por Fecha
            if (!string.IsNullOrEmpty(txtFiltroFecha.Text))
            {
                DateTime fechaFiltro = Convert.ToDateTime(txtFiltroFecha.Text);
                listaFiltrada = listaFiltrada.Where(x => x.FechaHora.Date == fechaFiltro.Date);
            }

            gvBitacora.DataSource = listaFiltrada.ToList();
            gvBitacora.DataBind();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFiltroUsuario.Text = "";
            ddlFiltroCriticidad.SelectedIndex = 0;
            txtFiltroFecha.Text = "";
            CargarGrillaBitacora();
        }
    }
}