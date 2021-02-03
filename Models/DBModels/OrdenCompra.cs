using System;
using System.Collections.Generic;

namespace InvenrarioBack.Models.DBModels
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
        public bool EstadoOrdenCompra { get; set; }

        public virtual Proveedor IdproveedorNavigation { get; set; }
        public virtual Usuario UserCreated { get; set; }
        public virtual ICollection<DetalleOrdenCompra> DetalleOrdenCompra { get; set; }
    }
}
