using System;
using System.Collections.Generic;

namespace InvenrarioBack.Models.DBModels
{
    public partial class Inventario
    {
        public int Idinventario { get; set; }
        public int Idproducto { get; set; }
        public int Cantidad { get; set; }

        public virtual Producto IdproductoNavigation { get; set; }
    }
}
