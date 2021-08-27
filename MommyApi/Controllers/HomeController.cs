namespace MommyApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : ApiController
    {


        [HttpGet]
        public ActionResult Index()
        {
            return Ok();
        }

    
    }
}
