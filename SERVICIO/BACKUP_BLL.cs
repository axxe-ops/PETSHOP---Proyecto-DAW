using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICIO
{
    public class BACKUP_BLL
    {
        private DAL.MP_BACKUP mapperBackup = new DAL.MP_BACKUP();

        public List<BE.BACKUP_INFO> ListarBackups()
        {
            return mapperBackup.Listar();
        }

        public void RealizarBackup(string rutaDestino)
        {
            if (string.IsNullOrWhiteSpace(rutaDestino))
                throw new Exception("La ruta de destino no puede estar vacía.");

            mapperBackup.EjecutarBackupFull(rutaDestino);
        }

        public void RestaurarBaseDatos(string rutaArchivo)
        {
            if (string.IsNullOrWhiteSpace(rutaArchivo))
                throw new Exception("Debe seleccionar un archivo de backup válido.");

            mapperBackup.RestaurarBaseDatos(rutaArchivo);
        }

    }
}
