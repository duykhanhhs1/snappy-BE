﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    [Table("Project")]
    public class Project : EntityBase<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual User User { get; set; }

        public virtual IList<Business> Businesses { get; set; }

    }
}


