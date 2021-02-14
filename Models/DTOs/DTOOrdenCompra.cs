using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioBack.Models.DTOs
{
    public class DTOOrdenCompra
    {
        public int IdordenCompra { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaEspectativa { get; set; }
        public int UserCreatedId { get; set; }
        public int Idproveedor { get; set; }
        public bool EstadoOrdenCompra { get; set; }
        public string Tipo { get; set; }

        public List<DTOOrdenCompraDetalle> DetalleOrdenCompra { get; set; } = new List<DTOOrdenCompraDetalle>();
    }
}
