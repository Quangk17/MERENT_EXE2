using Microsoft.AspNetCore.Mvc;

namespace MERENT_API.Controllers
{
    //cac controller khac sẽ thừa kế basecontroller
    [Route("api/[controller]/")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}
