using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Onion.Models
{
    [Table("Usuarios")]
    public class UsuarioView : BaseModel
    {
        public UsuarioView()
        {
            Projetos = new List<ProjetoDoUsuario>();
        }

        [Display(Name = "Nome de usuário")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Nome { get; set; }

        [Display(Name = "E-Mail")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Senha { get; set; }

        [Display(Name = "Confirmação da senha")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [NotMapped]
        [Compare("Senha", ErrorMessage = "As senhas inseridas não são iguais")]
        public string ConfSenha { get; set; }

        public List<ProjetoDoUsuario> Projetos { get; set; }
    }
}
