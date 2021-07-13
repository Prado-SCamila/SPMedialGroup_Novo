using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SpMedicalG_WebApi.Domains;
using SpMedicalG_WebApi.Interfaces;
using SpMedicalG_WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SpMedicalG_WebApi.Controllers
{

    //Controller respomsável pelos endpoints url

    //Define que a resposta da API será no formato Json
    [Produces("Application/json")]
    //Define que a rota de uma requisição será no formato domínio( www.site.com.br)/api/nomecontroller
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        //declaro o objeto que vai conter os métodos
        private IUsuarioRepository _usuarioRepository { get; set; }

        //Método construtor public ClasseController() {}
        public UsuariosController()
        {
            //crio o objeto que vai conter os metodos (instancio)
            _usuarioRepository = new UsuarioRepository();
        }
        

        //------------------------------------------------------------------------------------------------------
        //------------------------------------
        [Authorize(Roles = "administrador")]
    
        [HttpGet("lista_de_medicos")]
        public IActionResult Get()
        {
            List<UsuariosDomain> listaUsuarios = _usuarioRepository.ListarMedicos();

            // retorna o status code ok e uma lista no formato json
            return Ok(listaUsuarios);
        }


        //--------------------------------------------------------------------------------------------------------
        //--------------------FUNCIONALIDADE 1 - ADM CADASTRA NOVO USUÁRIO

        [Authorize(Roles = "administrador")]
        [HttpPost("cadastrar_usuario")]
        public IActionResult Post(UsuariosDomain novoUsuario)
        {
            // o objeto que contém os métodos irá chamar o método cadastrar
            _usuarioRepository.Cadastrar(novoUsuario);

            return StatusCode(201);
        }


        [Authorize(Roles = "administrador")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)

        {
            _usuarioRepository.Delete(id);

            //vai retornar um NO-CONTENT
            return StatusCode(204);
        }


        [Authorize(Roles = "administrador")]
        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            UsuariosDomain usuarioBuscado = _usuarioRepository.BuscarPorId(id);

            if (usuarioBuscado == null)
            {
                return NotFound("Nenhum usuario foi encontrado");
            }
            return Ok(usuarioBuscado);
        }

        [Authorize(Roles = "administrador")]
        [HttpPut("{id}")]
        public IActionResult PutUrl(int id, UsuariosDomain usuarioAtualizado)
        {
            UsuariosDomain usuarioBuscado = _usuarioRepository.BuscarPorId(id);

            if (usuarioBuscado == null)
            {
                return NotFound
                    (new
                    {
                        mensagem = "Usuario não encontrado",
                        erro = true
                    }
                    );
            }

            try
            {
                _usuarioRepository.AtualizarUrl(id, usuarioAtualizado);

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        // método para fazer Login

        
        [HttpPost("login")]
        public IActionResult Login(UsuariosDomain login)
        {
            UsuariosDomain usuarioBuscado = _usuarioRepository.Login(login.email, login.senha);

            if (usuarioBuscado == null)
            {
                return NotFound("Email ou senha inválido");
            }

            //inserir token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.email.ToString()),

                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.idUsuario.ToString()),

                new Claim(ClaimTypes.Role, usuarioBuscado.permissao.ToString()),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Usuario-chave-autenticacao"));

            //Define as credenciais do Token
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Gera o Token
            var token = new JwtSecurityToken
                (
                issuer: "SpMedicalG_WebApi", //emissor do Token
                audience: "SpMedicalG_WebApi", //destinatario do token
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
                );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    



        [Authorize(Roles = "administrador")]
        //associado ao método AtualizarIdCorpo no repositório
        [HttpPut]
        public IActionResult PutIdBody(UsuariosDomain usuarioAtualizado)
        {
            //cria o objeto usuarioBuscado que irá receber o valor buscado no bco de dados
            UsuariosDomain usuarioBuscado = _usuarioRepository.BuscarPorId(usuarioAtualizado.idUsuario);

            //verifica se algo foi encontrado
            if (usuarioBuscado != null)
            {
                //se sim, atualiza o registro
                try
                {
                    _usuarioRepository.AtualizarIdCorpo(usuarioAtualizado);

                    return NoContent();

                }
                catch (Exception erro)
                {
                    return BadRequest(erro);
                }

            }
         return NotFound
         (
            new
           {
             erro = true,
             mensagem = "Consulta não encontrada"
           }
         );

        }
    }
}

