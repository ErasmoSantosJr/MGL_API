using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.Model.Saida.GameDetail
{
    public class RetornoObterPlacaVideo : Retorno
    {
        public int CodigoPlacaVideo { get; set; }
        public string NomePlacaVideo { get; set; }
    }
}
