using IDYGS81_NoeliRodriguez.Models;
using Microsoft.EntityFrameworkCore;

namespace IDYGS81_NoeliRodriguez.Context
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
    }
}
