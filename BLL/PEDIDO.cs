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

        public void Insertar(BE.PEDIDO nuevoPedido)
        {
            mapperPedido.Insertar(nuevoPedido);
        }
    }
}
