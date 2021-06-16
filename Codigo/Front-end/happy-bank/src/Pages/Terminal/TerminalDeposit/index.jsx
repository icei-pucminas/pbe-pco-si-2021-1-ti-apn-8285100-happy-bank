import "./styles.css";
import LogoImg from "../../../images/logo.png";
import { Link } from "react-router-dom";
import React, { useState } from "react";
import { HiCheckCircle } from "react-icons/hi";
import DepositService from "../../../services/DepositService";
import Swal from "sweetalert2";
import Utils from "../../../services/Utils";
import { BiArrowBack } from "react-icons/bi";

export default function TerminalDeposit() {
  const [depositSucess, setDepositSucess] = useState(false);

  const [data, setData] = useState("");

  const [depositScreen, setDepositScreen] = useState(false);

  const [comprovantScreen, setComprovantScreen] = useState(false);

  const [agValue, setAgValue] = useState("");
  const [contaValue, setContaValue] = useState("");
  const [moneyValue, setMoneyValue] = useState(0);

  const [currentName, setCurrentName] = useState("");

  const sessionID = sessionStorage.getItem("customerId");

  const [dataDeposito, setDataDeposito] = useState({
    year: "",
    month: "",
    day: "",
    hours: "",
    minutes: "",
    seconds: "",
  });

  const d = new Date();

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
    window.location.reload();
  }

  function close() {
    setDepositScreen(false);
  }

  function openComprovant() {
    setComprovantScreen(true);
  }
  function closeComprovant() {
    setComprovantScreen(false);
  }

  function sendDeposit() {
    dataDeposito.year = d.getFullYear();
    dataDeposito.day = d.getDate();
    dataDeposito.month = String(d.getMonth() + 1).padStart(2, "0");
    dataDeposito.minutes = String(d.getMinutes()).padStart(2, "0");
    dataDeposito.seconds = String(d.getSeconds()).padStart(2, "0");
    dataDeposito.hours = String(d.getHours()).padStart(2, "0");

    doDeposit();
  }

  function verifyFields() {
    let message = "O(s) campo(s) ";
    let cont = 0;

    if (agValue == "") {
      message += " Agência";
      cont++;
    }

    if (contaValue == "") {
      message += cont > 0 ? ", Conta" : " Conta";
      cont++;
    }

    if (moneyValue == null || moneyValue == "" || moneyValue == 0) {
      message += cont > 0 ? ", Valor(R$)" : " Valor(R$)";
      cont++;
    }

    message += ", é(são) obrigatórios!";

    if (moneyValue < 10) {
      message +=
        cont > 0
          ? "\n O valor minimo para depósito é R$ 10,00"
          : " O valor minimo para depósito é R$ 10,00";
      cont++;
    }

    if (cont > 0) {
      Swal.fire("Atenção", message, "warning");
    }
    if (cont === 0) {
      findAccount();
    }
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
          <button className="deposit-back-btn" onClick={close}>
            <BiArrowBack />
          </button>
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
                <p>{Utils.formatarMoeda(+moneyValue)}</p>
                <p>
                  {("000" + contaValue).slice(-3)} -{" "}
                  {("00000" + agValue).slice(-5)}
                </p>
                <p>{data}</p>
              </div>
              <button onClick={openComprovant} className="share-btn">
                Imprimir comprovante
              </button>
            </div>
          </div>

          <div style={{ display: comprovantScreen ? "" : "" }}>
            <div id="comprovant-bg">
              <div id="comprovant-paper">
                <h1>Ola mundo</h1>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div style={{ display: comprovantScreen ? "" : "none" }}>
        <div className="comprovant-bg" onClick={closeDepositScreen}>
          <div className="comprovant-paper">
            <span className="margin-bottom">Happy Bank :)</span>
            <div className="line">
              <p>AUTO ATENDIMENTO</p>
              <p>-</p>
              <p>AG BETIM</p>
            </div>
            <div className="line">
              <p>
                DATA: {dataDeposito.day}/{dataDeposito.month}/
                {dataDeposito.year}
              </p>

              <p>
                HORA: {dataDeposito.hours}:{dataDeposito.minutes}:
                {dataDeposito.seconds}
              </p>
            </div>
            <div className="line margin-bottom">TERMINAL: 874654768</div>
            <div
              className="line margin-bottom"
              style={{ width: "30rem", textAlign: "center" }}
            >
              COMPROVANTE PROVISÓRIO DE DEPÓSITO EM DINHEIRO
            </div>
            <div className="line">
              <p>CONTA CREDITADA</p>
              <p>
                {("000" + agValue).slice(-3)} -{" "}
                {("00000" + contaValue).slice(-5)}
              </p>
            </div>
            <div className="line margin-bottom">
              <p>NOME</p>
              <p>{data}</p>
            </div>
            <div className="line margin-bottom">
              <p>VALOR TOTAL EM DINHEIRO</p>
              <p>{Utils.formatarMoeda(+moneyValue)}</p>
            </div>
            <div className="line" style={{ textAlign: "center" }}>
              1ª via
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
