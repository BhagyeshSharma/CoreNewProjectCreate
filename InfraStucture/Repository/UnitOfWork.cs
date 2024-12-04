using ClassDAL;
using Data;
using InfraStucture.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStucture.Repository
{
    public class UnitOfWork  : IUnitOfWork
    {
        private readonly UserMgMtContext _userMgMtContext;
        private readonly dbRepository _dbOperations;

        public UnitOfWork(UserMgMtContext userMgMtContext, dbRepository dbOperations)
        {
            _userMgMtContext = userMgMtContext;
            _dbOperations = dbOperations;
        }
        public IEmployeeRegRepository employeeRegRepository => new EmployeeRegRepository(_userMgMtContext, _dbOperations);    
        public async Task CommitTransactionAsync()
        {
            await _userMgMtContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
