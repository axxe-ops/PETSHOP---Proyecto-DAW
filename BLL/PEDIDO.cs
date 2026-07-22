using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PEDIDO
    {
        DAL.MP_PEDIDO mapperPedido = new DAL.MP_PEDIDO();

        public void ActualizarEstado(int idPedido, string nuevoEstado)
        {
            mapperPedido.ActualizarEstado(idPedido, nuevoEstado);
        }

        public void Insertar(BE.PEDIDO nuevoPedido)
        {
            mapperPedido.Insertar(nuevoPedido);
        }

        public List<BE.PEDIDO> Listar()
        {
            return mapperPedido.Listar();
        }

        public BE.PEDIDO ObtenerPorId(int idPedido)
        {
            return mapperPedido.ObtenerPorId(idPedido);
        }
    }
}
