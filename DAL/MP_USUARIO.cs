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
    public class MP_USUARIO : MAPPER<BE.USUARIO>
    {
        public override void Eliminar(BE.USUARIO obj)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int idUsuario)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Id", idUsuario));

            acceso.Escribir("sp_EliminarUsuario", parametros);
        }

        public override void Insertar(BE.USUARIO obj)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Nombre", obj.Nombre));
            parametros.Add(new SqlParameter("@Password", obj.Password));
            parametros.Add(new SqlParameter("@Permiso", obj.Permiso));
            parametros.Add(new SqlParameter("@Email", obj.Email));
            parametros.Add(new SqlParameter("@Telefono", obj.Telefono));

            acceso.Escribir("sp_InsertarUsuario", parametros);
        }

        public override List<BE.USUARIO> Listar()
        {
            List<BE.USUARIO> lista = new List<BE.USUARIO>();

            DataTable tabla = acceso.Leer("sp_ListarUsuarios");

            foreach (DataRow row in tabla.Rows)
            {
                BE.USUARIO usu = new BE.USUARIO();
                usu.Id = Convert.ToInt32(row["Id"]);
                usu.Nombre = row["Nombre"].ToString();
                usu.Password = row["Password"].ToString();
                usu.Permiso = row["Permiso"].ToString();
                usu.Email = row["Email"].ToString();
                usu.Telefono = row["Telefono"].ToString();

                lista.Add(usu);
            }

            return lista;
        }

        public override void Modificar(BE.USUARIO obj)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Id", obj.Id));
            parametros.Add(new SqlParameter("@Nombre", obj.Nombre));
            parametros.Add(new SqlParameter("@Password", obj.Password));
            parametros.Add(new SqlParameter("@Permiso", obj.Permiso));
            parametros.Add(new SqlParameter("@Email", obj.Email));
            parametros.Add(new SqlParameter("@Telefono", obj.Telefono));

            acceso.Escribir("sp_ModificarUsuario", parametros);
        }

        public USUARIO ObtenerPorId(int idUsuario)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Id", idUsuario));

            DataTable tabla = acceso.Leer("sp_ObtenerUsuarioPorId", parametros);

            if (tabla.Rows.Count > 0)
            {
                DataRow row = tabla.Rows[0];
                BE.USUARIO usu = new BE.USUARIO();
                usu.Id = Convert.ToInt32(row["Id"]);
                usu.Nombre = row["Nombre"].ToString();
                usu.Password = row["Password"].ToString();
                usu.Permiso = row["Permiso"].ToString();
                usu.Email = row["Email"].ToString();
                usu.Telefono = row["Telefono"].ToString();

                return usu;
            }

            return null;
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
