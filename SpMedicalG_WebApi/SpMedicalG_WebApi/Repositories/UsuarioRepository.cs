using SpMedicalG_WebApi.Domains;
using SpMedicalG_WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace SpMedicalG_WebApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string stringConexao = "Data Source= DESKTOP-840P8H6; initial catalog= SPmed;user id=sa;pwd=miladori23";
        /// <summary>
        /// Atualiza um usuario passando um id pelo corpo da requisição
        /// </summary>
        /// <param name="usuario">Objeto usuario com as novas informações</param>
        public void AtualizarIdCorpo(UsuariosDomain usuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateIdBody = "Update Usuarios SET nome = @nomeUsuario, senha = @senha, email = @email,idPermissao = @idPermissao WHERE idUsuario = @ID";

                using (SqlCommand cmd = new SqlCommand(queryUpdateIdBody, con))
                {
                    cmd.Parameters.AddWithValue("@nomeUsuario", usuario.nomeUsuario);
                    cmd.Parameters.AddWithValue("@email", usuario.email);
                    cmd.Parameters.AddWithValue("@senha", usuario.senha);
                    cmd.Parameters.AddWithValue("@idPermissao", usuario.idPermissao);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarUrl(int id, UsuariosDomain usuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdateUrl = "UPDATE Usuarios SET nome = @nomeUsuario, email = @email,senha = @senha, idPermissao = @idPermissao WHERE idUsuario = @ID";

                using (SqlCommand cmd = new SqlCommand(queryUpdateUrl, con))
                {
                    cmd.Parameters.AddWithValue("@nomeUsuario", usuario.nomeUsuario);
                    cmd.Parameters.AddWithValue("@email", usuario.email);
                    cmd.Parameters.AddWithValue("@senha", usuario.senha);
                    cmd.Parameters.AddWithValue("@idPermissao", usuario.idPermissao);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public UsuariosDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT idUsuario,idTipoUsuario, nome, email, senha, idPermissao FROM Usuarios WHERE idUsuario = @id";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        UsuariosDomain usuarioBuscado = new UsuariosDomain()
                        {
                            idUsuario = Convert.ToInt32(rdr[0]),
                            idTipoUsuario = Convert.ToInt32(rdr[1]),
                            nomeUsuario = rdr[2].ToString(),
                            email = rdr[3].ToString(),
                            senha = rdr[4].ToString(),
                            idPermissao = Convert.ToInt32(rdr[5]),
                        };
                        //Se algo for encontrado, retorna o que foi buscado
                        return usuarioBuscado;
                    }
                    //se nada for encontrado, devolve null
                    return null;
                }
            }
        }

        public void Cadastrar(UsuariosDomain novoUsuario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO Usuarios(nome), (idTipoUsuario),(email), (senha), (idPermissao) VALUES (@nomeUsuario),(@idTipoUsuario),(@email),(@senha), (@idPermissao)";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@nome", novoUsuario.nomeUsuario);
                    cmd.Parameters.AddWithValue("@idTipoUsuario", novoUsuario.idTipoUsuario);
                    cmd.Parameters.AddWithValue("@email", novoUsuario.email);
                    cmd.Parameters.AddWithValue("@senha", novoUsuario.senha);
                    cmd.Parameters.AddWithValue("@idPermissao",novoUsuario.idPermissao);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM Usuarios Where id=@ID";
                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<UsuariosDomain> ListarTodos()
        {
            //crio uma lista para ser lida
            List<UsuariosDomain> listaUsuarios = new List<UsuariosDomain>();
            //Declaro a sql connection passando a string de conexao como parametro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {

                //declaro a instrução a ser executada
                string querySelectAll = "SELECT idUsuario,idTipoUsuario, nome, email, senha, idPermissao FROM Usuarios";

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
                        UsuariosDomain usuario = new UsuariosDomain();

                        {
                            usuario.idUsuario = Convert.ToInt32(rdr[0]);
                            usuario.idTipoUsuario = Convert.ToInt32(rdr[1]);
                            usuario.nomeUsuario = rdr[2].ToString();
                            usuario.email = rdr[3].ToString();
                            usuario.senha = rdr[4].ToString();
                            usuario.idPermissao = Convert.ToInt32(rdr[5]);
                        };

                        listaUsuarios.Add(usuario);
                    }
                }
            }
            return listaUsuarios;
        }
        public UsuariosDomain Login(string email, string senha)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelect = "SELECT email, senha FROM  Usuarios WHERE email = @email AND senha = @senha";

                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@senha", senha);
                                      
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if(rdr.Read())
                    {
                        UsuariosDomain usuarioBuscado = new UsuariosDomain()
                        {
                            email = rdr[0].ToString(),
                            senha = rdr[1].ToString(),
                            //crio um objeto do tipo permissao para receber os valores do atributo permissao de outra entidade(classe ou tabela do bco)
                            
                        };
                        return usuarioBuscado;
                    } 
                }return null;
            }
        }
    }
}



       