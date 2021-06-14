import React, { useState } from "react";
import LoginService from "../../services/LoginService";

import { BiLogOutCircle } from "react-icons/bi";

import "./style.css";
import Swal from "sweetalert2";

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
    window.sessionStorage.setItem("accountBalance", saldo);
    setSaldo(saldoConvert);
  }
  getSaldo();

  function cl() {
    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: "btn-swal-confirm",
        cancelButton: "btn-swal-cancel",
      },
      buttonsStyling: false,
    });
    swalWithBootstrapButtons
      .fire({
        title: "Deseja mesmo sair?",
        text: "Isso fara vc sair em lek kkj",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Sim",
        cancelButtonText: "Não",
      })
      .then((result) => {
        if (result.value) {
          sessionStorage.clear();
          window.location.reload();
        }
      });
  }
  return (
    <div id="header-container">
      <div className="balance">
        <span>Saldo em conta</span>
        <strong>R$ {saldo}</strong>
      </div>
      <div className="acconut-name">
        <strong style={{ cursor: "pointer" }}>{sessionName}</strong>
        <span>
          {("000" + sessionAgency).slice(-3)} -{" "}
          {("00000" + sessionAccount).slice(-5)}
          <BiLogOutCircle alt="Sair" onClick={cl} className="loggout-btn" />
        </span>
      </div>
    </div>
  );
}

export default HeaderAccount;
