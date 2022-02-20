using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels;
using _468_.Net_Fundamentals.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        public async Task<int?> Create(int cardId, CommentCreateVM newComment)
        {
            try
            {
                var currentUserId = _currrentUser?.Id;

                var comment = new Comment
                {
                    UserId = currentUserId,
                    ParentId = newComment.ParentId,
                    CardId = cardId,
                    Content = newComment.Content,
                    CreatedOn = DateTime.Now
                };

                await _unitOfWork.Repository<Comment>().InsertAsync(comment);
                await _unitOfWork.SaveChangesAsync();

                return comment.Id;
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }
        }

        public async Task<IList<CommentVM>> GetAll(int cardId)
        {
            var commentVMs = await _unitOfWork.Repository<Comment>()
                .Query()
                .Where(_ => _.CardId == cardId && _.ParentId == null)
                .Select(comment => new CommentVM
                {
                    Id = comment.Id,
                    ParentId = comment.ParentId,
                    UserId = comment.UserId,
                    Content = comment.Content,
                    CardId = comment.CardId,
                    CreatedOn = comment.CreatedOn,
                }).ToListAsync();
            return commentVMs;
        }

        public async Task Update(int id, CommentCreateVM newComment)
        {
            try
            {
                var comment = await _unitOfWork.Repository<Comment>().FindAsync(id);

                comment.Content = newComment.Content;
                comment.UpdatedOn = DateTime.Now;

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }
        public async Task Delete(int id)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                await _unitOfWork.Repository<Comment>().DeleteAsync(id);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }
    }
}
