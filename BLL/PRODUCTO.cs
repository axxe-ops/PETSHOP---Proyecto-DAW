using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PRODUCTO
    {
        DAL.MP_PRODUCTO mapperProducto = new DAL.MP_PRODUCTO();

        public void ActualizarProducto(BE.PRODUCTO producto)
        {
            mapperProducto.Modificar(producto);
        }

        public List<BE.PRODUCTO> ListarProductos()
        {            
            return mapperProducto.Listar();
        }

        public BE.PRODUCTO ObtenerPorId(int idProducto)
        {
            return mapperProducto.ObtenerPorId(idProducto);
        }
    }
}
