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
    public class ClienteController : ControllerBase
    {

        private readonly InventarioContext _context;

        public ClienteController(InventarioContext context)
        {
            _context = context;

        }

        [HttpGet("ClienteList")]
        [Authorize]
        public async Task<ActionResult> GetClienteList()
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
        [Authorize]
        public async Task<ActionResult> GetClienteId(int IdCliente)
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

            var cliente = await _context.Cliente.FirstOrDefaultAsync(x => x.Idcliente == IdCliente);

            return Ok(cliente);
        }

        [HttpPost("AddCliente")]
        [Authorize]
        public async Task<ActionResult> PostClinte(Cliente clie)
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

            var existe = _context.Cliente.Any(x => x.Nombre.Contains(clie.Nombre));

            if (existe)
            {
                return Ok("El cliente ya existe");
            }

            Cliente item = new Cliente()
            {
                Nombre = clie.Nombre,
                Correo = clie.Correo,
                IdusuarioCreado = Int32.Parse(_id),
                FechaCreado = DateTime.Now,
                IdusuarioActualizo = Int32.Parse(_id),
                FechaActualizado = DateTime.Now,
                Estado = clie.Estado

            };

            _context.Cliente.Add(item);
            await _context.SaveChangesAsync();

            return Ok("Cliente agregado con exito!!!");
        }

        [HttpPut("UpdateCliente/{IdCliente}")]
        [Authorize]
        public async Task<ActionResult> PutCliente(int IdCliente, Cliente clie)
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

            var cliente = await _context.Cliente.FirstOrDefaultAsync(x => x.Idcliente == IdCliente);

            var validar = cliente == null;

            if (validar)
            {
                return NotFound();
            }

            cliente.Nombre = clie.Nombre;
            cliente.Correo = clie.Correo;
            cliente.IdusuarioActualizo = Int32.Parse(_id);
            cliente.FechaActualizado = DateTime.Now;
            cliente.Estado = clie.Estado;

            await _context.SaveChangesAsync();

            return Ok("Cliente actualizado con exito!!!");
        }

        [HttpDelete("DeleteCliente/{IdCliente}")]
        [Authorize]
        public async Task<ActionResult> DeleteCliente(int IdCliente)
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
