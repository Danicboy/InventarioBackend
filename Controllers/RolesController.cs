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
    public class RolesController : ControllerBase
    {
        private readonly InventarioContext _context;

        public RolesController(InventarioContext context)
        {
            _context = context;
        }

        [HttpGet("RoleList")]
        [Authorize]
        public async Task<ActionResult> GetRoleList()
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

            var lista = await _context.Roles.OrderBy(x => x.Idrole).Select(x => new
            {
                IdRole = x.Idrole,
                Nombre = x.Nombre,
                Descripcion = x.Descripcion
            }).ToListAsync();

            return Ok(lista);

        }

        [HttpGet("Role/{RoleId}")]
        [Authorize]
        public async Task<ActionResult> GetRoleId(int RoleId)
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

            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Idrole == RoleId);

            return Ok(role);
        }

        [HttpPost("AddRole")]
        [Authorize]
        public async Task<ActionResult> PostRole(Roles rol)
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

            Roles item = new Roles()
            {
                Nombre = rol.Nombre,
                Descripcion = rol.Descripcion
            };

            _context.Roles.Add(item);
            await _context.SaveChangesAsync();

            return Ok("Rol agregado con exito!!!");

        }

        [HttpPut("UpdateRole/{RoleId}")]
        [Authorize]
        public async Task<ActionResult> UpdateRole(int RoleId, Roles rol)
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

            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Idrole == RoleId);

            var validar = role == null;

            if (validar)
            {
                return NotFound();
            }

            role.Nombre = rol.Nombre;
            role.Descripcion = rol.Descripcion;

            await _context.SaveChangesAsync();

            return Ok("Rol actualizado con exito!!!");
        }

        [HttpDelete("DeleteRole/{RoleId}")]
        [Authorize]
        public async Task<ActionResult> DeleteRole(int RoleId)
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

            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Idrole == RoleId);

            var validar = role == null;

            if (validar)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return Ok("Rol eliminado con exito!!!");
        }

    }
}
