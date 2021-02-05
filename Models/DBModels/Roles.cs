using System;
using System.Collections.Generic;

namespace InventarioBack.Models.DBModels
{
    public partial class Roles
    {
        public Roles()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int Idrole { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
