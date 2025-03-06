using EfCoreDIExample.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace EfCoreDIExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
            Console.WriteLine($"StudentController - DbContext Instance ID: {_context.GetInstanceId()}");
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            Console.WriteLine($"Handling GET request - DbContext Instance ID: {_context.GetInstanceId()}");
            var students = await _context.Students.ToListAsync();
            return Ok(students);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Customer cust)
        {
            Console.WriteLine($"Handling POST request - DbContext Instance ID: {_context.GetInstanceId()}");
            _context.Students.Add(cust);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStudents), new { id = cust.Id }, cust);
        }
    }
}
