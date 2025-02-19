using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Day2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello World");
        }
    }
}
