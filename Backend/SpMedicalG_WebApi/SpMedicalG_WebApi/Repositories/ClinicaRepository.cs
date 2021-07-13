using SpMedicalG_WebApi.Domains;
using SpMedicalG_WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SpMedicalG_WebApi.Repositories
{
    public class ClinicaRepository : IClinicaRepository

    {

        private string stringConexao = "Data Source= DESKTOP-840P8H6; initial catalog=SPmed; user id=sa; pwd= miladori23";


        public ClinicaDomain AtualizarClinicaUrl(int id, ClinicaDomain clinica)
        {
            throw new NotImplementedException();
        }

        public ClinicaDomain BuscarClinicaPorId(int id)
        {
            throw new NotImplementedException();
        }
        //-----------------------------------------------------------------------------------------------------------------
        public void Cadastrar(ClinicaDomain novaClinica)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO Clinica (nomeFantasia),(cnpj),(razaoSocial),(endereco) VALUES (@nomeFantasia),(@cnpj),(@razaoSocial),(@endereco)";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@nomeFantasia", novaClinica.nomeFantasia);
                    cmd.Parameters.AddWithValue("@cnpj", novaClinica.cnpj);
                    cmd.Parameters.AddWithValue("@razaoSocial", novaClinica.razaoSocial);
                    cmd.Parameters.AddWithValue("@endereco", novaClinica.endereco);

                    con.Open();
                    cmd.ExecuteReader();
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------------

        public void DeletarClinica(int id)
        {
            throw new NotImplementedException();
        }

        public List<ClinicaDomain> ListarClinicas()
        {
            throw new NotImplementedException();
        }
    }
}
