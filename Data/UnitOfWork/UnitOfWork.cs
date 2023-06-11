using Core.Models;
using Data.Contexts;
using Data.Repository.Dapper;
using Data.Repository.Generic;
using System;

namespace Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext appDbContext;
        private readonly DapperDbContext dapperDbContext;
        private bool disposed;

        public UnitOfWork(AppDbContext appDbContext, DapperDbContext dapperDbContext)
        {
            this.appDbContext = appDbContext;
            this.dapperDbContext = dapperDbContext;
        }

        public IGenericRepository<T> GenericRepository<T>() where T : BaseEntity
        {
            return new GenericRepository<T>(appDbContext);
        }

        public IDapperRepository<T> DapperRepository<T>() where T : BaseEntity
        {
            return new DapperRepository<T>(dapperDbContext.CreateConnection());
        }

        public void SaveChanges()
        {
            appDbContext.SaveChanges();
        }

        public void SaveChangesAsync()
        {
            appDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Clean(true);
        }

        private void Clean(bool disposing)
        {
            if (!disposed)
            {
                if (disposing && appDbContext is not null)
                {
                    appDbContext.Dispose();
                }
            }

            disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}