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
    public class ProductoController : ControllerBase
    {
        private readonly InventarioContext _context;

        public ProductoController(InventarioContext context)
        {
            _context = context;
        }

        [HttpGet("ProductoList")]
        [Authorize]
        public async Task<ActionResult> GetProductoList()
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
        [Authorize]
        public async Task<ActionResult> GetProdcutoId(int IdProducto)
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

            var producto = await _context.Producto.FirstOrDefaultAsync(x => x.Idproducto == IdProducto);

            return Ok(producto);
        }

        [HttpPost("AddProducto")]
        [Authorize]
        public async Task<ActionResult> PostProducto(Producto pro)
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

            //var existe = _context.Producto.Any(x => x.Idproducto == pro.Idproducto);

            Producto item = new Producto()
            {
                Idmarca = pro.Idmarca,
                Idcategoria = pro.Idcategoria,
                Iddimension = pro.Iddimension,
                IdusuarioCreado = Int32.Parse(_id),
                FechaCreado = DateTime.Now,
                IdusuarioActualizo = Int32.Parse(_id),
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
        [Authorize]
        public async Task<ActionResult> UpdateProducto(int IdProducto, Producto pro)
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

            var producto = await _context.Producto.FirstOrDefaultAsync(x => x.Idproducto == IdProducto);

            var validar = producto == null;

            if (validar)
            {
                return NotFound();
            }

            producto.Idmarca = pro.Idmarca;
            producto.Idcategoria = pro.Idcategoria;
            producto.Iddimension = pro.Iddimension;
            producto.IdusuarioActualizo = Int32.Parse(_id);
            producto.FechaActualizado = DateTime.Now;
            producto.Estado = pro.Estado;
            producto.PrecioCompra = pro.PrecioCompra;
            producto.PrecioVenta = pro.PrecioVenta;

            await _context.SaveChangesAsync();

            return Ok("Producto actualizado con exito!!!");

        }

        [HttpDelete("DeleteProducto/{IdProducto}")]
        [Authorize]
        public async Task<ActionResult> DeleteProducto(int IdProducto)
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
