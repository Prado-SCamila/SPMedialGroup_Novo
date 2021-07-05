using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpMedicalG_WebApi.Domains
{
    public class MedicosDomain
    {
        public int idMedico { get; set; }
        public int idUsuario { get; set; }
        public int idEspecialidade { get; set; }
        public int idClinica { get; set; }
        public string crm { get; set; }
        public string nomeMedico { get; set; }
    }
}
