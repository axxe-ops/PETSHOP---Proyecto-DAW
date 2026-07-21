using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE
{
    public class CARRITO
    {
        public CARRITO()
        {
            Items = new List<ITEM_CARRITO>();
        }

        private List<ITEM_CARRITO> items;
		public List<ITEM_CARRITO> Items
		{
			get { return items; }
			set { items = value; }
		}

		private Decimal costoTotal;
		public Decimal CostoTotal
		{
			get { return costoTotal; }
			set { costoTotal = value; }
		}

		public Decimal CalcularTotal()
		{
			decimal total = 0;
			foreach (ITEM_CARRITO item in Items)
			{
				total = total + item.Subtotal;
			}

			return total;
		}


	}
}