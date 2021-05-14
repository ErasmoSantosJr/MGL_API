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
            DateTime dataAtual = DateTime.Now;
            string sql = "insert into Game (Nome_Game, Descricao_Game, IdCategoria_Game, SRC_Imagem_Game, DataCriacao_Game) Values (@Nome, @Descricao, @IdCategoria, @SRC_Imagem, @DataCriacao)";


            conexao.Execute(sql, new { @Nome = entrada.Nome, @Descricao = entrada.Descricao, @IdCategoria = entrada.Categoria, @SRC_Imagem = "Sem Imagem", @DataCriacao = "2001-02-02" });
            retorno.Game = entrada.Nome;

            return retorno;

        }

        public RetornoExibirGame ExibirGame(EntradaExibirGame entrada)
        {
            RetornoExibirGame retorno = new RetornoExibirGame();
            string sql = "select from Game where idGame = @idGame";
            conexao.Execute(sql, new { @id = entrada.idGame});


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
