using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TESTMVCCORE.Models.DB
{
    public partial class Type
    {
        [Key]
        public int Id { get; set; }
        [Column("Type")]
        [StringLength(5)]
        public string Type1 { get; set; } = null!;
        [StringLength(10)]
        public string TypeName { get; set; } = null!;
    }
}
