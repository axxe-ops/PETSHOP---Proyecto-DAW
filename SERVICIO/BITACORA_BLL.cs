using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICIO
{
    public class BITACORA_BLL
    {
        DAL.MP_BITACORA mapperBitacora = new DAL.MP_BITACORA();

        public void RegistrarBitacora(string descripcion, int criticidad)
        {
            BE.BITACORA log = new BE.BITACORA();
            log.Criticidad = criticidad;
            log.Descripcion = descripcion;

            var usuarioActual = SESSION_MANAGER.ObtenerInstancia().ObtenerUsuario();
            if (usuarioActual != null)
            {
                log.IdUsuario = usuarioActual.Id;
                log.NombreUsuario = usuarioActual.Nombre;
            }
            else
            {
                log.IdUsuario = 0;
                log.NombreUsuario = "Sistema / Anónimo";
            }

            InsertarBitacora(log);
        }

        public List<BE.BITACORA> Listar()
        {
            return mapperBitacora.Listar();
        }

        public void InsertarBitacora(BE.BITACORA bitacora)
        {
            mapperBitacora.Insertar(bitacora);
        }
    }
}
