using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MGL_API.db;
using MGL_API.Model.Entity.Usuario;
using MGL_API.Model.Entity.Game;
using MGL_API.Model.Entrada;
using MGL_API.Model.Saida;
using MGL_API.Model.Saida.Game;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using System.Net;
//Basicamente copiei e colei do UsuarioController e alterei o que achei necessario pro jogo
namespace MGL_API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
        {
        protected IConfiguration Configuration;

        public GameController(IConfiguration configuration) {
            Configuration = configuration;
        }
        [HttpPost]
        [Route("CadastroGame")]
        public ActionResult<RetornoCadastroGame> CadastraGame(EntradaCadastroGame entrada) 
            {
            string msg = "";
            #region Validar Entradas

            if (string.IsNullOrEmpty(entrada.Nome)) {
                msg = "A variável Nome é obrigatório!";
            }

            if (string.IsNullOrEmpty(entrada.Descricao)) {
                msg = "A variável Descrição é obrigatório!";
            }

            if (string.IsNullOrEmpty(entrada.Categoria)) {
                msg = "A variável Categoria é obrigatório!";
            }

            if (!string.IsNullOrEmpty(msg)) {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = msg };
            }
            #endregion

            RetornoCadastroGame retorno = new RetornoCadastroGame();
            //A parte que em teoria cadastra o jogo no banco de dados, o código do db.CadastrarGame fica no UtilitarioDB
            try {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection"))) {
                    retorno = db.CadastrarGame(entrada);

                    retorno.Mensagem = "Game cadastrado com sucesso!";
                    retorno.Sucesso = true;
                }

                return retorno;
            }
            catch {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao cadastrar Game." };
            }
        }


        [HttpPost]
        [Route("ExibirGame")]

        public ActionResult<RetornoExibirGame> ExibirGameId(EntradaExibirGame entrada) 
        {
            RetornoExibirGame retorno = new RetornoExibirGame();
            try 
            {

                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection"))) {
                    retorno = db.ExibirGame(entrada);                    
                    retorno.Sucesso = true;

                }

                return retorno;
            }
            catch
           {
              return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao exibir Game." };
            }
        }

    }
}
