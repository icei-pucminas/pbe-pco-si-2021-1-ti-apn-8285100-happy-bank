import "./styles.css";
import DrawCreate from "../../images/heroCreate.png";
import LogoImg from "../../images/logo.png";
import { Link } from "react-router-dom";
import React, { useState } from "react";
import { FaCircle } from "react-icons/fa";

export default function CreateAccount() {
  const [visibility, setVisibility] = useState(false);
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
  return (
    <div id="wrapper">
      <main className="mainSide">
        <h1>Criar uma conta</h1>

        <form action="">
          <div style={{ display: visibility ? "none" : "" }}>
            <input
              type="text"
              name="name"
              id="name"
              value={inputs.name}
              onChange={handleInputs}
              placeholder="Nome completo"
            />
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
            <input
              type="email"
              name="email"
              id="email"
              value={inputs.email}
              onChange={handleInputs}
              placeholder="E-mail"
            />
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
          <div style={{ display: visibility ? "" : "none" }}>
            <input
              type="text"
              name="rua"
              id="name"
              value={inputs.rua}
              onChange={handleInputs}
              placeholder="Endereço"
            />
            <input
              type="text"
              name="numero"
              id="cpf"
              value={inputs.numero}
              onChange={handleInputs}
              placeholder="Número"
            />
            <input
              type="text"
              name="bairro"
              id="rg"
              value={inputs.bairro}
              onChange={handleInputs}
              placeholder="Bairro"
            />
            <input
              type="email"
              name="telefone"
              id="email"
              value={inputs.telefone}
              onChange={handleInputs}
              placeholder="Telefone"
            />
            <input
              type="password"
              name="cidade"
              id="senha"
              value={inputs.cidade}
              onChange={handleInputs}
              placeholder="Cidade"
            />
            <input
              type="password"
              name="estado"
              id="confirmsenha"
              value={inputs.estado}
              onChange={handleInputs}
              placeholder="Estado"
            />
          </div>
          <div id="circles">
            <FaCircle id={visibility ? "circDisable" : "circActive"} />
            <FaCircle id={visibility ? "circActive" : "circDisable"} />
          </div>
        </form>
      </main>
      <div className="drawSide">
        <img src={DrawCreate} alt="" id="drawC" />
        <Link to="/">
          <img src={LogoImg} alt="" id="logo" />
        </Link>
      </div>
    </div>
  );
}
