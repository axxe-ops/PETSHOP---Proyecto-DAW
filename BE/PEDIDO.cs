using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE
{
    public class PEDIDO
    {
        public PEDIDO()
        {
			items = new List<ITEM_CARRITO> ();
        }

        private int id;
		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		private DateTime fecha;
		public DateTime Fecha
		{
			get { return fecha; }
			set { fecha = value; }
		}

		private USUARIO cliente;
		public USUARIO Cliente
		{
			get { return cliente; }
			set { cliente = value; }
		}

		private List<ITEM_CARRITO> items;
		public List<ITEM_CARRITO> Items
		{
			get { return items; }
			set { items = value; }
		}

		private Decimal montoTotal;
		public Decimal MontoTotal
		{
			get { return montoTotal; }
			set { montoTotal = value; }
		}

		private string estado;
        public string Estado
		{
			get { return estado; }
			set { estado = value; }
		}


	}
}