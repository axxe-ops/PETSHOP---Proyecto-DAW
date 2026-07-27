using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MP_DIGITOSVERIFICADORES
    {
        DAL.ACCESO acceso = new DAL.ACCESO();

        public void ActualizarDVH(string nombreTabla, int id, string nuevoDvh)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Tabla", nombreTabla));
            parametros.Add(new SqlParameter("@Id", id));
            parametros.Add(new SqlParameter("@DVH", nuevoDvh));

            // Ejecutamos un Stored Procedure genérico para actualizar el DVH según la tabla
            acceso.Escribir("sp_ActualizarDVHGenerico", parametros);
        }

        public void ActualizarDVV(string nombreTabla, int sumaDVH)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@NombreTabla", nombreTabla));
            parametros.Add(new SqlParameter("@DVV", sumaDVH));

            // Ejecuta el Store Procedure encargado de actualizar o insertar el dígito vertical global de la tabla
            acceso.Escribir("sp_ActualizarDVV", parametros);
        }

        public int ObtenerDVV(string nombreTabla)
        {
            int dvv = 0;

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@NombreTabla", nombreTabla));

            // Usamos el método Leer de tu objeto acceso (igual que en tus otros mappers)
            DataTable dt = acceso.Leer("SP_Obtener_DVV", parametros);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                // Asegúrate de que el campo coincida con lo que devuelve tu Stored Procedure
                dvv = Convert.ToInt32(row["DigitoVertical"]);
            }

            return dvv;
        }

       
    }
}
