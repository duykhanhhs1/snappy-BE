using _468_.Net_Fundamentals.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace _468_.Net_Fundamentals.Infrastructure
{
    public class UnitOfWorkBase : IUnitOfWork
    {
        public ApplicationDbContext _context { get; private set; }
        private Dictionary<string, object> _repositories { get; }


        private IDbContextTransaction _transaction;

        public UnitOfWorkBase(ApplicationDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<string, dynamic>();
        }


        public async Task BeginTransaction()
        {
            await StartNewTransactionIfNeeded();
        }

        private async Task StartNewTransactionIfNeeded()
        {
            if (_transaction == null)
            {
                _transaction = await _context.Database.BeginTransactionAsync();
            }
        }

        public async Task CommitTransaction()
        {
            await _context.SaveChangesAsync();

            if (_transaction == null) return;
            await _transaction.CommitAsync();

            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public void Dispose()
        {
            if (_context == null)
                return;
            //
            // Close connection
/*            if (_context.Database.GetDbConnection().State == ConnectionState.Open)
            {
                _context.Database.GetDbConnection().Close();
            }*/
            _context.Dispose();

            _context = null;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);
            var typeName = type.Name;

            lock (_repositories)
            {
                if (_repositories.ContainsKey(typeName))
                {
                    return (IRepository<T>)_repositories[typeName];
                }

                var repository = new RepositoryBase<T>(_context);

                _repositories.Add(typeName, repository);
                return repository;
            }
        }

        public async Task RollbackTransaction()
        {
            if (_transaction == null) return;

            await _transaction.RollbackAsync();

            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
