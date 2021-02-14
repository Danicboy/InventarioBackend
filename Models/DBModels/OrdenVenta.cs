using System;
using System.Collections.Generic;

namespace InventarioBack.Models.DBModels
{
    public partial class OrdenVenta
    {
        public OrdenVenta()
        {
            DetalleOrdenVenta = new HashSet<DetalleOrdenVenta>();
        }

        public int IdordenVenta { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaSalida { get; set; }
        public int UserCreatedId { get; set; }
        public int Idcliente { get; set; }
        public int? IdestadoOrdenVenta { get; set; }
        public string Tipo { get; set; }

        public virtual Cliente IdclienteNavigation { get; set; }
        public virtual EstadoOrdenVenta IdestadoOrdenVentaNavigation { get; set; }
        public virtual Usuario UserCreated { get; set; }
        public virtual ICollection<DetalleOrdenVenta> DetalleOrdenVenta { get; set; }
    }
}
