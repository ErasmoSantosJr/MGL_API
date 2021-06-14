using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.Model.Saida.GameDetail
{
    public class RetornoObterProcessador : Retorno
    {
        public int CodigoProcessador { get; set; }
        public string NomeProcessador { get; set; }
    }
}
