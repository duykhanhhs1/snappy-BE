﻿using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels;
using _468_.Net_Fundamentals.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Service
{
    public class TagService : RepositoryBase<Tag>, ITagService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TagService(ApplicationDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(TagCreateVM request)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var tag = new Tag
                {
                    Name = request.Name,
                    ProjectId = request.ProjectId
                };
              
                await _unitOfWork.Repository<Tag>().InsertAsync(tag);
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }
        public async Task<IList<TagVM>> GetAll(int projectId)
        {
            var allTags = await _unitOfWork.Repository<Tag>().GetAllAsync();

            var tags = from tag in allTags where tag.ProjectId == projectId select tag;

            var tagsVM = new List<TagVM>();

            foreach(var t in tags)
            {
                tagsVM.Add(new TagVM { Id = t.Id, Name = t.Name, ProjectId = t.ProjectId });
            }

            return tagsVM;
        }

        public async Task Update(int id, string name)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var tag =  await _unitOfWork.Repository<Tag>().FindAsync(id);

                tag.Name = name;

                await _unitOfWork.CommitTransaction();
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

                await _unitOfWork.Repository<Tag>().DeleteAsync(id);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }

    }
}
