using api.Data;
using api.Models;
using api.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
  private readonly ApplicationDbContext dbContext;

  public EmployeesController(ApplicationDbContext dbContext)
  {
    this.dbContext = dbContext;
  }

  [HttpGet]
  public IActionResult GetAllEmployees()
  {
    var allEmployees = dbContext.Employees.ToList();
    return Ok(allEmployees);
  }

  [HttpGet]
  [Route("{id:guid}")]
  public IActionResult GetEmployeeById(Guid id)
  {
    var employee = dbContext.Employees.Find(id);
    if (employee == null)
    {
      return NotFound($"Employee with the {id} not found ");
    }

    return Ok(employee);
  }

  [HttpPost]
  public IActionResult CreateEmployee(AddEmployeeDto addEmployeeDto)
  {
    var employeeEntity = new Employee()
    {
      Email = addEmployeeDto.Email,
      Name = addEmployeeDto.Name,
      Phone = addEmployeeDto.Phone,
      Salary = addEmployeeDto.Salary
    };

    dbContext.Employees.Add(employeeEntity);
    int sd = dbContext.SaveChanges();

    return StatusCode(StatusCodes.Status201Created, employeeEntity);
  }

  [HttpPut]
  [Route("{id:guid}")]
  public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
  {

    var employee = dbContext.Employees.Find(id);

    if (employee == null)
    {
      return NotFound($"Employee with the {id} not found ");
    }

    employee.Email = updateEmployeeDto.Email;
    employee.Name = updateEmployeeDto.Name;
    employee.Phone = updateEmployeeDto.Phone;
    employee.Salary = updateEmployeeDto.Salary ??= employee.Salary;

    dbContext.SaveChanges();

    return Ok(employee);
  }

  [HttpDelete]
  [Route("{id:guid}")]
  public IActionResult DeleteEmployee(Guid id)
  {
    var employee = dbContext.Employees.Find(id);
    if (employee == null)
    {
      return NotFound();
    }

    dbContext.Employees.Remove(employee);
    dbContext.SaveChanges();

    return Ok();
  }
}
