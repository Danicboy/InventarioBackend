using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioBack.Models.DTOs
{
    public class DTOOrdenVentaDetalle
    {
        public int IddetalleOv { get; set; }
        public int IdordenVenta { get; set; }
        public int Idproducto { get; set; }
        public int IdunidadMedida { get; set; }
        public int Cantidad { get; set; }
    }
}
