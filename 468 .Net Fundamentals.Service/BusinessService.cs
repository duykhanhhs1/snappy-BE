using _468_.Net_Fundamentals.Domain.Entities;
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

        public async Task Create(int projectId, BusinessCreateVM bus)
        {
            try
            {
                var business = new Business
                {
                    Name = bus.Name,
                    ProjectId = projectId
                };

                await _unitOfWork.Repository<Business>().InsertAsync(business);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }
        }

        public async Task<IList<BusinessVM>> GetAllByProject(int projectId)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

               /* var businessStored = await _unitOfWork.Repository<Business>()
                    .Query()
                    .Where(_ => _.ProjectId == projectId)
                    .ToListAsync();*/




                var businessesVM = await _unitOfWork.Repository<Business>()
                 .Query()
                 .Where(_ => _.ProjectId == projectId)
                 .Select(b => new BusinessVM
                 {
                     Id = b.Id,
                     Name = b.Name,
                     ProjectId = b.ProjectId,     
                 })
                 .ToListAsync();

                await _unitOfWork.CommitTransaction();
                return businessesVM;
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }
            
        }

        public async Task Update(int id, BusinessCreateVM bus)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var business = await _unitOfWork.Repository<Business>().FindAsync(id);
                business.Name = bus.Name;

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
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
                throw e;
            };
        }

    
    }
}
