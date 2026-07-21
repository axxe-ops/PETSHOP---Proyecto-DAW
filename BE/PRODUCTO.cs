using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class PRODUCTO
    {
		private int id;
		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		private string  nombre;
		public string  Nombre
		{
			get { return nombre; }
			set { nombre = value; }
		}


		private string tipo;
		public string Tipo
		{
			get { return tipo; }
			set { tipo = value; }
		}

		private Decimal precio;
		public Decimal Precio
		{
			get { return precio; }
			set { precio = value; }
		}

		private int stockActual;
		public int StockActual
		{
			get { return stockActual; }
			set { stockActual = value; }
		}

		private int stockMinimo;
		public int StockMinimo
		{
			get { return stockMinimo; }
			set { stockMinimo = value; }
		}





	}
}
