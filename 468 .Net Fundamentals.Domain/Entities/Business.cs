using _468_.Net_Fundamentals.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    [Table("Business")]
    public class Business : EntityBase<int>
    {
        public string Name { get; set; }
        public string ColorCode { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]      
        public virtual Project Project { get; set; }

        public Boolean IsDefault { get; set; }
        public Boolean IsDefaultFinish { get; set; }

    }
}
