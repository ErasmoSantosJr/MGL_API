using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGL_API.Model.Entity.Usuario
{
    public class UsuarioEntity
    {
        public int idUsuario { get; set; }
        public string Nome_Usuario { get; set; }
        public string Email_Usuario { get; set; }
        public string Login_Usuario { get; set; }
        public string Password_Usuario { get; set; }
        public int ADM_Usuario { get; set; }
        public DateTime DataCriacao_Usuario { get; set; }
        public DateTime DataNascimento_Usuario { get; set; }
    }
}
