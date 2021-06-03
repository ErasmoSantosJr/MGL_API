using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.Model.Saida.Game {
    public class RetornoExibirGame : Retorno{
        //Comentei o id_Game por que não acho que seja necessario exibir no frontend mas não queria apagar do código
        //public int Id_Game { get; set; }
        public string Nome_Game { get; set; }
        public string Descricao_Game { get; set; }
        public int IdCategoria_Game { get; set; }
        public string SRC_Imagem_Game { get; set; }
        public DateTime DataCriacao_Game { get; set; }
        public string Requisitos_Game { get; set; }
        public string Desenvolvedora_Game { get; set; }
        public string Publicadora_Game { get; set; }
        public string Plataformas_Game { get; set; }
        public string Classificacao_Game { get; set; }
    }
}
