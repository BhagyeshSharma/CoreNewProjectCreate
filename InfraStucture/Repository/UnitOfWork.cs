using ClassDAL;
using Data;
using InfraStucture.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStucture.Repository
{
    public class UnitOfWork  : IUnitOfWork
    {
        private readonly UserMgMtContext _userMgMtContext;
        private readonly dbRepository _dbOperations;
        private readonly IDbConnection _connection;
        public UnitOfWork(UserMgMtContext userMgMtContext, dbRepository dbOperations, IDbConnection dbConnection)
        {
            _userMgMtContext = userMgMtContext;
            _dbOperations = dbOperations;
            _connection = dbConnection;   
        }
        public IEmployeeRegRepository employeeRegRepository => new EmployeeRegRepository(_userMgMtContext, _dbOperations);
        public IStudentRegRepository studentRegRepository => new StudentRegRepository(_connection);
        public async Task CommitTransactionAsync()
        {
            await _userMgMtContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
