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
    public class MarcaController : ControllerBase
    {
        private readonly InventarioContext _context;

        public MarcaController(InventarioContext context)
        {
            _context = context;

        }

        [HttpGet("MarcaList")]
        public async Task<ActionResult> GetMarcaList()
        {
            var lista = await _context.Marca.OrderBy(x => x.Idmarca).Select(x => new
            {
                IdMarca = x.Idmarca,
                Nombre = x.Nombre,
                IdUsuarioCreado = x.IdusuarioCreadoNavigation.Nombre,
                FechaCreado = x.FechaCreado,
                IdUsuarioActualizado = x.IdusuarioActualizoNavigation.Nombre,
                FechaActualizado = x.FechaActualizado,
                Estado = x.Estado

            }).ToListAsync();

            return Ok(lista);
        }

        [HttpGet("Marca/{MarcaId}")]
        public async Task<ActionResult> GetMarcaId(int MarcaId)
        {
            var marca = await _context.Marca.FirstOrDefaultAsync(x => x.Idmarca == MarcaId);

            return Ok(marca);
        }

        [HttpPost("AddMarca")]
        public async Task<ActionResult> PostMarca(Marca mar)
        {
            var existe = _context.Marca.Any(x => x.Nombre.Contains(mar.Nombre));

            if (existe)
            {
                return Ok("La marca ya existe");
            }

            Marca item = new Marca()
            {
                Nombre = mar.Nombre,
                IdusuarioCreado = mar.IdusuarioCreado,
                FechaCreado = DateTime.Now,
                IdusuarioActualizo = mar.IdusuarioActualizo,
                FechaActualizado = DateTime.Now,
                Estado = mar.Estado
            };

            _context.Marca.Add(item);
            await _context.SaveChangesAsync();

            return Ok("Marca agregada con exito!!!");
        }

        [HttpPut("UpdateMarca/{MarcaId}")]
        public async Task<ActionResult> PutMarca(int MarcaId, Marca mar)
        {
            var marca = await _context.Marca.FirstOrDefaultAsync(x => x.Idmarca == MarcaId);

            var validar = marca == null;

            if (validar)
            {
                return NotFound();
            }

            marca.Nombre = mar.Nombre;
            marca.IdusuarioActualizo = mar.IdusuarioActualizo;
            marca.FechaActualizado = DateTime.Now;
            marca.Estado = mar.Estado;

            await _context.SaveChangesAsync();

            return Ok("Marca actualizada con exito!!!");
        }

        [HttpDelete("DeleteMarca/{MarcaId}")]
        public async Task<ActionResult> DeleteMarca(int MarcaId)
        {
            var marca = await _context.Marca.FirstOrDefaultAsync(x => x.Idmarca == MarcaId);

            var validar = marca == null;

            if (validar)
            {
                return NotFound();
            }

            _context.Marca.Remove(marca);
            await _context.SaveChangesAsync();

            return Ok("Marca Eliminada con exito!!!");
        }
    }
}
