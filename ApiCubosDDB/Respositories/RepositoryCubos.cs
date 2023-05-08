using ApiCubosDDB.Data;
using ApiCubosDDB.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace ApiCubosDDB.Respositories {
    public class RepositoryCubos {

        private CubosContext context;

        public RepositoryCubos(CubosContext context) {
            this.context = context;
        }

        #region USUARIOS
        public async Task<Usuario> FindUsuarioAsync(int id) {
            return await this.context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == id);
        }

        public async Task<Usuario> ExisteUsuarioAsync(string name, string pass) {
            Usuario? usuario = await this.context.Usuarios.FirstOrDefaultAsync(x => x.Nombre == name && x.Pass == pass);
            return usuario;
        }

        public async Task<bool> Register(string name, string email, string password, string image) {
            int newid = await this.context.Usuarios.AnyAsync() ? await this.context.Usuarios.MaxAsync(x => x.IdUsuario) + 1 : 1;
            Usuario user = new Usuario {
                IdUsuario = newid,
                Nombre = name,
                Email = email,
                Pass = password,
                Imagen = image
            };

            this.context.Usuarios.Add(user);
            return await context.SaveChangesAsync() > 0;
        }
        #endregion

        #region CUBOS
        public async Task<List<Cubo>> GetAllCubosAsync() {
            return await this.context.Cubos.ToListAsync();
        }

        public async Task<List<Cubo>> GetCubosMarcaAsync(string marca) {
            return await this.context.Cubos.Where(x => x.Marca == marca).ToListAsync();
        }

        public async Task<bool> InsertCuboAsync(string nombre, string marca, string imagen, int precio) {
            int newid = await this.context.Cubos.AnyAsync() ? await this.context.Cubos.MaxAsync(x => x.IdCubo) + 1 : 1;
            Cubo cubo = new Cubo {
                IdCubo = newid,
                Nombre = nombre,
                Marca = marca,
                Imagen = imagen,
                Precio = precio
            };
            this.context.Cubos.Add(cubo);
            return await this.context.SaveChangesAsync() > 0;
        }

        #endregion

        #region PEDIDOS
        public async Task<List<Pedido>> PedidosUsuarioAsync(int id) {
            return await this.context.Pedidos.Where(x => x.IdUsuario == id).ToListAsync();
        }

        public async Task<bool> AddPedidoAsync(int idCubo, int idUsuario) {
            int newid = await this.context.Pedidos.AnyAsync() ? await this.context.Pedidos.MaxAsync(x => x.IdPedido) + 1 : 1;
            DateTime date = DateTime.Now;
            Pedido pedido = new Pedido {
                IdPedido = newid,
                IdCubo = idCubo,
                IdUsuario = idUsuario,
                FechaPedido = date
            };

            this.context.Pedidos.Add(pedido);
            return await this.context.SaveChangesAsync() > 0;
        }
        #endregion
    }
}
