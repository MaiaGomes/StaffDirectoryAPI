using Microsoft.AspNetCore.Mvc;

namespace StaffDirectoryAPI.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcome to the Staff Directory API! The API is running.");
        }
    }
}
