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
        public Tarefa()
        {
            Prioridade = 0;
            Estado = EstadoDaTarefa.A_FAZER;
        }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Descricao { get; set; }

        [Range(0, 10, ErrorMessage = "Deve ser de 0 a 10")]

        public int? Prioridade { get; set; }

        public EstadoDaTarefa Estado { get; set; }

        public void Avancar()
        {
            if (Estado == EstadoDaTarefa.FAZENDO)
                Estado = EstadoDaTarefa.CONCLUIDO;

            if (Estado == EstadoDaTarefa.A_FAZER)
                Estado = EstadoDaTarefa.FAZENDO;
        }

        public void Regredir()
        {
            if (Estado == EstadoDaTarefa.FAZENDO)
                Estado = EstadoDaTarefa.A_FAZER;

            if (Estado == EstadoDaTarefa.CONCLUIDO)
                Estado = EstadoDaTarefa.FAZENDO;
        }
    }

}

public enum EstadoDaTarefa
{
    A_FAZER, FAZENDO, CONCLUIDO
}
