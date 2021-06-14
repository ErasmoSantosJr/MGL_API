using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.Model.Entity.GameDetail
{
    public class ObterClassificacaoEntity
    {
        public int CodClassificacao { get; set; }
        public string NomeClassificacao { get; set; }
        public int CodVisibilidade { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
