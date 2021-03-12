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
    public class ProveedorController : ControllerBase
    {
        private readonly InventarioContext _context;

        public ProveedorController(InventarioContext context)
        {
            _context = context;

        }

        [HttpGet("ProveedorList")]
        [Authorize]
        public async Task<ActionResult> GetProveedorList()
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

            var lista = await _context.Proveedor.OrderBy(x => x.Idproveedor).Select(x => new
            {
                IdProveedor = x.Idproveedor,
                Nombre = x.Nombre,
                Direccion = x.Direccion,
                Telefono = x.Telefono,
                IdUsuarioCreado = x.IdusuarioCreadoNavigation.Nombre,
                FechaCreado = x.FechaCreado,
                IdUsuarioActualizado = x.IdusuarioActualizoNavigation.Nombre,
                FechaActualizado = x.FechaActualizado,
                Estado = x.Estado

            }).ToListAsync();

            return Ok(lista);
        }

        [HttpGet("Proveedor/{ProveedorId}")]
        [Authorize]
        public async Task<ActionResult> GetProveedorId(int ProveedorId)
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

            var proveedor = await _context.Proveedor.FirstOrDefaultAsync(x => x.Idproveedor == ProveedorId);

            return Ok(proveedor);
        }

        [HttpPost("AddProveedor")]
        [Authorize]
        public async Task<ActionResult> PostProveedor(Proveedor pro)
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

            var existe = _context.Proveedor.Any(x => x.Nombre.Contains(pro.Nombre));

            if (existe)
            {
                return Ok("El proveedor ya existe");
            }

            Proveedor item = new Proveedor()
            {
                Nombre = pro.Nombre,
                Direccion = pro.Direccion,
                Telefono = pro.Telefono,
                IdusuarioCreado = Int32.Parse(_id),
                FechaCreado = DateTime.Now,
                IdusuarioActualizo = Int32.Parse(_id),
                FechaActualizado = DateTime.Now,
                Estado = pro.Estado
            };

            _context.Proveedor.Add(item);
            await _context.SaveChangesAsync();

            return Ok("Proveedor agregada con exito!!!");
        }

        [HttpPut("UpdateProveedor/{ProveedorId}")]
        [Authorize]
        public async Task<ActionResult> PutProveedor(int ProveedorId, Proveedor pro)
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

            var proveedor = await _context.Proveedor.FirstOrDefaultAsync(x => x.Idproveedor == ProveedorId);

            var validar = proveedor == null;

            if (validar)
            {
                return NotFound();
            }

            proveedor.Nombre = pro.Nombre;
            proveedor.Direccion = pro.Direccion;
            proveedor.Telefono = pro.Telefono;
            proveedor.IdusuarioCreado = Int32.Parse(_id);
            proveedor.FechaActualizado = DateTime.Now;
            proveedor.Estado = pro.Estado;

            await _context.SaveChangesAsync();

            return Ok("Proveedor actualizada con exito!!!");
        }

        [HttpDelete("DeleteProveedor/{ProveedorId}")]
        [Authorize]
        public async Task<ActionResult> DeleteProveedor(int ProveedorId)
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

            var proveedor = await _context.Proveedor.FirstOrDefaultAsync(x => x.Idproveedor == ProveedorId);

            var validar = proveedor == null;

            if (validar)
            {
                return NotFound();
            }

            _context.Proveedor.Remove(proveedor);
            await _context.SaveChangesAsync();

            return Ok("Proveedor Eliminada con exito!!!");
        }

    }
}
