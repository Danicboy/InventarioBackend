using System;
using System.Collections.Generic;

namespace InventarioBack.Models.DBModels
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleOrdenCompra = new HashSet<DetalleOrdenCompra>();
            DetalleOrdenVenta = new HashSet<DetalleOrdenVenta>();
            Inventario = new HashSet<Inventario>();
            MaximosMinimos = new HashSet<MaximosMinimos>();
        }

        public int Idproducto { get; set; }
        public int Idmarca { get; set; }
        public int Idcategoria { get; set; }
        public int? IdusuarioCreado { get; set; }
        public DateTime? FechaCreado { get; set; }
        public int? IdusuarioActualizo { get; set; }
        public DateTime? FechaActualizado { get; set; }
        public bool? Estado { get; set; }
        public int? Iddimension { get; set; }
        public int? PrecioCompra { get; set; }
        public int? PrecioVenta { get; set; }

        public virtual Categoria IdcategoriaNavigation { get; set; }
        public virtual Dimensiones IddimensionNavigation { get; set; }
        public virtual Marca IdmarcaNavigation { get; set; }
        public virtual Usuario IdusuarioActualizoNavigation { get; set; }
        public virtual Usuario IdusuarioCreadoNavigation { get; set; }
        public virtual ICollection<DetalleOrdenCompra> DetalleOrdenCompra { get; set; }
        public virtual ICollection<DetalleOrdenVenta> DetalleOrdenVenta { get; set; }
        public virtual ICollection<Inventario> Inventario { get; set; }
        public virtual ICollection<MaximosMinimos> MaximosMinimos { get; set; }
    }
}
