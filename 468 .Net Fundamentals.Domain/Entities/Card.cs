using _468_.Net_Fundamentals.Domain.EnumType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    [Table("Card")]
    public class Card : EntityBase<int>
    {
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Duedate { get; set; }

        public string Description { get; set; }

        public TaskPriority Priority { get; set; }

        public int BusinessId { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }

        public float Index { get; set; }


        [ForeignKey("BusinessId")]
        public virtual Business Business { get; set; }

        public virtual ICollection<CardTag> Tags { get; set; }

    }
}
