using System;
using System.Collections.Generic;

namespace InventarioBack.Models.DBModels
{
    public partial class Usuario
    {
        public Usuario()
        {
            CategoriaIdusuarioActualizoNavigation = new HashSet<Categoria>();
            CategoriaIdusuarioCreadoNavigation = new HashSet<Categoria>();
            ClienteIdusuarioActualizoNavigation = new HashSet<Cliente>();
            ClienteIdusuarioCreadoNavigation = new HashSet<Cliente>();
            DimensionesIdusuarioActualizoNavigation = new HashSet<Dimensiones>();
            DimensionesIdusuarioCreadoNavigation = new HashSet<Dimensiones>();
            InventarioIdusuarioActualizoNavigation = new HashSet<Inventario>();
            InventarioIdusuarioCreadoNavigation = new HashSet<Inventario>();
            MarcaIdusuarioActualizoNavigation = new HashSet<Marca>();
            MarcaIdusuarioCreadoNavigation = new HashSet<Marca>();
            MaximosMinimosIdusuarioActualizoNavigation = new HashSet<MaximosMinimos>();
            MaximosMinimosIdusuarioCreadoNavigation = new HashSet<MaximosMinimos>();
            OrdenCompra = new HashSet<OrdenCompra>();
            OrdenVenta = new HashSet<OrdenVenta>();
            ProductoIdusuarioActualizoNavigation = new HashSet<Producto>();
            ProductoIdusuarioCreadoNavigation = new HashSet<Producto>();
            ProveedorIdusuarioActualizoNavigation = new HashSet<Proveedor>();
            ProveedorIdusuarioCreadoNavigation = new HashSet<Proveedor>();
            UnidadesDeMedidaIdusuarioActualizoNavigation = new HashSet<UnidadesDeMedida>();
            UnidadesDeMedidaIdusuarioCreadoNavigation = new HashSet<UnidadesDeMedida>();
        }

        public int Idusuario { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string UserName { get; set; }
        public int Idrole { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Estado { get; set; }
        public string Contrasenia { get; set; }

        public virtual Roles IdroleNavigation { get; set; }
        public virtual ICollection<Categoria> CategoriaIdusuarioActualizoNavigation { get; set; }
        public virtual ICollection<Categoria> CategoriaIdusuarioCreadoNavigation { get; set; }
        public virtual ICollection<Cliente> ClienteIdusuarioActualizoNavigation { get; set; }
        public virtual ICollection<Cliente> ClienteIdusuarioCreadoNavigation { get; set; }
        public virtual ICollection<Dimensiones> DimensionesIdusuarioActualizoNavigation { get; set; }
        public virtual ICollection<Dimensiones> DimensionesIdusuarioCreadoNavigation { get; set; }
        public virtual ICollection<Inventario> InventarioIdusuarioActualizoNavigation { get; set; }
        public virtual ICollection<Inventario> InventarioIdusuarioCreadoNavigation { get; set; }
        public virtual ICollection<Marca> MarcaIdusuarioActualizoNavigation { get; set; }
        public virtual ICollection<Marca> MarcaIdusuarioCreadoNavigation { get; set; }
        public virtual ICollection<MaximosMinimos> MaximosMinimosIdusuarioActualizoNavigation { get; set; }
        public virtual ICollection<MaximosMinimos> MaximosMinimosIdusuarioCreadoNavigation { get; set; }
        public virtual ICollection<OrdenCompra> OrdenCompra { get; set; }
        public virtual ICollection<OrdenVenta> OrdenVenta { get; set; }
        public virtual ICollection<Producto> ProductoIdusuarioActualizoNavigation { get; set; }
        public virtual ICollection<Producto> ProductoIdusuarioCreadoNavigation { get; set; }
        public virtual ICollection<Proveedor> ProveedorIdusuarioActualizoNavigation { get; set; }
        public virtual ICollection<Proveedor> ProveedorIdusuarioCreadoNavigation { get; set; }
        public virtual ICollection<UnidadesDeMedida> UnidadesDeMedidaIdusuarioActualizoNavigation { get; set; }
        public virtual ICollection<UnidadesDeMedida> UnidadesDeMedidaIdusuarioCreadoNavigation { get; set; }
    }
}
