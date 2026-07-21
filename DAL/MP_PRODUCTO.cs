using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MP_PRODUCTO : MAPPER<BE.PRODUCTO>
    {
        public override void Eliminar(BE.PRODUCTO obj)
        {
            throw new NotImplementedException();
        }

        public override void Insertar(BE.PRODUCTO obj)
        {
            throw new NotImplementedException();
        }

        public override List<BE.PRODUCTO> Listar()
        {
            List<BE.PRODUCTO> lista = new List<BE.PRODUCTO>();

            DataTable dt = acceso.Leer("sp_ListarProductos");

            foreach (DataRow row in dt.Rows)
            {
                BE.PRODUCTO p = new BE.PRODUCTO();
                p.Id = (int)row["Id"];
                p.Nombre = row["Nombre"].ToString();
                p.Tipo = row["Tipo"].ToString();
                p.Precio = (decimal)row["Precio"];
                p.StockActual = (int)row["StockActual"];
                p.StockMinimo = (int)row["StockMinimo"];

                lista.Add(p);
            }
            return lista;
        }

        public override void Modificar(BE.PRODUCTO obj)
        {
            throw new NotImplementedException();
        }

        public BE.PRODUCTO ObtenerPorId(int idProducto)
        {
            BE.PRODUCTO producto = null;

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Id", idProducto));

            DataTable dt = acceso.Leer("sp_ObtenerProductoPorId", parametros);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                producto = new BE.PRODUCTO();
                producto.Id = (int)row["Id"];
                producto.Nombre = row["Nombre"].ToString();
                producto.Tipo = row["Tipo"].ToString();
                producto.Precio = (decimal)row["Precio"];
                producto.StockActual = (int)row["StockActual"];
                producto.StockMinimo = (int)row["StockMinimo"];
            }

            return producto;
        }
    }
}
