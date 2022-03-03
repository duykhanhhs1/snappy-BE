using _468_.Net_Fundamentals.Domain.Entities;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels;
using _468_.Net_Fundamentals.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace _468_.Net_Fundamentals.Service
{
    public class TodoService : RepositoryBase<Todo>, ITodoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TodoService(ApplicationDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int?> Create(int cardId, TodoCreateVM newTodo)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var todo = new Todo
                {
                    Name = newTodo.Name,
                    CardId = cardId,
                    IsCompleted = false
                };

                await _unitOfWork.Repository<Todo>().InsertAsync(todo);
                
                await _unitOfWork.CommitTransaction();

                return todo.Id;
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                return null;
            }
        }


        public async Task<IList<TodoVM>> GetAll(int cardId)
        {

            var todoVMs = await _unitOfWork.Repository<Todo>()
                .Query()
                .Where(_ => _.CardId == cardId)
                .Select(todo => new TodoVM {
                    Id = todo.Id,
                    IsCompleted = todo.IsCompleted,
                    Name = todo.Name,
                    CardId = todo.CardId
                }).ToListAsync();

            /*var todos = from todo in alltodo where todo.CardId == cardId select todo;

            var todoVMs = new List<TodoVM>();

            foreach (var todo in todos)
            {
                todoVMs.Add(new TodoVM { Name = todo.Name, CardId = todo.CardId });
            }*/

            return todoVMs;
        }       
        
        public async Task<IList<TodoVM>> GetAllByUser()
        {

            var todoVMs = await _unitOfWork.Repository<Todo>()
                .Query()
                .Select(todo => new TodoVM {
                    Id = todo.Id,
                    IsCompleted = todo.IsCompleted,
                    Name = todo.Name,
                    CardId = todo.CardId
                }).ToListAsync();

            /*var todos = from todo in alltodo where todo.CardId == cardId select todo;

            var todoVMs = new List<TodoVM>();

            foreach (var todo in todos)
            {
                todoVMs.Add(new TodoVM { Name = todo.Name, CardId = todo.CardId });
            }*/

            return todoVMs;
        }

        public async Task UpdateName(int id, TodoCreateVM newTodo)
        {
            try
            {
                var todo =  await _unitOfWork.Repository<Todo>().FindAsync(id);

                todo.Name = newTodo.Name;

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }
        public async Task UpdateComplete(int id, Boolean status)
        {
            try
            {
                var todo = await _unitOfWork.Repository<Todo>().FindAsync(id);

                todo.IsCompleted = status;

                await _unitOfWork.SaveChangesAsync();
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

                await _unitOfWork.Repository<Todo>().DeleteAsync(id);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
            }
        }
    }
}
