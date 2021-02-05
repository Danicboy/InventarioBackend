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
    public class ProductoController : ControllerBase
    {
        private readonly InventarioContext _context;

        public ProductoController(InventarioContext context)
        {
            _context = context;
        }

        [HttpGet("ProductoList")]
        public async Task<ActionResult> GetProductoList()
        {
            var lista = await _context.Producto.OrderBy(x => x.Idproducto).Select(x => new
            {
                IDProducto = x.Idproducto,
                Nombre = x.Nombre,
                IDMarca = x.Idmarca,
                PrecioUnitario = x.PrecioUnitario,
                IDCategoria = x.Idcategoria

            }).ToListAsync();

            return Ok(lista);
        }

        [HttpGet("Producto/{IdProducto}")]
        public async Task<ActionResult> GetProdcutoId(int IdProducto)
        {
            var producto = await _context.Producto.FirstOrDefaultAsync(x => x.Idproducto == IdProducto);

            return Ok(producto);
        }

        [HttpPost("AddProducto")]
        public async Task<ActionResult> PostProducto(Producto pro)
        {
            Producto item = new Producto()
            {
                Nombre = pro.Nombre,
                Idmarca = pro.Idmarca,
                PrecioUnitario = pro.PrecioUnitario,
                Idcategoria = pro.Idcategoria
            };

            _context.Producto.Add(item);
            await _context.SaveChangesAsync();

            return Ok("Producto agregado con exito!!!");
        }

        [HttpPut("UpdateProducto/{IdProducto}")]
        public async Task<ActionResult> UpdateProducto(int IdProducto, Producto pro)
        {
            var producto = await _context.Producto.FirstOrDefaultAsync(x => x.Idproducto == IdProducto);

            var validar = producto == null;

            if (validar)
            {
                return NotFound();
            }

            producto.Nombre = pro.Nombre;
            producto.Idmarca = pro.Idmarca;
            producto.PrecioUnitario = pro.PrecioUnitario;
            producto.Idcategoria = pro.Idcategoria;

            await _context.SaveChangesAsync();

            return Ok("Producto actualizado con exito!!!");

        }

        [HttpDelete("DeleteProducto/{IdProducto}")]
        public async Task<ActionResult> DeleteProducto(int IdProducto)
        {
            var producto = await _context.Producto.FirstOrDefaultAsync(x => x.Idproducto == IdProducto);

            var validar = producto == null;

            if (validar)
            {
                return NotFound();
            }

            _context.Producto.Remove(producto);
            await _context.SaveChangesAsync();

            return Ok("Producto eliminado con exito!!!");

        }
    }
}
