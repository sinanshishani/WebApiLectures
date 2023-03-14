using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicsOfWebApi.Controllers
{
    [Authorize]
    public class AnotherController : ControllerBase
    {
    }
}
