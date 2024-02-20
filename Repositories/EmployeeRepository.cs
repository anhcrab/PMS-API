using api.Databases;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
  public class EmployeeRepository(ApplicationDbContext context) : IEmployeeRepository
  {
    private readonly ApplicationDbContext _context = context;
    public async Task<Employee?> AddAsync(Employee model)
    {
      var employee = _context.Employees.FirstOrDefault(x => x.UserId == model.UserId || x.EmployeeId == model.EmployeeId);
      if (employee == null)
      {
        _context.Employees.Add(model);
        await _context.SaveChangesAsync();
        return await GetByEmailAsync(model.User.Email!);
      }
      return null;
    }

    public async Task DeleteAsync(string id)
    {
      var employee = _context.Employees.FirstOrDefault(x => x.EmployeeId == id);
      if (employee != null)
      {
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
      }
    }

    public async Task<List<Employee>?> GetAllAsync()
    {
      return await _context.Employees.ToListAsync();
    }

    public async Task<Employee?> GetAsync(string id)
    {
      return await _context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
    }

    public async Task<Employee?> GetByEmailAsync(string email)
    {
      return await _context.Employees.FirstOrDefaultAsync(x => x.User.Email == email);
    }

    public async Task UpdateAsync(string id, Employee model)
    {
      var employee = _context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
      if (employee != null)
      {
        _context.Employees.Update(model);
        await _context.SaveChangesAsync();
      }
    }
  }
}