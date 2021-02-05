using System;
using System.Collections.Generic;

namespace InventarioBack.Models.DBModels
{
    public partial class Usuario
    {
        public Usuario()
        {
            OrdenCompra = new HashSet<OrdenCompra>();
            OrdenVenta = new HashSet<OrdenVenta>();
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
        public virtual ICollection<OrdenCompra> OrdenCompra { get; set; }
        public virtual ICollection<OrdenVenta> OrdenVenta { get; set; }
    }
}
