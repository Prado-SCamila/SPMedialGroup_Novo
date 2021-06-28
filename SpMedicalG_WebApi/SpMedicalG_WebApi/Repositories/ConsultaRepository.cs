using SpMedicalG_WebApi.Domains;
using SpMedicalG_WebApi.Interfaces;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public void Cadastrar(ConsultasDomain novaConsulta)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int id)
        {
            throw new NotImplementedException();
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

