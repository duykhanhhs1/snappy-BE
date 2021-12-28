﻿using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels;
using _468_.Net_Fundamentals.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _468_.Net_Fundamentals.Service
{
    public class BusinessService : RepositoryBase<Business>, IBusinessService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BusinessService(ApplicationDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }

        // Business

        public async Task Create(BusinessVM request)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var business = new Business
                {
                    Name = request.Name,
                    ProjectId = request.ProjectId
                };

                await _unitOfWork.Repository<Business>().InsertAsync(business);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }

        public async Task<IList<BusinessVM>> GetAllByProject(int projectId)
        {
            var businessesVM = await _unitOfWork.Repository<Business>()
                .Query()
                .Where(_ => _.ProjectId == projectId)
                .Select(b => new BusinessVM
                {
                    Name = b.Name,
                    ProjectId = b.ProjectId
                })
                .ToListAsync();

            return businessesVM;
        }

        public async Task Update(int id, string name)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var business = await _unitOfWork.Repository<Business>().FindAsync(id);
                business.Name = name;

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            };
        }

        public async Task Delete(int id)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                await _unitOfWork.Repository<Business>().DeleteAsync(id);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            };
        }

    
    }
}
