using api.Dtos.Core;
using api.Interfaces.Core;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{
  public class RoleService(RoleManager<IdentityRole> roleManager) : IRoleService
  {
    private readonly RoleManager<IdentityRole> _manager = roleManager;
    public async Task<List<RoleDto>> AllAsync()
    {
      var result = await _manager.Roles.ToListAsync();
      if (result.IsNullOrEmpty()) return [];
      return result.Select(r => r.ToRoleDto()).ToList();
    }
  }
}