using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Net.Architecture.DataAccess.Contexts;
using Net.Architecture.DataAccess.Repository.RepositoryFactory;

namespace Net.Architecture.DataAccess.UnitOfWork
{
    public class UnitOfWork : RepositoryFactory<PostgreSqlContext>, IUnitOfWork, IAsyncDisposable
    {
        private IDbContextTransaction _transaction;
        private bool _disposed;

        public UnitOfWork(PostgreSqlContext context) : base(context)
        {
            //_context.ChangeTracker.AutoDetectChangesEnabled = false;
        }
        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        //Creates A New Transaction And Commits items
        public async Task<int> ForceSaveChangesAsync()
        {
            await CheckTransactionAsync();
            try
            {
                if (_context == null)
                    throw new ArgumentException("dbContext can not be null..!");
                int result = await _context.SaveChangesAsync();
                await CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                await RollbackAsync();
                throw new Exception("Error on SaveChangesAsync Method..! ", ex);
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                if (_context == null)
                    throw new ArgumentException("dbContext can not be null..!");
                int result = await _context.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error on SaveChangesAsync Method..! ", ex);
            }
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }

        public T Repository<T>() where T : class
        {
            return CreateDal<T>();
        }

        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
        }

        protected virtual async Task DisposeAsync(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    await _context.DisposeAsync();
            _disposed = true;
        }

        public async Task CheckTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        }

        public async Task CreateTransactionAsync()
        {
            if (_transaction?.GetDbTransaction().Connection == null)
                await CheckTransactionAsync();
        }


    }
}
