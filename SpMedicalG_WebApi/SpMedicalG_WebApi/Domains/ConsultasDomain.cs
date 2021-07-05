using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SpMedicalG_WebApi.Domains.MedicosDomain;
using static SpMedicalG_WebApi.Domains.SituacaoDomain;
using SpMedicalG_WebApi.Repositories;


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

        public int idUsuario { get; set; }

        public MedicosDomain medico { get; set; }

        public SituacaoDomain situacao { get; set; }

        public UsuariosDomain nomeUsuario { get; set; }

       

    }
}


