using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Onion.Models
{
    [Table("ProjetosDosUsuarios")]
    public class ProjetoDoUsuario
    {

        public int UsuarioId { get; set; }
        public UsuarioView Usuario { get; set; }

        public int ProjetoId { get; set; }
        public Projeto Projeto { get; set; }

    }
}
