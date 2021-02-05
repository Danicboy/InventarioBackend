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
    public class ProveedorController : ControllerBase
    {
        private readonly InventarioContext _context;

        public ProveedorController(InventarioContext context)
        {
            _context = context;

        }

        [HttpGet("ProveedorList")]
        public async Task<ActionResult> GetProveedorList()
        {
            var lista = await _context.Proveedor.OrderBy(x => x.Idproveedor).Select(x => new
            {
                IdProveedor = x.Idproveedor,
                Nombre = x.Nombre,
                Direccion = x.Direccion,
                Telefono = x.Telefono

            }).ToListAsync();

            return Ok(lista);
        }

        [HttpGet("Proveedor/{ProveedorId}")]
        public async Task<ActionResult> GetProveedorId(int ProveedorId)
        {
            var proveedor = await _context.Proveedor.FirstOrDefaultAsync(x => x.Idproveedor == ProveedorId);

            return Ok(proveedor);
        }

        [HttpPost("AddProveedor")]
        public async Task<ActionResult> PostProveedor(Proveedor pro)
        {

            Proveedor item = new Proveedor()
            {
                Nombre = pro.Nombre,
                Direccion = pro.Direccion,
                Telefono = pro.Telefono
            };

            _context.Proveedor.Add(item);
            await _context.SaveChangesAsync();

            return Ok("Proveedor agregada con exito!!!");
        }

        [HttpPut("UpdateProveedor/{ProveedorId}")]
        public async Task<ActionResult> PutProveedor(int ProveedorId, Proveedor mar)
        {
            var proveedor = await _context.Proveedor.FirstOrDefaultAsync(x => x.Idproveedor == ProveedorId);

            var validar = proveedor == null;

            if (validar)
            {
                return NotFound();
            }

            proveedor.Nombre = mar.Nombre;
            proveedor.Direccion = proveedor.Direccion;
            proveedor.Telefono = proveedor.Telefono;

            await _context.SaveChangesAsync();

            return Ok("Proveedor actualizada con exito!!!");
        }

        [HttpDelete("DeleteProveedor/{ProveedorId}")]
        public async Task<ActionResult> DeleteProveedor(int ProveedorId)
        {
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
