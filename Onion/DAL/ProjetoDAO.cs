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
            _context.Projetos.Update(projeto);
            _context.SaveChanges();
        }

        public List<Projeto> Listar()
        {
            return _context.Projetos.ToList();
        }
    }
}
