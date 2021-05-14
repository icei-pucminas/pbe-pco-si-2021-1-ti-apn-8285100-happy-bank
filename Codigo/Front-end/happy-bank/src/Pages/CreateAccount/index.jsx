import "./styles.css";
import DrawCreate from "../../images/heroCreate.png";
import LogoImg from "../../images/logo.png";

export default function CreateAccount() {
  return (
    <div id="wrapper">
      <main className="mainSide">
        <h1>Criar uma conta</h1>

        <form action="">
          <input type="text" id="name" placeholder="Nome completo" />
          <input type="text" id="cpf" placeholder="CPF" />
          <input type="text" id="rg" placeholder="RG" />
          <input type="text" id="email" placeholder="E-mail" />
          <input type="text" id="senha" placeholder="Senha" />
          <input type="text" id="confirmsenha" placeholder="Confirmar ssenha" />
        </form>
      </main>
      <div className="drawSide">
        <img src={DrawCreate} alt="" id="drawC" />
        <img src={LogoImg} alt="" id="logo" />
      </div>
    </div>
  );
}
