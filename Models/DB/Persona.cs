using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TESTMVCCORE.Models.DB
{
    public partial class Persona
    {
        public Persona()
        {
            PersonaDetail = new HashSet<PersonaDetail>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        [StringLength(5)]
        public string? Type { get; set; }

        [InverseProperty("Persona")]
        public virtual ICollection<PersonaDetail> PersonaDetail { get; set; }
    }
}
