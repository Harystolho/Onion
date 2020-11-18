using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Onion.Models;

namespace Onion.Controllers
{
    public class ProjetoController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Onion";
            return View();
        }

        public IActionResult Cadastrar()
        {
            ViewBag.Title = "Criar Projeto";
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Projeto projeto)
        {
            if(!ModelState.IsValid)
            {
                return View(projeto);
            }

            return RedirectToAction("Index", "Projeto");
        }
    }
}
