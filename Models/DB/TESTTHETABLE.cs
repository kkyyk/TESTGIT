using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TESTMVCCORE.Models.DB
{
    public partial class TESTTHETABLE
    {
        [Key]
        public int Id { get; set; }
        [StringLength(10)]
        public string? Name { get; set; }
        [StringLength(10)]
        public string? Number { get; set; }
    }
}
