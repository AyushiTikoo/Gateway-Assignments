import React from 'react';
import './Dashboard.css';
import { NavLink } from 'react-router-dom';

const Dashboard = () => {
  return (
    <div className='sidebar'>
      <NavLink activeClassName='selected' to='/home'>
        Home
      </NavLink>
      <NavLink activeClassName='selected' to='/customers'>
        Customers
      </NavLink>
      <NavLink activeClassName='selected' to='/addcustomers'>
        AddCustomers
      </NavLink>
    </div>
  );
};
export default Dashboard;
