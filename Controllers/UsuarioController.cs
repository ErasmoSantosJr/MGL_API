using MGL_API.db;
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
            Configuration = configuration;
        }

        [HttpGet]
        [Route("Cadastro")]
        public RetornoCadastroUsuario CadastraUsuario (EntradaCadastroUsuario entrada)
        {

            #region Validar Entradas

            #endregion

            RetornoCadastroUsuario retorno = new RetornoCadastroUsuario();

            using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
            {
                RetornoCadastroUsuario cadastro = db.CadastrarUsuario(entrada);
            }

                return retorno;
        }

    }
}
