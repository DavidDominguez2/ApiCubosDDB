using ApiCubosDDB.Helpers;
using ApiCubosDDB.Models;
using ApiCubosDDB.Respositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiCubosDDB.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {

        private RepositoryCubos repo;
        private HelperOAuthToken helper;

        public AuthController(RepositoryCubos repo, HelperOAuthToken helper) {
            this.repo = repo;
            this.helper = helper;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> LogIn(LogIn model) {
            Usuario usuario = await this.repo.ExisteUsuarioAsync(model.Name, model.Password);
            if (usuario == null) {
                return Unauthorized();
            } else {
                // DEBEMOS CREAR UNAS CREDENCIALES DENTRO DEL TOKEN
                SigningCredentials credentials = new SigningCredentials(this.helper.GetToken(), SecurityAlgorithms.HmacSha256);

                string jsonUsuario = JsonConvert.SerializeObject(usuario);
                Claim[] information = new[] {
                    new Claim("UserData", jsonUsuario)
                };

                // EL TOKEN SE GENERA CON UNA CLASE Y DEBEMOS INDICAR
                // LOS DATOS QUE CONFORMAN DICHO TOKEN
                JwtSecurityToken token = new JwtSecurityToken(
                    claims: information,
                    issuer: this.helper.Issuer,
                    audience: this.helper.Audience,
                    signingCredentials: credentials,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    notBefore: DateTime.UtcNow
                );
                return Ok(new {
                    response = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Register(Usuario usuario) {
            bool response = await this.repo.Register(usuario.Nombre, usuario.Email, usuario.Pass, usuario.Imagen);
            return (response) ? Ok() : Conflict();
        }
    }
}
