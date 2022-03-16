using System;
using System.Collections.Generic;
using System.Text;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.Entities;

namespace _468_.Net_Fundamentals.Infrastructure.Repositories
{
    public interface ICommentAttachmentRepository : IRepository<CommentAttachment>
    {

    }
    class CommentAttachmentRepository : RepositoryBase<CommentAttachment>, ICommentAttachmentRepository
    {
        public CommentAttachmentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
