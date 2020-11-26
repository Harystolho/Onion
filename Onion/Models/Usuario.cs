using Microsoft.AspNetCore.Identity;
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
        }

        public DateTime CriadoEm { get; set; }

    }
}
