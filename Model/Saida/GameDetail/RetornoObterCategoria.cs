using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.Model.Saida.GameDetail
{
    public class RetornoObterCategoria : Retorno
    {
        public int CodigoCategoria { get; set; }
        public string NomeCategoria { get; set; }
        public DateTime DataCricao { get; set; }
    }
}
