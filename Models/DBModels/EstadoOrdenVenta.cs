using System;
using System.Collections.Generic;

namespace InventarioBack.Models.DBModels
{
    public partial class EstadoOrdenVenta
    {
        public EstadoOrdenVenta()
        {
            OrdenVenta = new HashSet<OrdenVenta>();
        }

        public int IdestadoOrdenVenta { get; set; }
        public string NombreEstado { get; set; }

        public virtual ICollection<OrdenVenta> OrdenVenta { get; set; }
    }
}
