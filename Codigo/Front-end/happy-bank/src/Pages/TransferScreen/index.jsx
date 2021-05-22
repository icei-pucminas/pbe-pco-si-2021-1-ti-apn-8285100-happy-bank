import React from "react";

import HeaderAccount from "../../Component/HeaderAccount/HeaderAccount";

import LogoImg from "../../images/logo.png";

import { FiSend } from "react-icons/fi";

import { FaRegMoneyBillAlt } from "react-icons/fa";
import { AiOutlineFileText } from "react-icons/ai";
import { BiCreditCard } from "react-icons/bi";

import "./styles.css";

function TransferScreen() {
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

          <button className="transfer-btn">
            Transferir
            <FiSend />
          </button>
        </form>
      </div>
    </div>
  );
}

export default TransferScreen;
