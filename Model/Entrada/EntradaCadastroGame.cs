using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.Model.Entrada {
    //Paremetros de entrada do cadastro do jogo
    public class EntradaCadastroGame {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string Lancamento { get; set; }
        public string Requisitos { get; set; }
        public string Desenvolvedora { get; set; }
        public string Publicadora { get; set; }
        public string Plataformas { get; set; }
        public string Classificacao { get; set; }

    }
}
