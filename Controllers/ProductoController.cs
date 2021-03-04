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
                IDMarca = x.IdmarcaNavigation.Nombre,
                IDCategoria = x.IdcategoriaNavigation.Nombre,
                IDDimension = x.IddimensionNavigation.Nombre,
                IdUsuarioCreado = x.IdusuarioCreadoNavigation.Nombre,
                FechaCreado = x.FechaCreado,
                IdUsuarioActualizado = x.IdusuarioActualizoNavigation.Nombre,
                FechaActualizado = x.FechaActualizado,
                Estado = x.Estado,
                PrecioCompra = x.PrecioCompra,
                PrecioVenta = x.PrecioVenta

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
            var existe = _context.Producto.Any(x => x.Idproducto == pro.Idproducto);

            Producto item = new Producto()
            {
                Idmarca = pro.Idmarca,
                Idcategoria = pro.Idcategoria,
                Iddimension = pro.Iddimension,
                IdusuarioCreado = pro.IdusuarioCreado,
                FechaCreado = DateTime.Now,
                IdusuarioActualizo = pro.IdusuarioActualizo,
                FechaActualizado = DateTime.Now,
                Estado = pro.Estado,
                PrecioCompra = pro.PrecioCompra,
                PrecioVenta = pro.PrecioVenta
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

            producto.Idmarca = pro.Idmarca;
            producto.Idcategoria = pro.Idcategoria;
            producto.Iddimension = pro.Iddimension;
            producto.IdusuarioActualizo = pro.IdusuarioActualizo;
            producto.FechaActualizado = DateTime.Now;
            producto.Estado = pro.Estado;
            producto.PrecioCompra = pro.PrecioCompra;
            producto.PrecioVenta = pro.PrecioVenta;

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
