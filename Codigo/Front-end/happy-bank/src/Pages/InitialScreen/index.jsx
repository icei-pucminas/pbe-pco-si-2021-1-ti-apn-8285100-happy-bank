import React, { useState } from "react";
import { Link } from "react-router-dom";

import HeaderAccount from "../../Components/HeaderAccount/HeaderAccount";

import LogoImg from "../../images/logo.png";

import { FaRegMoneyBillAlt } from "react-icons/fa";
import { AiOutlineFileText } from "react-icons/ai";
import { BiCreditCard } from "react-icons/bi";

import "./styles.css";
import LoginService from "../../services/LoginService";

function InitialScreen() {
  return (
    <div>
      <HeaderAccount />
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
          <Link to="/extract">
            <button className="initial-btn">
              <div>
                <AiOutlineFileText />
              </div>
              <span>Extrato</span>
            </button>
          </Link>
          <button className="initial-btn">
            <div>
              <BiCreditCard />
            </div>
            <span>Cartão</span>
          </button>
        </div>

        {/* <img class="initial-logo" src={LogoImg} alt="" /> */}
      </main>
    </div>
  );
}

export default InitialScreen;
