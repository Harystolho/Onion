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
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<UsuarioView> Usuarios { get; set; }
    }
}
