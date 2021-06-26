using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpMedicalG_WebApi.Domains;

namespace SpMedicalG_WebApi.Interfaces
{
    interface IMedicoRepository
    {
        List<MedicosDomain> ListarTodos();

        MedicosDomain BuscarPorId(int id);

        void Cadastrar(MedicosDomain novoMedico);

        void AtualizarIdCorpo(MedicosDomain medico);

        void AtualizarUrl(int id, MedicosDomain medico);

        void Deletar(int id);


    }
}
