using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpMedicalG_WebApi.Domains;

namespace SpMedicalG_WebApi.Interfaces
{
    interface IConsultaRepository
    {
        List<ConsultasDomain> ListarTodos();

        ConsultasDomain BuscaConsulta(int id); // método para paciente ver suas consultas
      
        ConsultasDomain BuscarPorId (int id);// método para médico ver seus agendamentos

        void Cadastrar(ConsultasDomain novaConsulta);// Método para ADM cadastrar nova consulta

        ConsultasDomain AtualizarDescricao (int id, ConsultasDomain consulta);

        void Deletar(int id);
    }
}
