import React from "react";
import "./style.css";

// import { Container } from './styles';

function HeaderAccount({ children, Display, Name, Conta, Agencia, Saldo }) {
  const accountName = Name;
  const accountNumber = Conta;
  const accountAgence = Agencia;
  const accountBalance = Saldo;

  const saldoConvert = parseFloat(accountBalance).toFixed(2).replace(".", ",");

  return (
    <div id="header-container">
      <div className="balance">
        <span>Saldo em conta</span>
        <strong>R$ {saldoConvert}</strong>
      </div>
      <div className="acconut-name">
        <strong>{accountName}</strong>
        <span>
          ag: {accountAgence} - cc: {accountNumber}
        </span>
      </div>
    </div>
  );
}

export default HeaderAccount;
