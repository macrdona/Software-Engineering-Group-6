import React, { useState } from "react";
import { toast } from "react-toastify";
import "bootstrap/dist/css/bootstrap.min.css";
import "./CreateAccount.scss";

function CreateAccount() {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  const validate = (): boolean => {
    if (
      password !== confirmPassword ||
      name === "" ||
      email === "" ||
      password === ""
    ) {
      // Passwords don't match
      if (password !== confirmPassword) {
        // Display error to user
        toast.error("Passwords don't match!");
        return false;
      }

      // Field is missing
      if (
        name === "" ||
        email === "" ||
        password === "" ||
        confirmPassword === ""
      ) {
        toast.error("Missing one or more required fields.");
        return false;
      }
    }

    console.log(name, email, password);
    return true;
  };

  const handleSubmit = () => {
    if (validate()) {
      // Logic for sending API request
      // Display success and navigate to new page
    }
  };

  return (
    <div className="container-fluid flex-fill">
      <div className="row no-gutters h-100 flex-fill">
        <div className="d-none d-lg-flex col background-container"></div>
        <div className="col col-lg-6 p-5 d-flex flex-column align-items-center shadow-lg">
          <h1 className="display-1 mt-5 text-secondary">Create Account</h1>
          <div className="d-flex m-5 w-50">
            <form className="form d-flex flex-column gap-2 w-100">
              <div className="form-group w-100">
                <label
                  htmlFor="formGroupExampleInput"
                  className="form-label fs-5"
                >
                  Name
                </label>
                <input
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                  type="text"
                  className="form-control form-control-lg"
                  id="name"
                  required
                  autoComplete="off"
                />
              </div>

              <div className="form-group w-100">
                <label
                  htmlFor="formGroupExampleInput"
                  className="form-label fs-5"
                >
                  Email
                </label>
                <input
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  type="text"
                  className="form-control form-control-lg"
                  id="email"
                  required
                  autoComplete="off"
                />
              </div>

              <div className="form-group w-100">
                <label
                  htmlFor="formGroupExampleInput"
                  className="form-label  fs-5"
                >
                  Password
                </label>
                <input
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  type="text"
                  className="form-control form-control-lg"
                  id="password"
                  required
                  autoComplete="off"
                />
              </div>

              <div className="form-group w-100">
                <label
                  htmlFor="formGroupExampleInput"
                  className="form-label  fs-5"
                >
                  Confirm Password
                </label>
                <input
                  value={confirmPassword}
                  onChange={(e) => setConfirmPassword(e.target.value)}
                  type="text"
                  className="form-control form-control-lg"
                  id="confirmPassword"
                  required
                  autoComplete="off"
                />
              </div>
              <button
                onClick={(e) => {
                  e.preventDefault();
                  handleSubmit();
                }}
                className="btn btn-primary btn-lg mt-3 fs-2 mw-75 align-self-center text-nowrap"
                type="submit"
              >
                Create Account
              </button>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
}
export default CreateAccount;
