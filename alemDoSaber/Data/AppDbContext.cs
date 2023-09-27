using Microsoft.EntityFrameworkCore;

namespace RedeSocial.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tema>().ToTable("tb_temas");

            _ = modelBuilder.Entity<Tema>()
                .HasOne(_ => _.Tema)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Tema> Temas { get; set; } = null!;
    }
}
