import React from 'react';
import ReactDOM from 'react-dom';
import {Route, BrowserRouter as Router, Switch} from 'react-router-dom';

import './index.css';

import App from './pages/home/App';
import Login from './pages/Login/Login';
import NotFound from './pages/NotFound/NotFound';
import reportWebVitals from './reportWebVitals';


const rounting = (
  <Router>
    <div>
      <Switch>
        <Route exact path = "/" component = {App}/> {/*Home*/}
        <Route path = "/Login" component = {Login}/> {/*Login*/}
        <Route path = "/NotFound" component = {NotFound}/> {/*Login*/}
        <Redirect to = "/NotFound" /> {/*Redireciona p/ notfound caso nao encontre a rota*/}
      </Switch>
    </div>
  </Router>
)


ReactDOM.render( routing, document.getElementById('root'));


// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
