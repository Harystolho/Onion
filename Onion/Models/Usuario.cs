﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Onion.Models
{
    public class Usuario : IdentityUser
    {
        public Usuario()
        {
            CriadoEm = DateTime.Now;
            Projetos = new List<Projeto>();
        }

        public DateTime CriadoEm { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Senha { get; set; }

        public List<Projeto> Projetos { get; set; }
    }
}
