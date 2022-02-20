using System;
using System.Collections.Generic;
using System.Text;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.Entities;

namespace _468_.Net_Fundamentals.Infrastructure.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {

    }
    class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
