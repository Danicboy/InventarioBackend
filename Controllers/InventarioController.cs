using InventarioBack.Context;
using InventarioBack.Models.DBModels;
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
    public class InventarioController : ControllerBase
    {
        private readonly InventarioContext _context;

        public InventarioController(InventarioContext context)
        {
            _context = context;
        }

        [HttpGet("InventarioList")]
        public async Task<ActionResult> InventarioList()
        {
            var lista = await _context.Inventario.OrderBy(x => x.Idinventario).Select(x => new
            {
                IdInventario = x.Idinventario,
                IdProducto = x.Idproducto,
                cantidad = x.Cantidad,
                IdUsuarioCreado = x.IdusuarioCreado,
                fechaCreado = x.FechaCreado,
                idUsuarioActualizado = x.IdusuarioActualizo,
                fechaActualizado = x.FechaActualizado,
                estado = x.Estado
            }).ToListAsync();

            return Ok(lista);
        }

        [HttpGet("Inventario/{Id}")]
        public async Task<ActionResult> InventarioId(int Id)
        {
            var inventario = await _context.Inventario.FirstOrDefaultAsync(x => x.Idinventario == Id);

            return Ok(inventario);
        }

        [HttpPost("AddInventario")]
        public async Task<ActionResult> PostInventario(Inventario inventario)
        {

            var existe = _context.Inventario.Any(x => x.Idproducto == inventario.Idproducto);

            if (existe)
            {
                return Ok("El producto ya tiene un inventario creado");
            }

            Inventario item = new Inventario()
            {
                Idproducto = inventario.Idproducto,
                Cantidad = inventario.Cantidad,
                IdusuarioCreado = inventario.IdusuarioCreado,
                FechaCreado = DateTime.Now,
                IdusuarioActualizo = inventario.IdusuarioActualizo,
                FechaActualizado = DateTime.Now,
                Estado = inventario.Estado
            };

            _context.Inventario.Add(item);
            await _context.SaveChangesAsync();

            return Ok("Inventario para producto creado");
        }

        [HttpPut("UpdateInventario/{InventarioId}")]
        public async Task<ActionResult> UpdateInventario(int Id, Inventario inventario)
        {
            var invent = await _context.Inventario.FirstOrDefaultAsync(x => x.Idinventario == Id);

            var validar = invent == null;

            if (validar)
            {
                return NotFound();
            }

            invent.FechaActualizado = DateTime.Now;
            invent.IdusuarioActualizo = inventario.IdusuarioActualizo;
            invent.Estado = inventario.Estado;

            await _context.SaveChangesAsync();

            return Ok("Producto del inventario actualizado");

        }
    }
}
