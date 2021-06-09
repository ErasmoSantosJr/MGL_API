using MGL_API.Model.Entrada;
using MGL_API.Model.Saida;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MGL_API.Model.Entity.Usuario;
using MGL_API.Model.Entity.GameDetail;
using MGL_API.Model.Entrada.GameDetail;
using MGL_API.Model.Saida.GameDetail;
using MGL_API.Model.Saida.Game;

namespace MGL_API.db
{
    public class UtilitarioDB : IDisposable
    {
        private SqlConnection conexao;

        public UtilitarioDB(string queryString)
        {
            conexao = new SqlConnection(queryString);
            conexao.Open();
        }

        #region Usuario
        public RetornoCadastroUsuario CadastrarUsuario(EntradaCadastroUsuario entrada)
        {
            RetornoCadastroUsuario retorno = new RetornoCadastroUsuario();
            DateTime dataAtual = DateTime.Now;
            string sql = "insert into Usuario (Nome_Usuario, Email_Usuario, Login_Usuario, Password_Usuario, IdCatalogo_Usuario, ADM_Usuario, DataCriacao_Usuario, DataNascimento_Usuario) Values (@Nome, @Email, @Login, @Password, @Catalogo, @ADM, @DataCriacao, @DataNascimento)";


            conexao.Execute(sql, new { @Nome = entrada.Nome, @Email = entrada.Email, @Login = entrada.Login, @Password = entrada.Password, @Catalogo = 1, @ADM = 0, @DataCriacao = dataAtual, @DataNascimento = entrada.DataNascimento });

            retorno.Login = entrada.Login;

            return retorno;

        }

        public UsuarioEntity LogarUsuario(EntradaLoginUsuario entrada)
        {
            UsuarioEntity retorno = new UsuarioEntity();

            retorno = conexao.QueryFirstOrDefault<UsuarioEntity>(@"SELECT * from Usuario WHERE Email_Usuario = @email and Password_Usuario = @Password ", new
            { @Email = entrada.Email, @Password = entrada.Password });

            return retorno;

        }
        #endregion

        #region GameDetail Armazenamento
        public RetornoArmazenamento CadastrarArmazenamento(EntradaArmazenamento entrada)
        {
            RetornoArmazenamento retorno = new RetornoArmazenamento();
            DateTime dataAtual = DateTime.Now;
            string sql = "insert into Armazenamento (NomeArmazenamento, CodVisibilidade, DataCadastro) Values (@NomeArmazenamento, @CodVisibilidade, @DataCriacao)";


            conexao.Execute(sql, new { @NomeArmazenamento = entrada.NomeArmazenamento, @CodVisibilidade = 1, @DataCriacao = dataAtual });

            retorno.NomeArmazenamento = entrada.NomeArmazenamento;

            return retorno;

        }

        public bool DeletarArmazenamento(int? CodArmazenamento)
        {

            try
            {
                string sql = "Update Armazenamento set CodVisibilidade = @CodVisibilidade where CodArmazenamento = @CodArmazenamento and CodVisibilidade != 0";

                conexao.Execute(sql, new { @CodArmazenamento = CodArmazenamento, @CodVisibilidade = 0 });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<ObterArmazenamentoEntity> ObterListaArmazenamento()
        {
            List<ObterArmazenamentoEntity> retorno = conexao.Query<ObterArmazenamentoEntity>
                ("select * from Armazenamento where CodVisibilidade = 0").ToList();

            return retorno;
        }

        public ObterArmazenamentoEntity ObterArmazenamento()
        {
            ObterDetalhesEntity codigo = conexao.QueryFirstOrDefault<ObterDetalhesEntity>
                ("select CodDetalheArmazenamento from Detalhes where CodGameDetalhe = 1");

            string sql = "select * from Armazenamento where CodArmazenamento = @cod and CodVisibilidade = 0";

            ObterArmazenamentoEntity retorno = conexao.QueryFirstOrDefault<ObterArmazenamentoEntity>(
                sql, new { @cod = Convert.ToInt32(codigo.CodDetalheArmazenamento) });

            return retorno;
        }

        #endregion

        #region GameDetail Classificacao
        public RetornoClassificacao CadastrarClassificacao(EntradaClassificacao entrada)
        {
            RetornoClassificacao retorno = new RetornoClassificacao();
            DateTime dataAtual = DateTime.Now;
            string sql = "insert into Classificacao (NomeClassificacao, CodVisibilidade, DataCadastro) Values (@NomeClassificacao, @CodVisibilidade, @DataCriacao)";


            conexao.Execute(sql, new { @NomeClassificacao = entrada.NomeClassificacao, @CodVisibilidade = 1, @DataCriacao = dataAtual });

            retorno.NomeClassificacao = entrada.NomeClassificacao;

            return retorno;

        }

        public bool DeletarClassificacao(int? CodClassificacao)
        {

            try
            {
                string sql = "Update Classificacao set CodVisibilidade = @CodVisibilidade where CodClassificacao = @CodClassificacao and CodVisibilidade != 0";

                conexao.Execute(sql, new { @CodClassificacao = CodClassificacao, @CodVisibilidade = 0 });

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region GameDetail Memoria
        public RetornoMemoria CadastrarMemoria(EntradaMemoria entrada)
        {
            RetornoMemoria retorno = new RetornoMemoria();

            DateTime dataAtual = DateTime.Now;
            string sql = "insert into Memoria (NomeMemoria, CodVisibilidade, DataCadastro) Values (@NomeMemoria, @CodVisibilidade, @DataCriacao)";


            conexao.Execute(sql, new { @NomeMemoria = entrada.NomeMemoria, @CodVisibilidade = 1, @DataCriacao = dataAtual });

            retorno.NomeMemoria = entrada.NomeMemoria;

            return retorno;

        }

        public bool DeletarMemoria(int? CodMemoria)
        {

            try
            {
                string sql = "Update Memoria set CodVisibilidade = @CodVisibilidade where CodMemoria = @CodMemoria  and CodVisibilidade != 0";

                conexao.Execute(sql, new { @CodMemoria = CodMemoria, CodVisibilidade = 0 });

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region GameDetail PlacaVideo
        public RetornoPlacaVideo CadastrarPlacaVideo(EntradaPlacaVideo entrada)
        {
            RetornoPlacaVideo retorno = new RetornoPlacaVideo();

            DateTime dataAtual = DateTime.Now;
            string sql = "insert into PlacaVideo (NomePlacaVideo, CodVisibilidade, DataCadastro) Values (@NomePlacaVideo, @CodVisibilidade, @DataCriacao)";


            conexao.Execute(sql, new { NomePlacaVideo = entrada.NomePlacaVideo, @CodVisibilidade = 1, @DataCriacao = dataAtual });

            retorno.NomePlacaVideo = entrada.NomePlacaVideo;

            return retorno;

        }

        public bool DeletarPlacaVideo(int? CodPlacaVideo)
        {

            try
            {
                string sql = "Update PlacaVideo set CodVisibilidade = @CodVisibilidade where CodPlacaVideo = @CodPlacaVideo  and CodVisibilidade != 0";

                conexao.Execute(sql, new { @CodPlacaVideo = CodPlacaVideo, CodVisibilidade = 0 });

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region GameDetail Plataforma
        public RetornoPlataforma CadastrarPlataforma(EntradaPlataforma entrada)
        {
            RetornoPlataforma retorno = new RetornoPlataforma();

            DateTime dataAtual = DateTime.Now;
            string sql = "insert into Plataforma (NomePlataforma, CodVisibilidade, DataCadastro) Values (@NomePlataforma, @CodVisibilidade, @DataCriacao)";


            conexao.Execute(sql, new { @NomePlataforma = entrada.NomePlataforma, @CodVisibilidade = 1, @DataCriacao = dataAtual });

            retorno.NomePlataforma = entrada.NomePlataforma;

            return retorno;

        }

        public bool DeletarPlataforma(int? CodPlataforma)
        {

            try
            {
                string sql = "Update Plataforma set CodVisibilidade = @CodVisibilidade where CodPlataforma = @CodPlataforma and CodVisibilidade != 0";

                conexao.Execute(sql, new { @CodPlataforma = CodPlataforma, CodVisibilidade = 0 });

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region GameDetail Processador
        public RetornoProcessador CadastrarProcessador(EntradaProcessador entrada)
        {
            RetornoProcessador retorno = new RetornoProcessador();

            DateTime dataAtual = DateTime.Now;
            string sql = "insert into Processador (NomeProcessador, CodVisibilidade, DataCadastro) Values (@NomeProcessador, @CodVisibilidade, @DataCriacao)";


            conexao.Execute(sql, new { @NomeProcessador = entrada.NomeProcessador, @CodVisibilidade = 1, @DataCriacao = dataAtual });

            retorno.NomeProcessador = entrada.NomeProcessador;

            return retorno;

        }

        public bool DeletarProcessador(int? CodProcessador)
        {

            try
            {
                string sql = "Update Processador set CodVisibilidade = @CodVisibilidade where CodProcessador = @CodProcessador and CodVisibilidade != 0";

                conexao.Execute(sql, new { @CodProcessador = CodProcessador, CodVisibilidade = 0 });

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region GameDetail SO
        public RetornoSO CadastrarSO(EntradaSO entrada)
        {
            RetornoSO retorno = new RetornoSO();

            DateTime dataAtual = DateTime.Now;
            string sql = "insert into SO (NomeSO, CodVisibilidade, DataCadastro) Values (@NomeSO, @CodVisibilidade, @DataCriacao)";


            conexao.Execute(sql, new { @NomeSO = entrada.NomeSO, @CodVisibilidade = 1, @DataCriacao = dataAtual });

            retorno.NomeSO = entrada.NomeSO;

            return retorno;

        }

        public bool DeletarSO(int? CodSO)
        {

            try
            {
                string sql = "Update SO set CodVisibilidade = @CodVisibilidade where CodSO = @CodSO and CodVisibilidade != 0";

                conexao.Execute(sql, new { @CodSO = CodSO, CodVisibilidade = 0 });

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Game

        //Tentei reproduzir o CadastrarUsuario mas alterando as variaveis e insert de acordo com a tabela Game do banco de dados
        public RetornoCadastroGame CadastrarGame(EntradaCadastroGame entrada)
        {
            RetornoCadastroGame retorno = new RetornoCadastroGame();
            //DateTime dataAtual = DateTime.Now;
            string sql = "insert into Game (Nome_Game, Descricao_Game, IdCategoria_Game, SRC_Imagem_Game, DataCriacao_Game, Requisitos_Game, " +
                " Desenvolvedora_Game, Publicadora_Game, Plataformas_Game, Classificacao_Game) " +
                "Values (@Nome, @Descricao, @IdCategoria, @SRC_Imagem, @DataCriacao, @Requisitos, @Desenvolvedora, @Publicadora, @Plataformas, @Classificacao)";


            conexao.Execute(sql, new { @Nome = entrada.Nome, @Descricao = entrada.Descricao, @IdCategoria = entrada.Categoria, @SRC_Imagem = "Sem Imagem", @DataCriacao = entrada.Lancamento, @Requisitos = entrada.Requisitos, @Desenvolvedora = entrada.Desenvolvedora, @Publicadora = entrada.Publicadora, @Plataformas = entrada.Plataformas, @Classificacao = entrada.Classificacao });
            retorno.Game = entrada.Nome;

            return retorno;

        }
        //Tentando arrumar o bug na hora de exibir o game antes de continuar com a parte da avaliação
        public RetornoExibirGame ExibirGame(EntradaExibirGame entrada)
        {
            RetornoExibirGame retorno = new RetornoExibirGame();
            //string com o comando SQL pra buscar o nome do game
            string nomeGame = "select Nome_Game from Game where IdGame = @IdGame";
            SqlCommand GetNome = conexao.CreateCommand();
            //Parametro entrada.IdGame;
            GetNome.Parameters.AddWithValue("@IdGame", entrada.IdGame);
            GetNome.CommandText = nomeGame;
            //Converte o resultado do select pra string e armazena no retorno
            string retornoNome = (string)GetNome.ExecuteScalar();
            //Atribui o valor do retornoNome
            retorno.Nome_Game = retornoNome;

            string descricaoGame = "select Descricao_Game from Game where IdGame = @IdGame";
            SqlCommand GetDescricao = conexao.CreateCommand();
            GetDescricao.Parameters.AddWithValue("@IdGame", entrada.IdGame);
            GetDescricao.CommandText = descricaoGame;
            string retornoDescricao = (string)GetDescricao.ExecuteScalar();
            retorno.Descricao_Game = retornoDescricao;


            string categoriaGame = "select IdCategoria_Game from Game where IdGame = @IdGame";
            SqlCommand GetCategoria = conexao.CreateCommand();
            GetCategoria.Parameters.AddWithValue("@IdGame", entrada.IdGame);
            GetCategoria.CommandText = categoriaGame;
            int retornoCategoria = (int)GetCategoria.ExecuteScalar();
            retorno.IdCategoria_Game = retornoCategoria;


            string imagemGame = "select SRC_Imagem_Game from Game where IdGame = @IdGame";
            SqlCommand GetImagem = conexao.CreateCommand();
            GetImagem.Parameters.AddWithValue("@IdGame", entrada.IdGame);
            GetImagem.CommandText = imagemGame;
            string retornoImagem = (string)GetImagem.ExecuteScalar();
            retorno.SRC_Imagem_Game = retornoImagem;

            string dataGame = "select DataCriacao_Game from Game where IdGame = @IdGame";
            SqlCommand GetData = conexao.CreateCommand();
            GetData.Parameters.AddWithValue("@IdGame", entrada.IdGame);
            GetData.CommandText = dataGame;
            DateTime retornoData = (DateTime)GetData.ExecuteScalar();
            retorno.DataCriacao_Game = retornoData;

            string requisitosGame = "select Requisitos_Game from Game where IdGame = @IdGame";
            SqlCommand GetRequisitos = conexao.CreateCommand();
            GetRequisitos.Parameters.AddWithValue("@IdGame", entrada.IdGame);
            GetRequisitos.CommandText = requisitosGame;
            string retornoRequisitos = (string)GetRequisitos.ExecuteScalar();
            retorno.Requisitos_Game = retornoRequisitos;

            string desenvolvedoraGame = "select Desenvolvedora_Game from Game where IdGame = @IdGame";
            SqlCommand GetDesenvolvedora = conexao.CreateCommand();
            GetDesenvolvedora.Parameters.AddWithValue("@IdGame", entrada.IdGame);
            GetDesenvolvedora.CommandText = desenvolvedoraGame;
            string retornoDesenvolvedora = (string)GetDesenvolvedora.ExecuteScalar();
            retorno.Desenvolvedora_Game = retornoDesenvolvedora;

            string publicadoraGame = "select Publicadora_Game from Game where IdGame = @IdGame";
            SqlCommand GetPublicadora = conexao.CreateCommand();
            GetPublicadora.Parameters.AddWithValue("@IdGame", entrada.IdGame);
            GetPublicadora.CommandText = publicadoraGame;
            string retornoPublicadora = (string)GetPublicadora.ExecuteScalar();
            retorno.Publicadora_Game = retornoPublicadora;

            string plataformasGame = "select Plataformas_Game from Game where IdGame = @IdGame";
            SqlCommand GetPlataformas = conexao.CreateCommand();
            GetPlataformas.Parameters.AddWithValue("@IdGame", entrada.IdGame);
            GetPlataformas.CommandText = plataformasGame;
            string retornoPlataformas = (string)GetPlataformas.ExecuteScalar();
            retorno.Plataformas_Game = retornoPlataformas;

            string classificacaoGame = "select Classificacao_Game from Game where IdGame = @IdGame";
            SqlCommand GetClassificacao = conexao.CreateCommand();
            GetClassificacao.Parameters.AddWithValue("@IdGame", entrada.IdGame);
            GetClassificacao.CommandText = classificacaoGame;
            string retornoClassificacao = (string)GetClassificacao.ExecuteScalar();
            retorno.Classificacao_Game = retornoClassificacao;



            return retorno;
        }
        //Ainda em construção
        public RetornoAvaliarGame AvaliarGame(EntradaAvaliarGame entrada)
        {
            RetornoAvaliarGame retorno = new RetornoAvaliarGame();


            return retorno;
        }

        #endregion

        public void Dispose()
        {
            conexao.Close();
            conexao.Dispose();
        }
    }
}
