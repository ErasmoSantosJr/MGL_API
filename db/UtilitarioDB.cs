using MGL_API.Model.Entrada;
using MGL_API.Model.Saida;
using MGL_API.Model.Saida.Game;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MGL_API.Model.Entity.Usuario;
using MGL_API.Model.Entity.Game;


namespace MGL_API.db {
    public class UtilitarioDB : IDisposable {
        private SqlConnection conexao;

        public UtilitarioDB(string queryString) {
            conexao = new SqlConnection(queryString);
            conexao.Open();
        }
        //Tentei reproduzir o CadastrarUsuario mas alterando as variaveis e insert de acordo com a tabela Game do banco de dados
        public RetornoCadastroGame CadastrarGame(EntradaCadastroGame entrada) {
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


        public RetornoCadastroUsuario CadastrarUsuario(EntradaCadastroUsuario entrada) {
            RetornoCadastroUsuario retorno = new RetornoCadastroUsuario();
            DateTime dataAtual = DateTime.Now;
            string sql = "insert into Usuario (Nome_Usuario, Email_Usuario, Login_Usuario, Password_Usuario, IdCatalogo_Usuario, ADM_Usuario, DataCriacao_Usuario, DataNascimento_Usuario) Values (@Nome, @Email, @Login, @Password, @Catalogo, @ADM, @DataCriacao, @DataNascimento)";


            conexao.Execute(sql, new { @Nome = entrada.Nome, @Email = entrada.Email, @Login = entrada.Login, @Password = entrada.Password, @Catalogo = 1, @ADM = 0, @DataCriacao = dataAtual, @DataNascimento = entrada.DataNascimento });

            retorno.Login = entrada.Login;

            return retorno;

        }

        public UsuarioEntity LogarUsuario(EntradaLoginUsuario entrada) {
            UsuarioEntity retorno = new UsuarioEntity();

            retorno = conexao.QueryFirstOrDefault<UsuarioEntity>(@"SELECT * from Usuario WHERE Email_Usuario = @email and Password_Usuario = @Password ", new { @Email = entrada.Email, @Password = entrada.Password });

            return retorno;

        }


        public void Dispose() {
            conexao.Close();
            conexao.Dispose();
        }
    }
}
