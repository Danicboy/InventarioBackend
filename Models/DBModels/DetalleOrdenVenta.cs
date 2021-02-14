using System;
using System.Collections.Generic;

namespace InventarioBack.Models.DBModels
{
    public partial class DetalleOrdenVenta
    {
        public int IddetalleOv { get; set; }
        public int IdordenVenta { get; set; }
        public int Idproducto { get; set; }
        public int IdunidadMedida { get; set; }
        public int Cantidad { get; set; }

        public virtual OrdenVenta IdordenVentaNavigation { get; set; }
        public virtual Producto IdproductoNavigation { get; set; }
        public virtual UnidadesDeMedida IdunidadMedidaNavigation { get; set; }
    }
}
