using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SERVICIO
{
    public class SESSION_MANAGER
    {
        private static SESSION_MANAGER instancia;
        
        public SESSION_MANAGER() 
        {
        
        }

        public static SESSION_MANAGER ObtenerInstancia()
        {
            if (instancia == null)
            {
                return null;
            }
            return instancia;
        }

        public static bool Login(USUARIO usuario)
        {
            string passwordHasheada = SEGURIDAD.ENCRIPTADO.Hashear(usuario.Password);
            usuario.Password = passwordHasheada;

            DAL.USUARIO mapperUsuario = new DAL.USUARIO();
            BE.USUARIO usuarioLogueado = mapperUsuario.ValidarUsuario(usuario);

            if (usuarioLogueado != null)
            {
                HttpContext.Current.Session["Usuario"] = usuarioLogueado;
                return true;
            }

            return false;
        }

        public void Logout()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
            instancia = null;
        }

        public USUARIO ObtenerUsuario()
        {
            return (USUARIO)HttpContext.Current.Session["Usuario"];
        }

    }
}
