using Microsoft.EntityFrameworkCore;
using Onion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onion.DAL
{
    public class ProjetoDAO
    {
        private readonly Context _context;
        public ProjetoDAO(Context context) => _context = context;

        public void Cadastrar(Projeto projeto)
        {
            Alterar(projeto);
        }
        public void Alterar(Projeto projeto)
        {
            _context.Projetos.Update(projeto);
            _context.SaveChanges();
        }
        public List<Projeto> Listar() => _context.Projetos.ToList();

        public Projeto Buscar(int id) =>
            _context.Projetos
            .Where(p => p.Id == id)
            .Include(p => p.Tarefas)
            .ToList().First();

    }
}
