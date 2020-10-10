using gtp.api.Models;
using Microsoft.EntityFrameworkCore;

namespace gtp.api.Database
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options) : base (options) { }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}