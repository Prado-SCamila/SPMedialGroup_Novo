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
    [Produces("Application/Json")]

    [Route("api/[controller]")]
    [ApiController]
    public class ClinicaController : ControllerBase

    {
        private IClinicaRepository _clinicaRepository { get; set; }

        public ClinicaController()
        {
            _clinicaRepository = new ClinicaRepository();
        }


        [HttpPost("Cadastrar_Clinica")]
        public IActionResult Post(ClinicaDomain novaClinica)
        {
            _clinicaRepository.Cadastrar(novaClinica);

            return StatusCode(201);
        }

    }
}
