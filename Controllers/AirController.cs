using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirController : ControllerBase
    {

        protected IConfiguration Configuration;

        public AirController(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        [HttpGet]
        [Route("Echo")]
        public string Echo()
        {
            return $"Está funcionando!";
        }

    }
}
