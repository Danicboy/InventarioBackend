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
    public class OrdenesCompraController : ControllerBase
    {
        private readonly InventarioContext _context;

        public OrdenesCompraController(InventarioContext context)
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

            var lista = await _context.OrdenCompra.OrderBy(x => x.IdordenCompra).Select(x => new
            {
                idOrdenCompra = x.IdordenCompra,
                fechaCreacion = x.FechaCreacion,
                fechaEspectativa = x.FechaEspectativa,
                userCreatedId = _context.Usuario.FirstOrDefault(y => y.Idusuario == x.UserCreatedId).Nombre,
                idProveedor = x.IdproveedorNavigation.Nombre,
                estado = x.IdestadoOrdenCompraNavigation.NombreEstado,
                tipo = x.Tipo,
                total = x.Total,
                detalleOrdenCompra = _context.DetalleOrdenCompra.ToList()
            }).ToListAsync();

            return Ok(lista);
        }

        [HttpGet("OrdenCompra/{Id}")]
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

            var ordenCompra = await _context.OrdenCompra.Include(detalle => detalle.DetalleOrdenCompra).FirstOrDefaultAsync(x => x.IdordenCompra == Id);

            return Ok(ordenCompra);
        }

        [HttpPost("AddOrdenCompra")]
        [Authorize]
        public async Task<ActionResult> PostOrdenCompra(DTOOrdenCompra orden)
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

            OrdenCompra item = new OrdenCompra()
            {
                FechaCreacion = DateTime.Now,
                FechaEspectativa = orden.FechaEspectativa,
                UserCreatedId = Int32.Parse(_id),
                Idproveedor = orden.Idproveedor,
                IdestadoOrdenCompra = (int)EnumCompras.Compra,
                Tipo = orden.Tipo,
                SubTotal = orden.SubTotal,
                Impuesto = orden.Impuesto,
                Total = orden.Total
            };

            _context.OrdenCompra.Add(item);
            await _context.SaveChangesAsync();

            foreach (var detalle in orden.DetalleOrdenCompra)
            {
                DetalleOrdenCompra itemDetalle = new DetalleOrdenCompra()
                {
                    IdordenCompra = item.IdordenCompra,
                    Idproducto = detalle.Idproducto,
                    IdunidadMedida = detalle.IdunidadMedida,
                    Cantidad = detalle.Cantidad,
                    TotalUnidadCompra = detalle.TotalUnidadCompra

                };

                _context.DetalleOrdenCompra.Add(itemDetalle);

                //var producto = await _context.Inventario.FirstOrDefaultAsync(x => x.Idproducto == detalle.Idproducto);

                //producto.Cantidad += detalle.Cantidad;

                await _context.SaveChangesAsync();
            }

            return Ok("Orden de compra realizada");

        }

        [HttpPut("UpdateOrdenCompra")]
        [Authorize]
        public async Task<ActionResult> UpdateOrdenCompra(DTOOrdenCompra orden)
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

            var ordenCompra = await _context.OrdenCompra.FirstOrDefaultAsync(x => x.IdordenCompra == orden.IdordenCompra);

            var nextEstado = await Estados(ordenCompra.IdordenCompra);

            ordenCompra.IdestadoOrdenCompra = nextEstado;

            if (ordenCompra.IdestadoOrdenCompra == (int)EnumCompras.Entrega)
            {
                foreach (var detalle in orden.DetalleOrdenCompra)
                {
                    var producto = await _context.Inventario.FirstOrDefaultAsync(x => x.Idproducto == detalle.Idproducto);

                    producto.Cantidad += detalle.Cantidad;

                }
            }

            await _context.SaveChangesAsync();

            return Ok("Orden de compra actualizada");
        }

        [HttpPut("AnularOrdenCompra")]
        [Authorize]
        public async Task<ActionResult> AnularOrdenCompra(DTOOrdenCompra orden)
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

            var ordenCompra = await _context.OrdenCompra.FirstOrDefaultAsync(x => x.IdordenCompra == orden.IdordenCompra);

            if (ordenCompra.IdestadoOrdenCompra == (int)EnumCompras.Entrega || ordenCompra.IdestadoOrdenCompra == (int)EnumCompras.Anulada)
            {
                return Ok("Esta orden se encuentra entregada/anulada no es posible anularla");
            }

            ordenCompra.IdestadoOrdenCompra = (int)EnumCompras.Anulada;

            //foreach (var detalle in orden.DetalleOrdenCompra)
            //{
            //    var producto = await _context.Inventario.FirstOrDefaultAsync(x => x.Idproducto == detalle.Idproducto);

            //    producto.Cantidad -= detalle.Cantidad;

            //}

            await _context.SaveChangesAsync();

            return Ok("Orden de compra anulada");
        }

        public async Task<int> Estados(int idOrdenCompra)
        {

            int estado = 0;

            var ordenCompra = await _context.OrdenCompra.FirstOrDefaultAsync(x => x.IdordenCompra == idOrdenCompra);

            if (ordenCompra.IdestadoOrdenCompra == (int)EnumCompras.Compra)
            {
                estado = (int)EnumCompras.Pendiente;
            }
            else if (ordenCompra.IdestadoOrdenCompra == (int)EnumCompras.Pendiente)
            {
                estado = (int)EnumCompras.Espera;
            }
            else if (ordenCompra.IdestadoOrdenCompra == (int)EnumCompras.Espera)
            {
                estado = (int)EnumCompras.Entrega;
            }

            return estado;

            //var ordenCompra = await _context.OrdenCompra.FirstOrDefaultAsync(x => x.IdordenCompra == idOrdenCompra);

            //int estado = 0;

            //if (ordenCompra.IdestadoOrdenCompra == 1)
            //{
            //    estado = 2;
            //}
            //else if (ordenCompra.IdestadoOrdenCompra == 2)
            //{
            //    estado = 3;
            //}
            //else if(ordenCompra.IdestadoOrdenCompra == 3)
            //{
            //    estado = 4;
            //}

            //return estado;
        }
    }
}
