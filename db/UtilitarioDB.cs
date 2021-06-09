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

        public void Dispose()
        {
            conexao.Close();
            conexao.Dispose();
        }
    }
}
