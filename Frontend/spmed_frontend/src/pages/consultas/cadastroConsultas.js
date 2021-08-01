import { Component } from "react";
import axios from "axios";

export default class Consulta extends Component{
  constructor(props){
    super(props);
    this.state = {
      // nomeEstado : valorInicialEstado
      listaCosultas : [],
      listaMedicos : [],
      listaProntuarios : [],
      listaSituacao: [],
      idConsulta : 0,
      idMedico : 0,
      idUSuario: 0,
      descricao: '',
      nomeMedico : '',
      data : new Date(),
      hora : '',
      idSituacao : 0,
      idProntuario: 0
    };
  };

  buscarConsultas = () => {
    console.log('Esta função traz todos os atendimentos.');

    fetch('http://localhost:5000/api/consulta', {
      headers : {
        'Authorization' : 'Bearer ' + localStorage.getItem('usuario-login')
      }
    })

    .then(resposta => {
      if (resposta.status !== 200) {
        throw Error();
      };

      return resposta.json();
    })

    .then(resposta => this.setState({ listaConsultas : resposta }))
    
    .catch(erro => console.log(erro));
  };

  buscarUsuarios = () => {
    fetch('http://localhost:5000/api/usuarios', {
      headers : {
        'Authorization' : 'Bearer ' + localStorage.getItem('usuario-login')
      }
    })

    .then(resposta => {
      if (resposta.status !== 200) {
        throw Error();
      };

      return resposta.json();
    })

    .then(resposta => this.setState({ listaMedicos : resposta }))
    
    .catch(erro => console.log(erro));
  };

  buscarSituacao = () => {
    fetch('http://localhost:5000/api/situacao', {
      headers : {
        'Authorization' : 'Bearer ' + localStorage.getItem('usuario-login')
      }
    })

    .then(resposta => {
      if (resposta.status !== 200) {
        throw Error();
      };

      return resposta.json();
    })

    .then(resposta => this.setState({ listaSituacao : resposta }))
    
    .catch(erro => console.log(erro));
  };

  buscarMedicos = () => {
    fetch('http://localhost:5000/api/medicos', {
      headers : {
        'Authorization' : 'Bearer ' + localStorage.getItem('usuario-login')
      }
    })

    .then(resposta => {
      if (resposta.status !== 200) {
        throw Error();
      };

      return resposta.json();
    })

    .then(resposta => this.setState({ listaMedicos : resposta }))
    
    .catch(erro => console.log(erro));
  };

  buscarProntuarios = () => {
    fetch('http://localhost:5000/api/prontuarios', {
      headers : {
        'Authorization' : 'Bearer ' + localStorage.getItem('usuario-login')
      }
    })

    .then(resposta => {
      if (resposta.status !== 200) {
        throw Error();
      };

      return resposta.json();
    })

    .then(resposta => this.setState({ listaMedicos: resposta }))
    
    .catch(erro => console.log(erro));
  };

  componentDidMount(){

    this.buscarConsultas();
    this.buscarUsuarios();
    this.buscarProntuarios();
  };

  cadastrarConsulta = (event) => {
    event.preventDefault();

    let novaConsulta = {
      // chave (como está na API) : valor que será cadastrado
      idConsulta           :     this.state.idConsulta,
      idProntuario         :     this.state.idProntuario,
      idMedico             :     this.state.idMedico,
      dataConsulta         :     this.state.data + 'T' + this.state.hora,
      idSituacao           :     this.state.idSituacao,
      idUsuario            :     this.state.idUSuario

    };

    axios.post('http://localhost:5000/api/consulta', novaConsulta, {
      headers : {
        'Authorization' : 'Bearer ' + localStorage.getItem('usuario-login')
      }
    })

    .then(resposta => {
      if (resposta.status === 201) {
        console.log('Uma nova Consulta foi agendada!');
      }
    })

    .catch(erro => console.log(erro))

    .then(this.buscarConsultas);
  };

  atualizaStateCampo = (campo) => {
    //   exemplo       idVeterinario    :         1
    this.setState({ [campo.target.name] : campo.target.value })
  };

  render(){
    return(
      <div>
        <h1>Consultas</h1>

        <section>
          <h2>Cadastro de Consultas</h2>

          <table>

            <thead>
              <tr>
                <th>#</th>
                <th>Consulta</th>
                <th>Prontuario</th>
                <th>Médico</th>
                <th>Data da Consulta</th>
                <th>Situação</th>
                
              </tr>
            </thead>

            <tbody>

              {
                this.state.listaConsultas.map( (consulta) => {
                  return (
                    <tr key={consulta.idConsulta}>
                      <td>{consulta.idProntuario}</td>
                      <td>{consulta.idMedicoNavigation.nomeMedico}</td>
                      <td>{consulta.idConsultaNavigation.dataConsulta}</td>
                      <td>{Intl.DateTimeFormat("pt-BR", {
                        year: 'numeric', month: 'numeric', day: 'numeric',
                        hour: 'numeric', minute: 'numeric',
                        hour12: false
                      }).format(new Date(consulta.dataConsulta))}</td>
                      <td>{consulta.idSituacaoNavigation.idSituacao}</td>
                    
                    </tr>
                  )
                } )
              }

            </tbody>

          </table>
        </section>

        <section>
          <h2>Cadastro de Consulta</h2>

          {/* Formulário de cadastro */}
          <form onSubmit={this.cadastrarConsulta}>

    
            <select
              name="idConsulta"
              value={this.state.idConsulta}
              onChange={this.atualizaStateCampo}
            >
              <option value="0">Selecione o id da Consulta</option>

              {
                this.state.listaConsultas.map( (consulta) => {
                  return(
                    <option key={consulta.idConsulta} value={consulta.idConsulta}></option>
                  )
                } )
              }
              
            </select>

              <select
              name="idProntuario"
              value={this.state.idProntuario}
              onChange={this.atualizaStateCampo}
            >
              <option value="0">Selecione o prontuario do paciente</option>

              {
                this.state.listaProntuarios.map( (prontuario) => {
                  return(
                    <option key={prontuario.idProntuario} value={prontuario.idProntuario}>{prontuario.rg} - {prontuario.cpf}</option>
                  )
                } )
              }
              
            </select>

            <select
              name="idMedico"
              value={this.state.idMedico}
              onChange={this.atualizaStateCampo}
            >
              <option value="0">Selecione o Médico que fará o atendimento</option>

              {
                this.state.listaMedicos.map( (medico) => {
                  return(
                    <option key={medico.idMedico} value={medico.idMedico}>{medico.nomeMedico}</option>
                  )
                } )
              }
              
            </select>

            <input 
              // Data do atendimento
              type="date"
              name="data"
              value={this.state.data}
              onChange={this.atualizaStateCampo}
            />

            <input 
              // Hora do atendimento
              type="time"
              name="hora"
              value={this.state.hora}
              onChange={this.atualizaStateCampo}
            />

             <select
              name="idSituacao"
              value={this.state.idSituacao}
              onChange={this.atualizaStateCampo}
            >
              <option value="0">Selecione a situação do atendimento</option>

              {
                this.state.listaSituacao.map( (situacao) => {
                  return(
                    <option key={situacao.idSituacao} value={situacao.idSituacao}>{situacao.nomeSituacao}</option>
                  )
                } )
              }
              
            </select>

            <button type="submit">
              Cadastrar
            </button>

          </form>

        </section>
      </div>
    )
  }
}

// export default Atendimentos;