using SpMedicalG_WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpMedicalG_WebApi.Interfaces
{
    interface IClinicaRepository
    {
        void Cadastrar(ClinicaDomain novaClinica);

        List<ClinicaDomain> ListarClinicas();

        ClinicaDomain BuscarClinicaPorId( int id);

        ClinicaDomain AtualizarClinicaUrl(int id, ClinicaDomain clinica);

        void DeletarClinica(int id);


    }
}
