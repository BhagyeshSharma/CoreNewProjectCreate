using InfraStucture.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.Data.Common;
using ClassDAL;
using Entity.Modal;
using Microsoft.EntityFrameworkCore;

namespace InfraStucture.Repository;

public class EmployeeRegRepository : IEmployeeRegRepository 
{
    private readonly UserMgMtContext _userMgMtContext;
    private readonly dbRepository _dbOperations;

    public EmployeeRegRepository(UserMgMtContext userMgMtContext, dbRepository dbOperations)
    {
        _userMgMtContext = userMgMtContext;
        _dbOperations = dbOperations;
    }
    public async Task<bool> SaveAsync(EmployeeReg employee)
    {
        try
        {
            await _userMgMtContext.EmployeeReg.AddAsync(employee);
            await _userMgMtContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            // Log the exception (logging not shown here)
            return false;
        }
    }

    // Method to update an employee
    public async Task<EmployeeReg> UpdateAsync(EmployeeReg employee)
    {
        try
        {
            var existingEmployee = await _userMgMtContext.EmployeeReg.FindAsync(employee.EmployeeId);
            if (existingEmployee != null)
            {
                existingEmployee.EmployeeName = employee.EmployeeName; // Update other properties as needed
                existingEmployee.EmpFatherName = employee.EmpFatherName;
                existingEmployee.StateId = employee.StateId;
                existingEmployee.FileUpload = employee.FileUpload;
                // Add other properties to update here

                await _userMgMtContext.SaveChangesAsync();
                return existingEmployee;
            }
            return null; // Employee not found
        }
        catch (Exception ex)
        {
            // Log the exception (logging not shown here)
            return null;
        }
    }

    // Method to get an employee by ID
    public async Task<EmployeeReg> GetByIdAsync(int id)
    {
        try
        {
            return await _userMgMtContext.EmployeeReg.FindAsync(id);
        }
        catch (Exception ex)
        {
            // Log the exception (logging not shown here)
            return null;
        }
    }

    // Method to fetch all employees
    public async Task<IEnumerable<EmployeeReg>> GetAllAsync()
    {
        try
        {
            return await _userMgMtContext.EmployeeReg.ToListAsync();
        }
        catch (Exception ex)
        {
            // Log the exception (logging not shown here)
            return Enumerable.Empty<EmployeeReg>(); // Return an empty list on error
        }
    }
}
