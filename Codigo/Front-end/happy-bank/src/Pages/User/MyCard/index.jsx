import React from "react";
import "./styles.css";
import CardImg from "../../../images/hb-card.png";
import { BiArrowBack } from "react-icons/bi";

import HeaderAccount from "../../../Components/HeaderAccount/HeaderAccount";
import { Link } from "react-router-dom";

function MyCard() {
  return (
    <div>
      <HeaderAccount />
      <Link to="/myaccount">
        <button className="btn-back">
          <BiArrowBack />
        </button>
      </Link>
      <div className="my-card-content card-screen">
        <h1>Meu cartão Happy</h1>
        <img src={CardImg} alt="" style={{ width: "80rem" }} />
      </div>
    </div>
  );
}

export default MyCard;
