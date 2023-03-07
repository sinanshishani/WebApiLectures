using Microsoft.AspNetCore.Mvc;

namespace BasicsOfWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebApiLectureController : ControllerBase
    {
        private readonly List<OrangeJuice> _orangeJuices;

        private readonly ILogger<WebApiLectureController> _logger;

        private readonly IHttpContextAccessor _contextAccessor;

        public WebApiLectureController(ILogger<WebApiLectureController> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _contextAccessor = httpContextAccessor;
            _orangeJuices = new List<OrangeJuice>
            {
                new OrangeJuice {Name = "ahmad", Description = "desc 1", Quantity = 2},
                new OrangeJuice {Name = "bahaa", Description = "desc 2", Quantity = 4},
                new OrangeJuice {Name = "sameer", Description = "desc 3", Quantity = 26},
                new OrangeJuice {Name = "abdullah", Description = "desc 4", Quantity = 54},
                new OrangeJuice {Name = "laith", Description = "desc 5", Quantity = 7}
            };
        }

        [HttpGet]
        public JsonResult Get(string name, int quantity)
        {
            var httpcontext = _contextAccessor.HttpContext;

            var httpRequest = httpcontext?.Request;

            var httpResponse = httpcontext?.Response;

            return new JsonResult(_orangeJuices.Where(a => a.Name.Contains(name) && a.Quantity > quantity).ToList());
        }

        [HttpPost]
        public string Post([FromBody] OrangeJuice inputDto)
        {
            return inputDto.Name;
        }


    }


    public class OrangeJuice
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}