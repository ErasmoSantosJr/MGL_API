using MGL_API.db;
using MGL_API.Model.Entity.GameDetail;
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

        [HttpGet]
        [Route("ObterArmazenamentoGame")]
        public ActionResult<RetornoObterArmazenamento> ObterArmazenamento()
        {

            RetornoObterArmazenamento retorno = new RetornoObterArmazenamento();

            try
            {
                using (UtilitarioDB db = new UtilitarioDB(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    ObterArmazenamentoEntity lista = db.ObterArmazenamento();


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

        #endregion

    }
}

