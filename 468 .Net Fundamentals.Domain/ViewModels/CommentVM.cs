using System;
using System.Collections.Generic;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.ViewModels
{
    public class CommentVM
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int? ParentId { get; set; }
        public string Content { get; set; }
        public List<CommentVM> SubComments { get; set; }
        public List<AttachmentVM> Attachments { get; set; }
        public int CardId { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
