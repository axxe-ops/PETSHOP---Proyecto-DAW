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
    public class MP_BACKUP : MAPPER<BE.BACKUP_INFO>
    {
        public override void Eliminar(BACKUP_INFO obj)
        {
            throw new NotImplementedException();
        }

        public override void Insertar(BACKUP_INFO obj)
        {
            throw new NotImplementedException();
        }

        public override List<BACKUP_INFO> Listar()
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@Db", "PETSHOP"));

            DataTable tabla = acceso.Leer("sp_ListarBackups", parametros);
            List<BACKUP_INFO> lista = new List<BACKUP_INFO>();

            foreach (DataRow r in tabla.Rows)
            {
                lista.Add(new BACKUP_INFO
                {
                    RutaArchivo = r["RutaArchivo"].ToString(),
                    Fecha = Convert.ToDateTime(r["Fecha"]),
                    NombreBaseDatos = r["NombreBaseDatos"].ToString()
                });
            }

            return lista;
        }

        public override void Modificar(BACKUP_INFO obj)
        {
            throw new NotImplementedException();
        }

        public void RestaurarBaseDatos(string rutaArchivo)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@Path", rutaArchivo));

            // Usamos el método que se conecta a master para evitar el bloqueo
            acceso.EscribirEnMaster("sp_RestaurarBaseDatos", parametros);
        }

        public void EjecutarBackupFull(string rutaDestino)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@Path", rutaDestino));           

            acceso.Escribir("sp_RealizarBackupFull", parametros);
        }
    }
}
