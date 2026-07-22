using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MP_USUARIO : MAPPER<BE.USUARIO>
    {
        public override void Eliminar(BE.USUARIO obj)
        {
            throw new NotImplementedException();
        }

        public override void Insertar(BE.USUARIO obj)
        {
            throw new NotImplementedException();
        }

        public override List<BE.USUARIO> Listar()
        {
            throw new NotImplementedException();
        }

        public override void Modificar(BE.USUARIO obj)
        {
            throw new NotImplementedException();
        }

        public BE.USUARIO ValidarUsuario(BE.USUARIO usu)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@nombre", usu.Nombre));
            parametros.Add(new SqlParameter("@pass", usu.Password));

            DataTable dt = acceso.Leer("sp_ValidarUsuario", parametros);

            if (dt.Rows.Count > 0)
            {
                BE.USUARIO usuario = new BE.USUARIO();
                usuario.Id = (int)dt.Rows[0]["Id"];
                usuario.Nombre = dt.Rows[0]["Nombre"].ToString();
                usuario.Permiso = dt.Rows[0]["Permiso"].ToString();
                usuario.Email = dt.Rows[0]["Email"] != DBNull.Value ? dt.Rows[0]["Email"].ToString() : string.Empty;
                usuario.Telefono = dt.Rows[0]["Telefono"] != DBNull.Value ? dt.Rows[0]["Telefono"].ToString() : string.Empty;
                return usuario;
            }
            else
            {
                return null;
            }
        }
    }
}
