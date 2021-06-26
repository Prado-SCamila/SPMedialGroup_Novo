using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpMedicalG_WebApi.Domains
{
    public class UsuariosDomain
    {
        public int idUsuario { get; set; }
        public int idTipoUsuario { get; set; }

        public string nomeUsuario { get; set; }

        public string email { get; set; }

        public string senha { get; set; }
    }
}
