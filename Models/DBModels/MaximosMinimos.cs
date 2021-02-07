using System;
using System.Collections.Generic;

namespace InventarioBack.Models.DBModels
{
    public partial class MaximosMinimos
    {
        public int IdmaxMin { get; set; }
        public int Idproducto { get; set; }
        public int MinimoAceptable { get; set; }
        public int MaximoAceptable { get; set; }
        public int? IdusuarioCreado { get; set; }
        public DateTime? FechaCreado { get; set; }
        public int? IdusuarioActualizo { get; set; }
        public DateTime? FechaActualizado { get; set; }
        public bool? Estado { get; set; }

        public virtual Producto IdproductoNavigation { get; set; }
        public virtual Usuario IdusuarioActualizoNavigation { get; set; }
        public virtual Usuario IdusuarioCreadoNavigation { get; set; }
    }
}
