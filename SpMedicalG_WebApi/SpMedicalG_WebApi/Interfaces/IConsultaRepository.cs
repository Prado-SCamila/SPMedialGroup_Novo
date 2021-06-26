﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpMedicalG_WebApi.Domains;

namespace SpMedicalG_WebApi.Interfaces
{
    interface IConsultaRepository
    {
        List<ConsultasDomain> ListarTodos();

        ConsultasDomain BuscarPorId (int id);

        void Cadastrar(ConsultasDomain novaConsulta);

        void AtualizarIdCorpo(ConsultasDomain consulta);

        void AtualizarUrl(int id, ConsultasDomain consulta);

        void Deletar(int id);
    }
}