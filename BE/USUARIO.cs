using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class USUARIO
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



    }
}
