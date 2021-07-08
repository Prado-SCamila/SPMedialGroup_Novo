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
        private string stringConexao = "Data Source=LAB08DESK1601\\SQLEXPRESS; initial catalog= SPmed;user id=sa;pwd= sa132";


        public void AtualizarIdCorpo(ProntuariosDomain prontuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateIdBody = "Update Prontuarios SET idUsuario = @idUsuario, dataNascimento = @dataNasc, telefone = @telefone, RG = @RG, CPF = @CPF, endereco = @endereco WHERE idProntuario = @ID";

                using (SqlCommand cmd = new SqlCommand(queryUpdateIdBody, con))
                {
                    cmd.Parameters.AddWithValue("@idUsuario", prontuario.idUsuario);
                    cmd.Parameters.AddWithValue("@dataNasc", prontuario.dataNasc);
                    cmd.Parameters.AddWithValue("@telefone", prontuario.telefone);
                    cmd.Parameters.AddWithValue("@RG", prontuario.RG);
                    cmd.Parameters.AddWithValue("@CPF", prontuario.CPF);
                    cmd.Parameters.AddWithValue("@endereco", prontuario.endereco);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }
        

        public void AtualizarUrl(int id, ProntuariosDomain prontuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //????
                string queryUpdateUrl = "UPDATE Prontuarios SET dataNascimento = @dataNascimento ,telefone = @telefone, rg = @rg, cpf = @cpf, endereco = @endereco  WHERE idProntuario = @ID";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    cmd.Parameters.AddWithValue("@dataNascimento", prontuario.dataNasc);
                    cmd.Parameters.AddWithValue("@telefone", prontuario.telefone);
                    cmd.Parameters.AddWithValue("@rg", prontuario.RG);
                    cmd.Parameters.AddWithValue("@cpf", prontuario.CPF);
                    cmd.Parameters.AddWithValue("@endereco", prontuario.endereco);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public ProntuariosDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT idProntuario, idUsuario, dataNascimento, telefone,rg,cpf,endereco FROM Prontuarios WHERE idProntuario = @id";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    rdr = cmd.ExecuteReader();

                    //Verifica se há dados para serem lidos
                    if (rdr.Read())
                    {
                        ProntuariosDomain prontuarioBuscado = new ProntuariosDomain()
                        {
                            idProntuario = Convert.ToInt32(rdr[0]),
                            idUsuario = Convert.ToInt32(rdr[1]),
                            dataNasc = Convert.ToDateTime(rdr[2]),
                            telefone = rdr[3].ToString(),
                            RG = rdr[4].ToString(),
                            CPF = rdr[5].ToString(),
                            endereco = rdr[6].ToString(),

                        };
                   
                    return prontuarioBuscado;
                    }
                    //se nada for encontrado, devolve null
                    return null;
                }
            }
        }

        public void Cadastrar(ProntuariosDomain prontuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO Prontuarios (cpf),(rg),(telefone),(endereco) VALUES (@cpf), (@rg),(@telefone),(@endereco)";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    //preciso de um metodo cadastrar pra cada campo da tabela prontuarios?
                    cmd.Parameters.AddWithValue("@cpf", prontuario.CPF);
                    cmd.Parameters.AddWithValue("@rg", prontuario.RG);
                    cmd.Parameters.AddWithValue("@telefone", prontuario.telefone);
                    cmd.Parameters.AddWithValue("@endereco", prontuario.endereco);

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
            }

        }
        public List<ProntuariosDomain> ListarTodos()
        {
            //crio uma lista para ser lida
            List<ProntuariosDomain> listaProntuarios = new List<ProntuariosDomain>();
            //Declaro a sql connection passando a string de conexao como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {

                //declaro a instrução a ser executada
                string querySelectAll = "SELECT idProntuario, idUsuario, dataNascimento, telefone, RG, CPF, endereco FROM Prontuarios";

                //abre a conexão com o bco de dados
                con.Open();
                //Declara o objeto que vai ler a tabela no bco de dados
                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                //enquanto houverem registros para serem lidos, o laço se repete
                while (rdr.Read())
                {
                    ProntuariosDomain prontuario = new ProntuariosDomain();

                    {
                        prontuario.idProntuario = Convert.ToInt32(rdr[0]);
                        prontuario.idUsuario = Convert.ToInt32(rdr[1]);
                        prontuario.dataNasc = Convert.ToDateTime(rdr[2]);
                        prontuario.telefone = rdr[3].ToString();
                        prontuario.RG = rdr[4].ToString();
                        prontuario.CPF = rdr[5].ToString();
                        prontuario.endereco = rdr[6].ToString();
                    };

                
                        listaProntuarios.Add(prontuario);
                    }
                }
            } return listaProntuarios;
        }
    }
}

     
    

       