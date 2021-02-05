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
    public class MaximosMinimosController : ControllerBase
    {
        private readonly InventarioContext _context;

        public MaximosMinimosController(InventarioContext context)
        {
            _context = context;
        }

        [HttpGet("MaxMinList")]
        public async Task<ActionResult> GetMaxMixList()
        {
            var lista = await _context.MaximosMinimos.OrderBy(x => x.IdmaxMin).Select(x => new
            {
                IDMaxMin = x.IdmaxMin,
                IDProducto = x.Idproducto,
                MinimoAceptable = x.MinimoAceptable,
                MaximoAceptable = x.MaximoAceptable
            }).ToListAsync();

            return Ok(lista);
        }

        [HttpGet("MaxMin/{IdMaxMin}")]
        public async Task<ActionResult> GetMaxMixId(int IdMaxMin)
        {
            var maxMin = await _context.MaximosMinimos.FirstOrDefaultAsync(x => x.IdmaxMin == IdMaxMin);

            return Ok(maxMin);
        }     
        
        [HttpPost("AddMaxMin")]
        public async Task<ActionResult> PostMaxMix(MaximosMinimos maxmin)
        {
            MaximosMinimos item = new MaximosMinimos()
            {
                Idproducto = maxmin.Idproducto,
                MinimoAceptable = maxmin.MinimoAceptable,
                MaximoAceptable = maxmin.MaximoAceptable
            };

            _context.MaximosMinimos.Add(item);
            await _context.SaveChangesAsync();

            return Ok("MaxMin agregado con exito!!!");

        }

        [HttpPut("UpdateMaxMin/{IdMaxMin}")]
        public async Task<ActionResult> UpdateMaxMin(int IdMaxMin, MaximosMinimos maxmin)
        {
            var maxMin = await _context.MaximosMinimos.FirstOrDefaultAsync(x => x.IdmaxMin == IdMaxMin);

            var validar = maxMin == null;

            if (validar)
            {
                return NotFound();
            }

            maxMin.Idproducto = maxmin.Idproducto;
            maxMin.MinimoAceptable = maxmin.MinimoAceptable;
            maxMin.MaximoAceptable = maxmin.MaximoAceptable;

            await _context.SaveChangesAsync();

            return Ok("MaxMin actualizado con exito!!!");
        }

        [HttpDelete("DeleteMaxMin/{idMaxMin}")]
        public async Task<ActionResult> DeleteMaxMin(int idMaxMin)
        {
            var maxMin = await _context.MaximosMinimos.FirstOrDefaultAsync(x => x.IdmaxMin == idMaxMin);

            var validar = maxMin == null;

            if (validar)
            {
                return NotFound();
            }

            _context.MaximosMinimos.Remove(maxMin);
            await _context.SaveChangesAsync();

            return Ok("MaxMin eliminado con exito!!!");
        }
    }
}
