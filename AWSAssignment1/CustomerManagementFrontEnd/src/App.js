import React from 'react';
import Dashboard from './components/Dashboard/Dashboard';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import Customers from './components/Customers/Customers';
import AddCustomers from './components/Customers/AddCustomers';
import ViewCustomers from './components/Customers/ViewCustomers';
import Home from './components/Home';

function App() {
  return (
    <>
      <Router>
        <Dashboard />
        <Switch>
          <Route path='/home' component={Home} exact />

          <Route exact path='/addcustomers' component={AddCustomers} />

          <Route exact path='/customers' component={Customers} />

          <Route exact path='/customers/view/:id' component={ViewCustomers} />
        </Switch>
      </Router>
    </>
  );
}

export default App;
