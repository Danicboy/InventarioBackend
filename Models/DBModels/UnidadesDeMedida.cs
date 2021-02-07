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
        public int? IdusuarioCreado { get; set; }
        public DateTime? FechaCreado { get; set; }
        public int? IdusuarioActualizo { get; set; }
        public DateTime? FechaActualizado { get; set; }
        public bool? Estado { get; set; }

        public virtual Usuario IdusuarioActualizoNavigation { get; set; }
        public virtual Usuario IdusuarioCreadoNavigation { get; set; }
        public virtual ICollection<DetalleOrdenCompra> DetalleOrdenCompra { get; set; }
        public virtual ICollection<DetalleOrdenVenta> DetalleOrdenVenta { get; set; }
    }
}
