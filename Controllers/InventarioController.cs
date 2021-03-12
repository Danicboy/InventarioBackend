using InventarioBack.Context;
using InventarioBack.Models.DBModels;
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
    public class InventarioController : ControllerBase
    {
        private readonly InventarioContext _context;

        public InventarioController(InventarioContext context)
        {
            _context = context;
        }

        [HttpGet("InventarioList")]
        [Authorize]
        public async Task<ActionResult> InventarioList()
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

            var lista = await _context.Inventario.OrderBy(x => x.Idinventario).Select(x => new
            {
                IdInventario = x.Idinventario,
                IdProducto = x.IdproductoNavigation.IdmarcaNavigation.Nombre + x.IdproductoNavigation.IddimensionNavigation.Nombre,
                cantidad = x.Cantidad,
                IdUsuarioCreado = x.IdusuarioCreadoNavigation.Nombre,
                fechaCreado = x.FechaCreado,
                idUsuarioActualizado = x.IdusuarioActualizoNavigation.Nombre,
                fechaActualizado = x.FechaActualizado,
                estado = x.Estado
            }).ToListAsync();

            return Ok(lista);
        }

        [HttpGet("Inventario/{Id}")]
        [Authorize]
        public async Task<ActionResult> InventarioId(int Id)
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

            var inventario = await _context.Inventario.FirstOrDefaultAsync(x => x.Idinventario == Id);

            return Ok(inventario);
        }

        [HttpPost("AddInventario")]
        [Authorize]
        public async Task<ActionResult> PostInventario(Inventario inventario)
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

            var existe = _context.Inventario.Any(x => x.Idproducto == inventario.Idproducto);

            if (existe)
            {
                return Ok("El producto ya tiene un inventario creado");
            }

            Inventario item = new Inventario()
            {
                Idproducto = inventario.Idproducto,
                Cantidad = inventario.Cantidad,
                IdusuarioCreado = Int32.Parse(_id),
                FechaCreado = DateTime.Now,
                IdusuarioActualizo = Int32.Parse(_id),
                FechaActualizado = DateTime.Now,
                Estado = inventario.Estado
            };

            _context.Inventario.Add(item);
            await _context.SaveChangesAsync();

            return Ok("Inventario para producto creado");
        }

        [HttpPut("UpdateInventario/{InventarioId}")]
        [Authorize]
        public async Task<ActionResult> UpdateInventario(int Id, Inventario inventario)
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

            var invent = await _context.Inventario.FirstOrDefaultAsync(x => x.Idinventario == Id);

            var validar = invent == null;

            if (validar)
            {
                return NotFound();
            }

            invent.FechaActualizado = DateTime.Now;
            invent.IdusuarioActualizo = Int32.Parse(_id);
            invent.Estado = inventario.Estado;

            await _context.SaveChangesAsync();

            return Ok("Producto del inventario actualizado");

        }
    }
}
