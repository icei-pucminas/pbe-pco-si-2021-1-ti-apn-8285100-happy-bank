import React, { useState } from "react";
import LoginService from "../../services/LoginService";

import "./style.css";

// import { Container } from './styles';

function HeaderAccount() {
  const sessionName = sessionStorage.getItem("customerName");
  const sessionAccount = sessionStorage.getItem("accountNumber");
  const sessionAgency = sessionStorage.getItem("agencyNumber");
  const sessionID = sessionStorage.getItem("customerId");

  const [saldo, setSaldo] = useState("");

  async function getSaldo() {
    const saldo = (await LoginService.myBalance(sessionID)).data;
    const saldoConvert = parseFloat(saldo).toFixed(2).replace(".", ",");
    setSaldo(saldoConvert);
  }
  getSaldo();

  function cl() {
    const sair = window.confirm("Deseja mesmo sair?");

    if (sair) {
      sessionStorage.clear();
      window.location.reload();
    }
  }
  return (
    <div id="header-container">
      <div className="balance">
        <span>Saldo em conta</span>
        <strong>R$ {saldo}</strong>
      </div>
      <div className="acconut-name">
        <strong onClick={cl} style={{ cursor: "pointer" }}>
          {sessionName}
        </strong>
        <span>
          {("000" + sessionAgency).slice(-3)} -{" "}
          {("00000" + sessionAccount).slice(-5)}
        </span>
      </div>
    </div>
  );
}

export default HeaderAccount;
