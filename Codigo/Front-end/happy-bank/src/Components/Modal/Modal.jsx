import React from "react";
import "./style.css";

// import { Container } from './styles';

function Modal({ children, Display }) {
  return (
    <div id="modal-wrapper" style={{ dispalay: Display ? "flex" : "none" }}>
      <div id="modal">
        <div className="modal-content">{children}</div>
      </div>
    </div>
  );
}

export default Modal;
