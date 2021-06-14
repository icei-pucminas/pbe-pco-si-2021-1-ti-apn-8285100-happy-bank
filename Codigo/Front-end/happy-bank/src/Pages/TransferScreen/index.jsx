import React, { useState } from "react";
import { Link } from "react-router-dom";

import HeaderAccount from "../../Components/HeaderAccount/HeaderAccount";

import LogoImg from "../../images/logo.png";

import { FiSend } from "react-icons/fi";
import { FaCheckCircle } from "react-icons/fa";
import { BiArrowBack } from "react-icons/bi";

import { FaRegMoneyBillAlt } from "react-icons/fa";
import { AiOutlineFileText } from "react-icons/ai";
import { BiCreditCard } from "react-icons/bi";

import DepositService from "../../services/DepositService";
import TransferService from "../../services/TransferService";
import Utils from "../../services/Utils";

import Swal from "sweetalert2";

import "./styles.css";

function TransferScreen() {
  const [errorScreen, setErrorScreen] = useState(false);
  const [sucessScreen, setSucessScreen] = useState(false);
  const [confirmData, setconfirmData] = useState(false);
  const [inputs, setInputs] = useState({
    agValue: "",
    contaValue: "",
    value: "",
  });
  const [data, setData] = useState({
    accountId: "",
    customerName: "",
  });

  function handleInputs({ target }) {
    const { value, name } = target;
    const clone = { ...inputs };
    clone[name] = value;
    setInputs(clone);
  }

  const transferData = {
    value: inputs.value,
    accountDestinyId: data.accountId,
  };

  async function findAccount() {
    if (inputs.value <= accountBalance) {
      if (inputs.contaValue !== sessionAccount) {
        try {
          const responseData = (
            await DepositService.FindAccount(inputs.contaValue, inputs.agValue)
          ).data;
          data.accountId = responseData.accountId;
          data.customerName = responseData.customerName;
          confirmDataScreen();
        } catch (error) {
          setErrorScreen(true);
        }
      } else setErrorScreen(true);
    } else setErrorScreen(true);
  }

  function closeScreen() {
    setErrorScreen(false);
    setconfirmData(false);
  }

  async function doTransfer() {
    try {
      await TransferService.doTransfer(transferData, sessionID);
      transferSucess();
    } catch (error) {
      transferError();
    }
  }

  function backError() {
    setErrorScreen(false);
  }

  function confirmDataScreen() {
    setconfirmData(true);
  }

  function transferError() {
    setErrorScreen(true);
    console.log("Erro na transferencia");
  }

  function transferSucess() {
    setSucessScreen(true);
    console.log("Sucesso na transferencia");
  }

  function reload() {
    window.location.reload();
  }

  /* 
  {
      "value": 25,
      "accountDestinyId": "7f46025c-4435-f664-d09b-63559f2873d1"
  }
*/

  const sessionName = sessionStorage.getItem("customerName");
  const sessionAccount = sessionStorage.getItem("accountNumber");
  const sessionAgency = sessionStorage.getItem("agencyNumber");
  const accountBalance = sessionStorage.getItem("accountBalance");
  const sessionID = sessionStorage.getItem("customerId");

  return (
    <div>
      <HeaderAccount />
      <Link to="/myaccount">
        <button className="btn-back">
          <BiArrowBack />
        </button>
      </Link>

      <h1 class="transfer-title">Realizar Transferencia</h1>
      <div className="transfer-content">
        <form action="">
          <div style={{ display: "flex", gap: "20px" }}>
            <input
              type="number"
              name="agValue"
              value={inputs.agValue}
              onChange={handleInputs}
              placeholder="Agência"
            />
            <input
              type="number"
              name="contaValue"
              value={inputs.contaValue}
              onChange={handleInputs}
              placeholder="Conta"
            />
          </div>
          <div style={{ alignSelf: "center" }}>
            <input
              type="number"
              name="value"
              value={inputs.value}
              onChange={handleInputs}
              placeholder="Valor (R$)"
            />
          </div>

          <button className="transfer-btn" type="button" onClick={findAccount}>
            Transferir
            <FiSend />
          </button>
        </form>
      </div>

      <div>
        <div
          className="confirm-data"
          style={{ display: confirmData ? "" : "none" }}
        >
          <h1>Transferência</h1>
          <h2>Confira se os dados estão corretos:</h2>

          <div className="account-data">
            <div className="account-field">
              <span>Agencia</span>
              <strong>{inputs.agValue}</strong>
            </div>

            <div className="account-field">
              <span>Conta</span>
              <strong>{inputs.contaValue}</strong>
            </div>
          </div>

          <div className="value-data account-field ">
            <span>Valor</span>
            <strong>{Utils.formatarMoeda(+inputs.value)}</strong>
          </div>

          <span style={{ fontSize: "3rem", color: "#fff" }}>
            {data.customerName}
          </span>

          <div className="btn-wrapper">
            <button className="back" onClick={closeScreen}>
              Voltar
            </button>
            <button className="send" onClick={doTransfer}>
              Realizar Trasnferência
            </button>
          </div>
        </div>
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
          <button
            className="back-error-btn"
            type="button"
            onClick={closeScreen}
          >
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
            <p>{Utils.formatarMoeda(+inputs.value)}</p>
            <p>
              {inputs.contaValue} - {inputs.agValue}
            </p>
            <p>{data.customerName}</p>
          </div>

          <button className="share-btn" onClick={reload}>
            Compartilhar
          </button>
        </div>
      </div>
    </div>
  );
}

export default TransferScreen;
