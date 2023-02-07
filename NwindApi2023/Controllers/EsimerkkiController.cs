using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NwindApi2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EsimerkkiController : ControllerBase
    {

        [HttpGet]
        public string TestiMetodi()
        {
            return("Yhteys luotu onnistuneesti");
        }


    }
}
