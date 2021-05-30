import React from "react";
import "./style.css";

// import { Container } from './styles';

function HeaderAccount({ children, Display, Name, Conta, Agencia, Saldo }) {
  const accountName = Name;
  const accountNumber = Conta;
  const accountAgence = Agencia;
  const accountBalance = Saldo;

  const saldoConvert = parseFloat(accountBalance).toFixed(2).replace(".", ",");
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
        <strong>R$ {saldoConvert}</strong>
      </div>
      <div className="acconut-name">
        <strong onClick={cl} style={{ cursor: "pointer" }}>
          {accountName}
        </strong>
        <span>
          {("000" + accountAgence).slice(-3)} -{" "}
          {("00000" + accountNumber).slice(-5)}
        </span>
      </div>
    </div>
  );
}

export default HeaderAccount;
