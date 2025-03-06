using EFCoreWithoutDIApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreWebApiWithoutDI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Constructor
        public EmployeesController()
        {
            _context = new AppDbContext(); // No DI, direct instantiation
        }

        // GET: api/products
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            return _context.Employees.ToList();
        }

        // POST: api/products
        [HttpPost]
        public ActionResult<Employee> CreateEmployee(Employee emp)
        {
            _context.Employees.Add(emp);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetEmployees), new { id = emp.Id }, emp);
        }
    }
}
