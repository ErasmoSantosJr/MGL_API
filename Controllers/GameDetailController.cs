using MGL_API.db;
using MGL_API.Model.Entity.GameDetail;
using MGL_API.Model.Entrada.Game;
using MGL_API.Model.Entrada.GameDetail;
using MGL_API.Model.Saida.GameDetail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MGL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameDetailController : ControllerBase
    {
        protected IConfiguration Configuration;

        public GameDetailController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #region Armazenamento

        [HttpPost]
        [Route("CadastrarArmazenamento")]
        public ActionResult<RetornoArmazenamento> CadastraArmazenamento(EntradaArmazenamento entrada)
        {
            string msg = "";
            #region Validar Entradas

            if (string.IsNullOrEmpty(entrada.NomeArmazenamento))
            {
                msg = "A variável NomeArmazenamento é obrigatório!";
            }

            if (!string.IsNullOrEmpty(msg))
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = msg };
            }
            #endregion

            RetornoArmazenamento retorno = new RetornoArmazenamento();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {
                    retorno = db.CadastrarArmazenamento(entrada);

                    retorno.Mensagem = "Armazenamento cadastrado com sucesso!";
                    retorno.Sucesso = true;
                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao cadastrar Armazenamento." };
            }
        }

        [HttpPost]
        [Route("DeletarArmazenamento")]
        public ActionResult<RetornoExcluir> DeletaArmazenamento(EntradaArmazenamento entrada)
        {
            string msg = "";
            #region Validar Entradas

            if (entrada.CodArmazenamento.Equals(null) || entrada.CodArmazenamento < 0)
            {
                msg = "A variável CodArmazenamento é obrigatório!";
            }

            if (!string.IsNullOrEmpty(msg))
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = msg };
            }
            #endregion

            RetornoExcluir retorno = new RetornoExcluir();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    bool valida = db.DeletarArmazenamento(entrada.CodArmazenamento);

                    if (valida == true)
                    {
                        retorno.Codigo = entrada.CodArmazenamento;
                        retorno.Mensagem = "Armazenamento com o codigo " + entrada.CodArmazenamento + " deletado com sucesso!";
                        retorno.Sucesso = true;
                    }
                    else
                    {
                        retorno.Codigo = entrada.CodArmazenamento;
                        retorno.Mensagem = "Erro ao deletar Armazenamento com o codigo " + entrada.CodArmazenamento;
                        retorno.Sucesso = false;
                    }
                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao deletar Armazenamento." };
            }
        }

        [HttpGet]
        [Route("ObterListaArmazenamento")]
        public ActionResult<List<RetornoObterArmazenamento>> ObterListaArmazenamento()
        {

            List<RetornoObterArmazenamento> retorno = new List<RetornoObterArmazenamento>();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    List<ObterArmazenamentoEntity> lista = db.ObterListaArmazenamento();


                    foreach (ObterArmazenamentoEntity item in lista)
                    {
                        retorno.Add(
                          new RetornoObterArmazenamento()
                          {
                              CodigoArmazenamento = item.CodArmazenamento,
                              NomeArmazenamento = item.NomeArmazenamento,
                              Sucesso = true,
                              Mensagem = "Lista de armazenamento recuperado com sucesso."
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
        [Route("ObterArmazenamentoGame")]
        public ActionResult<RetornoObterArmazenamento> ObterArmazenamento([FromForm] string CodigoGame)
        {

            #region Validar Entradas

            if (CodigoGame == null)
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = "Parâmetro CodigoGame incorreto." };
            }

            #endregion

            RetornoObterArmazenamento retorno = new RetornoObterArmazenamento();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    ObterArmazenamentoEntity lista = db.ObterArmazenamento(CodigoGame);


                    retorno.CodigoArmazenamento = lista.CodArmazenamento;
                    retorno.NomeArmazenamento = lista.NomeArmazenamento;
                    retorno.Sucesso = true;
                    retorno.Mensagem = "Lista de armazenamento recuperado com sucesso.";

                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao obter lista de armazenamento." };
            }

        }

        #endregion

        #region Classificacao

        [HttpPost]
        [Route("CadastrarClassificacao")]
        public ActionResult<RetornoClassificacao> CadastraClassificacao(EntradaClassificacao entrada)
        {
            string msg = "";
            #region Validar Entradas

            if (string.IsNullOrEmpty(entrada.NomeClassificacao))
            {
                msg = "A variável NomeClassificacao é obrigatório!";
            }

            if (!string.IsNullOrEmpty(msg))
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = msg };
            }
            #endregion

            RetornoClassificacao retorno = new RetornoClassificacao();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {
                    retorno = db.CadastrarClassificacao(entrada);

                    retorno.Mensagem = "Classificação indicativa cadastrado com sucesso!";
                    retorno.Sucesso = true;
                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao cadastrar Classificação indicativa." };
            }
        }

        [HttpPost]
        [Route("DeletarClassificacao")]
        public ActionResult<RetornoExcluir> DeletaClassificacao(EntradaClassificacao entrada)
        {
            string msg = "";
            #region Validar Entradas

            if (entrada.CodClassificacao.Equals(null) || entrada.CodClassificacao < 0)
            {
                msg = "A variável CodClassificacao é obrigatório!";
            }

            if (!string.IsNullOrEmpty(msg))
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = msg };
            }
            #endregion

            RetornoExcluir retorno = new RetornoExcluir();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    bool valida = db.DeletarClassificacao(entrada.CodClassificacao);

                    if (valida == true)
                    {
                        retorno.Codigo = entrada.CodClassificacao;
                        retorno.Mensagem = "Classificação com o codigo " + entrada.CodClassificacao + " deletado com sucesso!";
                        retorno.Sucesso = true;
                    }
                    else
                    {
                        retorno.Codigo = entrada.CodClassificacao;
                        retorno.Mensagem = "Erro ao deletar Classificação com o codigo " + entrada.CodClassificacao;
                        retorno.Sucesso = false;
                    }
                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao deletar Armazenamento." };
            }
        }

        [HttpGet]
        [Route("ObterListaClassificacao")]
        public ActionResult<List<RetornoObterClassificacao>> ObterListaClassificacao()
        {

            List<RetornoObterClassificacao> retorno = new List<RetornoObterClassificacao>();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    List<ObterClassificacaoEntity> lista = db.ObterListaClassificacao();


                    foreach (ObterClassificacaoEntity item in lista)
                    {
                        retorno.Add(
                          new RetornoObterClassificacao()
                          {
                              CodigoClassificacao = item.CodClassificacao,
                              NomeClassificacao = item.NomeClassificacao,
                              Sucesso = true,
                              Mensagem = "Lista de classificação recuperada com sucesso."
                          }
                            );
                    }
                    return retorno;
                }

            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao obter lista de classificação." };
            }

        }

        [HttpPost]
        [Route("ObterClassificacaoGame")]
        public ActionResult<RetornoObterClassificacao> ObterClassificacao([FromForm] string CodigoGame)
        {

            #region Validar Entradas

            if (CodigoGame == null)
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = "Parâmetro CodigoGame incorreto." };
            }

            #endregion

            RetornoObterClassificacao retorno = new RetornoObterClassificacao();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    ObterClassificacaoEntity lista = db.ObterClassificacao(CodigoGame);


                    retorno.CodigoClassificacao = lista.CodClassificacao;
                    retorno.NomeClassificacao = lista.NomeClassificacao;
                    retorno.Sucesso = true;
                    retorno.Mensagem = "Lista de classificação recuperado com sucesso.";

                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao obter lista de classificacao." };
            }

        }
        #endregion

        #region Memoria

        [HttpPost]
        [Route("CadastrarMemoria")]
        public ActionResult<RetornoMemoria> CadastraMemoria(EntradaMemoria entrada)
        {
            string msg = "";
            #region Validar Entradas

            if (string.IsNullOrEmpty(entrada.NomeMemoria))
            {
                msg = "A variável NomeMemoria é obrigatório!";
            }

            if (!string.IsNullOrEmpty(msg))
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = msg };
            }
            #endregion

            RetornoMemoria retorno = new RetornoMemoria();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {
                    retorno = db.CadastrarMemoria(entrada);

                    retorno.Mensagem = "Memoria cadastrado com sucesso!";
                    retorno.Sucesso = true;
                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao cadastrar Memoria." };
            }
        }

        [HttpPost]
        [Route("DeletarMemoria")]
        public ActionResult<RetornoExcluir> DeletaMemoria(EntradaMemoria entrada)
        {
            string msg = "";
            #region Validar Entradas

            if (entrada.CodMemoria.Equals(null) || entrada.CodMemoria < 0)
            {
                msg = "A variável CodMemoria é obrigatório!";
            }

            if (!string.IsNullOrEmpty(msg))
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = msg };
            }
            #endregion

            RetornoExcluir retorno = new RetornoExcluir();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    bool valida = db.DeletarMemoria(entrada.CodMemoria);

                    if (valida == true)
                    {
                        retorno.Codigo = entrada.CodMemoria;
                        retorno.Mensagem = "Memória com o codigo " + entrada.CodMemoria + " deletado com sucesso!";
                        retorno.Sucesso = true;
                    }
                    else
                    {
                        retorno.Codigo = entrada.CodMemoria;
                        retorno.Mensagem = "Erro ao deletar Memória com o codigo " + entrada.CodMemoria;
                        retorno.Sucesso = false;
                    }
                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao deletar Armazenamento." };
            }
        }

        [HttpGet]
        [Route("ObterListaMemoria")]
        public ActionResult<List<RetornoObterMemoria>> ObterListaMemoria()
        {

            List<RetornoObterMemoria> retorno = new List<RetornoObterMemoria>();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    List<ObterMemoriaEntity> lista = db.ObterListaMemoria();


                    foreach (ObterMemoriaEntity item in lista)
                    {
                        retorno.Add(
                          new RetornoObterMemoria()
                          {
                              CodigoMemoria = item.CodMemoria,
                              NomeMemoria = item.NomeMemoria,
                              Sucesso = true,
                              Mensagem = "Lista de memoria recuperada com sucesso."
                          }
                            );
                    }
                    return retorno;
                }

            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao obter lista de memoria." };
            }

        }


        [HttpPost]
        [Route("ObterMemoriaGame")]
        public ActionResult<RetornoObterMemoria> ObterMemoria([FromForm] string CodigoGame)
        {

            #region Validar Entradas

            if (CodigoGame == null)
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = "Parâmetro CodigoGame incorreto." };
            }

            #endregion

            RetornoObterMemoria retorno = new RetornoObterMemoria();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    ObterMemoriaEntity lista = db.ObterMemoria(CodigoGame);


                    retorno.CodigoMemoria = lista.CodMemoria;
                    retorno.NomeMemoria = lista.NomeMemoria;
                    retorno.Sucesso = true;
                    retorno.Mensagem = "Lista de memoria recuperado com sucesso.";

                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao obter lista de memoria." };
            }

        }



        #endregion

        #region PlacaVideo

        [HttpPost]
        [Route("CadastrarPlacaVideo")]
        public ActionResult<RetornoPlacaVideo> CadastraPlacaVideo(EntradaPlacaVideo entrada)
        {
            string msg = "";
            #region Validar Entradas

            if (string.IsNullOrEmpty(entrada.NomePlacaVideo))
            {
                msg = "A variável NomePlacaVideo é obrigatório!";
            }

            if (!string.IsNullOrEmpty(msg))
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = msg };
            }
            #endregion

            RetornoPlacaVideo retorno = new RetornoPlacaVideo();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {
                    retorno = db.CadastrarPlacaVideo(entrada);

                    retorno.Mensagem = "Placa Video cadastrado com sucesso!";
                    retorno.Sucesso = true;
                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao cadastrar Memória." };
            }
        }

        [HttpPost]
        [Route("DeletarPlacaVideo")]
        public ActionResult<RetornoExcluir> DeletaPlacaVideo(EntradaPlacaVideo entrada)
        {
            string msg = "";
            #region Validar Entradas

            if (entrada.CodPlacaVideo.Equals(null) || entrada.CodPlacaVideo < 0)
            {
                msg = "A variável CodPlacaVideo é obrigatório!";
            }

            if (!string.IsNullOrEmpty(msg))
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = msg };
            }
            #endregion

            RetornoExcluir retorno = new RetornoExcluir();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    bool valida = db.DeletarPlacaVideo(entrada.CodPlacaVideo);

                    if (valida == true)
                    {
                        retorno.Codigo = entrada.CodPlacaVideo;
                        retorno.Mensagem = "PlacaVideo com o codigo " + entrada.CodPlacaVideo + " deletado com sucesso!";
                        retorno.Sucesso = true;
                    }
                    else
                    {
                        retorno.Codigo = entrada.CodPlacaVideo;
                        retorno.Mensagem = "Erro ao deletar PlacaVideo com o codigo " + entrada.CodPlacaVideo;
                        retorno.Sucesso = false;
                    }
                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao deletar PlacaVideo." };
            }
        }

        [HttpGet]
        [Route("ObterListaPlacaVideo")]
        public ActionResult<List<RetornoObterPlacaVideo>> ObterListaPlacaVideo()
        {

            List<RetornoObterPlacaVideo> retorno = new List<RetornoObterPlacaVideo>();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    List<ObterPlacaVideoEntity> lista = db.ObterListaPlacaVideo();


                    foreach (ObterPlacaVideoEntity item in lista)
                    {
                        retorno.Add(
                          new RetornoObterPlacaVideo()
                          {
                              CodigoPlacaVideo = item.CodPlacaVideo,
                              NomePlacaVideo = item.NomePlacaVideo,
                              Sucesso = true,
                              Mensagem = "Lista de PlacaVideo recuperado com sucesso."
                          }
                            );
                    }
                    return retorno;
                }

            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao obter lista de PlacaVideo." };
            }

        }


        [HttpPost]
        [Route("ObterPlacaVideoGame")]
        public ActionResult<RetornoObterPlacaVideo> ObterPlacaVideo([FromForm] string CodigoGame)
        {

            #region Validar Entradas

            if (CodigoGame == null)
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = "Parâmetro CodigoGame incorreto." };
            }

            #endregion

            RetornoObterPlacaVideo retorno = new RetornoObterPlacaVideo();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    ObterPlacaVideoEntity lista = db.ObterPlacaVideo(CodigoGame);


                    retorno.CodigoPlacaVideo = lista.CodPlacaVideo;
                    retorno.NomePlacaVideo = lista.NomePlacaVideo;
                    retorno.Sucesso = true;
                    retorno.Mensagem = "Lista de PLacaVideo recuperado com sucesso.";

                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao obter lista de armazenamento." };
            }

        }

        #endregion

        #region Plataforma

        [HttpPost]
        [Route("CadastrarPlataforma")]
        public ActionResult<RetornoPlataforma> CadastraPlataforma(EntradaPlataforma entrada)
        {
            string msg = "";
            #region Validar Entradas

            if (string.IsNullOrEmpty(entrada.NomePlataforma))
            {
                msg = "A variável NomePlataforma é obrigatório!";
            }

            if (!string.IsNullOrEmpty(msg))
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = msg };
            }
            #endregion

            RetornoPlataforma retorno = new RetornoPlataforma();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {
                    retorno = db.CadastrarPlataforma(entrada);

                    retorno.Mensagem = "Plataforma cadastrado com sucesso!";
                    retorno.Sucesso = true;
                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao cadastrar Plataforma." };
            }
        }

        [HttpPost]
        [Route("DeletarPlataforma")]
        public ActionResult<RetornoExcluir> DeletaPlataforma(EntradaPlataforma entrada)
        {
            string msg = "";
            #region Validar Entradas

            if (entrada.CodPlataforma.Equals(null) || entrada.CodPlataforma < 0)
            {
                msg = "A variável CodPlacaVideo é obrigatório!";
            }

            if (!string.IsNullOrEmpty(msg))
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = msg };
            }
            #endregion

            RetornoExcluir retorno = new RetornoExcluir();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    bool valida = db.DeletarPlataforma(entrada.CodPlataforma);

                    if (valida == true)
                    {
                        retorno.Codigo = entrada.CodPlataforma;
                        retorno.Mensagem = "Plataforma com o codigo " + entrada.CodPlataforma + " deletado com sucesso!";
                        retorno.Sucesso = true;
                    }
                    else
                    {
                        retorno.Codigo = entrada.CodPlataforma;
                        retorno.Mensagem = "Erro ao deletar Plataforma com o codigo " + entrada.CodPlataforma;
                        retorno.Sucesso = false;
                    }
                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao deletar Plataforma." };
            }
        }

        [HttpGet]
        [Route("ObterListaPlataforma")]
        public ActionResult<List<RetornoObterPlataforma>> ObterListaPlataforma()
        {

            List<RetornoObterPlataforma> retorno = new List<RetornoObterPlataforma>();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    List<ObterPlataformaEntity> lista = db.ObterListaPlataforma();


                    foreach (ObterPlataformaEntity item in lista)
                    {
                        retorno.Add(
                          new RetornoObterPlataforma()
                          {
                              CodigoPlataforma = item.CodPlataforma,
                              NomePlataforma = item.NomePlataforma,
                              Sucesso = true,
                              Mensagem = "Lista de plataforma recuperado com sucesso."
                          }
                            );
                    }
                    return retorno;
                }

            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao obter lista de plataforma." };
            }

        }

        [HttpPost]
        [Route("ObterPlataformaGame")]
        public ActionResult<RetornoObterPlataforma> ObterPlataforma([FromForm] string CodigoGame)
        {

            #region Validar Entradas

            if (CodigoGame == null)
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = "Parâmetro CodigoGame incorreto." };
            }

            #endregion

            RetornoObterPlataforma retorno = new RetornoObterPlataforma();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    ObterPlataformaEntity lista = db.ObterPlataforma(CodigoGame);


                    retorno.CodigoPlataforma = lista.CodPlataforma;
                    retorno.NomePlataforma = lista.NomePlataforma;
                    retorno.Sucesso = true;
                    retorno.Mensagem = "Lista de plataforma recuperado com sucesso.";

                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao obter lista de armazenamento." };
            }

        }

        #endregion

        #region Processador

        [HttpPost]
        [Route("CadastrarProcessador")]
        public ActionResult<RetornoProcessador> CadastraProcessador(EntradaProcessador entrada)
        {
            string msg = "";
            #region Validar Entradas

            if (string.IsNullOrEmpty(entrada.NomeProcessador))
            {
                msg = "A variável NomeProcessador é obrigatório!";
            }

            if (!string.IsNullOrEmpty(msg))
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = msg };
            }
            #endregion

            RetornoProcessador retorno = new RetornoProcessador();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {
                    retorno = db.CadastrarProcessador(entrada);

                    retorno.Mensagem = "Processador cadastrado com sucesso!";
                    retorno.Sucesso = true;
                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao cadastrar Processador." };
            }
        }

        [HttpPost]
        [Route("DeletarProcessador")]
        public ActionResult<RetornoExcluir> DeletaProcessador(EntradaProcessador entrada)
        {
            string msg = "";
            #region Validar Entradas

            if (entrada.CodProcessador.Equals(null) || entrada.CodProcessador < 0)
            {
                msg = "A variável CodProcessador é obrigatório!";
            }

            if (!string.IsNullOrEmpty(msg))
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = msg };
            }
            #endregion

            RetornoExcluir retorno = new RetornoExcluir();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    bool valida = db.DeletarProcessador(entrada.CodProcessador);

                    if (valida == true)
                    {
                        retorno.Codigo = entrada.CodProcessador;
                        retorno.Mensagem = "Processador com o codigo " + entrada.CodProcessador + " deletado com sucesso!";
                        retorno.Sucesso = true;
                    }
                    else
                    {
                        retorno.Codigo = entrada.CodProcessador;
                        retorno.Mensagem = "Erro ao deletar Processador com o codigo " + entrada.CodProcessador;
                        retorno.Sucesso = false;
                    }
                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao deletar Processador." };
            }
        }

        [HttpGet]
        [Route("ObterListaProcessador")]
        public ActionResult<List<RetornoObterProcessador>> ObterListaProcessador()
        {

            List<RetornoObterProcessador> retorno = new List<RetornoObterProcessador>();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    List<ObterProcessadorEntity> lista = db.ObterListaProcessador();


                    foreach (ObterProcessadorEntity item in lista)
                    {
                        retorno.Add(
                          new RetornoObterProcessador()
                          {
                              CodigoProcessador = item.CodProcessador,
                              NomeProcessador = item.NomeProcessador,
                              Sucesso = true,
                              Mensagem = "Lista de processador recuperado com sucesso."
                          }
                            );
                    }
                    return retorno;
                }

            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao obter lista de processador." };
            }

        }

        [HttpPost]
        [Route("ObterProcessadorGame")]
        public ActionResult<RetornoObterProcessador> ObterProcessador([FromForm] string CodigoGame)
        {

            #region Validar Entradas

            if (CodigoGame == null)
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = "Parâmetro CodigoGame incorreto." };
            }

            #endregion

            RetornoObterProcessador retorno = new RetornoObterProcessador();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    ObterProcessadorEntity lista = db.ObterProcessador(CodigoGame);


                    retorno.CodigoProcessador = lista.CodProcessador;
                    retorno.NomeProcessador = lista.NomeProcessador;
                    retorno.Sucesso = true;
                    retorno.Mensagem = "Lista de processador recuperado com sucesso.";

                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao obter lista de processador." };
            }

        }

        #endregion

        #region SO

        [HttpPost]
        [Route("CadastrarSO")]
        public ActionResult<RetornoSO> CadastraSO(EntradaSO entrada)
        {
            string msg = "";
            #region Validar Entradas

            if (string.IsNullOrEmpty(entrada.NomeSO))
            {
                msg = "A variável NomeSO é obrigatório!";
            }

            if (!string.IsNullOrEmpty(msg))
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = msg };
            }
            #endregion

            RetornoSO retorno = new RetornoSO();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {
                    retorno = db.CadastrarSO(entrada);

                    retorno.Mensagem = "Sistema Operacional cadastrado com sucesso!";
                    retorno.Sucesso = true;
                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao cadastrar Sistema Operacional." };
            }
        }

        [HttpPost]
        [Route("DeletarSO")]
        public ActionResult<RetornoExcluir> DeletaSO(EntradaSO entrada)
        {
            string msg = "";
            #region Validar Entradas

            if (entrada.CodSO.Equals(null) || entrada.CodSO < 0)
            {
                msg = "A variável CodSO é obrigatório!";
            }

            if (!string.IsNullOrEmpty(msg))
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = msg };
            }
            #endregion

            RetornoExcluir retorno = new RetornoExcluir();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    bool valida = db.DeletarSO(entrada.CodSO);

                    if (valida == true)
                    {
                        retorno.Codigo = entrada.CodSO;
                        retorno.Mensagem = "Sistema Operacional com o codigo " + entrada.CodSO + " deletado com sucesso!";
                        retorno.Sucesso = true;
                    }
                    else
                    {
                        retorno.Codigo = entrada.CodSO;
                        retorno.Mensagem = "Erro ao deletar Sistema Operacional com o codigo " + entrada.CodSO;
                        retorno.Sucesso = false;
                    }
                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao deletar Sistema Operacional." };
            }
        }

        [HttpGet]
        [Route("ObterListaSO")]
        public ActionResult<List<RetornoObterSO>> ObterListaSO()
        {

            List<RetornoObterSO> retorno = new List<RetornoObterSO>();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    List<ObterSOEntity> lista = db.ObterListaSO();


                    foreach (ObterSOEntity item in lista)
                    {
                        retorno.Add(
                          new RetornoObterSO()
                          {
                              CodigoSO = item.CodSO,
                              NomeSO = item.NomeSO,
                              Sucesso = true,
                              Mensagem = "Lista de SO recuperado com sucesso."
                          }
                            );
                    }
                    return retorno;
                }

            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao obter lista de SO." };
            }

        }

        [HttpPost]
        [Route("ObterSOGame")]
        public ActionResult<RetornoObterSO> ObterSO([FromForm] string CodigoGame)
        {

            #region Validar Entradas

            if (CodigoGame == null)
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = "Parâmetro CodigoGame incorreto." };
            }

            #endregion

            RetornoObterSO retorno = new RetornoObterSO();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    ObterSOEntity lista = db.ObterSO(CodigoGame);


                    retorno.CodigoSO = lista.CodSO;
                    retorno.NomeSO = lista.NomeSO;
                    retorno.Sucesso = true;
                    retorno.Mensagem = "Lista de SO recuperado com sucesso.";

                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao obter lista de SO." };
            }

        }

        #endregion

        #region Categoria

        [HttpPost]
        [Route("ObterCategoriaGame")]
        public ActionResult<RetornoObterCategoria> ObterCategoriaGame([FromForm] string CodigoGame)
        {

            #region Validar Entradas

            if (CodigoGame == null)
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.BadRequest, Content = "Parâmetro CodigoGame incorreto." };
            }

            #endregion

            RetornoObterCategoria retorno = new RetornoObterCategoria();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    ObterCategoriaEntity lista = db.ObterCategoria(CodigoGame);


                    retorno.CodigoCategoria = lista.IdCategoria;
                    retorno.NomeCategoria = lista.Nome_Categoria;
                    retorno.Sucesso = true;
                    retorno.Mensagem = "Categoria recuperado com sucesso.";

                }

                return retorno;
            }
            catch
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Erro ao obter lista de SO." };
            }

        }

        #endregion

    }
}

