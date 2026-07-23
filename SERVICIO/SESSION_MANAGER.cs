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

            SERVICIO.BITACORA_BLL gestorBitacora = new BITACORA_BLL();

            if (usuarioValidado != null)
            {
                HttpContext.Current.Session["Usuario"] = usuarioValidado;

                gestorBitacora.RegistrarBitacora("El usuario " + usuarioValidado.Nombre + " inició sesión correctamente.", 1);

                return true;
            }
            else
            {
                gestorBitacora.RegistrarBitacora("Intento fallido de inicio de sesión con el usuario: " + usuario.Nombre, 3);
            }

            return false;
        }

        public void Logout()
        {
            SERVICIO.BITACORA_BLL gestorBitacora = new BITACORA_BLL();
            gestorBitacora.RegistrarBitacora("El usuario: " + usuarioLogueado.Nombre + " cerró sesión correctamente.", 1);

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
