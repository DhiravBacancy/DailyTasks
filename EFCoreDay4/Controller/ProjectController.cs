using EFCore_Day_5_Full_CRUD.DBContext;
using EFCore_Day_5_Full_CRUD.DTOs;
using EFCore_Day_5_Full_CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore_Day_5_Full_CRUD.Controller
{
    [ApiController]
    [Controller]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly AppDBContext _dbContext;
        public ProjectController(AppDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        [HttpPost]
        public IActionResult CreateProject(ProjectDTO project)
        {
            try
            {
                var newProject = new Project
                {
                    ProjectName = project.ProjectName,
                    StartDate = project.StartDate
                };

                _dbContext.Projects.Add(newProject);
                _dbContext.SaveChanges();

                return Ok("Project Added Successfully..");
            }
            catch(Exception ex)
            {
                return BadRequest($"Exception: {ex}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetProjectById(int id)
        {
            try
            {
                var fetchData = _dbContext.Projects.Include(ep => ep.EmployeeProjects)
                                                   .ThenInclude(e => e.Employee)
                                                   .Where(c => c.ProjectId == id)
                                                   .Select(s => new
                                                   {
                                                       ProjectName = s.ProjectName,
                                                       StartDate = s.StartDate,
                                                       EmployeeList = s.EmployeeProjects.Select(se => new
                                                       {
                                                           Id = se.Employee.EmployeeId,
                                                           Name = se.Employee.Name,
                                                           Email = se.Employee.Email
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
        public async Task<IActionResult> UpdateProjectById(int id, [FromBody] ProjectDTO project)
        {
            try
            {
                var updateData = _dbContext.Projects.FirstOrDefault(e => e.ProjectId == id);

                updateData.ProjectName = project.ProjectName;
                updateData.StartDate = project.StartDate;

                _dbContext.Projects.Update(updateData);

                await _dbContext.SaveChangesAsync();

                return Ok($"Employee with Id: {id} is updated...");
            }
            catch (Exception ex)
            {
                return BadRequest($"Exception: {ex}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectById(int id)
        {
            try
            {
                var updateData = _dbContext.Projects.FirstOrDefault(e => e.ProjectId == id);

                _dbContext.Projects.Remove(updateData);

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