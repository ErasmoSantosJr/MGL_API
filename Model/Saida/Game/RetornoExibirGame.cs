﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.Model.Saida.Game {
    public class RetornoExibirGame : Retorno{
        public string Nome_Game { get; set; }
        public string Descricao_Game { get; set; }
        public string idCategoria_Game { get; set; }
        public string SRC_Imagem_Game { get; set; }
        public DateTime DataCriacao_Game { get; set; }
    }
}