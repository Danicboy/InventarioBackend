using System;
using System.Collections.Generic;

namespace InventarioBack.Models.DBModels
{
    public partial class Descuento
    {
        public Descuento()
        {
            OrdenVenta = new HashSet<OrdenVenta>();
        }

        public int IdDescuento { get; set; }
        public string Descripcion { get; set; }
        public decimal Valor { get; set; }

        public virtual ICollection<OrdenVenta> OrdenVenta { get; set; }
    }
}
