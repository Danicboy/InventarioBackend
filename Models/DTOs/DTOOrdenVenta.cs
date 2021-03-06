﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioBack.Models.DTOs
{
    public class DTOOrdenVenta
    {
        public int IdordenVenta { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaSalida { get; set; }
        public int UserCreatedId { get; set; }
        public int Idcliente { get; set; }
        public string Tipo { get; set; }
        public int? IdestadoOrdenVenta { get; set; }
        public decimal? SubTotal { get; set; }
        public int? IdDescuento { get; set; }
        public decimal? MontoDescuento { get; set; }
        public decimal? Impuesto { get; set; }
        public decimal? Total { get; set; }

        public List<DTOOrdenVentaDetalle> DetalleOrdenVenta { get; set; } = new List<DTOOrdenVentaDetalle>();
    }
}
