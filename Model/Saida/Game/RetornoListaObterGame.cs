using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.Model.Saida.Game
{
    public class RetornoListaObterGame : Retorno
    {

        public int CodigoGame { get; set; }
        public string NomeGame { get; set; }

    }
}
