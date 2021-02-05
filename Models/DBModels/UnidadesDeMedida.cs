using System;
using System.Collections.Generic;

namespace InventarioBack.Models.DBModels
{
    public partial class UnidadesDeMedida
    {
        public UnidadesDeMedida()
        {
            DetalleOrdenCompra = new HashSet<DetalleOrdenCompra>();
            DetalleOrdenVenta = new HashSet<DetalleOrdenVenta>();
        }

        public int IdunidadMedida { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<DetalleOrdenCompra> DetalleOrdenCompra { get; set; }
        public virtual ICollection<DetalleOrdenVenta> DetalleOrdenVenta { get; set; }
    }
}
