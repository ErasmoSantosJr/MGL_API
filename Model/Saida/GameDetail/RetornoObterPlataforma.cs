using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.Model.Saida.GameDetail
{
    public class RetornoObterPlataforma : Retorno
    {
        public int CodigoPlataforma { get; set; }
        public string NomePlataforma { get; set; }
    }
}
