import React, { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import axios from "axios";
import "./Customers.css";

const Customers = (props) => {
  let history = useHistory();
  const [users, setUser] = useState([]);

  useEffect(() => {
    loadUser();
  }, []);
  const loadUser = async () => {
    axios
      .get(`https://xssatfu8jb.execute-api.ap-south-1.amazonaws.com/customers`)
      .then((response) => {
        console.log(response.data);
        setUser(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  return (
    <div className="content-customer">
      <div className="container py-4">
        <h2 style={{ display: "inline-block" }}>CUSTOMER DETAILS</h2>
        <div className="scrollme">
          <div className="row">
            <div className="col-sm-10">
              <div className="table-responsive">
                <table className="table">
                  <thead style={{ backgroundColor: "#7eb7d6", color: "white" }}>
                    <tr>
                      <th>Name</th>
                      <th>Email</th>
                      <th>Contact No</th>
                      <th>Status</th>
                      <th>Image</th>
                      <th>Actions</th>
                    </tr>
                  </thead>
                  <tbody>
                    {users.map((user) => (
                      <tr key={user.id}>
                        <td>{user.name}</td>
                        <td>{user.email}</td>
                        <td>{user.contactNo}</td>
                        <td>{user.status}</td>
                        <td>
                          <img
                            src={user.image}
                            style={{ width: "50px", height: "50px" }}
                            alt="new"
                          />
                        </td>
                        <td>
                          <button
                            className="but-cust"
                            onClick={() =>
                              history.push({
                                pathname: "/customers/view/" + user.id,
                                state: { detail: user.id },
                              })
                            }
                          >
                            View
                          </button>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Customers;
