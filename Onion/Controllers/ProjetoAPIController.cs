using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.DAL;
using Onion.Models;

namespace Onion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetoAPIController : ControllerBase
    {

        private readonly ProjetoDAO _projetoDAO;
        private readonly UsuarioDAO _usuarioDAO;

        public ProjetoAPIController(ProjetoDAO projetoDAO, UsuarioDAO usuarioDAO)
        {
            _projetoDAO = projetoDAO;
            _usuarioDAO = usuarioDAO;
        }

        [HttpGet]
        [Route("Listar")]
        public List<Projeto> Listar()
        {
            return _projetoDAO.Listar();
        }

        [HttpGet]
        [Route("ListarComAutenticacao")]
        public List<Projeto> ListarComAutenticacao()
        {
            var user = _usuarioDAO.BuscarPorNome(User.Identity.Name);
            return _projetoDAO.Listar(user.Id);
        }

    }
}
