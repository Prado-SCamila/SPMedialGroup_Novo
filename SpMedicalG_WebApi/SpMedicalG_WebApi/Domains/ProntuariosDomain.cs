using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpMedicalG_WebApi.Domains
{
    public class ProntuariosDomain
    {
        public int idProntuario { get; set; }
        public int idUsuario { get; set; }
        public DateTime dataNasc { get; set; }
        public int telefone { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string endereco { get; set; }
            
}
}
