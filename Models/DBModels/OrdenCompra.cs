using System;
using System.Collections.Generic;

namespace InventarioBack.Models.DBModels
{
    public partial class OrdenCompra
    {
        public OrdenCompra()
        {
            DetalleOrdenCompra = new HashSet<DetalleOrdenCompra>();
        }

        public int IdordenCompra { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaEspectativa { get; set; }
        public int UserCreatedId { get; set; }
        public int Idproveedor { get; set; }
        public string Tipo { get; set; }
        public int? IdestadoOrdenCompra { get; set; }

        public virtual EstadoOrdenCompra IdestadoOrdenCompraNavigation { get; set; }
        public virtual Proveedor IdproveedorNavigation { get; set; }
        public virtual Usuario UserCreated { get; set; }
        public virtual ICollection<DetalleOrdenCompra> DetalleOrdenCompra { get; set; }
    }
}
