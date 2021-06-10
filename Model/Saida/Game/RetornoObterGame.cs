using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.Model.Saida.Game {
    public class RetornoObterGame : Retorno
    
    {


        public int CodigoGame { get; set; }
        public string NomeGame { get; set; }
        public string DescricaoGame { get; set; }
        public int IdCategoriaGame { get; set; }
        public string SrcImagemGame { get; set; }

    }
}
