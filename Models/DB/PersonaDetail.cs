using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TESTMVCCORE.Models.DB
{
    public partial class PersonaDetail
    {
        [Key]
        public int Id { get; set; }
        public int PersonaId { get; set; }
        [StringLength(10)]
        public string Comment { get; set; } = null!;
    }
}
