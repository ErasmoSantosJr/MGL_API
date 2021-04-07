using MGL_API.Model.Entrada;
using MGL_API.Model.Saida;
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
    public class UsuarioController : ControllerBase
    {

        protected IConfiguration Configuration;

        public UsuarioController(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        [HttpGet]
        [Route("Cadastro")]
        public RetornoCadastroUsuario CadastraUsuario (EntradaCadastroUsuario entrada)
        {
            RetornoCadastroUsuario retorno = new RetornoCadastroUsuario();
            return retorno;
        }

    }
}
