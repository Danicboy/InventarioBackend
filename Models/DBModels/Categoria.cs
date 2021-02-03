using System;
using System.Collections.Generic;

namespace InvenrarioBack.Models.DBModels
{
    public partial class Categoria
    {
        public Categoria()
        {
            Producto = new HashSet<Producto>();
        }

        public int Idcategoria { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Producto> Producto { get; set; }
    }
}
