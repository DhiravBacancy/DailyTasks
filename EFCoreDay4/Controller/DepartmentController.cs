using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EFCore_Day_5_Full_CRUD.DBContext;
using EFCore_Day_5_Full_CRUD.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EFCore_Day_5_Full_CRUD.Controller
{
    [ApiController]
    [Controller]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly AppDBContext _dbContext;
        public DepartmentController(AppDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        [HttpPost]
        public IActionResult CreateNewDepartment([FromBody]string department)
        {
            try
            {
                var newDepartment = new Department
                {
                    DepartmentName = department
                };

                _dbContext.Departments.Add(newDepartment);

                _dbContext.SaveChanges();
                return Ok($"{department} added Successfully");
            }
            catch(Exception ex)
            {
                return BadRequest($"Something wents wrong while requesting your request.: {ex}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetDepartmentById(int id)
        {
            try
            {
                var fetchData = _dbContext.Departments.Include(e => e.Employees)
                                                      .Where(d => d.DepartmentId == id)
                                                      .Select(s => new
                                                      {
                                                          ID = s.DepartmentId,
                                                          Name = s.DepartmentName,
                                                          Employees = s.Employees.Select(e => new
                                                          {
                                                              EmployeeId = e.EmployeeId,
                                                              EmployeeName = e.Name,
                                                              EmployeeEmail = e.Email
                                                          })
                                                      });
                return Ok(fetchData);
            }
            catch(Exception ex)
            {
                return BadRequest("Something wents wrong while requesting your request.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDepartmentById(int id, [FromBody] string department)
        {
            try
            {
                var updateData = _dbContext.Departments.FirstOrDefault(d => d.DepartmentId == id);

                updateData.DepartmentName = department;

                _dbContext.Departments.Update(updateData);
                _dbContext.SaveChanges();

                return Ok($"{id} : Department Added Successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("Something wents wrong while requesting your request.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDepartmentById(int id)
        {
            try
            {
                var data = _dbContext.Departments.FirstOrDefault(d => d.DepartmentId == id);

                _dbContext.Departments.Remove(data);  // Hard Delete 
                _dbContext.SaveChanges();
                return Ok($"Department with {id} deleted Successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("Something wents wrong while requesting your request.");
            }
        }
    }
}