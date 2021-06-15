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
using MGL_API.Model.Entity.GameDetail;
//Basicamente copiei e colei do UsuarioController e alterei o que achei necessario pro jogo
namespace MGL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        protected IConfiguration Configuration;

        public GameController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        [HttpPost]
        [Route("CadastroGame")]
        public ActionResult<RetornoCadastroGame> CadastraGame(EntradaCadastroGame entrada)
        {
            string msg = "";
            #region Validar Entradas

            if (string.IsNullOrEmpty(entrada.Nome))
            {
                msg = "A variável Nome é obrigatória!";
            }

            if (string.IsNullOrEmpty(entrada.Descricao))
            {
                msg = "A variável Descrição é obrigatória!";
            }

            if (string.IsNullOrEmpty(entrada.Categoria))
            {
                msg = "A variável Categoria é obrigatória!";
            }

            if (string.IsNullOrEmpty(entrada.Lancamento))
            {
                msg = "A variável Data de lançamento é obrigatória!";
            }

            if (string.IsNullOrEmpty(entrada.Requisitos))
            {
                msg = "A variável Requisitos é obrigatória!";
            }

            if (string.IsNullOrEmpty(entrada.Desenvolvedora))
            {
                msg = "A variável Desenvolvedora é obrigatória!";
            }

            if (string.IsNullOrEmpty(entrada.Publicadora))
            {
                msg = "A variável Publicadora é obrigatória!";
            }

            if (string.IsNullOrEmpty(entrada.Plataformas))
            {
                msg = "A variável Plataformas é obrigatória!";
            }

            if (string.IsNullOrEmpty(entrada.Classificacao))
            {
                msg = "A variável Classificação é obrigatória!";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = msg };
            }

            #endregion


            RetornoCadastroGame retorno = new RetornoCadastroGame();
            //A parte que em teoria cadastra o jogo no banco de dados, o código do db.CadastrarGame fica no UtilitarioDB
            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {
                    retorno = db.CadastrarGame(entrada);

                    retorno.Mensagem = "Game cadastrado com sucesso!";
                    retorno.Sucesso = true;
                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao cadastrar Game." };
            }
        }


        [HttpPost]
        [Route("ObterListaGame")]
        public ActionResult<List<RetornoListaObterGame>> ObterListaGame()
        {

            List<RetornoListaObterGame> retorno = new List<RetornoListaObterGame>();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    List<ObterGameEntity> lista = db.ObterListaGame();


                    foreach (ObterGameEntity item in lista)
                    {
                        retorno.Add(
                          new RetornoListaObterGame()
                          {
                              CodigoGame = item.IdGame,
                              NomeGame = item.Nome_Game,
                              Sucesso = true,
                              Mensagem = "Lista de Games recuperado com sucesso."
                          }
                            );
                    }
                    return retorno;
                }

            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao obter lista de armazenamento." };
            }

        }


        [HttpPost]
        [Route("ObterGame")]
        public ActionResult<RetornoObterGame> ObterGame([FromForm] string CodigoGame)
        {

            #region Valida entrada


            if (CodigoGame == null)
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = "Necessário inserir o parâmetro CodigoGame." };
            }

            #endregion

            RetornoObterGame retorno = new RetornoObterGame();
            try
            {

                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {


                    ObterGameEntity lista = db.ObterGame(CodigoGame);

                    retorno.CodigoGame = lista.IdGame;
                    retorno.NomeGame = lista.Nome_Game;
                    retorno.DescricaoGame = lista.Descricao_Game;
                    retorno.IdCategoriaGame = lista.IdCategoria_Game;
                    retorno.SrcImagemGame = lista.SRC_Imagem_Game;
                    retorno.Mensagem = "Sucesso ao recuperar Game";
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
