using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Onion.DAL;
using Onion.Models;

namespace Onion.Controllers
{
    public class TarefaController : Controller
    {
        private readonly ProjetoDAO _projetoDAO;
        private readonly TarefaDAO _tarefaDAO;
        public TarefaController(ProjetoDAO projetoDAO, TarefaDAO tarefaDAO)
        {
            _projetoDAO = projetoDAO;
            _tarefaDAO = tarefaDAO;
        }
        [HttpGet]
        public IActionResult Cadastrar(int IdProjeto)
        {
            ViewBag.Title = "Criar Tarefa";
            ViewBag.IdProjeto = IdProjeto;
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Tarefa tarefa, int idProjeto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IdProjeto = idProjeto;
                return View(tarefa);
            }

            Projeto projeto = _projetoDAO.Buscar(idProjeto);
            projeto.AdicionarTarefa(tarefa);
            _projetoDAO.Alterar(projeto);

            return RedirectToAction("Detalhar", "Projeto", new { id = idProjeto });
        }

        public IActionResult Detalhar(int id)
        {
            Projeto projeto = _projetoDAO.Buscar(id);
            ViewBag.Title = projeto.Nome;
            return View(projeto);
        }

        public IActionResult CadastrarTarefa(Projeto projeto, Tarefa tarefa)
        {

            return RedirectToAction("Detalhar", "Projeto");
        }

        [HttpGet]
        public IActionResult EditarTarefa(int IdTarefa, int IdProjeto)
        {
            ViewBag.Title = "Editar Tarefa";
            ViewBag.IdProjeto = IdProjeto;
            return View(_tarefaDAO.Buscar(IdTarefa));
        }

        [HttpPost]
        public IActionResult EditarTarefa(Tarefa tarefa, int IdProjeto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IdProjeto = IdProjeto;
                return View(tarefa);
            }

            _tarefaDAO.Alterar(tarefa);

            return RedirectToAction("Detalhar", "Projeto", new { id = IdProjeto });
        }
    }
}
