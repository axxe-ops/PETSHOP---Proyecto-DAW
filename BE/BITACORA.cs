using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BITACORA
    {
		private int id;
		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		private DateTime fechaHora;
		public DateTime FechaHora
		{
			get { return fechaHora; }
			set { fechaHora = value; }
		}

		private int idUsuario;
		public int IdUsuario
		{
			get { return idUsuario; }
			set { idUsuario = value; }
		}

		private string nombreUsuario;
		public string NombreUsuario
		{
			get { return nombreUsuario; }
			set { nombreUsuario = value; }
		}

		private int criticidad;
		public int Criticidad
		{
			get { return criticidad; }
			set { criticidad = value; }
		}

		private string descripcion;
		public string Descripcion
		{
			get { return descripcion; }
			set { descripcion = value; }
		}





	}
}
