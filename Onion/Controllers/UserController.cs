using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Onion.Models;

namespace Onion.Controllers
{
    public class UserController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public UserController(Context context, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: User/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            ViewBag.Title = "Criar usuario";
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create([Bind("Nome,Email,Senha,Id,CriadoEm,ConfSenha")] UsuarioView usuarioView)
        {
            IdentityUser user = new IdentityUser();
            if (ModelState.IsValid)
            {
                Usuario usuario = new Usuario
                {
                    UserName = usuarioView.Nome,
                    Email = usuarioView.Email,
                };

                IdentityResult resultado = await _userManager.CreateAsync(usuario, usuarioView.Senha);
                if (resultado.Succeeded)
                {
                    _context.Add(usuarioView);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                AdicionarErros(resultado);
            }
            return View(usuarioView);
        }

        public void AdicionarErros(IdentityResult resultado)
        {
            foreach (var erro in resultado.Errors)
            {
                ModelState.AddModelError("", erro.Description);
            }
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            ViewBag.Title = "Fazer login";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email,Senha")] UsuarioView usuarioView)
        {
            if(usuarioView.Email == null)
            {
                ModelState.AddModelError("", "Usuario nao existe");
                return View(usuarioView);
            }

            Usuario usuario = await _userManager.FindByEmailAsync(usuarioView.Email);
            if (usuario == null)
            {
                ModelState.AddModelError("", "Usuario nao existe");
                return View(usuarioView);
            }

            await _signInManager.SignOutAsync();
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(usuario, usuarioView.Senha, false, false);

            if (result.Succeeded) return Redirect("/");

            ModelState.AddModelError("", "Senha invalida");
            return View(usuarioView);
        }

        public async Task<IActionResult> Sair()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/User/Login");
        }

    }
}
