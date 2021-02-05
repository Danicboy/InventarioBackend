using InventarioBack.Context;
using InventarioBack.Models.DBModels;
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
        public async Task<ActionResult> GetCategoriaList()
        {
            var lista = await _context.Categoria.OrderBy(x => x.Idcategoria).Select(x => new
            {
                IdCateogira = x.Idcategoria,
                Nombre = x.Nombre,

            }).ToListAsync();

            return Ok(lista);
        }

        [HttpGet("Categoria/{CategoriaId}")]
        public async Task<ActionResult> GetCategoriaId(int CategoriaId)
        {
            var categoria = await _context.Categoria.FirstOrDefaultAsync(x => x.Idcategoria == CategoriaId);

            return Ok(categoria);
        }

        [HttpPost("AddCategoria")]
        public async Task<ActionResult> PostCategoria(Categoria cate)
        {

            Categoria item = new Categoria()
            {
                Nombre = cate.Nombre
            };

            _context.Categoria.Add(item);
            await _context.SaveChangesAsync();

            return Ok("Categoria agregada con exito!!!");
        }

        [HttpPut("UpdateCategoria/{CategoriaId}")]
        public async Task<ActionResult> PutCategoria(int CategoriaId, Categoria cate)
        {
            var categoria = await _context.Categoria.FirstOrDefaultAsync(x => x.Idcategoria == CategoriaId);

            var validar = categoria == null;

            if (validar)
            {
                return NotFound();
            }

            categoria.Nombre = cate.Nombre;

            await _context.SaveChangesAsync();

            return Ok("Categoria actualizada con exito!!!");
        }

        [HttpDelete("DeleteCategoria/{CategoriaId}")]
        public async Task<ActionResult> DeleteCategoria(int CategoriaId)
        {
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
