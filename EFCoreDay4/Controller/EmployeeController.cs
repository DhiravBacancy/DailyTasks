using EFCore_Day_5_Full_CRUD.DBContext;
using EFCore_Day_5_Full_CRUD.DTOs;
using EFCore_Day_5_Full_CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore_Day_5_Full_CRUD.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDBContext _dbContext;
        public EmployeeController(AppDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeDTO employee)
        {
            try
            {
                var newEmployee = new Employee
                {
                    Name = employee.Name,
                    Email = employee.Email,
                    DepartmentId = employee.DepartmentId
                };

                _dbContext.Employees.Add(newEmployee);
                await _dbContext.SaveChangesAsync();

                return Ok("New Employee Added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Exception: {ex}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            try
            {
                var fetchData = _dbContext.Employees.Include(ep => ep.EmployeeProjects)
                                                    .ThenInclude(p => p.Project)
                                                    .Where(e => e.EmployeeId == id)
                                                    .Select(s => new
                                                    {
                                                        Name = s.Name,
                                                        Email = s.Email,
                                                        ProjectDetails = s.EmployeeProjects.Select(ns => new
                                                        {
                                                            ProjectName = ns.Project.ProjectName,
                                                            Role = ns.Role,
                                                            StartDate = ns.Project.StartDate
                                                        })
                                                    });

                return Ok(fetchData);
            }
            catch (Exception ex)
            {
                return BadRequest($"Exception: {ex}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeById(int id, [FromBody] EmployeeDTO employee)
        {
            try
            {
                var updateData = _dbContext.Employees.FirstOrDefault(e => e.EmployeeId == id);

                if (updateData == null)
                    return NotFound($"Employee with Id: {id} not found.");

                updateData.Name = employee.Name;
                updateData.Email = employee.Email;
                updateData.DepartmentId = employee.DepartmentId;

                _dbContext.Employees.Update(updateData);
                await _dbContext.SaveChangesAsync();

                return Ok($"Employee with Id: {id} is updated...");
            }
            catch (Exception ex)
            {
                return BadRequest($"Exception: {ex}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeById(int id)
        {
            try
            {
                var employee = _dbContext.Employees.FirstOrDefault(e => e.EmployeeId == id);

                if (employee == null)
                    return NotFound($"Employee with Id: {id} not found.");

                _dbContext.Employees.Remove(employee);
                await _dbContext.SaveChangesAsync();

                return Ok($"Employee with Id: {id} is Deleted...");
            }
            catch (Exception ex)
            {
                return BadRequest($"Exception: {ex}");
            }
        }
    }
}