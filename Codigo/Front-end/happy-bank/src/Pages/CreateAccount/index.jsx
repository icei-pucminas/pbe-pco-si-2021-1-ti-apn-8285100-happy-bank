import "./styles.css";

import DrawCreate from "../../images/heroCreate.png";
import LogoImg from "../../images/logo.png";
import SelfieImg from "../../images/selfie.png";

import { Link } from "react-router-dom";
import React, { useState } from "react";

import { FaCircle } from "react-icons/fa";
import { FaCheckCircle } from "react-icons/fa";
import { FiSend } from "react-icons/fi";
import Swal from "sweetalert2";

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

    setTimeout(() => {
      CreateAccountService.OpenAccount(responseData.customerId);
      openModal();
    }, 300);
  }

  // Requisiao POST - FIM

  function verifySpace(field) {
    let cont = 0;
    let achou = false;
    for (let i = 0; i < field.length; i++) {
      if (field[i] === " ") {
        cont++;
      }
    }
    if (field.length === cont) {
      achou = true;
    }
    return achou;
  }

  function verifyField(event) {
    event.preventDefault();
    let message = "O(s) campo(s) ";
    let cont = 0;
    if (inputs.name === "" || verifySpace(inputs.name)) {
      message += "Nome ";
      cont++;
    }
    if (inputs.govNumber === "" || verifySpace(inputs.govNumber)) {
      message += cont > 0 ? ", CPF" : " CPF";
      cont++;
    }
    if (inputs.birthDate === "" || verifySpace(inputs.birthDate)) {
      message += cont > 0 ? ", Data de nascimento" : " Data de nascimento";
      cont++;
    }
    if (inputs.email === "" || verifySpace(inputs.email)) {
      message += cont > 0 ? ", E-mail" : " E-mail";
      cont++;
    }
    if (inputs.password === "" || verifySpace(inputs.password)) {
      message += cont > 0 ? ", Senha" : " Senha";
      cont++;
    }
    if (inputs.street === "" || verifySpace(inputs.street)) {
      message += cont > 0 ? ", Endereço" : " Endereço";
      cont++;
    }
    if (inputs.addressNumber === "" || verifySpace(inputs.addressNumber)) {
      message += cont > 0 ? ", Número" : " Número";
      cont++;
    }
    if (inputs.district === "" || verifySpace(inputs.district)) {
      message += cont > 0 ? ", Bairro" : " Bairro";
      cont++;
    }
    if (inputs.phone === "" || verifySpace(inputs.phone)) {
      message += cont > 0 ? ", Telefone" : " Telefone";
      cont++;
    }
    if (inputs.city === "") {
      message += cont > 0 ? ", Cidade" : " Cidade";
      cont++;
    }
    if (inputs.state === "" || verifySpace(inputs.state)) {
      message += cont > 0 ? ", Estado" : " Estado";
      cont++;
    }
    message += ",  é(são) obrigatório(s)!!";
    if (cont > 0) {
      Swal.fire("Atenção", message, "warning");
    }
    if (cont === 0) {
      verifyPass(event);
    }
  }

  function verifyPass(event) {
    if (confirmSenha.confirmsenha !== inputs.password) {
      Swal.fire("Erro", "As senhas não conferem", "error");
    } else {
      submitData(event);
    }
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
          <img src={SelfieImg} alt="" className="selfie-img" />
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
