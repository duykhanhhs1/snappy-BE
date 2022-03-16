using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    [Table("Todo")]
    public class Todo : EntityBase<int>
    {
        public string Name { get; set; }
        public Boolean IsCompleted { get; set; }

        public int CardId { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey("CardId")]
        public virtual Card Card { get; set; }
    }
}
