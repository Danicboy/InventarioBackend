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

        public virtual Producto IdproductoNavigation { get; set; }
    }
}
