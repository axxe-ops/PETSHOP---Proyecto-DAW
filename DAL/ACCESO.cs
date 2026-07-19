using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ACCESO
    {
        private string connectionString = @"Integrated Security=SSPI;Initial Catalog=PETSHOP;Data Source=localhost\SQLEXPRESS";

        public int Escribir(string nombreStoredProcedure, List<SqlParameter> parametros = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(nombreStoredProcedure, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parametros != null) cmd.Parameters.AddRange(parametros.ToArray());

                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }


        public object EjecutarEscalar(string nombreStoredProcedure, List<SqlParameter> parametros = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(nombreStoredProcedure, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parametros != null) cmd.Parameters.AddRange(parametros.ToArray());

                    con.Open();
                    return cmd.ExecuteScalar(); // Devuelve el primer valor de la consulta
                }
            }
        }

        public DataTable Leer(string nombreStoredProcedure, List<SqlParameter> parametros = null)
        {
            DataTable dt = new DataTable();
            try
            { // Agregamos un try/catch para ver si hay un error de SQL
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(nombreStoredProcedure, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (parametros != null) cmd.Parameters.AddRange(parametros.ToArray());

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        con.Open();
                        da.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                string error = ex.Message;
            }
            return dt;
        }

        public SqlParameter CrearParametro(string nombre, string valor)
        {
            SqlParameter param = new SqlParameter(nombre, valor);
            param.DbType = DbType.String;
            return param;
        }
        public SqlParameter CrearParametro(string nombre, int valor)
        {
            SqlParameter param = new SqlParameter(nombre, valor);
            param.DbType = DbType.Int32;
            return param;
        }
        public SqlParameter CrearParametro(string nombre, float valor)
        {
            SqlParameter param = new SqlParameter(nombre, valor);
            param.DbType = DbType.Single;
            return param;
        }
        public SqlParameter CrearParametro(string nombre, DateTime fecha)
        {
            SqlParameter param = new SqlParameter(nombre, fecha);
            param.DbType = DbType.DateTime;
            return param;
        }


    }
}
