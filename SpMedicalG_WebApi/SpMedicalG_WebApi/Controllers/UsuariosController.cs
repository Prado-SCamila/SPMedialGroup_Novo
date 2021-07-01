using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpMedicalG_WebApi.Domains;
using SpMedicalG_WebApi.Interfaces;
using SpMedicalG_WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// Lista todos os usuarios
        /// </summary>
        /// <returns> Retorna uma Lista e um Status Code</returns>
        [HttpGet]
        public IActionResult Get()
        {
            List<UsuariosDomain> listaUsuarios = _usuarioRepository.ListarTodos();

            // retorna o status code ok e uma lista no formato json
            return Ok(listaUsuarios);
        }
        //metodo cadastrar irá retornar status code 201- created
        [HttpPost]
        public IActionResult Post(UsuariosDomain novoUsuario)
        {
            // o objeto que contém os métodos irá chamar o método cadastrar
            _usuarioRepository.Cadastrar(novoUsuario);

            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)

        {
            _usuarioRepository.Delete(id);

            //vai retornar um NO-CONTENT
            return StatusCode(204);
        }
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

