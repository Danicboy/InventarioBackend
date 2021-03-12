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
    public class UnidadesMedidasController : ControllerBase
    {
        private readonly InventarioContext _context;

        public UnidadesMedidasController(InventarioContext context)
        {
            _context = context;
        }

        [HttpGet("UnidadesMedidasList")]
        [Authorize]
        public async Task<ActionResult> GetUnidadesMedidasList()
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

            var lista = await _context.UnidadesDeMedida.OrderBy(x => x.IdunidadMedida).Select(x => new
            {
                IdUnidadesMedidas = x.IdunidadMedida,
                Nombre = x.Nombre,
                IdUsuarioCreado = x.IdusuarioCreadoNavigation.Nombre,
                FechaCreado = x.FechaCreado,
                IdUsuarioActualizado = x.IdusuarioActualizoNavigation.Nombre,
                FechaActualizado = x.FechaActualizado,
                Estado = x.Estado

            }).ToListAsync();

            return Ok(lista);

        }

        [HttpGet("UnidadesMedidas/{UnidadesMedidasId}")]
        [Authorize]
        public async Task<ActionResult> GetUnidadesMedidasId(int UnidadesMedidasId)
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

            var unidadesMedidas = await _context.UnidadesDeMedida.FirstOrDefaultAsync(x => x.IdunidadMedida == UnidadesMedidasId);

            return Ok(unidadesMedidas);
        }

        [HttpPost("AddUnidadesMedidas")]
        [Authorize]
        public async Task<ActionResult> PostUnidadesMedidas(UnidadesDeMedida uni)
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

            var existe = _context.UnidadesDeMedida.Any(x => x.Nombre.Contains(uni.Nombre));

            if (existe)
            {
                return Ok("La medida ya existe");
            }

            UnidadesDeMedida item = new UnidadesDeMedida()
            {
                Nombre = uni.Nombre,
                IdusuarioCreado = Int32.Parse(_id),
                FechaCreado = DateTime.Now,
                IdusuarioActualizo = Int32.Parse(_id),
                FechaActualizado = DateTime.Now,
                Estado = uni.Estado
            };

            _context.UnidadesDeMedida.Add(item);
            await _context.SaveChangesAsync();

            return Ok("Unidades y Medidas agregado con exito!!!");

        }

        [HttpPut("UpdateUnidadesMedidas/{UnidadesMedidasId}")]
        [Authorize]
        public async Task<ActionResult> UpdateUnidadesMedidas(int UnidadesMedidasId, UnidadesDeMedida uni)
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

            var unidadesMedidas = await _context.UnidadesDeMedida.FirstOrDefaultAsync(x => x.IdunidadMedida == UnidadesMedidasId);

            var validar = unidadesMedidas == null;

            if (validar)
            {
                return NotFound();
            }

            unidadesMedidas.Nombre = uni.Nombre;
            unidadesMedidas.IdusuarioActualizo = Int32.Parse(_id);
            unidadesMedidas.FechaActualizado = DateTime.Now;
            unidadesMedidas.Estado = uni.Estado;

            await _context.SaveChangesAsync();

            return Ok("Unidades y Medidas actualizado con exito!!!");
        }

        [HttpDelete("DeleteUnidadesMedidas/{UnidadesMedidasId}")
        [Authorize]
        public async Task<ActionResult> DeleteUnidadesMedidas(int UnidadesMedidasId)
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
