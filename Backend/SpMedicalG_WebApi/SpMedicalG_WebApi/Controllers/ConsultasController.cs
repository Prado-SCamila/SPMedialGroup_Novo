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

        //------------------------------------------------------------------------------------------
        //---A

        [HttpGet("lista_consultas/todas")]
        public IActionResult Get()
        {
            List<ConsultasDomain> listaConsultas = _consultaRepository.ListarTodos();

            return Ok(listaConsultas);
        }


        //--------------------------------------------------------------------------------------------------------
        //-----FUNCIONALIDADE 2- ADM AGENDA CONSULTA

        [Authorize(Roles = "administrador")]
        [HttpPost("cadastro_de_consultas")]
        public IActionResult Post(ConsultasDomain novaConsulta)
        {
            _consultaRepository.Cadastrar(novaConsulta);

            return StatusCode(201);
        }

        //---------------------------------------------------------------------------------------------------------
        //------FUNCIONALIDADE 3- ADM CANCELA AGENDAMENTO
        [Authorize(Roles = "administrador")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)

        {
            _consultaRepository.Deletar(id);

            //vai retornar um NO-CONTENT
            return StatusCode(204);
        }

        //--------------------------------------------------------------------------------------------------------
        //------FUNCIONALIDADE 5 - MÉDICO VISUALIZA CONSULTAS VINCULADAS A ELE

        [Authorize(Roles = "medico")]
        [Route("lista_de_consultas/medico")]
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

        //-----------------------------------------------------------------------------------------------------
        //-------------------------FUNCIONALIDADE 6 - MÉDICO INCLUI DESCRICAO NA CONSULTA DO PACIENTE
        [Authorize(Roles = "medico")]
        [HttpPut("{id}")]
        public IActionResult PutUrl(int id, ConsultasDomain consultaAtualizada)
        {
            ConsultasDomain consultaBuscada = _consultaRepository.AtualizarDescricao(id, consultaAtualizada);

            if (consultaBuscada == null)
            {
                return NotFound
                    (new
                    {
                        mensagem = "consulta não encontrada",
                        erro = true
                    }
                    );
            }
            //tratamento de erros
            try
            {
                _consultaRepository.AtualizarDescricao(id, consultaAtualizada);

                return NoContent();
            }

            catch (Exception)
            {
                return BadRequest();
            }

        }
        //-------------------------------------------------------------------------------------------------------
        //----FUNCIONALIDADE 7 - PACIENTE VISUALIZA SUAS CONSULTAS AGENDADAS

        [HttpGet("{id}")]
        [Route("lista_de_consultas/paciente")]
        public IActionResult GetId(int id)
        {
            ConsultasDomain consultasPaciente = _consultaRepository.BuscaConsulta(id);

            if (consultasPaciente == null)
            {
                return NotFound("Você não tem consultas agendadas");
            }
            return Ok(consultasPaciente);
        }
    }
}
              //  Preciso do Método para atualizar a descricao pelo idBody
              //  FALTA INCLUIR FUNCIONALIDADES 1 e 4


        /*------FUNCIONALIDADE 6 - MÉDICO INCLUI DESCRIÇÃO NA CONSULTA
        [Authorize(Roles = "medico")]
        [HttpPut("atualizar_consulta")]
        public IActionResult PutIdBody(ConsultasDomain consulta)
        {
            //cria o objeto usuarioBuscado que irá receber o valor buscado no bco de dados
            ConsultasDomain consultaBuscada = _consultaRepository.BuscarPorId(consulta.idProntuario);

            //verifica se algo foi encontrado
            if (consultaBuscada != null)
            {
                //se sim, atualiza o registro
                try
                {
                    _consultaRepository.AtualizarDescricao(consulta);

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
}*/

