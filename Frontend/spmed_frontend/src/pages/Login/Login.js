import { render } from '@testing-library/react';
import {Component} from 'react';

class Login extends Component
{
    constuctor(props){
         super(props);
         this.state = {
          email : '',
         senha:'',
         }
    }
}



//------------Aqui eu insiro as tags html 
render();{
    return(
        <div>
            <main>
                <section><h2> Tela de Login</h2> </section>
          </main>
        </div>
    );
}

export default Login;