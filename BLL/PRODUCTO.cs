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
