using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.Model.Entity.GameDetail
{
    public class ObterPlataformaEntity
    {
        public int CodPlataforma { get; set; }
        public string NomePlataforma { get; set; }
        public int CodVisibilidade { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
