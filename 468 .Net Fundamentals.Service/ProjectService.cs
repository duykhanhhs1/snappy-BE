using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels;
using _468_.Net_Fundamentals.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Service
{
    public class ProjectService : RepositoryBase<Project>, IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICurrrentUser _currrentUser;

        public ProjectService(ApplicationDbContext context, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, ICurrrentUser currrentUser) : base(context)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _currrentUser = currrentUser;
        }
        private readonly IBusinessService _businessService;

        public async Task Create(ProjectCreateVM newProject)
        {
            try
            {
                var currentUserId = _currrentUser?.Id;

                var project = new Project
                {
                    Name = newProject.Name,
                    ColorCode = newProject.ColorCode,
                    CreatedBy = currentUserId,
                };

                await _unitOfWork.Repository<Project>().InsertAsync(project);
                var bus = new Business
                {
                    Name = "Công việc",
                    ColorCode = "#3BB227",
                    ProjectId = project.Id,
                };
                await _unitOfWork.Repository<Business>().InsertAsync(bus);
                var bus1 = new Business
                {
                    Name = "Hoàn thành",
                    ColorCode = "#0AD8AF",
                    ProjectId = project.Id,
                    IsDefault = true,
                    IsDefaultFinish = true,
                };
                await _unitOfWork.Repository<Business>().InsertAsync(bus1);
                var bus2 = new Business
                {
                    Name = "Thất bại",
                    ColorCode = "#F63C3C",
                    ProjectId = project.Id,
                    IsDefault = true,
                    IsDefaultFinish = false,
                };
                await _unitOfWork.Repository<Business>().InsertAsync(bus2);
                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }
        }

        public async Task<ProjectVM> Get(int id)
        {
            try
            {
                var project = await _unitOfWork.Repository<Project>().FindAsync(id);

                var projectVM = new ProjectVM
                {
                    Id = project.Id,
                    ColorCode = project.ColorCode,
                    Name = project.Name,
                    CreatedBy = project.CreatedBy,
                };

                return projectVM;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public async Task<IList<ProjectVM>> GetAll()
        {
            try
            {
                /*// Gets list of claims.
                IEnumerable<Claim> claim = identity.Claims;

                // Gets name from claims. Generally it's an email address.
                var usernameClaim = claim
                    .Where(x => x.Type == ClaimTypes.Name)
                    .FirstOrDefault();

                // Finds user.
                var user = await _userManager
                    .FindByNameAsync(usernameClaim.Value);*/
                var currentUserId = _currrentUser?.Id;

                var projectVMs = await _unitOfWork.Repository<Project>()
                    .Query()
                    .Where(_ => _.CreatedBy == currentUserId)
                    .Select(project => new ProjectVM
                    {
                        Id = project.Id,
                        Name = project.Name,
                        ColorCode = project.ColorCode,
                        CreatedBy = project.CreatedBy,
                    })
                    .ToListAsync();
                return projectVMs;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public async Task Update(int id, string name)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var project = await _unitOfWork.Repository<Project>().FindAsync(id);
                project.Name = name;

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw e;
            }
        }
        public async Task Delete(int id)
        {
            try
            {
                await _unitOfWork.Repository<Project>().DeleteAsync(id);

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
