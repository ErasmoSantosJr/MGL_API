using MGL_API.Model.Entrada;
using MGL_API.Model.Saida;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.db
{
    public class UtilitarioDB
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

            return retorno;
        }


    }
}
