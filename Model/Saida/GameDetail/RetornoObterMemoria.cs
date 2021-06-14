using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.Model.Saida.GameDetail
{
    public class RetornoObterMemoria : Retorno
    {
        public int CodigoMemoria { get; set; }
        public string NomeMemoria { get; set; }
    }
}
