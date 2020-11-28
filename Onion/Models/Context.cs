using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onion.Models
{
    public class Context : IdentityDbContext<Usuario>
    {
        public Context(DbContextOptions options) : base(options) { }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<ProjetoDoUsuario> ProjetoDoUsuarios { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<UsuarioView> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjetoDoUsuario>().HasKey(pu => new { pu.UsuarioId, pu.ProjetoId });

            modelBuilder.Entity<UsuarioView>()
                .HasMany(u => u.Projetos)
                .WithOne(p => p.Usuario)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
