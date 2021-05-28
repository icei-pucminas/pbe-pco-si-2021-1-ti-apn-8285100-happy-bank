import "./styles.css";
import React, { useState } from "react";
import Swal from "sweetalert2";

import DrawImage from "../../images/draw.png";
import logo from "../../images/logo.png";
import { Link } from "react-router-dom";
import LoginService from "../../services/LoginService";

export default function Login() {
  const [dataLogin, setDatalogin] = useState({
    email: "",
    password: "",
  });

  async function login(event) {
    event.preventDefault();

    const responseData = (await LoginService.signIn(dataLogin)).data;

    if (responseData) {
      const saldo = (await LoginService.myBalance(responseData.customerId))
        .data;
      sessionStorage.setItem("customerId", responseData.customerId);
      sessionStorage.setItem("customerName", responseData.customerName);
      sessionStorage.setItem("accountNumber", responseData.accountNumber);
      sessionStorage.setItem("agencyNumber", responseData.agencyNumber);
      sessionStorage.setItem("accountBalance", saldo);
      window.location.replace("http://localhost:3000/myaccount");
    } else {
      Swal.fire("Erro", "Email ou senha incorreta!!", "error");
    }
  }

  function handleInputs({ target }) {
    const { value, name } = target;
    const clone = { ...dataLogin };
    clone[name] = value;
    setDatalogin(clone);
  }

  return (
    <div id="login">
      <div id="form-side">
        <h1 id="welcome">Bem vindo!</h1>
        <form onSubmit={login}>
          <input
            type="email"
            placeholder="E-mail"
            onChange={handleInputs}
            name="email"
          />
          <input
            type="password"
            placeholder="Senha"
            onChange={handleInputs}
            name="password"
          />

          <Link to="/createaccount" id="link">
            Criar conta
          </Link>

          <button type="submit">Login</button>
        </form>
      </div>
      <div id="draw-side">
        <img className="draw" src={DrawImage} alt="Draw" />
        <Link to="/" class="back-link">
          <img className="logo" src={logo} alt="Logo" />
        </Link>
      </div>
    </div>
  );
}
