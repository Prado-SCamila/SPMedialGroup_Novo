using Microsoft.AspNetCore.Authorization;
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
        private string stringConexao = "Data Source= DESKTOP-840P8H6; initial catalog=SPmed; user id=sa; pwd= miladori23";

        //----------------------------------------------------------------------------------------------
        // 
        public List<ConsultasDomain> ListarTodos()
        {
            List<ConsultasDomain> listaConsultas = new List<ConsultasDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {

                string querySelectAll = "SELECT idConsulta, idProntuario, idMedico, dataConsulta, idsituacao ,descricao ,idUsuario  FROM Consultas";

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
                            consulta.descricao = rdr[5].ToString();
                            consulta.idUsuario = Convert.ToInt32(rdr[6]);

                        };

                        listaConsultas.Add(consulta);
                    }
                }
            }
            return listaConsultas;
        }
        //------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------
        //--------FUNCIONALIDADE 6 - MÉDICO INCLUI DESCRIÇÃO NA CONSULTA
        public ConsultasDomain AtualizarDescricao (int id, ConsultasDomain consulta)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl = "UPDATE Consultas SET descricao = @descricao WHERE idProntuario = @ID";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    cmd.Parameters.AddWithValue("@descricao", consulta.descricao);
                    
                    con.Open();

                    //executa o comando
                    cmd.ExecuteNonQuery();
                }

                ConsultasDomain consultaAtualizada = BuscaConsulta(id);

                return consultaAtualizada;
            }
        }
        
        //--------------------------------------------------------------------------------------------------
       //Método para paciente buscar consultas dele no sistema
        public ConsultasDomain BuscaConsulta(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                
                string querySelectById = "SELECT dataConsulta AS [Data da Consulta], nomeMedico AS[Médico], situacao AS[Status] FROM Consultas INNER JOIN Prontuarios ON Prontuarios.idProntuario = Consultas.idProntuario INNER JOIN Usuarios ON Usuarios.idUsuario = Consultas.idUsuario INNER JOIN Situacao ON Situacao.idSituacao = Consultas.idSituacao INNER JOIN Medicos ON Medicos.idMedico = Consultas.idMedico WHERE nome = @nome";

                con.Open();

                SqlDataReader xx;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    xx = cmd.ExecuteReader();
                    if(xx.Read())
                    {
                        ConsultasDomain consultasPaciente = new ConsultasDomain()
                        {
                            dataConsulta = Convert.ToDateTime(xx[0]),
                        

                            medico = new MedicosDomain()
                            {
                               nomeMedico = xx[1].ToString()
                            },

                            situacao = new SituacaoDomain()
                            {
                                situacao = xx[2].ToString(),
                            },


                           //Terminar no controller esse metodo e fazer tokens.

                        };
                        return consultasPaciente;
                    }
                    return null;
                }

            }
               
        }
        //------------------------------------------------------------------------------------------------------
        // Método para o médico ver os agendamentos vinculados a ele
        public ConsultasDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT  nome, dataConsulta, situacao ,descricao FROM Consultas INNER JOIN Usuarios ON Usuarios.idUsuario = Consultas.idUsuario INNER JOIN Situacao ON Situacao.idSituacao = Consultas.idSituacao WHERE idMedico = 3";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@idMedico", id);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        //crio um objeto do tipo ConsultasDomain para conter os valores lidos
                        ConsultasDomain consultaBuscada = new ConsultasDomain()
                        {
                            nomeUsuario = new UsuariosDomain
                            {
                                nomeUsuario = rdr[0].ToString(),
                            },

                            dataConsulta = Convert.ToDateTime(rdr[1]),
                            
                            situacao = new SituacaoDomain
                            {
                                situacao = rdr[2].ToString(),
                            },

                            descricao = rdr[3].ToString(),
                            
                        };

                        //Se algo for encontrado, retorna o que foi buscado
                        return consultaBuscada;
                    }

                    //se nada for encontrado, devolve null
                    return null;
                }
            }
        }
    
        //----------------------------------------------------------------------------------------------------------
        // Método para ADM cadastrar nova consulta
        //
        public void Cadastrar(ConsultasDomain novaConsulta)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO Consultas(idConsulta, idProntuario, idMedico, dataConsulta, idSituacao, idUsuario) VALUES (@idConsulta, @idProntuario, @idMedico, @dataConsulta, @idSituacao, @idUsuario)";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    //passa o valor inserido para o parametro 
                    cmd.Parameters.AddWithValue("@idConsulta", novaConsulta.idConsulta);
                    cmd.Parameters.AddWithValue("@idProntuario", novaConsulta.idProntuario);
                    cmd.Parameters.AddWithValue("@idMedico", novaConsulta.idMedico);
                    cmd.Parameters.AddWithValue("@dataConsulta", novaConsulta.dataConsulta);
                    cmd.Parameters.AddWithValue("@idSituacao", novaConsulta.idSituacao);
                    cmd.Parameters.AddWithValue("@idUsuario", novaConsulta.idUsuario);

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

        
    }
}






