using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Onion.Models
{
    [Table("Tarefas")]
    public class Tarefa : BaseModel
    {
        Tarefa()
        {
            Prioridade = 0;
            Estado = EstadoDaTarefa.A_FAZER;
        }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Descricao { get; set; }

        public int Prioridade { get; set; }

        public EstadoDaTarefa Estado { get; set; }

    }

}

public enum EstadoDaTarefa
{
    A_FAZER, FAZENDO, CONCLUIDO
}
