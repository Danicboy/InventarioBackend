using InvenrarioBack.Context;
using InvenrarioBack.Models.DBModels;
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

            Marca item = new Marca()
            {
                Nombre = mar.Nombre
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
