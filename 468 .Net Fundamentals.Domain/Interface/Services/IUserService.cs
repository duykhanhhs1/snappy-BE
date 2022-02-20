using _468_.Net_Fundamentals.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Domain.Interface.Services
{
    public interface IUserService
    {
        public Task<IActionResult> CurrentUser();

        public Task<UserVM> GetCurrentUser();

        /*public Task Create();*/
        public Task<IList<UserVM>> GetAll();
        public Task<UserVM> Get(string id);

        public Task AddCardAssign(int cardId, string userId);

        public Task<IList<CardAssignVM>> GetAllCardAssign(int cardId);

        public Task DeleteCardAssign(int cardId, string userId);


        /*public Task Update(int id, string name);
        public Task Delete(int id);*/
    }
}
