using System;
using System.Collections.Generic;

namespace InvenrarioBack.Models.DBModels
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
        public bool EstadoOrdenVenta { get; set; }

        public virtual Cliente IdclienteNavigation { get; set; }
        public virtual Usuario UserCreated { get; set; }
        public virtual ICollection<DetalleOrdenVenta> DetalleOrdenVenta { get; set; }
    }
}
