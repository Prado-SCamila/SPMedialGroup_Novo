import { Component } from "react";

export default class Consultamed extends Component{
  constructor(props){
    super(props);
    this.state = {
      // nomeEstado : valorInicialEstado
      listaConsultas : []
    };
  };

  buscarConsultas = () => {
    console.log('Esta função traz todos os atendimentos.');

    fetch('http://localhost:5000/api/consultas/medico', {
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

  componentDidMount(){
    this.buscarConsultas();
  };


  render(){
    return(
      <div>
        <h1>Minhas Consultas</h1>

        <section>
          <h2>Lista de Consultas</h2>

          <table>

            <thead>
              <tr>
                <th>#</th>
                <th>idConsulta</th>
                <th>Data da Consulta</th>
                <th>Situacao</th>
                <th>Prontuario</th>
                
              </tr>
            </thead>

            <tbody>

              {
                this.state.listaConsultas.map( (consulta) => {
                  return (
                    <tr key={consulta.idConsulta}>
                      <td>{consulta.idConsulta}</td>
                      <td>{Intl.DateTimeFormat("pt-BR", {
                        year: 'numeric', month: 'numeric', day: 'numeric',
                        hour: 'numeric', minute: 'numeric',
                        hour12: false
                      }).format(new Date(consulta.dataConsulta))}</td>
                      <td>{consulta.idSituacaoNavigation.nomeSituacao}</td>
                      <td>{consulta.idMedicoNavigation.nomeMedico}</td>
                    </tr>
                  )
                } )
              }

            </tbody>

          </table>
        </section>
      </div>
    )
  }
}

// export default Atendimentos;