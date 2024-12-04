using Entity.Modal;
using Entity.ViewModal;

namespace InfraStucture.Contract;

public interface IEmployeeRegRepository
{
    Task<bool> SaveAsync(EmployeeReg employee); // Method to save an employee
    Task<EmployeeReg> UpdateAsync(EmployeeReg employee); // Method to update an employee
    Task<EmployeeReg> GetByIdAsync(int id); // Method to get an employee by ID
    Task<IEnumerable<EmployeeReg>> GetAllAsync(); // Method to fetch all employees
}
