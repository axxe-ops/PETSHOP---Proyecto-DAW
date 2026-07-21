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
        private BE.USUARIO usuarioLogueado;

        public SESSION_MANAGER() 
        {
        
        }

        public static SESSION_MANAGER ObtenerInstancia()
        {
            if (instancia == null)
            {
                instancia = new SESSION_MANAGER();
            }
            return instancia;
        }

        public static bool Login(USUARIO usuario)
        {
            string passwordHasheada = SEGURIDAD.ENCRIPTADO.Hashear(usuario.Password);
            usuario.Password = passwordHasheada;

            DAL.MP_USUARIO mapperUsuario = new DAL.MP_USUARIO();
            BE.USUARIO usuarioValidado = mapperUsuario.ValidarUsuario(usuario);

            if (usuarioValidado != null)
            {
                HttpContext.Current.Session["Usuario"] = usuarioValidado;
                return true;
            }

            return false;
        }

        public void Logout()
        {
            usuarioLogueado = null;
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }

        public USUARIO ObtenerUsuario()
        {
            // Si no está en memoria, intentamos rescatarlo de la sesión web
            if (usuarioLogueado == null && HttpContext.Current.Session["Usuario"] != null)
            {
                usuarioLogueado = (BE.USUARIO)HttpContext.Current.Session["Usuario"];
            }
            return usuarioLogueado;
        }

    }
}
