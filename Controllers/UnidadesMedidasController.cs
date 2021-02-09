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
    public class UnidadesMedidasController : ControllerBase
    {
        private readonly InventarioContext _context;

        public UnidadesMedidasController(InventarioContext context)
        {
            _context = context;
        }

        [HttpGet("UnidadesMedidasList")]
        public async Task<ActionResult> GetUnidadesMedidasList()
        {
            var lista = await _context.UnidadesDeMedida.OrderBy(x => x.IdunidadMedida).Select(x => new
            {
                IdUnidadesMedidas = x.IdunidadMedida,
                Nombre = x.Nombre,
                IdUsuarioCreado = x.IdusuarioCreado,
                FechaCreado = x.FechaCreado,
                IdUsuarioActualizado = x.IdusuarioActualizo,
                FechaActualizado = x.FechaActualizado,
                Estado = x.Estado

            }).ToListAsync();

            return Ok(lista);

        }

        [HttpGet("UnidadesMedidas/{UnidadesMedidasId}")]
        public async Task<ActionResult> GetUnidadesMedidasId(int UnidadesMedidasId)
        {
            var unidadesMedidas = await _context.UnidadesDeMedida.FirstOrDefaultAsync(x => x.IdunidadMedida == UnidadesMedidasId);

            return Ok(unidadesMedidas);
        }

        [HttpPost("AddUnidadesMedidas")]
        public async Task<ActionResult> PostUnidadesMedidas(UnidadesDeMedida uni)
        {
            UnidadesDeMedida item = new UnidadesDeMedida()
            {
                Nombre = uni.Nombre,
                IdusuarioCreado = uni.IdusuarioCreado,
                FechaCreado = DateTime.Now,
                IdusuarioActualizo = uni.IdusuarioActualizo,
                FechaActualizado = DateTime.Now,
                Estado = uni.Estado
            };

            _context.UnidadesDeMedida.Add(item);
            await _context.SaveChangesAsync();

            return Ok("Unidades y Medidas agregado con exito!!!");

        }

        [HttpPut("UpdateUnidadesMedidas/{UnidadesMedidasId}")]
        public async Task<ActionResult> UpdateUnidadesMedidas(int UnidadesMedidasId, UnidadesDeMedida uni)
        {
            var unidadesMedidas = await _context.UnidadesDeMedida.FirstOrDefaultAsync(x => x.IdunidadMedida == UnidadesMedidasId);

            var validar = unidadesMedidas == null;

            if (validar)
            {
                return NotFound();
            }

            unidadesMedidas.Nombre = uni.Nombre;
            unidadesMedidas.IdusuarioActualizo = uni.IdusuarioActualizo;
            unidadesMedidas.FechaActualizado = DateTime.Now;
            unidadesMedidas.Estado = uni.Estado;

            await _context.SaveChangesAsync();

            return Ok("Unidades y Medidas actualizado con exito!!!");
        }

        [HttpDelete("DeleteUnidadesMedidas/{UnidadesMedidasId}")]
        public async Task<ActionResult> DeleteUnidadesMedidas(int UnidadesMedidasId)
        {
            var unidadesMedidas = await _context.UnidadesDeMedida.FirstOrDefaultAsync(x => x.IdunidadMedida == UnidadesMedidasId);

            var validar = unidadesMedidas == null;

            if (validar)
            {
                return NotFound();
            }

            _context.UnidadesDeMedida.Remove(unidadesMedidas);
            await _context.SaveChangesAsync();

            return Ok("Unidades y Medidas eliminado con exito!!!");
        }
    }
}
