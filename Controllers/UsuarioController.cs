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
    public class UsuarioController : ControllerBase
    {
        private readonly InventarioContext _context;

        public UsuarioController(InventarioContext context)
        {
            _context = context;
        }

        [HttpGet("UsuarioList")]
        public async Task<ActionResult> getUsuarioLista()
        {

            var lista = await _context.Usuario.OrderBy(x => x.Idusuario).Select(x => new
            {
                Idusuario = x.Idusuario,
                Nombre = x.Nombre,
                Correo = x.Correo,
                UserName = x.UserName,
                Idrole = x.IdroleNavigation.Nombre,
                FechaCreacion = x.FechaCreacion,
                Estado = x.Estado
            }).ToListAsync();

            return Ok(lista);
        }

        [HttpGet("Usuario/{UsuarioId}")]
        public async Task<ActionResult> getUsuarioId( int UsuarioId)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.Idusuario == UsuarioId);

            return Ok(usuario);
        }

        [HttpPost("AddUsuario")]
        public async Task<ActionResult> addUsuario(Usuario usuario)
        {
            Usuario item = new Usuario()
            {
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                UserName = usuario.UserName,
                Idrole = usuario.Idrole,
                FechaCreacion = usuario.FechaCreacion,
                Estado = usuario.Estado
            };

            _context.Usuario.Add(item);

            await _context.SaveChangesAsync();

            return Ok("Usuario agregado con exito!!!");
        }

        [HttpPut("UpdateUsuario/{UsuarioId}")]
        public async Task<ActionResult> updateUusario(int UsuarioId, Usuario usuario)
        {
            var user = await _context.Usuario.FirstOrDefaultAsync(x => x.Idusuario == UsuarioId);

            var validar = user == null;

            if (validar)
            {
                return NotFound();
            }

            user.Nombre = usuario.Nombre;
            user.Correo = usuario.Correo;
            user.UserName = usuario.UserName;
            user.Idrole = usuario.Idrole;
            user.FechaCreacion = usuario.FechaCreacion;
            user.Estado = usuario.Estado;


            await _context.SaveChangesAsync();

            return Ok("Usuario actualizado con exito!!!");
        }
    }
}
