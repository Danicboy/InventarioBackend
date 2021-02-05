﻿using System;
using System.Collections.Generic;

namespace InvenrarioBack.Models.DBModels
{
    public partial class DetalleOrdenCompra
    {
        public int IddetalleOc { get; set; }
        public int IdordenCompra { get; set; }
        public int Idproducto { get; set; }
        public int Iddimension { get; set; }
        public int IdunidadMedida { get; set; }
        public int Cantidad { get; set; }

        public virtual Dimensiones IddimensionNavigation { get; set; }
        public virtual OrdenCompra IdordenCompraNavigation { get; set; }
        public virtual Producto IdproductoNavigation { get; set; }
        public virtual UnidadesDeMedida IdunidadMedidaNavigation { get; set; }
    }
}