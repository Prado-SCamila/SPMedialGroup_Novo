using Microsoft.AspNetCore.Authorization;
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
    //indica que a resposta da requisição será no formato Json
    [Produces("Application/Json")]

    [Route("api/[controller]")]
    [ApiController]
    public class ProntuariosController : ControllerBase
    {
        //declaro que o objeto _protuariorepository está conectado a interface IprontuarioRepository
        private IProntuarioRepository _prontuarioRepository { get; set; }

        //método contrutor que cria o objeto que conterá os métodos e que já foi declarado.
        public ProntuariosController()
        {
            _prontuarioRepository = new ProntuarioRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<ProntuariosDomain> listaProntuarios = _prontuarioRepository.ListarTodos();

            return Ok(listaProntuarios);
        }

        [Authorize(Roles = "medico")]
        [HttpPost]
        public IActionResult Post(ProntuariosDomain novoProntuario)
        {
            _prontuarioRepository.Cadastrar(novoProntuario);

            return StatusCode(201);
        }

        [Authorize(Roles = "medico")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)

        {
            _prontuarioRepository.Deletar(id);

            //vai retornar um NO-CONTENT
            return StatusCode(204);
        }

        [Authorize(Roles = "administrador, medico")]
        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            ProntuariosDomain prontuarioBuscado = _prontuarioRepository.BuscarPorId(id);

            if (prontuarioBuscado == null)
            {
                return NotFound("Nenhum prontuario foi encontrado");
            }
            return Ok(prontuarioBuscado);
        }

        [Authorize(Roles = "medico")]
        [HttpPut("{id}")]
        public IActionResult PutUrl(int id, ProntuariosDomain prontuarioAtualizado)
        {
            ProntuariosDomain prontuarioBuscado = _prontuarioRepository.BuscarPorId(id);

            if (prontuarioBuscado == null)
            {
                return NotFound
                    (new
                    {
                        mensagem = "prontuario não encontrado",
                        erro = true
                    }
                    );
            }

            try
            {
                _prontuarioRepository.AtualizarUrl(id, prontuarioAtualizado);

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        
        [Authorize(Roles = "medico")]
        [HttpPut]
            public IActionResult PutIdBody(ProntuariosDomain prontuarioAtualizado)
            {
                //cria o objeto prontuarioBuscado que irá receber o valor buscado no bco de dados
               ProntuariosDomain prontuarioBuscado = _prontuarioRepository.BuscarPorId(prontuarioAtualizado.idProntuario);

                //verifica se algo foi encontrado
                if (prontuarioBuscado != null)
                {
                    //se sim, atualiza o registro
                    try
                    {
                        _prontuarioRepository.AtualizarIdCorpo(prontuarioAtualizado);

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
                 mensagem = "Prontuario não encontrado"
             }
       );
        }
    }
}

