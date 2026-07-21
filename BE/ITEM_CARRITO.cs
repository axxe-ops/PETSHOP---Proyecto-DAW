using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE
{
    public class ITEM_CARRITO
    {
		private PRODUCTO producto;
		public PRODUCTO Producto
		{
			get { return producto; }
			set { producto = value; }
		}

		private int cantidad;
		public int Cantidad
		{
			get { return cantidad; }
			set { cantidad = value; }
		}

		private Decimal subTotal;
		public Decimal Subtotal
		{
			get { return subTotal; }
			set { subTotal = value; }
		}

		public void CalcularSubTotal()
		{
			Subtotal = Cantidad * Producto.Precio;
		}

	}
}