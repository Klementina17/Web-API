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
            _logger.LogInformation("Index endpoint was hit.");
            return Ok(new { message = "Welcome to the API" });
        }

        [HttpGet("error")]
        public IActionResult Error()
        {
            _logger.LogError("An error occurred in the Error endpoint.");
            return Problem(detail: "An error occurred.", statusCode: 500);
        }
    }
}
