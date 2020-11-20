using Microsoft.EntityFrameworkCore;
using Onion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onion.DAL
{
    public class TarefaDAO
    {
        private readonly Context _context;
        public TarefaDAO(Context context) => _context = context;

        public void Alterar(Tarefa tarefa)
        {
            _context.Tarefas.Update(tarefa);
            _context.SaveChanges();
        }

        public void Deletar(int IdTarefa)
        {
            _context.Tarefas.Remove(Buscar(IdTarefa));
            _context.SaveChanges();
        }

        public Tarefa Buscar(int id) => _context.Tarefas.Find(id);

    }
}
