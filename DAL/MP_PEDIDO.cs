using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MP_PEDIDO : MAPPER<BE.PEDIDO>
    {
        public void ActualizarEstado(int idPedido, string nuevoEstado)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@IdPedido", idPedido));
            parametros.Add(new SqlParameter("@Estado", nuevoEstado));

            acceso.Escribir("sp_ActualizarEstadoPedido", parametros);
        }

        public PEDIDO ObtenerPorId(int idPedido)
        {
            PEDIDO pedido = null;

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@IdPedido", idPedido));

            DataTable dtCabecera = acceso.Leer("sp_ObtenerPedidoPorId", parametros);

            if (dtCabecera.Rows.Count > 0)
            {
                DataRow row = dtCabecera.Rows[0];
                pedido = new PEDIDO();
                pedido.Id = Convert.ToInt32(row["Id"]);
                pedido.Fecha = Convert.ToDateTime(row["Fecha"]);
                pedido.Estado = row["Estado"].ToString();
                pedido.MontoTotal = Convert.ToDecimal(row["MontoTotal"]);

                // Asignamos el Cliente (Usuario) asociado al pedido
                pedido.Cliente = new USUARIO();
                pedido.Cliente.Id = Convert.ToInt32(row["IdUsuario"]);
                pedido.Cliente.Nombre = row["NombreCliente"].ToString();

                // 2. Obtenemos los ítems (detalle) de este pedido
                List<SqlParameter> parametrosDetalle = new List<SqlParameter>();
                parametrosDetalle.Add(new SqlParameter("@IdPedido", idPedido));
                
                DataTable dtDetalle = acceso.Leer("sp_ObtenerDetallePedidoPorId", parametrosDetalle);
                foreach (DataRow rDetalle in dtDetalle.Rows)
                {
                    ITEM_CARRITO item = new ITEM_CARRITO();
                    item.Cantidad = Convert.ToInt32(rDetalle["Cantidad"]);
                    item.Subtotal = Convert.ToDecimal(rDetalle["Subtotal"]);

                    item.Producto = new PRODUCTO();
                    item.Producto.Id = Convert.ToInt32(rDetalle["IdProducto"]);
                    item.Producto.Nombre = rDetalle["NombreProducto"].ToString();
                    item.Producto.Tipo = rDetalle["TipoProducto"].ToString();
                    item.Producto.Precio = Convert.ToDecimal(rDetalle["PrecioUnitario"]);

                    pedido.Items.Add(item);
                }
            }

            return pedido;
        }


        public override void Eliminar(PEDIDO obj)
        {
            throw new NotImplementedException();
        }

        public override void Insertar(PEDIDO obj)
        {
            //Insertamos la Cabecera del Pedido y recuperar el ID generado
            List<SqlParameter> parametrosCabecera = new List<SqlParameter>();
            parametrosCabecera.Add(new SqlParameter("@IdUsuario", obj.Cliente.Id));
            parametrosCabecera.Add(new SqlParameter("@Fecha", obj.Fecha));
            parametrosCabecera.Add(new SqlParameter("@Estado", obj.Estado));
            parametrosCabecera.Add(new SqlParameter("@MontoTotal", obj.MontoTotal));

            int idPedidoGenerado = Convert.ToInt32(acceso.EjecutarEscalar("sp_InsertarPedido", parametrosCabecera));

            //Insertamos cada ítem del detalle del pedido
            foreach (var item in obj.Items)
            {
                List<SqlParameter> parametrosDetalle = new List<SqlParameter>();
                parametrosDetalle.Add(new SqlParameter("@IdPedido", idPedidoGenerado));
                parametrosDetalle.Add(new SqlParameter("@IdProducto", item.Producto.Id));
                parametrosDetalle.Add(new SqlParameter("@Cantidad", item.Cantidad));
                parametrosDetalle.Add(new SqlParameter("@Subtotal", item.Subtotal));

                acceso.Escribir("sp_InsertarDetallePedido", parametrosDetalle);
            }
        }

        public override List<PEDIDO> Listar()
        {
            List<PEDIDO> lista = new List<PEDIDO>();
            DataTable dt = acceso.Leer("sp_ObtenerTodosLosPedidos", null);

            foreach (DataRow row in dt.Rows)
            {
                PEDIDO pedido = new PEDIDO();
                pedido.Id = Convert.ToInt32(row["Id"]);
                pedido.Fecha = Convert.ToDateTime(row["Fecha"]);
                pedido.Estado = row["Estado"].ToString();
                pedido.MontoTotal = Convert.ToDecimal(row["MontoTotal"]);

                pedido.Cliente = new USUARIO();
                pedido.Cliente.Id = Convert.ToInt32(row["IdUsuario"]);
                pedido.Cliente.Nombre = row["NombreCliente"].ToString();

                lista.Add(pedido);
            }

            return lista;
        }

        public override void Modificar(PEDIDO obj)
        {
            throw new NotImplementedException();
        }
    }
}
