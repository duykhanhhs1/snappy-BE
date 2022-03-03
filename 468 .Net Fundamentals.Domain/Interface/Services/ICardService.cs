using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.EnumType;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Domain.Interface.Services
{
    public interface ICardService
    {
        public Task Create(int busId, CardCreateVM name);
        public Task<IList<CardVM>> GetAllByBusiness(int busId);
        public Task<CardVM> GetDetail(int id);
        public Task Delete(int id);

        public Task CardMovement(int id, CardMovementVM data);

        public Task UpdateName(int id, CardNameVM newName);
        public Task UpdatePriority(int id, TaskPriority newPriority);
        public Task UpdateDescription(int id, CardDescriptionVM newDescription);
        public Task UpdateDuedate(int id, CardDueDateVM newDuedate);
        public Task<IList<CardVM>> GetAllByUser();
    }
}
