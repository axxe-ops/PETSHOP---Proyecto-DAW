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
            BE.USUARIO usuarioValidado = ValidarUsuario(usuario);

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

        public static BE.USUARIO ValidarUsuario(USUARIO usu)
        {
            // Nos aseguramos de que el hasheo ocurra de forma centralizada acá
            string passwordHasheada = SEGURIDAD.ENCRIPTADO.Hashear(usu.Password);

            USUARIO usuarioParaValidar = new USUARIO();
            usuarioParaValidar.Nombre = usu.Nombre;
            usuarioParaValidar.Password = passwordHasheada;

            DAL.MP_USUARIO mapperUsuario = new DAL.MP_USUARIO();
            return mapperUsuario.ValidarUsuario(usuarioParaValidar);
        }

    }
}
