using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class USUARIO : IVerificarDigitos
    {
		private int id;
		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		private string nombre;
		public string Nombre
		{
			get { return nombre; }
			set { nombre = value; }
		}

		private string password;
		public string Password
		{
			get { return password; }
			set { password = value; }
		}

		private string permiso;
		public string Permiso
		{
			get { return permiso; }
			set { permiso = value; }
		}

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string telefono;
        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }


		//dv horizontal
		private string digitoVerificador;
		public string DigitoVerificador
		{
			get { return digitoVerificador; }
			set { digitoVerificador = value; }
		}

        private string dvh;
        public string Dvh
        {
            get { return dvh; }
            set { dvh = value; }
        }

        public string CalcularDVH()
        {
            string datos = Id.ToString() + Nombre + Password + Permiso;
            return SEGURIDAD.ENCRIPTADO.Hashear(datos);
        }
    }
}
