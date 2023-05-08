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
    public class CubosController : ControllerBase {

        private RepositoryCubos repo;

        public CubosController(RepositoryCubos repo) {
            this.repo = repo;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Cubo>>> GetCubos() {
            return await this.repo.GetAllCubosAsync();
        }

        [HttpGet]
        [Route("[action]/{marca}")]
        public async Task<ActionResult<List<Cubo>>> GetCubosMarca(string marca) {
            return await this.repo.GetCubosMarcaAsync(marca);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> InsertCubo(Cubo cubo) {
            await this.repo.InsertCuboAsync(cubo.Nombre, cubo.Marca, cubo.Imagen, cubo.Precio);
            return Ok();
        }
    }
}
