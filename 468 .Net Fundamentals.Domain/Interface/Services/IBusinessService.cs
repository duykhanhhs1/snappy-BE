﻿using _468_.Net_Fundamentals.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Domain.Interface.Services
{
    public interface IBusinessService
    {
        // Business
        public Task Create(int projectId, string name);
        public Task<IList<BusinessVM>> GetAllByProject(int projectId);
        public Task Update(int id, string name);
        public Task Delete(int id);
    }
}
