using ApiCubosDDB.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCubosDDB.Data {
    public class CubosContext : DbContext {

        public CubosContext(DbContextOptions<CubosContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cubo> Cubos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }

    }
}
