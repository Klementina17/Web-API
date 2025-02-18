using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BasicWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new { message = "Welcome to the API" });
        }

        [HttpGet("error")]
        public IActionResult Error()
        {
            return Problem(detail: "An error occurred.", statusCode: 500);
        }
    }
}
