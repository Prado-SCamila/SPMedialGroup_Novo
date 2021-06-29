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
        private string stringConexao= "Data Source=DESKTOP-840P8H6\\SQLEXPRESS; initial catalog=Spmed;user id=sa;pwd=miladori23"

        public void AtualizarIdCorpo(ConsultasDomain consulta)
        {
            throw new NotImplementedException();
        }

        public void AtualizarUrl(int id, ConsultasDomain consulta)
        {
            throw new NotImplementedException();
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
                            dataConsulta = rdr.["dataConsulta"].ToString()
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

                using (SqlCommand cmd = new SqlCommand(queryDelete,con))
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

            using (SqlConnection con = new SqlConnection (stringConexao))
            { 

            using querySelectAll = "SELECT idConsulta, dataConsulta, situacao, descricao FROM Consultas";

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
                ConsultasDomain consulta = new ConsultasDomain();

                {
                    idConsulta = Convert.ToInt32(rdr[0]);
                    consulta = rdr[1].ToString();
                };

                listaConsultas.Add(consultas);
            }
        }
    }
    return listaConsultas;
}
    }

