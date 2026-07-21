using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MP_PEDIDO : MAPPER<BE.PEDIDO>
    {
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
            throw new NotImplementedException();
        }

        public override void Modificar(PEDIDO obj)
        {
            throw new NotImplementedException();
        }
    }
}
