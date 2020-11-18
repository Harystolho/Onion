using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Onion.Models
{
    [Table("Projetos")]
    public class Projeto : BaseModel
    {
        public Projeto()
        {
            Tarefas = new List<Tarefa>();
        }

        [Required(ErrorMessage = "Campo obrigatório!")]
        [MinLength(5, ErrorMessage = "Mínimo 5 caracteres!")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres!")]
        public string Nome { get; set; }

        public List<Tarefa> Tarefas { get; set; }

    }
}
