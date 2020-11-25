using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public UserController(Context context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarioView = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuarioView == null)
            {
                return NotFound();
            }

            return View(usuarioView);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            foreach(var erro in resultado.Errors)
            {
                ModelState.AddModelError("", erro.Description);
            }
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarioView = await _context.Usuarios.FindAsync(id);
            if (usuarioView == null)
            {
                return NotFound();
            }
            return View(usuarioView);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Email,Senha,Id,CriadoEm")] UsuarioView usuarioView)
        {
            if (id != usuarioView.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarioView);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioViewExists(usuarioView.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuarioView);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarioView = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuarioView == null)
            {
                return NotFound();
            }

            return View(usuarioView);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuarioView = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuarioView);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioViewExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
