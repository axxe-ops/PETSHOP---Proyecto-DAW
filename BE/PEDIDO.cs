using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BE
{
    public class PEDIDO : IVerificarDigitos
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

        private string dvh;
        public string Dvh
        {
            get { return dvh; }
            set { dvh = value; }
        }

        public string CalcularDVH()
        {
            string clienteId = Cliente != null ? Cliente.Id.ToString() : "";

            // Forzamos el formato numérico con punto para que coincida siempre
            string montoStr = MontoTotal.ToString("0.00", CultureInfo.InvariantCulture);

            string cadena = $"{Id}{Fecha:yyyyMMddHHmmss}{clienteId}{montoStr}{Estado}";

            return SEGURIDAD.ENCRIPTADO.Hashear(cadena);
        }
    }
}