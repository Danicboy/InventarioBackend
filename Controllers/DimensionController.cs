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
    public class DimensionController : ControllerBase
    {
        private readonly InventarioContext _context;

        public DimensionController(InventarioContext context)
        {
            _context = context;

        }

        [HttpGet("DimensionList")]
        [Authorize]
        public async Task<ActionResult> GetDimensionList()
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
        [Authorize]
        public async Task<ActionResult> GetDimensionId(int DimensionId)
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

            var dimension = await _context.Dimensiones.FirstOrDefaultAsync(x => x.Iddimension == DimensionId);

            return Ok(dimension);
        }

        [HttpPost("AddDimension")]
        [Authorize]
        public async Task<ActionResult> PostDimension(Dimensiones dim)
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

            var existe = _context.Dimensiones.Any(x => x.Nombre.Contains(dim.Nombre));

            if (existe)
            {
                return Ok("La dimension ya existe");
            }

            Dimensiones item = new Dimensiones()
            {
                Nombre = dim.Nombre,
                IdusuarioCreado = Int32.Parse(_id),
                FechaCreado = DateTime.Now,
                IdusuarioActualizo = Int32.Parse(_id),
                FechaActualizado = DateTime.Now,
                Estado = dim.Estado
            };

            _context.Dimensiones.Add(item);
            await _context.SaveChangesAsync();

            return Ok("Dimension agregada con exito!!!");
        }

        [HttpPut("UpdateDimension/{DimensionId}")]
        [Authorize]
        public async Task<ActionResult> PutDimension(int DimensionId, Dimensiones dim)
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

            var dimension = await _context.Dimensiones.FirstOrDefaultAsync(x => x.Iddimension == DimensionId);

            var validar = dimension == null;

            if (validar)
            {
                return NotFound();
            }

            dimension.Nombre = dim.Nombre;
            dimension.IdusuarioActualizo = Int32.Parse(_id);
            dimension.FechaActualizado = DateTime.Now;
            dimension.Estado = dim.Estado;

            await _context.SaveChangesAsync();

            return Ok("Dimension actualizada con exito!!!");
        }

        [HttpDelete("DeleteDimension/{DimensionId}")]
        [Authorize]
        public async Task<ActionResult> DeleteDimension(int DimensionId)
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
