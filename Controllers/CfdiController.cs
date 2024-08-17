using Microsoft.AspNetCore.Mvc;

namespace XMLCfdiGenerator.Controllers
{
    [ApiController]
    [Produces("Application/XML")]
    public class CfdiController : Controller
    {
        [HttpGet]
        [Route("Example")]
        public async Task<string> Index()
        {
            return "";
        }
    }
}
