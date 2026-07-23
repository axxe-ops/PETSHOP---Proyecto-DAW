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
    public class MP_BITACORA : MAPPER<BE.BITACORA>
    {
        public override void Eliminar(BITACORA obj)
        {
            throw new NotImplementedException();
        }

        public override void Insertar(BITACORA obj)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
                        
            parametros.Add(new SqlParameter("@IdUsuario", obj.IdUsuario));            
            parametros.Add(new SqlParameter("@NombreUsuario", string.IsNullOrEmpty(obj.NombreUsuario) ? (object)DBNull.Value : obj.NombreUsuario));
            parametros.Add(new SqlParameter("@Criticidad", obj.Criticidad));
            parametros.Add(new SqlParameter("@Descripcion", obj.Descripcion));

            acceso.Escribir("sp_InsertarBitacora", parametros);
        }

        public override List<BITACORA> Listar()
        {
            List<BE.BITACORA> lista = new List<BE.BITACORA>();
            
            DataTable tabla = acceso.Leer("sp_ListarBitacora");

            foreach (DataRow row in tabla.Rows)
            {
                BE.BITACORA bitacora = new BE.BITACORA();
                bitacora.Id = Convert.ToInt32(row["Id"]);
                bitacora.FechaHora = Convert.ToDateTime(row["FechaHora"]);

                // Manejamos posibles nulos por si el evento no está asociado a un usuario fijo
                bitacora.IdUsuario = row["IdUsuario"] != DBNull.Value ? Convert.ToInt32(row["IdUsuario"]) : 0;
                bitacora.NombreUsuario = row["NombreUsuario"] != DBNull.Value ? row["NombreUsuario"].ToString() : "Sistema";

                // Mapeamos la criticidad como entero (1 a 5)
                bitacora.Criticidad = Convert.ToInt32(row["Criticidad"]);
                bitacora.Descripcion = row["Descripcion"].ToString();

                lista.Add(bitacora);
            }

            return lista;
        }

        public override void Modificar(BITACORA obj)
        {
            throw new NotImplementedException();
        }
    }
}
