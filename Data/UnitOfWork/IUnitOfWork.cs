using Core.Models;
using Data.Repository.Dapper;
using Data.Repository.Generic;
using System;

namespace Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDapperRepository<T> DapperRepository<T>() where T : BaseEntity;
        IGenericRepository<T> GenericRepository<T>() where T : BaseEntity;

        void SaveChanges();
        void SaveChangesAsync();
    }
}