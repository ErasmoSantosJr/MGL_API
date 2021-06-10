using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.Model.Entity.GameDetail
{
    public class ObterDetalhesEntity
    {
        public int CodDetalhes { get; set; }
        public int CodGame { get; set; }
        public int CodVisibilidade { get; set; }
        public int CodDetalhesClass { get; set; }
        public int CodDetalhesSO { get; set; }
        public int CodDetalhesProcessador { get; set; }
        public int CodDetalhesPlacaVideo { get; set; }
        public int CodDetalhesMemoria { get; set; }
        public int CodDetalhesArmazenamento { get; set; }
        public int CodDetalhesPlataforma { get; set; }
        public DateTime DataCadastro { get; set; }

    }
}
