using Microsoft.EntityFrameworkCore;
using RedeSocial.Model;

namespace RedeSocial.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tema>().ToTable("tb_temas");
            modelBuilder.Entity<Postagem>().ToTable("tb_postagens");
            modelBuilder.Entity<User>().ToTable("tb_user");

            _ = modelBuilder.Entity<Postagem>()
                .HasOne(_ => _.Tema)
                .WithMany(c => c.Postagem)
                .HasForeignKey("TemaId")
                .OnDelete(DeleteBehavior.Cascade);

            _ = modelBuilder.Entity<Postagem>()
                .HasOne(_ => _.User)
                .WithMany(c => c.Postagem)
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);

        }

        public DbSet<Tema> Temas { get; set; } = null!;
        public DbSet<Postagem> Postagens { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
    }
}
