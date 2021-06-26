using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pMedicalG_WebApi.Domains;
using SpMedicalG_WebApi.Domains;

namespace SpMedicalG_WebApi.Interfaces
{
    interface IUsuarioRepository
    {
        List<UsuariosDomain> ListarTodos();

        UsuariosDomain BuscarPorId(int id);

        void Cadastrar(UsuariosDomain novoUsuario);

        void AtualizarIdCorpo(UsuariosDomain usuario);

        void AtualizarUrl(int id, UsuariosDomain usuario);

        void Delete(int id);
    }
}
