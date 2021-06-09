using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.Model.Entity.Game {
    //Entidade do Game com todas as colunas do banco de dados
    public class GameEntity {
        public int IdGame { get; set; }
        public string Nome_Game { get; set; }
        public string Descricao_Game { get; set; }
        public string IdCategoria_Game { get; set; }
        public string SRC_Imagem_Game { get; set; }
        public DateTime DataCriacao_Game { get; set; }
        public string Requisitos_Game { get; set; }
        public string Desenvolvedora_Game { get; set; }
        public string Publicadora_Game { get; set; }
        public string Plataformas_Game { get; set; }
        public string Classificacao_Game { get; set; }
    }
}
