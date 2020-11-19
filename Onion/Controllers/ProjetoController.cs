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
        public ProjetoController(ProjetoDAO projetoDAO)
        {
            _projetoDAO = projetoDAO;
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

        public IActionResult AvancarTarefa(int IdTarefa, int IdProjeto)
        {

            return RedirectToAction("Detalhar", "Projeto", new { id = IdProjeto });
        }

    }
}
