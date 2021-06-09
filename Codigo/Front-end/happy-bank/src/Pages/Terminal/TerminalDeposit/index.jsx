import "./styles.css";
import LogoImg from "../../../images/logo.png";
import { Link } from "react-router-dom";
import React, { useState } from "react";
import { HiCheckCircle } from "react-icons/hi";
import DepositService from "../../../services/DepositService";
import Swal from "sweetalert2";
import Utils from "../../../services/Utils";

export default function TerminalDeposit() {
  const [depositSucess, setDepositSucess] = useState(false);

  const [data, setData] = useState("");

  const [depositScreen, setDepositScreen] = useState(false);

  const [agValue, setAgValue] = useState("");
  const [contaValue, setContaValue] = useState("");
  const [moneyValue, setMoneyValue] = useState(0);

  const [currentName, setCurrentName] = useState("");

  const sessionID = sessionStorage.getItem("customerId");

  const account = {
    accountNumber: contaValue,
    agencyNumber: agValue,
    envelopeCode: "PC123456",
    value: moneyValue,
  };

  async function doDeposit() {
    await DepositService.DoDeposit(account).then(() => {
      setDepositSucess(true);
    });
  }

  async function findAccount() {
    try {
      const responseData = (
        await DepositService.FindAccount(contaValue, agValue)
      ).data.customerName;
      setData(responseData);
      setDepositScreen(true);
    } catch (error) {
      Swal.fire("Ops!", "Conta não encontrada!", "error");
    }
  }

  function closeDepositScreen() {
    window.location.reload(false);
  }

  function sendDeposit() {
    doDeposit();
  }

  function verifyFields() {
    if (agValue == "") {
      alert("ERRO - Campo Agência obrigatório");
      return;
    }

    if (contaValue == "") {
      alert("ERRO - Campo Conta obrigatório");
      return;
    }

    if (moneyValue == null || moneyValue == "" || moneyValue == 0) {
      alert("ERRO - Campo Valor(R$) obrigatório");
      return;
    }

    if (moneyValue < 10) {
      alert("ERRO - O valor minimo para depósito é R$ 10,00");
      return;
    }

    findAccount();
  }

  return (
    <div id="deposit">
      <div className="main-side">
        <h1>Depósito</h1>

        <form action="">
          <div>
            <input
              type="text"
              placeholder="Agência"
              onChange={(e) => setAgValue(e.target.value)}
              required
            />
            <input
              type="text"
              placeholder="Conta"
              onChange={(e) => setContaValue(e.target.value)}
              required
            />
          </div>

          <input
            type="number"
            placeholder="Valor"
            onChange={(e) => setMoneyValue(e.target.value)}
            required
          />
        </form>

        <button type="send" onClick={verifyFields}>
          Prosseguir
        </button>
      </div>

      <div className="image-side">
        <Link to="/">
          <img src={LogoImg} id="logo" alt="" />
        </Link>
        <h2>Com Happy Bank é muito fácil fazer um depósito</h2>
      </div>

      <div style={{ display: depositScreen ? "" : "none" }}>
        <div id="deposit-screen">
          <div className="main-side">
            <div></div>
            <h1 className="deposit-title">Depósito</h1>
            <h2 className="deposit-subtitle">
              Confira se os dados estão corretos
            </h2>

            <div className="account-data">
              <div className="account-field">
                <span>Agência</span>
                <strong>{agValue.padStart(3, "0")}</strong>
              </div>
              <div className="account-field">
                <span>Conta</span>
                <strong>{contaValue.padStart(5, "0")}</strong>
              </div>
            </div>

            <strong className="dataName">{data}</strong>

            <p className="deposit-text">
              Se estiver tudo certo, pode inserir o dinheiro no terminal
              <br></br>{" "}
              <span style={{ fontSize: "5rem" }}>
                {Utils.formatarMoeda(+moneyValue)}
              </span>
            </p>
            <button onClick={sendDeposit} className="realize-deposit">
              Realizar Deposito
            </button>
          </div>

          <div className="image-side">
            <Link to="/">
              <img src={LogoImg} id="logo" alt="" />
            </Link>
            <h2>Com Happy Bank é muito fácil fazer um depósito</h2>
          </div>

          <div style={{ display: depositSucess ? "" : "none" }}>
            <div className="deposit-sucess">
              <img src={LogoImg} alt="" />
              <h1 style={{ color: "#000", fontSize: "3rem" }}>
                Deposito realizado com sucesso
              </h1>
              <HiCheckCircle className="sucess-circle" />
              <div>
                <p>R$ {moneyValue}</p>
                <p>
                  {("000" + contaValue).slice(-3)} -{" "}
                  {("00000" + agValue).slice(-5)}
                </p>
                <p>{data}</p>
              </div>
              <button onClick={closeDepositScreen} className="share-btn">
                Imprimir comprovante
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
