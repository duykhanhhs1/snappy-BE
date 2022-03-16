using System;
using System.Collections.Generic;
using System.Text;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.Entities;

namespace _468_.Net_Fundamentals.Infrastructure.Repositories
{
    public interface IAttachmentRepository : IRepository<Attachment>
    {

    }
    class AttachmentRepository : RepositoryBase<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
