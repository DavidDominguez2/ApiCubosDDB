using ApiCubosDDB.Models;
using ApiCubosDDB.Respositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ApiCubosDDB.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase {

        private RepositoryCubos repo;

        public PedidosController(RepositoryCubos repo) {
            this.repo = repo;
        }

        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public async Task<ActionResult> RealizarPedido(int idCubo) {
            Claim claim = HttpContext.User.Claims.SingleOrDefault(x => x.Type == "UserData");
            string jsonUsuario = claim.Value;
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(jsonUsuario);

            await this.repo.AddPedidoAsync(idCubo, usuario.IdUsuario);
            return Ok();
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public async Task<ActionResult<List<Pedido>>> PedidosUsuario() {
            Claim claim = HttpContext.User.Claims.SingleOrDefault(x => x.Type == "UserData");
            string jsonUsuario = claim.Value;
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(jsonUsuario);

            return await this.repo.PedidosUsuarioAsync(usuario.IdUsuario);
        }
    }
}
