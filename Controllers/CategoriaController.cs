using InventarioBack.Context;
using InventarioBack.Models.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvenrarioBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly InventarioContext _context;
        

        public CategoriaController(InventarioContext context)
        {
            _context = context;

        }

        [HttpGet("CategoriaList")]
        [Authorize]
        public async Task<ActionResult> GetCategoriaList()
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


            var lista = await _context.Categoria.OrderBy(x => x.Idcategoria).Select(x => new
            {
                IdCateogira = x.Idcategoria,
                Nombre = x.Nombre,
                IdusuarioCreado = x.IdusuarioCreadoNavigation.Nombre,
                FechaCreado = x.FechaCreado,
                IdusuarioActualizo = x.IdusuarioActualizoNavigation.Nombre,
                FechaActualizado = x.FechaActualizado,
                Estado = x.Estado

            }).ToListAsync();

            return Ok(lista);
        }

        [HttpGet("Categoria/{CategoriaId}")]
        [Authorize]
        public async Task<ActionResult> GetCategoriaId(int CategoriaId)
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

            var categoria = await _context.Categoria.FirstOrDefaultAsync(x => x.Idcategoria == CategoriaId);

            return Ok(categoria);
        }

        [HttpPost("AddCategoria")]
        [Authorize]
        public async Task<ActionResult> PostCategoria(Categoria cate)
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

            var existe = _context.Categoria.Any(x => x.Nombre.Contains(cate.Nombre));

            if (existe)
            {
                return Ok("La categoria ya existe");
            }

            Categoria item = new Categoria()
            {
                Nombre = cate.Nombre,
                IdusuarioCreado = Int32.Parse(_id),
                FechaCreado = DateTime.Now,
                IdusuarioActualizo = Int32.Parse(_id),
                FechaActualizado = DateTime.Now,
                Estado = cate.Estado
            };

            _context.Categoria.Add(item);
            await _context.SaveChangesAsync();

            return Ok("Categoria agregada con exito!!!");
        }

        [HttpPut("UpdateCategoria/{CategoriaId}")]
        [Authorize]
        public async Task<ActionResult> PutCategoria(int CategoriaId, Categoria cate)
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

            var categoria = await _context.Categoria.FirstOrDefaultAsync(x => x.Idcategoria == CategoriaId);

            var validar = categoria == null;

            if (validar)
            {
                return NotFound();
            }

            categoria.Nombre = cate.Nombre;
            categoria.IdusuarioActualizo = Int32.Parse(_id);
            categoria.FechaActualizado = DateTime.Now;
            categoria.Estado = cate.Estado;

            await _context.SaveChangesAsync();

            return Ok("Categoria actualizada con exito!!!");
        }

        [HttpDelete("DeleteCategoria/{CategoriaId}")]
        [Authorize]
        public async Task<ActionResult> DeleteCategoria(int CategoriaId)
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

            var categoria = await _context.Categoria.FirstOrDefaultAsync(x => x.Idcategoria == CategoriaId);

            var validar = categoria == null;

            if (validar)
            {
                return NotFound();
            }

            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();

            return Ok("Categoria Eliminada con exito!!!");
        }

    }
}
