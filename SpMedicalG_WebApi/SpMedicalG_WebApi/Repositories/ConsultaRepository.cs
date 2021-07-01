using SpMedicalG_WebApi.Domains;
using SpMedicalG_WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SpMedicalG_WebApi.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        private string stringConexao = "Data Source=DESKTOP-840P8H6; initial catalog=SPmed;user id=sa;pwd=miladori23";

        public void AtualizarIdCorpo(ConsultasDomain consulta)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateIdBody = "UPDATE Consultas SET idUsuario = @idUsuario, idProntuario= @idProntuario, idMedico = @idMedico, dataConsulta = @dataConsulta, idSituacao = @idSituacao, descricao = @descricao WHERE idConsulta = @ID";

                using (SqlCommand cmd = new SqlCommand(queryUpdateIdBody, con))
                {
                    cmd.Parameters.AddWithValue("@idConsulta", consulta.idConsulta);
                    cmd.Parameters.AddWithValue("@idProntuario", consulta.idProntuario);
                    cmd.Parameters.AddWithValue("@idMedico", consulta.idMedico);
                    cmd.Parameters.AddWithValue("@dataConsulta", consulta.dataConsulta);
                    cmd.Parameters.AddWithValue("@idSituacao", consulta.idSituacao);
                    cmd.Parameters.AddWithValue("@descricao", consulta.descricao);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarUrl(int id, ConsultasDomain consulta)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl = "UPDATE Consultas SET situacao = @situacao WHERE idConsulta = @ID";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@idSituacao", consulta.idSituacao);

                    con.Open();

                    //executa o comando
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public ConsultasDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT idConsulta, dataConsulta FROM Consultas WHERE idConsulta = @id";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        ConsultasDomain consultaBuscada = new ConsultasDomain()
                        {
                            idConsulta = Convert.ToInt32(rdr[0]),
                            idProntuario = Convert.ToInt32(rdr[1]),
                            idMedico = Convert.ToInt32(rdr[2]),
                            dataConsulta = Convert.ToDateTime(rdr[3]),
                            idSituacao = Convert.ToInt32(rdr[4]),
                            descricao = rdr[5].ToString(),
                        };
                        //Se algo for encontrado, retorna o que foi buscado
                        return consultaBuscada;
                    }

                    //se nada for encontrado, devolve null
                    return null;
                }
            }
        }
    

        public void Cadastrar(ConsultasDomain novaConsulta)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO Consultas(dataConsulta) VALUES (@dataConsulta)";//??

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    //passa o valor inserido para o parametro 
                    cmd.Parameters.AddWithValue("@dataConsulta", novaConsulta.dataConsulta);

                    //abre a conexao
                    con.Open();

                    //Executa a Query
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM Consultas Where idConsulta = @ID";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<ConsultasDomain> ListarTodos()
        {
            List<ConsultasDomain> listaConsultas = new List<ConsultasDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {

                string querySelectAll = "SELECT idConsulta,idProntuario, idMedico, dataConsulta, idsituacao, descricao FROM Consultas";

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
                        ConsultasDomain consulta = new ConsultasDomain();

                        {
                            consulta.idConsulta = Convert.ToInt32(rdr[0]);
                            consulta.idProntuario = Convert.ToInt32(rdr[1]);
                            consulta.idMedico = Convert.ToInt32(rdr[2]); 
                            consulta.dataConsulta = Convert.ToDateTime(rdr[3]);
                            consulta.idSituacao = Convert.ToInt32(rdr[4]); ;
                            consulta.descricao =rdr[5].ToString();
                        };

                        listaConsultas.Add(consulta);
                    }
                }
            }
            return listaConsultas;
        } 
    }
}






