using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BACKUP_INFO
    {

		private string rutaArchivo;
		public string RutaArchivo
		{
			get { return rutaArchivo; }
			set { rutaArchivo = value; }
		}

		private DateTime fecha;
		public DateTime Fecha
		{
			get { return fecha; }
			set { fecha = value; }
		}

		private string nombreBaseDatos;
		public string NombreBaseDatos
		{
			get { return nombreBaseDatos; }
			set { nombreBaseDatos = value; }
		}





	}
}
