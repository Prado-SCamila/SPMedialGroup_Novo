using SpMedicalG_WebApi.Domains;
using SpMedicalG_WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SpMedicalG_WebApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public void AtualizarIdCorpo(UsuariosDomain usuario)
        {
            throw new NotImplementedException();
        }

        public void AtualizarUrl(int id, UsuariosDomain usuario)
        {
            throw new NotImplementedException();
        }

        public UsuariosDomain BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(UsuariosDomain novoUsuario)
        {
            using (SqlCommection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO Usuarios(nomeUsuario) VALUES (' " + novoUsuario + " ')";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    con.Open();
                }
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<UsuariosDomain> ListarTodos()
        {
            //crio uma lista para ser lida
            List<UsuariosDomain> listaUsuarios = new List<UsuariosDomain>();
            //Declaro a sql connection passando a string de conexao como parametro
            using (SqlConnection con = new SqlConnection (stringConexao))
            { 

            //declaro a instrução a ser executada
            using querySelectAll = "SELECT idUsuario, nome, email, senha FROM Usuarios";

            //abre a conexão com o bco de dados
            con.Open();

            //Declara o objeto que vai ler a tabela no bco de dados
            SqlDataReader rdr;

            using (SqlCommmand cmd = new SqlCommand (querySelectAll, con))
            {
                rdr = cmd.ExecuteReader();
            }
            //enquanto houverem registros para serem lidos, o laço se repete
            while (rdr.Read())
            {
                UsuariosDomain usuario = new UsuariosDomain();

                {
                    idUsuario = Convert.ToInt32(rdr[0]);
                        usuario = rdr[1].ToString();
                };

                listaUsuarios.Add(usuario);
            }
        }
    }
    return listaUsuarios;
}
}
}