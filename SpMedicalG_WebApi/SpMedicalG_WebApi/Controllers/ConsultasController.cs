using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpMedicalG_WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpMedicalG_WebApi.Controllers
{
    [Produces("Application/Json")]

    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        //declaro o objeto que vai receber os métodos
        private IConsultaRespository _consultaRepository { get; set; }

        // método construtor
        public ConsultasController()
        {
            //crio o objeto
            _consultaRepository = new ConsultaRespository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<ConsultasDomain> listaConsultas = _consultaRepository.ListarTodos();

            return Ok(listaConsultas);
        }

        [HttpPost]
        public IActionResult Post()
        {
            _consultaRepository.Cadastrar();

            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)

        {
            _consultaRepository.Delete(id);

            //vai retornar um NO-CONTENT
            return StatusCode(204);
        }

        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            ConsultasDomain consultaBuscada = _consultaRepository.BuscarPorId(id);

            if(consultaBuscada ==null)
            {
                return NotFound("Nenhuma consulta foi encontrada");
            }
            return Ok(consultaBuscada);
        }

    }
}
