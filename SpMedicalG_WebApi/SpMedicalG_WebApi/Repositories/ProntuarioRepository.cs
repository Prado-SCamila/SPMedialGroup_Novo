using SpMedicalG_WebApi.Domains;
using SpMedicalG_WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpMedicalG_WebApi.Repositories
{
    public class ProntuarioRepository : IProntuarioRepository
    {
        public void AtualizarIdCorpo(ProntuariosDomain prontuario)
        {
            throw new NotImplementedException();
        }

        public void AtualizarUrl(int id, ProntuariosDomain prontuario)
        {
            throw new NotImplementedException();
        }

        public ProntuariosDomain BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(ProntuariosDomain prontuarios)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProntuariosDomain> ListarTodos()
        {
            //crio uma lista para ser lida
            List<ProntuariosDomain> listaProntuarios = new List<ProntuariosDomain>();
            //Declaro a sql connection passando a string de conexao como parametro
            using (SqlConnection con = new SqlConnection (stringConexao))
            { 

                 //declaro a instrução a ser executada
                 using querySelectAll = "SELECT idProntuario, dataNascimento, telefone, RG, CPF, endereco FROM Prontuarios";

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
               ProntuariosDomain prontuario = new ProntuariosDomain();

            {
                       idProntuario = Convert.ToInt32(rdr[0]);
                        prontuario = rdr[1].ToString();
            };

             listaProntuarios.Add(prontuario);
            }
        }
    }
      return listaProntuarios;
    }
}
}

