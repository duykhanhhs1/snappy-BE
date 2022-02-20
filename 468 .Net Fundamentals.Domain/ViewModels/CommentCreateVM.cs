using System;
using System.Collections.Generic;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.ViewModels
{
    public class CommentCreateVM
    {
        public string UserId { get; set; }
        public int? ParentId { get; set; }

        public string? Content { get; set; }
    }
}
