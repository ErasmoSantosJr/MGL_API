﻿using MGL_API.Model.Entrada;
using MGL_API.Model.Saida;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

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
            string sql = "insert into Usuario (Nome_Usuario, Email_Usuario, Login_Usuario, Password_Usuario, IdCatalogo_Usuario, ADM_Usuario, DataCriacao_Usuario) Values (@Nome, @Email, @Login, @Password, @Catalogo, @ADM, @Data)";

            conexao.Execute(sql, new { @Nome = entrada.Nome, @Email = entrada.Email, @Login = entrada.Login, @Password = entrada.Password, @Catalogo = 1, @ADM = 0, @Data = entrada.DataNascimento });

            RetornoCadastroUsuario retorno = new RetornoCadastroUsuario();

            return retorno;
        }

        public void Dispose()
        {
            conexao.Close();
            conexao.Dispose();
        }
    }
}
