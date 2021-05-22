import "./styles.css";

import DrawCreate from "../../images/heroCreate.png";
import LogoImg from "../../images/logo.png";
import SelfieImg from "../../images/selfie.png";

import { Link } from "react-router-dom";
import React, { useState } from "react";

import { FaCircle } from "react-icons/fa";
import { FiSend } from "react-icons/fi";

import Modal from "../../Component/Modal/Modal";

export default function CreateAccount() {
  const [visibility, setVisibility] = useState(false);
  const [modal, setModal] = useState(false);
  const [inputs, setInputs] = useState({
    name: "",
    cpf: "",
    data: "",
    email: "",
    senha: "",
    confirmsenha: "",
    rua: "",
    numero: "",
    bairro: "",
    cidade: "",
    estado: "",
    telefone: "",
  });
  function handleInputs({ target }) {
    const { value, name } = target;
    const clone = { ...inputs };
    clone[name] = value;
    setInputs(clone);
  }

  function abrirModal() {
    console.log("Abrindo modal");
    setModal(true);
  }

  function closeModal() {
    setModal(false);
  }

  async function criarConta(event) {
    event.preventDefault();
  }

  return (
    <div id="wrapper">
      <main className="mainSide">
        <h1>Criar uma conta</h1>

        <form onSubmit={criarConta}>
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
                name="cpf"
                id="cpf"
                value={inputs.cpf}
                onChange={handleInputs}
                placeholder="CPF"
              />
              <input
                type="date"
                name="data"
                id="data"
                value={inputs.data}
                onChange={handleInputs}
                placeholder="Nascimento"
              />
            </div>

            <input
              type="email"
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
                name="senha"
                id="senha"
                value={inputs.senha}
                onChange={handleInputs}
                placeholder="Senha"
              />
              <input
                type="password"
                name="confirmsenha"
                id="confirmsenha"
                value={inputs.confirmsenha}
                onChange={handleInputs}
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
              name="rua"
              id="rua"
              value={inputs.rua}
              onChange={handleInputs}
              placeholder="Endereço"
            />
            <div style={{ display: "flex", gap: "20px" }}>
              <input
                type="text"
                name="numero"
                id="numero"
                value={inputs.numero}
                onChange={handleInputs}
                placeholder="Número"
              />
              <input
                type="text"
                name="bairro"
                id="bairro"
                value={inputs.bairro}
                onChange={handleInputs}
                placeholder="Bairro"
              />
            </div>

            <input
              type="text"
              name="telefone"
              id="tel"
              value={inputs.telefone}
              onChange={handleInputs}
              placeholder="Telefone"
            />
            <div style={{ display: "flex", gap: "20px" }}>
              <input
                type="text"
                name="cidade"
                id="cidade"
                value={inputs.cidade}
                onChange={handleInputs}
                placeholder="Cidade"
              />
              <input
                type="text"
                name="estado"
                id="estado"
                value={inputs.estado}
                onChange={handleInputs}
                placeholder="Estado"
              />
            </div>

            <button className="btn-create" onClick={abrirModal}>
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
          <button className="send-selfie" onClick={closeModal}>
            <FiSend />
            Enviar
          </button>
        </Modal>
      </div>
    </div>
  );
}
