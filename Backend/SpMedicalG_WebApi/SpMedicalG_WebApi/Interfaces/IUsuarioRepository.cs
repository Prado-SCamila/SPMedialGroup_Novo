using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpMedicalG_WebApi.Domains;

namespace SpMedicalG_WebApi.Interfaces
{
    interface IUsuarioRepository
    {
        List<UsuariosDomain> ListarMedicos();

        UsuariosDomain BuscarPorId(int id);

        UsuariosDomain Login(string email, string senha);

        void Cadastrar(UsuariosDomain novoUsuario);

        void AtualizarIdCorpo(UsuariosDomain usuario);

        void AtualizarUrl(int id, UsuariosDomain usuario);

        void Delete(int id);
    }
}
