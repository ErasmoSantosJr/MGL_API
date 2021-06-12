using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.Model.Entity.GameDetail
{
    public class ObterMemoriaEntity
    {
        public int CodMemoria { get; set; }
        public string NomeMemoria { get; set; }
        public int CodVisibilidade { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
