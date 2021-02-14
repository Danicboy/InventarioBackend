using System;
using System.Collections.Generic;

namespace InventarioBack.Models.DBModels
{
    public partial class Dimensiones
    {
        public Dimensiones()
        {
            Producto = new HashSet<Producto>();
        }

        public int Iddimension { get; set; }
        public string Nombre { get; set; }
        public int? IdusuarioCreado { get; set; }
        public DateTime? FechaCreado { get; set; }
        public int? IdusuarioActualizo { get; set; }
        public DateTime? FechaActualizado { get; set; }
        public bool? Estado { get; set; }

        public virtual Usuario IdusuarioActualizoNavigation { get; set; }
        public virtual Usuario IdusuarioCreadoNavigation { get; set; }
        public virtual ICollection<Producto> Producto { get; set; }
    }
}
