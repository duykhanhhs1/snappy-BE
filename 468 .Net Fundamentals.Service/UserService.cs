using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.EnumType;
using _468_.Net_Fundamentals.Domain.Interface;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels;
using _468_.Net_Fundamentals.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Service
{
    public class UserService : RepositoryBase<AppUser>, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICurrrentUser _currrentUser;
        public UserService(ApplicationDbContext context, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, ICurrrentUser user) : base(context)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _currrentUser = user;
        }

        public async Task<IActionResult> CurrentUser()
        {
            try
            {
                var id = _currrentUser?.Id;
                var user =  await _userManager.FindByIdAsync(id);

                return new OkObjectResult(new
                {
                    id = user.Id,
                    userName = user.UserName,
                    email = user.Email,
                    imagePath = user.ImagePath
                });

            }
            catch (Exception e)
            {

                throw e;
            }
        }


        public async Task<UserVM> GetCurrentUser()
        {
            var id = _currrentUser?.Id;
            var userVM = await _unitOfWork.Repository<AppUser>()
              .Query()
              .Where(_ => _.Id == id)
              .Select(u => new UserVM
              {
                  Id = u.Id,
                  UserName = u.UserName,
                  Email = u.Email,
                  ImagePath = u.ImagePath
              }).FirstOrDefaultAsync();

            return userVM;
        }

        public async Task AddCardAssign(int cardId, string userId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var currentUserId = _currrentUser?.Id;


                // Saving user assign
                var cardAssign = new CardAssign
                {
                    CardId = cardId,
                    AssignTo = userId
                };

                // Save history
                var activity = new Activity
                {
                    CardId = cardId,
                    UserId = currentUserId,
                    Action = AcctionEnumType.AssignUser,
                    CurrentValue = userId.ToString(),
                    OnDate = DateTime.Now
                };

                await _unitOfWork.Repository<CardAssign>().InsertAsync(cardAssign);
                await _unitOfWork.Repository<Activity>().InsertAsync(activity);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }
        }

        public async Task<IList<CardAssignVM>> GetAllCardAssign(int cardId)
        {
            var cardAssignVM = await _unitOfWork.Repository<CardAssign>()
             .Query()
             .Where(_ => _.CardId == cardId)
             .Select(c => new CardAssignVM
             {
                 CardId = c.CardId,
                 AssignTo = c.AssignTo,
                 UserName = c.User.UserName,
                 Email = c.User.Email,
                 ImagePath = c.User.ImagePath
             }).ToListAsync();

            return cardAssignVM;
        }

        public async Task<UserVM> Get(string Id)
        {
            var userVM = await _unitOfWork.Repository<AppUser>()
              .Query()
              .Where(_ => _.Id == Id)
              .Select(u => new UserVM
              {
                  Id = u.Id,
                  UserName = u.UserName,
                  Email = u.Email,
                  ImagePath = u.ImagePath
              }).FirstOrDefaultAsync();

            return userVM;
        }

        public async Task<IList<UserVM>> GetAll()
        {
            var userVMs = await _unitOfWork.Repository<AppUser>()
              .Query()
              .Select(u => new UserVM
              {
                  Id = u.Id,
                  UserName = u.UserName,
                  Email = u.Email,
                  ImagePath = u.ImagePath
              }).ToListAsync();

            return userVMs;
        }

        public async Task DeleteCardAssign(int cardId, string userId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();


                var cardAssign = await _unitOfWork.Repository<CardAssign>()
                    .Query()
                    .Where(_ => _.CardId == cardId && _.AssignTo == userId)
                    .FirstOrDefaultAsync();

                await _unitOfWork.Repository<CardAssign>().DeleteAsync(cardAssign);

                // Hardcode for login user
                // var user = await _unitOfWork.Repository<AppUser>().FindAsync(1);
                var currentUserId = _currrentUser?.Id;
                // Save history
                var activity = new Activity
                {
                    CardId = cardId,
                    UserId = currentUserId,
                    Action = AcctionEnumType.RemoveAssignUser,
                    CurrentValue = cardAssign.AssignTo.ToString(),
                    OnDate = DateTime.Now
                };

                await _unitOfWork.Repository<Activity>().InsertAsync(activity);


                await _unitOfWork.CommitTransaction();

            }
            catch (Exception e)
            {

                throw e;
            }
                  
        }

    }
}
