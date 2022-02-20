using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using _468_.Net_Fundamentals.Domain.ViewModels;

namespace _468_.Net_Fundamentals.Domain.Interface.Services
{
    public interface ICommentService
    {
        public Task Create(int cardId, CommentCreateVM comment);
    }
}
