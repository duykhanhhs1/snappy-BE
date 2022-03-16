using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.Entities
{
    [Table("CommentAttachment")]
    public class CommentAttachment
    {
        public int CommentId { get; set; }

        public int AttachmentId { get; set; }

        [ForeignKey("CommentId")]
        public virtual Comment Comment { get; set; }

        [ForeignKey("AttachmentId")]
        public virtual Attachment Attachment { get; set; }

    }
}
