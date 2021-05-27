import "./styles.css";

import axios from "axios";

import DrawCreate from "../../images/heroCreate.png";
import LogoImg from "../../images/logo.png";
import SelfieImg from "../../images/selfie.png";

import { Link } from "react-router-dom";
import React, { useState } from "react";

import { FaCircle } from "react-icons/fa";
import { FaCheckCircle } from "react-icons/fa";
import { FiSend } from "react-icons/fi";

import Modal from "../../Component/Modal/Modal";
import CreateAccountService from "../../services/CreateAccountService";

export default function CreateAccount() {
  const [visibility, setVisibility] = useState(false);
  const [modal, setModal] = useState(false);
  const [validationScreen, setValidationScreen] = useState(false);
  const [confirmSenha, setConfirmSenha] = useState({
    confirmsenha: "",
  });
  const [inputs, setInputs] = useState({
    name: "",
    govNumber: "",
    birthDate: "",
    email: "",
    password: "",
    //confirmsenha: "",
    street: "",
    addressNumber: "",
    district: "",
    city: "",
    state: "",
    phone: "",
  });

  //Requisção POST criar conta

  async function submitData(event) {
    event.preventDefault();

    const responseData = (await CreateAccountService.CustomerRegister(inputs))
      .data;

    console.log(responseData.customerId);

    setTimeout(() => {
      CreateAccountService.OpenAccount(responseData.customerId);
      openModal();
    }, 1000);
  }

  // Requisiao POST - FIM

  function verifyField(event) {
    event.preventDefault();
    if (inputs.name == "") {
      alert("Campo nome é obrigatório!");
      return;
    }
    if (inputs.govNumber == "") {
      alert("Campo cpf é obrigatório!");
      return;
    }
    if (inputs.birthDate == "") {
      alert("Campo data é obrigatório!");
      return;
    }
    if (inputs.email == "") {
      alert("Campo email é obrigatório!");
      return;
    }
    if (inputs.password == "") {
      alert("Campo senha é obrigatório!");
      return;
    }
    if (confirmSenha.confirmsenha !== inputs.password) {
      console.log(confirmSenha.confirmsenha);
      console.log(inputs.password);
      alert("As senhas não conferem");
      return;
    }
    if (inputs.district == "") {
      alert("Campo bairro é obrigatório!");
      return;
    }

    if (inputs.city == "") {
      alert("Campo cidade é obrigatório!");
      return;
    }
    if (inputs.phone == "") {
      alert("Campo telefone é obrigatório!");
      return;
    }
    if (inputs.district == "") {
      alert("Campo estado é obrigatório!");
      return;
    }
    if (inputs.state == "") {
      alert("Campo rua é obrigatório!");
      return;
    }
    submitData(event);
  }

  function handleInputs({ target }) {
    const { value, name } = target;
    const clone = { ...inputs };
    clone[name] = value;
    setInputs(clone);
  }

  function handleConfirm({ target }) {
    const { value, name } = target;
    const clone = { ...confirmSenha };
    clone[name] = value;
    setConfirmSenha(clone);
  }

  function openModal() {
    console.log("Abrindo modal");
    setModal(true);
  }

  function openValidationScreen() {
    setValidationScreen(true);
  }

  function closeModal() {
    window.location.reload();
  }

  return (
    <div id="wrapper">
      <main className="mainSide">
        <h1>Criar uma conta</h1>

        <form onSubmit={verifyField}>
          <div
            style={{ display: visibility ? "none" : "" }}
            className="form-wrapper"
          >
            <input
              type="text"
              name="name"
              id="name"
              value={inputs.name}
              onChange={handleInputs}
              placeholder="Nome completo"
            />
            <div style={{ display: "flex", gap: "20px" }}>
              <input
                type="text"
                name="govNumber"
                id="cpf"
                value={inputs.govNumber}
                onChange={handleInputs}
                placeholder="CPF"
              />
              <input
                type="date"
                name="birthDate"
                id="data"
                value={inputs.birthDate}
                onChange={handleInputs}
                placeholder="Nascimento"
              />
            </div>

            <input
              type="string"
              name="email"
              id="email"
              value={inputs.email}
              onChange={handleInputs}
              placeholder="E-mail"
            />
            <div style={{ display: "flex", gap: "20px" }}>
              {" "}
              <input
                type="password"
                name="password"
                id="senha"
                value={inputs.password}
                onChange={handleInputs}
                placeholder="Senha"
              />
              <input
                type="password"
                value={confirmSenha.confirmsenha}
                onChange={handleConfirm}
                name="confirmsenha"
                id="confirmsenha"
                placeholder="Confirmar senha"
              />
            </div>
          </div>

          <div
            style={{ display: visibility ? "" : "none" }}
            className="form-wrapper"
          >
            <input
              type="text"
              name="street"
              id="rua"
              value={inputs.street}
              onChange={handleInputs}
              placeholder="Endereço"
            />
            <div style={{ display: "flex", gap: "20px" }}>
              <input
                type="text"
                name="addressNumber"
                id="numero"
                value={inputs.addressNumber}
                onChange={handleInputs}
                placeholder="Número"
              />
              <input
                type="text"
                name="district"
                id="bairro"
                value={inputs.district}
                onChange={handleInputs}
                placeholder="Bairro"
              />
            </div>

            <input
              type="text"
              name="phone"
              id="tel"
              value={inputs.phone}
              onChange={handleInputs}
              placeholder="Telefone"
            />
            <div style={{ display: "flex", gap: "20px" }}>
              <input
                type="text"
                name="city"
                id="cidade"
                value={inputs.city}
                onChange={handleInputs}
                placeholder="Cidade"
              />
              <input
                type="text"
                name="state"
                id="estado"
                value={inputs.state}
                onChange={handleInputs}
                placeholder="Estado"
              />
            </div>

            <button type="submit" className="btn-create">
              Criar Conta
            </button>
          </div>
        </form>

        <div id="circles">
          <button className="btn-circle" onClick={() => setVisibility(false)}>
            <FaCircle id={visibility ? "circDisable" : "circActive"} />
          </button>
          <button className="btn-circle" onClick={() => setVisibility(true)}>
            <FaCircle id={visibility ? "circActive" : "circDisable"} />
          </button>
        </div>
      </main>
      <div className="drawSide">
        <img src={DrawCreate} alt="" id="drawC" />
        <Link to="/">
          <img src={LogoImg} alt="" id="logo" />
        </Link>
      </div>

      <div style={{ display: modal ? "" : "none" }}>
        <Modal>
          <h1>Não cubra seu rosto</h1>
          <p>
            Na foto você deve segurar o documento ao lado do seu rosto (selfie),
            sem cobrir seu rosto e forma legível
          </p>
          <img src={SelfieImg} alt="" />
          <button className="send-selfie" onClick={openValidationScreen}>
            <FiSend />
            Enviar
          </button>
        </Modal>

        <div
          style={{
            display: validationScreen ? "" : "none",
          }}
        >
          <Modal>
            <h1>Validação realziada com sucesso</h1>
            <FaCheckCircle
              style={{
                color: "green",
                fontSize: "8rem",
              }}
            />
            <p>
              A sua solicitação do procedimento de verificação Happy Bank foi
              realizada com sucesso :)
            </p>
            <button
              className="validation-btn"
              onClick={() =>
                window.location.replace("http://localhost:3000/login")
              }
            >
              <Link
                style={{
                  textDecoration: "none",
                  color: "#fff",
                }}
                onClick={closeModal}
              >
                Continuar
              </Link>
            </button>
          </Modal>
        </div>
      </div>
    </div>
  );
}
