using InventarioBack.Class.Encrypt;
using InventarioBack.Context;
using InventarioBack.Models.DBModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InventarioBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly InventarioContext _context;

        public IConfiguration Configuration { get; }

        public LoginController(InventarioContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> PostLogin(Usuario log)
        {
            //var passencryp = Encrypter.GetSHA256(log.Contrasenia);

            var passencryp = log.Contrasenia;

            var usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.Contrasenia == passencryp);

            if (usuario != null)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {

                    Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("Idusuario", usuario.Idusuario.ToString())
                }),

                    //Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString())), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return Ok( new { userToken = token } );
            }
            else
            {
                return NotFound("Usuario no encontrado!!!");
            }
        }
    }
}
