import React from "react";
import { Link } from "react-router-dom";

import HeaderAccount from "../../Component/HeaderAccount/HeaderAccount";

import LogoImg from "../../images/logo.png";

import { FaRegMoneyBillAlt } from "react-icons/fa";
import { AiOutlineFileText } from "react-icons/ai";
import { BiCreditCard } from "react-icons/bi";

import "./styles.css";

function InitialScreen() {
  const sessionName = sessionStorage.getItem("customerName");
  const sessionAccount = sessionStorage.getItem("accountNumber");
  const sessionAgency = sessionStorage.getItem("agencyNumber");
  const accountBalance = sessionStorage.getItem("accountBalance");

  return (
    <div>
      <HeaderAccount
        Name={sessionName}
        Agencia={sessionAgency}
        Conta={sessionAccount}
        Saldo={accountBalance}
      />
      <main className="initial-screen-content">
        <h1>Acesso rapido</h1>
        <div className="btn-wrapper">
          <Link to="/transfer" style={{ textDecoration: "none" }}>
            <button className="initial-btn">
              <div>
                <FaRegMoneyBillAlt />
              </div>
              <span>Transferencia</span>
            </button>
          </Link>
          <button className="initial-btn">
            <div>
              <AiOutlineFileText />
            </div>
            <span>Extrato</span>
          </button>
          <button className="initial-btn">
            <div>
              <BiCreditCard />
            </div>
            <span>Cartão</span>
          </button>
        </div>

        <img class="initial-logo" src={LogoImg} alt="" />
      </main>
    </div>
  );
}

export default InitialScreen;
