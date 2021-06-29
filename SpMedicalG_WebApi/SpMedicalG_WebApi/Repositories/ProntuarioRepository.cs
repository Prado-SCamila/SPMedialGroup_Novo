using SpMedicalG_WebApi.Domains;
using SpMedicalG_WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT idProntuario,dataNascimento, telefone,rg,cpf,endereco FROM Prontuarios WHERE idProntuario = @id";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    rdr = cmd.ExecuteReader();

                    //Verifica se há dados para serem lidos
                    if (rdr.Read())
                    {
                        ProntuariosDomain prontuarioBuscado = new ProntuariosDomain()
                        {
                            idProntuario = Convert.ToInt32(rdr[0]),
                            dataNasc = rdr.["dataConsulta"].ToString(),
                            telefone = rdr.["telefone"].ToString(),
                            RG = rdr.["rg"].ToString(),
                            CPF = rdr.["cpf"].ToString(),
                            endereco = rdr.["endereco"].ToString(),

                        };
                        //Se algo for encontrado, retorna o que foi buscado
                        return prontuarioBuscado;
                    }
                    //se nada for encontrado, devolve null
                    return null;
                }
            }

        public void Cadastrar(ProntuariosDomain novoProntuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO Prontuarios (cpf),(rg),(telefone),(endereco) VALUES (@cpf), (@rg),(@telefone),(@endereco)";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    //preciso de um metodo cadastrar pra cada campo da tabela prontuarios?
                    cmd.Parameters.AddWithValue("@cpf", novoProntuario.CPF);
                    cmd.Parameters.AddWithValue("@rg", novoProntuario.RG);
                    cmd.Parameters.AddWithValue("@telefone", novoProntuario.telefone);
                    cmd.Parameters.AddWithValue("@endereco", novoProntuario.endereco);
                    
                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM Prontuarios Where id= @ID";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
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

        public List<ProntuariosDomain> ListarTodos()
        {
            throw new NotImplementedException();
        }
    }

