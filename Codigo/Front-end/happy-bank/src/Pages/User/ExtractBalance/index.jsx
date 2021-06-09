import React, { useState } from "react";
import HeaderAccount from "../../../Components/HeaderAccount/HeaderAccount";
import ExtractService from "../../../services/ExtractService";
import Utils from "../../../services/Utils";

import "./styles.css";

function ExtractBalance() {
  const [extract, setExtract] = useState([]);
  const [inputs, setInputs] = useState({
    data_start: "",
    data_end: "",
  });

  async function getExtract(data_start, data_end) {
    try {
      const sessionID = sessionStorage.getItem("customerId");
      const responseData = (
        await ExtractService.Extract(sessionID, data_start, data_end)
      ).data;
      console.log(responseData);
      setExtract(responseData);
    } catch (error) {}
  }

  function handleInputs({ target }) {
    const { value, name } = target;
    const clone = { ...inputs };
    clone[name] = value;
    setInputs(clone);
  }

  async function filterExtract(event) {
    event.preventDefault();
    getExtract(inputs.data_start, inputs.data_end);
  }

  getExtract("2021-06-01", "2021-06-31");

  console.log(extract, "extractr");
  return (
    <div>
      <HeaderAccount />
      <div id="extract-balance-content">
        <h1>Extrato</h1>
        <form className="data-wrapper" onSubmit={filterExtract}>
          <div style={{ display: "flex" }}>
            <div style={{ marginRight: "5rem" }}>
              <label htmlFor="data_start">Data inicio</label>
              <input
                id="data_start"
                className="data-input"
                name="data_start"
                type="date"
                value={inputs.data_start}
                onChange={handleInputs}
              />
            </div>
            <div>
              <label htmlFor="data_end">Data fim</label>
              <input
                id="data_end"
                className="data-input"
                name="data_end"
                type="date"
                value={inputs.data_end}
                onChange={handleInputs}
              />
            </div>
          </div>
          <button type="submit" name="filter">
            Filtrar
          </button>
        </form>
        <div className="extract-balance-wrapper">
          <ul class="extract-list">
            {extract.map((item, index, arr) => {
              if (index != 0 && index != arr.length - 1) {
                return (
                  <li key={item.id}>
                    <span>{Utils.formatarData(item.executionDate)}</span>
                    <span>{item.description}</span>{" "}
                    <strong style={{ color: "darkgreen" }}>
                      {Utils.formatarMoeda(item.value)}
                    </strong>
                  </li>
                );
              }
            })}
          </ul>
        </div>
      </div>
    </div>
  );
}

export default ExtractBalance;
