using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    [Table("Attachment")]
    public class Attachment : EntityBase<int>
    {
        public string Url { get; set; }
        public string Name { get; set; }

    }
}
