using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("Home/[action]")]
public class HomeController : ControllerBase
{
    [HttpGet("Error")]
    public IActionResult Error()
    {
        // Retrieve and log detailed error information
        return Problem("An error occurred while processing your request.");
    }
}
    