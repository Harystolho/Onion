using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Onion.DAL;
using Onion.Models;

namespace Onion.Controllers
{
    public class ProjetoController : Controller
    {
        private readonly ProjetoDAO _projetoDAO;
        private readonly TarefaDAO _tarefaDAO;
        private readonly UsuarioDAO _usuarioDAO;
        private readonly UserManager<Usuario> _userManager;
        public ProjetoController(ProjetoDAO projetoDAO, TarefaDAO tarefaDAO, UserManager<Usuario> userManager, UsuarioDAO usuarioDAO)
        {
            _projetoDAO = projetoDAO;
            _tarefaDAO = tarefaDAO;
            _usuarioDAO = usuarioDAO;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            ViewBag.Title = "Onion";

            var user = _usuarioDAO.BuscarPorNome(User.Identity.Name);

            return View(_projetoDAO.Listar(user.Id));
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

            var user = _usuarioDAO.BuscarPorNome(User.Identity.Name);

            var pu = new ProjetoDoUsuario()
            {
                Usuario = user,
                Projeto = projeto
            };

            user.Projetos.Add(pu);
            _usuarioDAO.Atualizar(user);

            return RedirectToAction("Index", "Projeto");
        }

        public IActionResult Detalhar(int id, string Pesquisa)
        {
            Projeto projeto = _projetoDAO.Buscar(id);
            ViewBag.Title = projeto.Nome;
            ViewBag.Pesquisa = Pesquisa ?? "";

            if (Pesquisa != null && Pesquisa != "")
            {
                projeto.Tarefas = projeto.Tarefas.FindAll(t =>
                {
                    return t.Descricao.ToLower().Contains(Pesquisa.ToLower());
                });
            }

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

        public IActionResult Compartilhar(int Id, string Email)
        {
            var user = _usuarioDAO.BuscarPorEmail(Email);

            if (user == null) return RedirectToAction("Detalhar", "Projeto", new { id = Id, erro = "Esse usuario nao existe" });
            if (user.Nome == User.Identity.Name) return RedirectToAction("Detalhar", "Projeto", new { id = Id });

            var pu = new ProjetoDoUsuario()
            {
                Usuario = user,
                Projeto = _projetoDAO.Buscar(Id)
            };

            user.Projetos.Add(pu);
            _usuarioDAO.Atualizar(user);

            return RedirectToAction("Detalhar", "Projeto", new { id = Id });
        }

    }
}
