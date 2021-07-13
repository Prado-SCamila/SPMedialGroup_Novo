using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpMedicalG_WebApi.Domains;

namespace SpMedicalG_WebApi.Interfaces
{
    interface IProntuarioRepository
    {
        List<ProntuariosDomain> ListarTodos();

        ProntuariosDomain BuscarPorId(int id);

        void Cadastrar(ProntuariosDomain prontuarios);

        void AtualizarIdCorpo(ProntuariosDomain prontuario);

        void AtualizarUrl(int id, ProntuariosDomain prontuario);

        void Deletar(int id);
    }
}
