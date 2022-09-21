using Microsoft.AspNetCore.Mvc;

namespace ApiConsumer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestConsumerController : ControllerBase
    {
        public TestConsumerController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}