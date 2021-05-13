using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.Model.Entrada {
    //Paremetros de entrada do cadastro do jogo, coloquei poucos parametros para facilitar os testes
    public class EntradaCadastroGame {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }

    }
}
