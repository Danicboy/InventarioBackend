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
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        public async Task<ActionResult> OrdenesLista()
        {
            string _id = null;

            try
            {
                _id = User.Claims.First(x => x.Type == "Idusuario").Value;
            }
            catch (Exception)
            {
                return await Task.FromResult(StatusCode(401, "Acceso restringido"));
            }

            var lista = await _context.OrdenVenta.OrderBy(x => x.IdordenVenta).Select(x => new
            {
                idOrdenVenta = x.IdordenVenta,
                fechaCreacion = x.FechaCreacion,
                fechaSalida = x.FechaSalida,
                userCreatedId = _context.Usuario.FirstOrDefault(y => y.Idusuario == x.UserCreatedId).Nombre,
                idCliente = x.IdclienteNavigation.Nombre,
                estado = x.IdestadoOrdenVentaNavigation.NombreEstado,
                tipo = x.Tipo,
                total = x.Total,
                detalleOrdenVenta = _context.DetalleOrdenVenta.ToList()
            }).ToListAsync();

            return Ok(lista);
        }

        [HttpGet("OrdenVenta/{Id}")]
        [Authorize]
        public async Task<ActionResult> OrdenCompraId(int Id)
        {
            string _id = null;

            try
            {
                _id = User.Claims.First(x => x.Type == "Idusuario").Value;
            }
            catch (Exception)
            {
                return await Task.FromResult(StatusCode(401, "Acceso restringido"));
            }

            var ordenVenta = await _context.OrdenVenta.Include(detalle => detalle.DetalleOrdenVenta).FirstOrDefaultAsync(x => x.IdordenVenta == Id);

            return Ok(ordenVenta);
        }

        [HttpPost("AddOrdenVenta")]
        [Authorize]
        public async Task<ActionResult> PostOrdenCompra(DTOOrdenVenta orden)
        {
            string _id = null;

            try
            {
                _id = User.Claims.First(x => x.Type == "Idusuario").Value;
            }
            catch (Exception)
            {
                return await Task.FromResult(StatusCode(401, "Acceso restringido"));
            }

            OrdenVenta item = new OrdenVenta()
            {
                FechaCreacion = DateTime.Now,
                FechaSalida = orden.FechaSalida,
                UserCreatedId = Int32.Parse(_id),
                Idcliente = orden.Idcliente,
                IdestadoOrdenVenta = (int)EnumVentas.Proceso,
                Tipo = orden.Tipo,
                SubTotal = orden.SubTotal,
                IdDescuento = orden.IdDescuento,
                MontoDescuento = orden.MontoDescuento,
                Impuesto = orden.Impuesto,
                Total = orden.Total
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
                    TotalUnidadVenta = detalle.TotalUnidadVenta
                };

                _context.DetalleOrdenVenta.Add(itemDetalle);

                //var producto = await _context.Inventario.FirstOrDefaultAsync(x => x.Idproducto == detalle.Idproducto);

                //producto.Cantidad += detalle.Cantidad;

                await _context.SaveChangesAsync();
            }

            return Ok("Orden de venta realizada");

        }

        [HttpPut("UpdateOrdenVenta")]
        [Authorize]
        public async Task<ActionResult> UpdateOrdenVenta(DTOOrdenVenta orden)
        {
            string _id = null;

            try
            {
                _id = User.Claims.First(x => x.Type == "Idusuario").Value;
            }
            catch (Exception)
            {
                return await Task.FromResult(StatusCode(401, "Acceso restringido"));
            }

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
        [Authorize]
        public async Task<ActionResult> AnularOrdenCompra(DTOOrdenVenta orden)
        {
            string _id = null;

            try
            {
                _id = User.Claims.First(x => x.Type == "Idusuario").Value;
            }
            catch (Exception)
            {
                return await Task.FromResult(StatusCode(401, "Acceso restringido"));
            }

            var ordenVenta = await _context.OrdenVenta.FirstOrDefaultAsync(x => x.IdordenVenta == orden.IdordenVenta);

            if (ordenVenta.IdestadoOrdenVenta == (int)EnumVentas.Entregada || ordenVenta.IdestadoOrdenVenta == (int)EnumVentas.Anulada)
            {
                return Ok("Esta orden se encuentra entregada/anulada no es posible anularla");
            }

            ordenVenta.IdestadoOrdenVenta = (int)EnumVentas.Anulada;

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
