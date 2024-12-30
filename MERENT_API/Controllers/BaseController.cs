using Microsoft.AspNetCore.Mvc;

namespace MERENT_API.Controllers
{
    //cac controller khac sẽ thừa kế basecontroller
    [Route("api/[controller]/")]
    [ApiController]
    //fix some thing
    public class BaseController : ControllerBase
    {
    }
}
