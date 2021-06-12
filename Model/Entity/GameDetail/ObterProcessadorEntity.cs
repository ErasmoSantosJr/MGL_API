using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.Model.Entity.GameDetail
{
    public class ObterProcessadorEntity
    {
        public int CodProcessador { get; set; }
        public string NomeProcessador { get; set; }
        public int CodVisibilidade { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
