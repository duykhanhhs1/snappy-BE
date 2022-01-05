﻿using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private IProjectService _projectService;
        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<int> Create([FromBody] string name)
        {
            return await _projectService.Create(name);
        }

        [HttpGet]
        public async Task<IList<ProjectVM>> GetAll()
        {
            return await _projectService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ProjectVM> Get(int id)
        {
            return await _projectService.Get(id);
        }

        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] string name)
        {
            await _projectService.Update(id, name);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _projectService.Delete(id);
        }

        
    }
}
