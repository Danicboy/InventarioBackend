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
    public class DimensionController : ControllerBase
    {
        private readonly InventarioContext _context;

        public DimensionController(InventarioContext context)
        {
            _context = context;

        }

        [HttpGet("DimensionList")]
        public async Task<ActionResult> GetDimensionList()
        {
            var lista = await _context.Dimensiones.OrderBy(x => x.Iddimension).Select(x => new
            {
                IdDimension = x.Iddimension,
                Nombre = x.Nombre,
                IdUsuarioCreado = x.IdusuarioCreadoNavigation.Nombre,
                FechaCreado = x.FechaCreado,
                IdUsuarioActualizado = x.IdusuarioActualizoNavigation.Nombre,
                FechaActualizado = x.FechaActualizado,
                Estado = x.Estado

            }).ToListAsync();

            return Ok(lista);
        }

        [HttpGet("Dimension/{DimensionId}")]
        public async Task<ActionResult> GetDimensionId(int DimensionId)
        {
            var dimension = await _context.Dimensiones.FirstOrDefaultAsync(x => x.Iddimension == DimensionId);

            return Ok(dimension);
        }

        [HttpPost("AddDimension")]
        public async Task<ActionResult> PostDimension(Dimensiones dim)
        {
            var existe = _context.Dimensiones.Any(x => x.Nombre.Contains(dim.Nombre));

            if (existe)
            {
                return Ok("La dimension ya existe");
            }

            Dimensiones item = new Dimensiones()
            {
                Nombre = dim.Nombre,
                IdusuarioCreado = dim.IdusuarioCreado,
                FechaCreado = DateTime.Now,
                IdusuarioActualizo = dim.IdusuarioActualizo,
                FechaActualizado = DateTime.Now,
                Estado = dim.Estado
            };

            _context.Dimensiones.Add(item);
            await _context.SaveChangesAsync();

            return Ok("Dimension agregada con exito!!!");
        }

        [HttpPut("UpdateDimension/{DimensionId}")]
        public async Task<ActionResult> PutDimension(int DimensionId, Dimensiones dim)
        {
            var dimension = await _context.Dimensiones.FirstOrDefaultAsync(x => x.Iddimension == DimensionId);

            var validar = dimension == null;

            if (validar)
            {
                return NotFound();
            }

            dimension.Nombre = dim.Nombre;
            dimension.IdusuarioActualizo = dim.IdusuarioActualizo;
            dimension.FechaActualizado = DateTime.Now;
            dimension.Estado = dim.Estado;

            await _context.SaveChangesAsync();

            return Ok("Dimension actualizada con exito!!!");
        }

        [HttpDelete("DeleteDimension/{DimensionId}")]
        public async Task<ActionResult> DeleteDimension(int DimensionId)
        {
            var dimension = await _context.Dimensiones.FirstOrDefaultAsync(x => x.Iddimension == DimensionId);

            var validar = dimension == null;

            if (validar)
            {
                return NotFound();
            }

            _context.Dimensiones.Remove(dimension);
            await _context.SaveChangesAsync();

            return Ok("Dimension Eliminada con exito!!!");
        }

    }
}
