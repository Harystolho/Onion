using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Onion.DAL;
using Onion.Models;

namespace Onion.Controllers
{
    public class ProjetoController : Controller
    {
        private readonly ProjetoDAO _projetoDAO;
        private readonly TarefaDAO _tarefaDAO;
        public ProjetoController(ProjetoDAO projetoDAO, TarefaDAO tarefaDAO)
        {
            _projetoDAO = projetoDAO;
            _tarefaDAO = tarefaDAO;
        }
        public IActionResult Index()
        {
            ViewBag.Title = "Onion";
            return View(_projetoDAO.Listar());
        }

        public IActionResult Cadastrar()
        {
            ViewBag.Title = "Criar Projeto";
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Projeto projeto)
        {
            if (!ModelState.IsValid)
            {
                return View(projeto);
            }

            _projetoDAO.Cadastrar(projeto);

            return RedirectToAction("Index", "Projeto");
        }

        public IActionResult Detalhar(int id)
        {
            Projeto projeto = _projetoDAO.Buscar(id);
            ViewBag.Title = projeto.Nome;
            return View(projeto);
        }

        public IActionResult DeletarProjeto(int IdProjeto)
        {
            _projetoDAO.Deletar(IdProjeto);
            return RedirectToAction("Index", "Projeto");
        }

        public IActionResult AvancarTarefa(int IdTarefa, int IdProjeto)
        {
            Tarefa tarefa = _tarefaDAO.Buscar(IdTarefa);
            tarefa.Avancar();
            _tarefaDAO.Alterar(tarefa);

            return RedirectToAction("Detalhar", "Projeto", new { id = IdProjeto });
        }

        public IActionResult RegredirTarefa(int IdTarefa, int IdProjeto)
        {
            Tarefa tarefa = _tarefaDAO.Buscar(IdTarefa);
            tarefa.Regredir();
            _tarefaDAO.Alterar(tarefa);

            return RedirectToAction("Detalhar", "Projeto", new { id = IdProjeto });
        }

        public IActionResult ApagarTarefa(int IdTarefa, int IdProjeto)
        {
            _tarefaDAO.Deletar(IdTarefa);
            return RedirectToAction("Detalhar", "Projeto", new { id = IdProjeto });
        }

    }
}
