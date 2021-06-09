using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.Model.Saida.GameDetail
{
    public class RetornoObterArmazenamento : Retorno
    {
        public int CodigoArmazenamento { get; set; }
        public string NomeArmazenamento { get; set; }
    }
}
