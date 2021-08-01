import { Component } from "react";
import axios from 'axios';
import { parseJwt } from '../services/auth';

export default class Login extends Component{
  constructor(props){
    super(props);
    this.state = {
      email : '',
      senha : ''
    };
  };

  efetuaLogin = (event) => {
    event.preventDefault();

    axios.post('http://localhost:5000/api/login', {
      email : this.state.email,
      senha : this.state.senha
    })

    .then(resposta => {
      if (resposta.status === 200) {
        localStorage.setItem('usuario-login', resposta.data.token)
        console.log('Meu token é: ' + resposta.data.token)

        if (parseJwt().role === "1") {
          console.log(this.props)
          this.props.history.push('/cadastroConsultas')
        }

        if (parseJwt().role === "2") {
          console.log(this.props)
          this.props.history.push('/minhasConsultas_med')
        }
        else {
          this.props.history.push('/minhasConsultas_pac')
        }
      }
    })

    .catch(erro => console.log(erro));
  };

  atualizaStateCampo = (campo) => {
    //   exemplo          email         :     adm@adm.com
    this.setState({ [campo.target.name] : campo.target.value })
  };

  render(){
    return(
      <div>
        <h1>Login</h1>

        <section>
          <form onSubmit={this.efetuaLogin}>
            
            <input 
              // E-mail
              type="text"
              name="email"
              value={this.state.email}
              onChange={this.atualizaStateCampo}
              placeholder="username"
            />

            <input 
              // Senha
              type="password"
              name="senha"
              value={this.state.senha}
              onChange={this.atualizaStateCampo}
              placeholder="password"
            />

            <button type="submit">
              Login
            </button>

          </form>
        </section>
      </div>
    )
  }
}