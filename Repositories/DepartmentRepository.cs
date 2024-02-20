using api.Databases;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
  public class DepartmentRepository(ApplicationDbContext context) : IDepartmentRepository
  {
    private readonly ApplicationDbContext _context = context;
    public async Task<Department?> AddAsync(Department model)
    {
      var department = _context.Departments.FirstAsync(x => x.Id == model.Id);
      if (department == null)
      {
        _context.Departments.Add(model);
        await _context.SaveChangesAsync();
        return await GetAsync(model.Id);
      }
      return null;
    }

    public async Task DeleteAsync(string id)
    {
      var department = _context.Departments.FirstOrDefault(x => x.Id == id);
      if (department != null)
      {
        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
      }
    }

    public async Task<List<Department>?> GetAllAsync()
    {
      return await _context.Departments.ToListAsync();
    }

    public async Task<Department?> GetAsync(string id)
    {
      return await _context.Departments.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(string id, Department model)
    {
      var department = _context.Departments.FirstOrDefault(x => x.Id == id);
      if (department != null)
      {
        _context.Departments.Update(model);
        await _context.SaveChangesAsync();
      }
    }
  }
}