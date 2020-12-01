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
    public class TarefaAPIController : ControllerBase
    {

        private readonly ProjetoDAO _projetoDAO;
        private readonly UsuarioDAO _usuarioDAO;

        public TarefaAPIController(ProjetoDAO projetoDAO, UsuarioDAO usuarioDAO)
        {
            _projetoDAO = projetoDAO;
            _usuarioDAO = usuarioDAO;
        }

        [HttpGet]
        [Route("Listar/{IdProjeto}")]
        public IActionResult Listar(int IdProjeto)
        {
            try
            {
                return Ok(_projetoDAO.Buscar(IdProjeto).Tarefas);
            }
            catch (InvalidOperationException)
            {
                return BadRequest(new { msg = "Id do projeto invalido" });
            }
        }

        [HttpPost]
        [Route("Criar/{IdProjeto}")]
        public IActionResult Criar(int IdProjeto, Tarefa Tarefa)
        {
            try
            {
                Projeto projeto = _projetoDAO.Buscar(IdProjeto);

                if (ModelState.IsValid)
                {
                    projeto.AdicionarTarefa(Tarefa);
                    _projetoDAO.Alterar(projeto);
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (InvalidOperationException)
            {
                return BadRequest(new { msg = "Id do projeto invalido" });
            }
        }

    }
}

/* 

Content-Type: application/json

{
 "Descricao": "Tudo bem?",
 "Prioridade": 8,
 "Estado": 0
}

*/