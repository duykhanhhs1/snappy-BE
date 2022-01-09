﻿using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Domain.Interface.Services
{
    public interface ITagService
    {
        public Task CreateOnProject(int projectId, string name);
        public Task<IList<TagVM>> GetAllOnProject(int projectId);

        public Task AddCardTag(int cardId, int tagId);
        public Task<IList<CardTagVM>> GetAllCardTag(int cardId);

        public Task Update(int id, string name);
        public Task Delete(int id);
    }
}
