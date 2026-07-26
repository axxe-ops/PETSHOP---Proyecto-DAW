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
            var sql = @"
                SELECT TOP 50
                    bmf.physical_device_name AS RutaArchivo,
                    bs.backup_finish_date    AS Fecha,
                    bs.database_name         AS NombreBaseDatos
                FROM msdb.dbo.backupset bs
                JOIN msdb.dbo.backupmediafamily bmf
                    ON bs.media_set_id = bmf.media_set_id
                WHERE bs.database_name = @Db
                ORDER BY bs.backup_finish_date DESC";

            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("@Db", "PETSHOP")
            };

            DataTable tabla = acceso.Leer(sql, parametros);
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
            // Forzamos a modo monousuario para desconectar sesiones y poder restaurar
            var sql = @"
                ALTER DATABASE [PETSHOP] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
        
                RESTORE DATABASE [PETSHOP] 
                FROM DISK = @Path 
                WITH REPLACE;
        
                ALTER DATABASE [PETSHOP] SET MULTI_USER;";

            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("@Path", rutaArchivo)
            };

            acceso.Escribir(sql, parametros);
        }

        public void EjecutarBackupFull(string rutaDestino)
        {
            var sql = "BACKUP DATABASE [PETSHOP] TO DISK = @Path WITH INIT, COMPRESSION, CHECKSUM;";

            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("@Path", rutaDestino)
            };

            acceso.Escribir(sql, parametros);
        }
    }
}
