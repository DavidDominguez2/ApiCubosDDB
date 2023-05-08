using ApiCubosDDB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ApiCubosDDB.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase {

        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public async Task<ActionResult<Usuario>> GetPerfil() {
            Claim claim = HttpContext.User.Claims.SingleOrDefault(x => x.Type == "UserData");
            string jsonUsuario = claim.Value;
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(jsonUsuario);
            return usuario;
        }
    }
}
