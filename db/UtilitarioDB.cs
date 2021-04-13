using MGL_API.Model.Entrada;
using MGL_API.Model.Saida;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MGL_API.Model.Entity.Usuario;

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

        public void Dispose()
        {
            conexao.Close();
            conexao.Dispose();
        }
    }
}
