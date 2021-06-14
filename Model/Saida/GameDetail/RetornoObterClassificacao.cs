using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.Model.Saida.GameDetail
{
    public class RetornoObterClassificacao : Retorno
    {
        public int CodigoClassificacao { get; set; }
        public string NomeClassificacao { get; set; }
    }
}
