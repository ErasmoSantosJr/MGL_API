using MGL_API.db;
using MGL_API.Model.Entity.Usuario;
using MGL_API.Model.Entrada;
using MGL_API.Model.Saida;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
//Controle Usuario
//Controle usuario2
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

        [HttpPost]
        [Route("Cadastro")]
        public ActionResult<RetornoCadastroUsuario> CadastraUsuario(EntradaCadastroUsuario entrada)
        {
            string msg = "";
            #region Validar Entradas

            if (string.IsNullOrEmpty(entrada.Nome))
            {
                msg = "A variável Nome é obrigatório!";
            }

            if (string.IsNullOrEmpty(entrada.Email))
            {
                msg = "A variável Email é obrigatório!";
            }

            if (string.IsNullOrEmpty(entrada.Login))
            {
                msg = "A variável Login é obrigatório!";
            }

            if (string.IsNullOrEmpty(entrada.Password))
            {
                msg = "A variável Password é obrigatório!";
            }

            if (string.IsNullOrEmpty(entrada.DataNascimento))
            {
                msg = "A variável DataNascimento é obrigatório!";
            }

            if (!string.IsNullOrEmpty(msg))
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = msg };
            }
            #endregion

            RetornoCadastroUsuario retorno = new RetornoCadastroUsuario();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {
                    retorno = db.CadastrarUsuario(entrada);

                    retorno.Mensagem = "Usuário cadastrado com sucesso!";
                    retorno.Sucesso = true;
                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao cadastrar Usuário."};
            }
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult<RetornoLoginUsuario> LoginUsuario(EntradaLoginUsuario entrada)
        {
            string msg = "";
            #region Validar Entradas

            if (string.IsNullOrEmpty(entrada.Email) && string.IsNullOrEmpty(entrada.Login))
            {
                msg = "A variável Email ou Login são obrigatório!";
            }

            if (string.IsNullOrEmpty(entrada.Password))
            {
                msg = "A variável Password é obrigatório!";
            }

            if (!string.IsNullOrEmpty(msg))
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = msg };
            }
            #endregion

            UsuarioEntity login = new UsuarioEntity();
            RetornoLoginUsuario retorno = new RetornoLoginUsuario();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {
                    login = db.LogarUsuario(entrada);

                    retorno.Login = login.Login_Usuario;
                    retorno.Sucesso = true;
                    retorno.Mensagem = "Usuário autenticado com sucesso!";
                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao autenticar Usuário." };
            }
        }

    }
}
