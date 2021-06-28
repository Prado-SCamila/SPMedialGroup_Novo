using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpMedicalG_WebApi.Domains
{
    public class ConsultasDomain
    {
        public int idConsulta { get; set; }

        public int idProntuario { get; set; }

        public int idMedico { get; set; }

        public DateTime dataConsulta { get; set; }

        public int idSituacao { get; set; }

        public string  descricao { get; set; }

    }
}


