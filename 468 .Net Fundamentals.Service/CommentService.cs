using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels;
using _468_.Net_Fundamentals.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace _468_.Net_Fundamentals.Service
{
    public class CommentService : RepositoryBase<Comment>, ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICurrrentUser _currrentUser;

        public CommentService(ApplicationDbContext context, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, ICurrrentUser currrentUser) : base(context)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _currrentUser = currrentUser;
        }

        public async Task Create(int cardId, CommentCreateVM newComment)
        {
            try
            {
                var currentUserId = _currrentUser?.Id;

                var comment = new Comment
                {
                    UserId = currentUserId,
                    CardId = cardId,
                    Content = newComment.Content,
                    CreatedOn = DateTime.Now
                };

                await _unitOfWork.Repository<Comment>().InsertAsync(comment);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }
        }
    }
}
