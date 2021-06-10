using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.Model.Entity.GameDetail
{
    public class ObterGameEntity
    {
        public int IdGame { get; set; }
        public string Nome_Game { get; set; }
        public string Descricao_Game { get; set; }
        public int IdCategoria_Game { get; set; }
        public string SRC_Imagem_Game { get; set; }
        public DateTime DataCriacao { get; set; }

    }
}
