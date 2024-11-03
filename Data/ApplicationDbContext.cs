using api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data;
public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions options) : base(options)
  {
  }

  public DbSet<Employee> Employees { get; set; }
}