using System;
using System.Collections.Generic;

namespace InventarioBack.Models.DBModels
{
    public partial class Cliente
    {
        public Cliente()
        {
            OrdenVenta = new HashSet<OrdenVenta>();
        }

        public int Idcliente { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool EstadoCliente { get; set; }

        public virtual ICollection<OrdenVenta> OrdenVenta { get; set; }
    }
}
