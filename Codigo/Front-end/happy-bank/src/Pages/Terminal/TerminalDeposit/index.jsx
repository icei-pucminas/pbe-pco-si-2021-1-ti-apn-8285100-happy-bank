import "./styles.css";
import LogoImg from "../../../images/logo.png";
import { Link } from "react-router-dom";
import React, { useState } from "react";
import { HiCheckCircle } from "react-icons/hi";

export default function TerminalDeposit() {
  const [depositSucess, setDepositSucess] = useState(false);

  const [depositScreen, setDepositScreen] = useState(false);

  const [agValue, setAgValue] = useState("");
  const [contaValue, setContaValue] = useState("");
  const [moneyValue, setMoneyValue] = useState(null);

  const [currentName, setCurrentName] = useState("");

  const contas = [
    {
      agencia: 123,
      conta: 45678,
      nome: "Jośe da Silva",
    },
    {
      agencia: 1,
      conta: 123456,
      nome: "Agatha Christie",
    },
  ];

  let currentAccountName;

  function contaExiste() {
    let achou = false;
    contas.forEach((conta) => {
      if (agValue == conta.agencia && contaValue == conta.conta && !achou) {
        setCurrentName(conta.nome);
        setDepositScreen(true);
        achou = true;
      }
    });
    if (!achou) {
      alert("Conta nao encontrada!");
    }
  }

  function closeDepositScreen() {
    window.location.reload(false);
  }

  function sendDeposit() {
    setDepositSucess(true);
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

    contaExiste();
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
                <strong>{agValue}</strong>
              </div>
              <div className="account-field">
                <span>Conta</span>
                <strong>{contaValue}</strong>
              </div>
            </div>

            <p className="deposit-text">
              Se estiver tudo certo, pode inserir o dinheiro ou cheque no
              terminal
              <br></br>(R$ {moneyValue})
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
                  {contaValue} - {agValue}
                </p>
                <p>{currentName}</p>
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
