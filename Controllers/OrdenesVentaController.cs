using InventarioBack.Context;
using InventarioBack.Models.DBModels;
using InventarioBack.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesVentaController : ControllerBase
    {
        private readonly InventarioContext _context;

        public OrdenesVentaController(InventarioContext context)
        {
            _context = context;
        }

        [HttpGet("Lista")]
        public async Task<ActionResult> OrdenesLista()
        {
            var lista = await _context.OrdenVenta.OrderBy(x => x.IdordenVenta).Select(x => new
            {
                idOrdenVenta = x.IdordenVenta,
                fechaCreacion = x.FechaCreacion,
                fechaSalida = x.FechaSalida,
                userCreatedId = x.UserCreatedId,
                idCliente = x.Idcliente,
                estado = x.IdestadoOrdenVenta,
                tipo = x.Tipo,
                detalleOrdenVenta = _context.DetalleOrdenVenta.ToList()
            }).ToListAsync();

            return Ok(lista);
        }

        [HttpGet("OrdenVenta/{Id}")]
        public async Task<ActionResult> OrdenCompraId(int Id)
        {
            var ordenVenta = await _context.OrdenVenta.Include(detalle => detalle.DetalleOrdenVenta).FirstOrDefaultAsync(x => x.IdordenVenta == Id);

            return Ok(ordenVenta);
        }

        [HttpPost("AddOrdenVenta")]
        public async Task<ActionResult> PostOrdenCompra(DTOOrdenVenta orden)
        {
            OrdenVenta item = new OrdenVenta()
            {
                FechaCreacion = DateTime.Now,
                FechaSalida = orden.FechaSalida,
                UserCreatedId = orden.UserCreatedId,
                Idcliente = orden.Idcliente,
                IdestadoOrdenVenta = (int)EnumVentas.Proceso,
                Tipo = orden.Tipo
            };

            _context.OrdenVenta.Add(item);
            await _context.SaveChangesAsync();

            foreach (var detalle in orden.DetalleOrdenVenta)
            {
                DetalleOrdenVenta itemDetalle = new DetalleOrdenVenta()
                {
                    IdordenVenta = item.IdordenVenta,
                    Idproducto = detalle.Idproducto,
                    IdunidadMedida = detalle.IdunidadMedida,
                    Cantidad = detalle.Cantidad,
                };

                _context.DetalleOrdenVenta.Add(itemDetalle);

                //var producto = await _context.Inventario.FirstOrDefaultAsync(x => x.Idproducto == detalle.Idproducto);

                //producto.Cantidad += detalle.Cantidad;

                await _context.SaveChangesAsync();
            }

            return Ok("Orden de venta realizada");

        }

        [HttpPut("UpdateOrdenVenta")]
        public async Task<ActionResult> UpdateOrdenVenta(DTOOrdenVenta orden)
        {
            var ordenVenta = await _context.OrdenVenta.FirstOrDefaultAsync(x => x.IdordenVenta == orden.IdordenVenta);

            var nextEstado = await Estados(ordenVenta.IdordenVenta);

            ordenVenta.IdestadoOrdenVenta = nextEstado;

            if (ordenVenta.IdestadoOrdenVenta == (int)EnumVentas.Entregada)
            {
                foreach (var detalle in orden.DetalleOrdenVenta)
                {
                    var producto = await _context.Inventario.FirstOrDefaultAsync(x => x.Idproducto == detalle.Idproducto);

                    producto.Cantidad -= detalle.Cantidad;

                }
            }

            await _context.SaveChangesAsync();

            return Ok("Orden de venta actualizada");
        }

        [HttpPut("AnularOrdenVenta")]
        public async Task<ActionResult> AnularOrdenCompra(DTOOrdenCompra orden)
        {

            var ordenCompra = await _context.OrdenCompra.FirstOrDefaultAsync(x => x.IdordenCompra == orden.IdordenCompra);

            if (ordenCompra.IdestadoOrdenCompra == (int)EnumVentas.Entregada || ordenCompra.IdestadoOrdenCompra == (int)EnumVentas.Anulada)
            {
                return Ok("Esta orden se encuentra entregada/anulada no es posible anularla");
            }

            ordenCompra.IdestadoOrdenCompra = (int)EnumVentas.Anulada;

            //foreach (var detalle in orden.DetalleOrdenCompra)
            //{
            //    var producto = await _context.Inventario.FirstOrDefaultAsync(x => x.Idproducto == detalle.Idproducto);

            //    producto.Cantidad -= detalle.Cantidad;

            //}

            await _context.SaveChangesAsync();

            return Ok("Orden de venta anulada");
        }

        public async Task<int> Estados(int idOrdenVenta)
        {
            int estado = 0;

            var ordenVenta = await _context.OrdenVenta.FirstOrDefaultAsync(x => x.IdordenVenta == idOrdenVenta);

            if (ordenVenta.IdestadoOrdenVenta == (int)EnumVentas.Proceso)
            {
                estado = (int)EnumVentas.Entregada;
            }

            return estado;
        }
    }
}
