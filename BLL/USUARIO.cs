using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class USUARIO
    {
        DAL.MP_USUARIO mapperUsuario = new DAL.MP_USUARIO();
        public void ActualizarUsuario(BE.USUARIO usu)
        {
            mapperUsuario.Modificar(usu);
        }

        public void Eliminar(int idUsuario)
        {
            mapperUsuario.Eliminar(idUsuario);
        }

        public void Insertar(BE.USUARIO nuevoUsu)
        {
            mapperUsuario.Insertar(nuevoUsu);
        }

        public List<BE.USUARIO> Listar()
        {
            return mapperUsuario.Listar();  
        }

        public BE.USUARIO ObtenerPorId(int idUsuario)
        {
            return mapperUsuario.ObtenerPorId(idUsuario);
        }
    }
}
