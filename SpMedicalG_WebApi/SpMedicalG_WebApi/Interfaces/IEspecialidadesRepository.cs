using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpMedicalG_WebApi.Domains;

namespace SpMedicalG_WebApi.Interfaces
{
    interface IEspecialidadesRepository
    {
        List<EspecialidadesDomain> ListarTodos();

        EspecialidadesDomain BuscarPorId(int id);

        void Cadastrar(EspecialidadesDomain novaEspecialidade);

        void AtualizarIdCorpo(EspecialidadesDomain especialidade);

        void AtualizarUrl(int id, EspecialidadesDomain especialidade);

        void Deletar(int id);
    }
}
