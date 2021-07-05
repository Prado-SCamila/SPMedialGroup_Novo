using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpMedicalG_WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpMedicalG_WebApi.Repositories;
using SpMedicalG_WebApi.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace SpMedicalG_WebApi.Controllers
{
    [Produces("Application/Json")]

    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        //declaro o objeto que vai receber os métodos
        private IConsultaRepository _consultaRepository { get; set; }

        // método construtor
        public ConsultasController()
        {
            //crio o objeto
            _consultaRepository = new ConsultaRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<ConsultasDomain> listaConsultas = _consultaRepository.ListarTodos();

            return Ok(listaConsultas);
        }

        [Authorize(Roles = "administrador")]
        [HttpPost]
        public IActionResult Post(ConsultasDomain novaConsulta)
        {
            _consultaRepository.Cadastrar(novaConsulta);

            return StatusCode(201);
        }

        [Authorize(Roles = "administrador")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)

        {
            _consultaRepository.Deletar(id);

            //vai retornar um NO-CONTENT
            return StatusCode(204);
        }

        [Authorize(Roles = "medico")]
        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            ConsultasDomain consultaBuscada = _consultaRepository.BuscarPorId(id);

            if (consultaBuscada == null)
            {
                return NotFound("Nenhuma consulta foi encontrada");
            }
            return Ok(consultaBuscada);
        }

        /// <summary>
        /// Atualiza uma consulta existente passando o id como parametro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="consultaAtualizada"></param>
        /// <returns> no content</returns>
        //colocar o id aqui na rota pois o método precisa do id como parametro

        [Authorize(Roles = "administrador")]
        [HttpPut("{id}")]
        public IActionResult PutUrl(int id, ConsultasDomain consultaAtualizada)
        {
            ConsultasDomain consultaBuscada = _consultaRepository.BuscarPorId(id);

            if (consultaBuscada == null)
            {
                return NotFound
                    (new
                    {
                        mensagem = "Usuario não encontrado",
                        erro = true
                    }
                    );
            }
            //tratamento de erros
            try
            {
                _consultaRepository.AtualizarUrl(id, consultaAtualizada);

                return NoContent();
            }

            catch (Exception)
            {
                return BadRequest();
            }

        }
        [Authorize(Roles = "medico")]
        [HttpPut]
        public IActionResult PutIdBody(ConsultasDomain consultaAtualizada)
        {
            //cria o objeto usuarioBuscado que irá receber o valor buscado no bco de dados
            ConsultasDomain consultaBuscado = _consultaRepository.BuscarPorId(consultaAtualizada.idConsulta);

            //verifica se algo foi encontrado
            if (consultaBuscado != null)
            {
                //se sim, atualiza o registro
                try
                {
                    _consultaRepository.AtualizarIdCorpo(consultaAtualizada);

                    return NoContent();

                }
                catch (Exception erro)
                {
                    return BadRequest(erro);
                }
                
            }
            //Caso não seja enconrtado, retorna o NOTFOUND com uma mensagem personalizada
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

