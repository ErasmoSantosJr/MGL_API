using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.Model.Entity.GameDetail
{
    public class ObterDetalhesEntity
    {
        public int CodDetalhe { get; set; }
        public int CodClassDetalhe { get; set; }
        public int CodDetalheSO { get; set; }
        public int CodProcesso { get; set; }
        public int CodDetalhePlacaVideo { get; set; }
        public int CodDetalheArmazenamento { get; set; }
        public int CodDetalhePlataforma { get; set; }
        public DateTime DataCadastro { get; set; }

    }
}
