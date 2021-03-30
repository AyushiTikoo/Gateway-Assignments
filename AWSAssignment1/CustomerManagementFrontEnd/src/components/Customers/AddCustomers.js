import React, { useState } from "react";
import axios from "axios";
import { useHistory } from "react-router-dom";
import "./AddCustomers.css";

const AddCustomers = () => {
  let history = useHistory();
  const [name, setName] = useState();
  const [email, setEmail] = useState();
  const [password, setPassword] = useState();
  const [status, setStatus] = useState();
  const [contactNo, setContactNo] = useState();
  const [image, setImage] = useState();

  const handleSubmit = async (e) => {
    e.preventDefault();

    const fd = new FormData();
    fd.append("name", name);
    fd.append("email", email);
    fd.append("password", password);
    fd.append("status", status);
    fd.append("contactNo", contactNo);
    fd.append("image", image);

    try {
      await axios.post(
        "https://xssatfu8jb.execute-api.ap-south-1.amazonaws.com/customers",
        fd
      );
      history.push("/customers");
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div className="content-add">
      <div className="container">
        <form
          className="form-horizontal"
          id="table-content"
          role="form"
          onSubmit={handleSubmit}
        >
          <div className="center">
            <h2 className="h2-AddCustomer">ADD CUSTOMERS</h2>
          </div>

          <div className="form-group">
            <label className="col-sm-3 control-label">Name:</label>
            <div className="col-sm-7">
              <input
                required
                onChange={(e) => setName(e.target.value)}
                type="text"
                id="Name"
                placeholder="Name"
                className="form-control"
              />
            </div>
          </div>
          <div className="form-group">
            <label className="col-sm-3 control-label">Email:</label>
            <div className="col-sm-7">
              <input
                required
                onChange={(e) => setEmail(e.target.value)}
                type="email"
                id="email"
                placeholder="Email"
                className="form-control"
              />
            </div>
          </div>
          <div className="form-group">
            <label className="col-sm-3 control-label">Password:</label>
            <div className="col-sm-7">
              <input
                pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}"
                title="Must contain at least one number and one uppercase and lowercase letter, and at least 8 or more characters"
                required
                onChange={(e) => setPassword(e.target.value)}
                type="password"
                id="password"
                placeholder="Password"
                className="form-control"
              />
            </div>
          </div>

          <div className="form-group">
            <label className="col-sm-3 control-label">Contact Number:</label>
            <div className="col-sm-7">
              <input
                required
                pattern="[0-9]{10}"
                onChange={(e) => setContactNo(e.target.value)}
                type="text"
                id="contactNo"
                placeholder="ContactNo"
                className="form-control"
              />
            </div>
          </div>

          <div className="form-group">
            <label className="col-sm-3 control-label">Status:</label>
            <div className="col-sm-7">
              <select
                required
                onChange={(e) => setStatus(e.target.value)}
                id="status"
                className="form-control"
              >
                <option selected disabled>
                  Select
                </option>
                <option>Active</option>
                <option>Deactive</option>
              </select>
            </div>
          </div>

          <div className="form-group">
            <label className="col-sm-3 control-label">Upload Image:</label>
            <div className="col-sm-7">
              <input
                required
                onChange={(e) => setImage(e.target.files[0])}
                type="file"
                id="Image"
                placeholder="Image"
                className="Image"
              />
            </div>
          </div>

          <div className="form-group">
            <div className="col-sm-7 col-sm-offset-3">
              <button
                type="submit"
                className="btn btn-block"
                id="add-button"
                style={{ backgroundColor: "#7eb7d6" }}
              >
                Submit
              </button>
            </div>
          </div>
        </form>
      </div>
    </div>
  );
};
export default AddCustomers;
