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
        [HttpPost]
        public IActionResult Post()
        {
            _prontuarioRepository.Cadastrar();

            return StatusCode(201);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)

        {
            _prontuarioRepository.Deletar(id);

            //vai retornar um NO-CONTENT
            return StatusCode(204);
        }

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
    }
}
