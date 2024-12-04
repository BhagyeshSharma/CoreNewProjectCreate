using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InfraStucture.Repository;

namespace InfraStucture.Contract
{
    public interface IUnitOfWork
    {
        IEmployeeRegRepository employeeRegRepository { get; }
        IStudentRegRepository studentRegRepository { get; }
        Task CommitTransactionAsync();
    }
}
