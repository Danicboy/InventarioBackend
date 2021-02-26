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
    public class ClienteController : ControllerBase
    {

        private readonly InventarioContext _context;

        public ClienteController(InventarioContext context)
        {
            _context = context;

        }

        [HttpGet("ClienteList")]
        public async Task<ActionResult> GetClienteList()
        {
            var lista = await _context.Cliente.OrderBy(x => x.Idcliente).Select(x => new
            {
                IdCliente = x.Idcliente,
                Nombre = x.Nombre,
                Correo = x.Correo,
                IdUsuarioCreado = x.IdusuarioCreadoNavigation.Nombre,
                FechaCreado = x.FechaCreado,
                IdUsuarioActualizado = x.IdusuarioActualizoNavigation.Nombre,
                FechaActualizado = x.FechaActualizado,
                Estado = x.Estado

            }).ToListAsync();

            return Ok(lista);

        }

        [HttpGet("Cliente/{IdCliente}")]
        public async Task<ActionResult> GetClienteId(int IdCliente)
        {
            var cliente = await _context.Cliente.FirstOrDefaultAsync(x => x.Idcliente == IdCliente);

            return Ok(cliente);
        }

        [HttpPost("AddCliente")]
        public async Task<ActionResult> PostClinte(Cliente clie)
        {
            var existe = _context.Cliente.Any(x => x.Nombre.Contains(clie.Nombre));

            if (existe)
            {
                return Ok("El cliente ya existe");
            }

            Cliente item = new Cliente()
            {
                Nombre = clie.Nombre,
                Correo = clie.Correo,
                IdusuarioCreado = clie.IdusuarioCreado,
                FechaCreado = DateTime.Now,
                IdusuarioActualizo = clie.IdusuarioActualizo,
                FechaActualizado = DateTime.Now,
                Estado = clie.Estado

            };

            _context.Cliente.Add(item);
            await _context.SaveChangesAsync();

            return Ok("Cliente agregado con exito!!!");
        }

        [HttpPut("UpdateCliente/{IdCliente}")]
        public async Task<ActionResult> PutCliente(int IdCliente, Cliente clie)
        {
            var cliente = await _context.Cliente.FirstOrDefaultAsync(x => x.Idcliente == IdCliente);

            var validar = cliente == null;

            if (validar)
            {
                return NotFound();
            }

            cliente.Nombre = clie.Nombre;
            cliente.Correo = clie.Correo;
            cliente.IdusuarioActualizo = clie.IdusuarioActualizo;
            cliente.FechaActualizado = DateTime.Now;
            cliente.Estado = clie.Estado;

            await _context.SaveChangesAsync();

            return Ok("Cliente actualizado con exito!!!");
        }

        [HttpDelete("DeleteCliente/{IdCliente}")]
        public async Task<ActionResult> DeleteCliente(int IdCliente)
        {
            var cliente = await _context.Cliente.FirstOrDefaultAsync(x => x.Idcliente == IdCliente);

            var validar = cliente == null;

            if (validar)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return Ok("Cliente eliminado con exito!!!");

        }
    }
}
