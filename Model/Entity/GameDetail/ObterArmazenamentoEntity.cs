using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.Model.Entity.GameDetail
{
    public class ObterArmazenamentoEntity
    {
        public int CodArmazenamento { get; set; }
        public string NomeArmazenamento { get; set; }
        public int CodVisibilidade { get; set; }
        public DateTime DataCadastro { get; set; }

    }
}
