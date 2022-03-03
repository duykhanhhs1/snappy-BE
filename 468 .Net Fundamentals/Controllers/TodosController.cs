using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;
        public TodosController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpPost("/api/card/{cardId}/todos")]
        public async Task Create(int cardId, [FromBody] TodoCreateVM todo)
        {
            await _todoService.Create(cardId, todo);
        }

        [HttpGet("/api/card/{cardId}/todos")]
        public async Task<IList<TodoVM>> GetAll(int cardId)
        {
            return await _todoService.GetAll(cardId);
        }       
        
        
        [HttpGet("/api/todos-by-user")]
        public async Task<IList<TodoVM>> GetAllByUser()
        {
            return await _todoService.GetAllByUser();
        }
         
        [HttpPut("{id}/name")]
        public async Task UpdateName(int id, [FromBody] TodoCreateVM todo)
        {
            await _todoService.UpdateName(id, todo);
        }

        [HttpPut("{id}/iscompleted")]
        public async Task UpdateComplete(int id, [FromBody] Boolean isCompleted)
        {
            await _todoService.UpdateComplete(id, isCompleted);
        }

        [HttpDelete("{id}")]
        public async Task Delete (int id)
        {
            await _todoService.Delete(id);
        }
    }
}
