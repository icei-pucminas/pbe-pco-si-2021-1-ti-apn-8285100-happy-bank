import React, { useState } from "react";
import { Link } from "react-router-dom";

import HeaderAccount from "../../Components/HeaderAccount/HeaderAccount";

import LogoImg from "../../images/logo.png";

import { FiSend } from "react-icons/fi";
import { FaCheckCircle } from "react-icons/fa";

import { FaRegMoneyBillAlt } from "react-icons/fa";
import { AiOutlineFileText } from "react-icons/ai";
import { BiCreditCard } from "react-icons/bi";

import "./styles.css";

function TransferScreen() {
  const [errorScreen, setErrorScreen] = useState(false);
  const [sucessScreen, setSucessScreen] = useState(false);

  function backError() {
    setErrorScreen(false);
  }

  function transferError() {
    setErrorScreen(true);
    console.log("Erro na transferencia");
  }

  function transferSucess() {
    setSucessScreen(true);
    console.log("Sucesso na transferencia");
  }
  const sessionName = sessionStorage.getItem("customerName");
  const sessionAccount = sessionStorage.getItem("accountNumber");
  const sessionAgency = sessionStorage.getItem("agencyNumber");
  const accountBalance = sessionStorage.getItem("accountBalance");
  return (
    <div>
      <HeaderAccount />
      <h1 class="transfer-title">Realizar Transferencia</h1>
      <div className="transfer-content">
        <form action="">
          <div style={{ display: "flex", gap: "20px" }}>
            <input type="number" placeholder="Agência" />
            <input type="number" placeholder="Conta" />
          </div>
          <div style={{ display: "flex", gap: "20px" }}>
            <input type="number" placeholder="Banco" />
            <input type="number" placeholder="Valor (R$)" />
          </div>

          <button
            className="transfer-btn"
            type="button"
            onClick={transferError}
          >
            Transferir
            <FiSend />
          </button>
        </form>
      </div>

      <div>
        <div
          className="transfer-error"
          style={{ display: errorScreen ? "" : "none" }}
        >
          <h1>Ops! Houve um problema</h1>
          <strong>
            Por favor, verifique os dados informados ou se o saldo é suficiente
          </strong>
          <button className="back-error-btn" type="button" onClick={backError}>
            Voltar
          </button>
        </div>
      </div>

      <div>
        <div
          className="transfer-sucess"
          style={{ display: sucessScreen ? "" : "none" }}
        >
          <img src={LogoImg} alt="Logo" style={{ width: "30rem" }} />
          <h1>Transferência realizada com sucesso!</h1>
          <FaCheckCircle className="sucess-circle" />
          <div>
            <p>R$ 100,00</p>
            <p>002124 - 001</p>
            <p>João da Silva</p>
          </div>

          <button className="share-btn">Compartilhar</button>
        </div>
      </div>
    </div>
  );
}

export default TransferScreen;
