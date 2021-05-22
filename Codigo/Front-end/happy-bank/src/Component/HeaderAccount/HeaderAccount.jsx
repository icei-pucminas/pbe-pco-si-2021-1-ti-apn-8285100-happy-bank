import React from "react";
import "./style.css";

// import { Container } from './styles';

function HeaderAccount({ children, Display }) {
  let saldo = 100;
  const accountName = "José da Silva";

  return (
    <div id="header-container">
      <div className="balance">
        <span>Saldo em conta</span>
        <strong>R$ {saldo.toFixed(2).replace(".", ",")}</strong>
      </div>
      <div className="acconut-name">
        <strong>{accountName}</strong>
      </div>
    </div>
  );
}

export default HeaderAccount;
