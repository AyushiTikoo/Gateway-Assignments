import React, { useState, useEffect } from "react";
import { Link, useLocation } from "react-router-dom";
import axios from "axios";
import "./ViewCustomers.css";

const ViewCustomers = (history) => {
  const location = useLocation();
  const [users, setUser] = useState([]);

  useEffect(() => {
    loadUser();
  }, [location]);
  const loadUser = async () => {
    axios
      .get(
        ` https://xssatfu8jb.execute-api.ap-south-1.amazonaws.com/customers/${location.state.detail}`
      )
      .then((response) => {
        console.log(response);
        setUser(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  };
  return (
    <div className="view-cust">
      <div className="container py-4">
        <div className="adjust">
          <div className="col-md-9">
            <h2 style={{ textAlign: "center" }}>YOUR DETAILS</h2>
            <div className="list-group" id="list-grp">
              <a href="#" className="list-group-item">
                <h4 className="list-group-item-heading">NAME</h4>
                <p className="list-group-item-text">{users.name}</p>
              </a>
              <a href="#" className="list-group-item">
                <h4 className="list-group-item-heading">EMAIL</h4>
                <p className="list-group-item-text">{users.email}</p>
              </a>
              <a href="#" className="list-group-item">
                <h4 className="list-group-item-heading">CONTACT NUMBER</h4>
                <p className="list-group-item-text">{users.contactNo}</p>
              </a>
              <a href="#" className="list-group-item">
                <h4 className="list-group-item-heading">IMAGE</h4>
                <p className="list-group-item-text">
                  <img
                    src={users.image}
                    style={{ width: "100px", height: "100px" }}
                    alt="new"
                  />
                </p>
              </a>
              <a href="#" className="list-group-item">
                <h4 className="list-group-item-heading">STATUS</h4>
                <p className="list-group-item-text">{users.status}</p>
              </a>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ViewCustomers;
