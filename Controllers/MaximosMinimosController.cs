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
    public class MaximosMinimosController : ControllerBase
    {
        private readonly InventarioContext _context;

        public MaximosMinimosController(InventarioContext context)
        {
            _context = context;
        }

        [HttpGet("MaxMinList")]
        [Authorize]
        public async Task<ActionResult> GetMaxMixList()
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

            var lista = await _context.MaximosMinimos.OrderBy(x => x.IdmaxMin).Select(x => new
            {
                IDMaxMin = x.IdmaxMin,
                IDProducto = x.IdproductoNavigation.IdmarcaNavigation.Nombre + x.IdproductoNavigation.IddimensionNavigation.Nombre,
                MinimoAceptable = x.MinimoAceptable,
                MaximoAceptable = x.MaximoAceptable,
                IdUsuarioCreado = x.IdusuarioCreadoNavigation.Nombre,
                FechaCreado = x.FechaCreado,
                IdUsuarioActualizado = x.IdusuarioActualizoNavigation.Nombre,
                FechaActualizado = x.FechaActualizado,
                Estado = x.Estado

            }).ToListAsync();

            return Ok(lista);
        }

        [HttpGet("MaxMin/{IdMaxMin}")]
        [Authorize]
        public async Task<ActionResult> GetMaxMixId(int IdMaxMin)
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

            var maxMin = await _context.MaximosMinimos.FirstOrDefaultAsync(x => x.IdmaxMin == IdMaxMin);

            return Ok(maxMin);
        }     
        
        [HttpPost("AddMaxMin")]
        [Authorize]
        public async Task<ActionResult> PostMaxMix(MaximosMinimos maxmin)
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

            MaximosMinimos item = new MaximosMinimos()
            {
                Idproducto = maxmin.Idproducto,
                MinimoAceptable = maxmin.MinimoAceptable,
                MaximoAceptable = maxmin.MaximoAceptable,
                IdusuarioCreado = Int32.Parse(_id),
                FechaCreado = DateTime.Now,
                IdusuarioActualizo = Int32.Parse(_id),
                FechaActualizado = DateTime.Now,
                Estado = maxmin.Estado
            };

            _context.MaximosMinimos.Add(item);
            await _context.SaveChangesAsync();

            return Ok("MaxMin agregado con exito!!!");

        }

        [HttpPut("UpdateMaxMin/{IdMaxMin}")]
        [Authorize]
        public async Task<ActionResult> UpdateMaxMin(int IdMaxMin, MaximosMinimos maxmin)
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

            var maxMin = await _context.MaximosMinimos.FirstOrDefaultAsync(x => x.IdmaxMin == IdMaxMin);

            var validar = maxMin == null;

            if (validar)
            {
                return NotFound();
            }

            maxMin.Idproducto = maxmin.Idproducto;
            maxMin.MinimoAceptable = maxmin.MinimoAceptable;
            maxMin.MaximoAceptable = maxmin.MaximoAceptable;
            maxMin.IdusuarioActualizo = Int32.Parse(_id);
            maxMin.FechaActualizado = DateTime.Now;
            maxMin.Estado = maxmin.Estado;

            await _context.SaveChangesAsync();

            return Ok("MaxMin actualizado con exito!!!");
        }

        [HttpDelete("DeleteMaxMin/{idMaxMin}")]
        [Authorize]
        public async Task<ActionResult> DeleteMaxMin(int idMaxMin)
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
