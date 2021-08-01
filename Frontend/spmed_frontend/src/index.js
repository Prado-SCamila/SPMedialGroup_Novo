import React from 'react';
import ReactDOM from 'react-dom';
import { Route, BrowserRouter as Router, Switch, Redirect } from 'react-router-dom';

import { usuarioAutenticado, parseJwt } from './pages/services/auth';
//--------------Páginas----------------------------------------------------------------
import './index.css';
import App from './pages/home/App';
import Login from './pages/login/login';
import NotFound from './pages/notFound/notFound';
import Consulta from './pages/consultas/cadastroConsultas';
import Consultaspac from './pages/consultas/minhasConsultas_pac';
import Consultamed from './pages/consultas/minhasConsultas_med';
import reportWebVitals from './reportWebVitals';

  //-----------------------------------------------------------------------------------
const PermissaoAdm = ({ component : Component }) => (
  <Route 
    render = { props => 
      usuarioAutenticado() && parseJwt().role === "1" ?
      <Component {...props} /> :
      <Redirect to="/login" />
    }
  />
)

const PermissaoMed = ({ component : Component }) => (
  <Route 
    render = { props => 
      usuarioAutenticado() && (parseJwt().role === "2") ?
      <Component {...props} /> :
      <Redirect to="/login" />
    }
  />
)


const PermissaoPac = ({ component : Component }) => (
  <Route 
    render = { props => 
      usuarioAutenticado() && (parseJwt().role === "3") ?
      <Component {...props} /> :
      <Redirect to="/login" />
    }
  />
)

const routing = (
  <Router>
    <div>
      <Switch>
        <Route exact path="/" component={App} />
        <PermissaoAdm path="/cadastroConsultas" component={Consulta} />
        <PermissaoMed path="/minhasConsultas_med" component={Consultamed} />
        <PermissaoPac path ="/minhasConsultas_pac" component={Consultaspac}/>
        <Route path="/login" component={Login} />
        <Route exact path="/notFound" component={NotFound} />
        <Redirect to="/notFound" />
      </Switch>
    </div>
  </Router>
)

ReactDOM.render(routing, document.getElementById('root'));

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
