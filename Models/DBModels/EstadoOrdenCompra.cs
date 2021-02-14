using System;
using System.Collections.Generic;

namespace InventarioBack.Models.DBModels
{
    public partial class EstadoOrdenCompra
    {
        public EstadoOrdenCompra()
        {
            OrdenCompra = new HashSet<OrdenCompra>();
        }

        public int IdestadoOrdenCompra { get; set; }
        public string NombreEstado { get; set; }

        public virtual ICollection<OrdenCompra> OrdenCompra { get; set; }
    }
}
