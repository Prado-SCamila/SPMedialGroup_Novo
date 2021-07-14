import { render } from '@testing-library/react';
import React, {Component} from 'react';
import axios from 'axios';

class Login extends Component
{
    constructor(props){
         super(props);
         this.state = {
          email : '',
          senha:'',
          erroMensagem: ''
         }
        }
    
    }
    //Função que faz a chamada para a API realizar o login
        efetuaLogin = (event) =>{
            //ignora o comportamento padrão do navegador de recarregar a página
            event.PreventDefault();

            //Define a URl e o corpo da requisição
            axios.post('http://localhost:5000/api/login',{
            email : this.state.email,
            senha: this.state.senha
        })
        
        //resposta da requisição
        .then(resposta => {
            if (resposta.status === 200) {
                localStorage.setItem('usuario-login', resposta.data.token);
                console.log('Meu token é: ' + resposta.data.token);
            }
        })
    

        .catch( () => {
            this.setState( { erroMensagem : "email ou senha inválidos"}))
        
        }
        


        //função que a atualiza o state de acordo com o input
        atualizastateCampo = (campo) => {
            this.setState ({ [campo.target.name] : [campo.target.value]})
        
        
    

//------------Aqui eu insiro as tags html 
//posso chamar as variáveis que eu criei
render(){
    return(
        <div>
            <main>
                <section>
                    <h2> Tela de Login</h2>              

                    <form onSubmit = {this.efetuaLogin}>
                         <label htmlFor="email"> Digite seu Email </label>
                         <input type = "text" name = "email" value = {this.state.email}
                         onChange= {this.AtualizaStateCampo}
                         placeholder="email"/>

                        <label htmlFor="pwd"> Digite sua Senha</label>
                         <input type = "password" name = "senha" value = {this.state.senha}
                         onChange= {this.AtualizaStateCampo}
                         placeholder="email"/>
                     
                     <p style = {{ color: 'red'}}>{this.state.erroMensagem}</p>

                     <button type ="submit"> Login </button>
                     
                     </form>
                </section>
          </main>
        </div>
    );
    }
    //nome dos inputs na form deve ser igual aos nomes dados aos states. no construtor.



export default Login;

