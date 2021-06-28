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
    ///<summary>
    ///Controller respomsável pelos endpoints url
    //////<summary>
    

    //Define que a resposta da API será no formato Json
    [Produces("application/json")]
    //Define que a rota de uma requisição será no formato domínio( www.site.com.br)/api/nomecontroller
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public UsuariosController()
        {
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
    }

}
