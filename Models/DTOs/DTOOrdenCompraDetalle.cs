using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioBack.Models.DTOs
{
    public class DTOOrdenCompraDetalle
    {
        public int IddetalleOc { get; set; }
        public int IdordenCompra { get; set; }
        public int Idproducto { get; set; }
        public int IdunidadMedida { get; set; }
        public int Cantidad { get; set; }
        public decimal? TotalUnidadCompra { get; set; }
    }
}
