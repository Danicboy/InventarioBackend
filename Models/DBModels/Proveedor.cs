﻿using System;
using System.Collections.Generic;

namespace InvenrarioBack.Models.DBModels
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            OrdenCompra = new HashSet<OrdenCompra>();
        }

        public int Idproveedor { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public virtual ICollection<OrdenCompra> OrdenCompra { get; set; }
    }
}