import React from "react";
import { BrowserRouter, Route } from "react-router-dom";
import CreateAccount from "./Pages/CreateAccount";
import InitialScreen from "./Pages/InitialScreen";
import Landing from "./Pages/Landing";
import Login from "./Pages/Login";
import TerminalDeposit from "./Pages/Terminal/TerminalDeposit";
import TransferScreen from "./Pages/TransferScreen";
import PrivateRoute from "./PrivateRoute";

export default function Routes() {
  return (
    <BrowserRouter>
      <Route path="/" exact component={Landing} />
      <Route path="/login" component={Login} />
      <Route path="/createaccount" component={CreateAccount} />
      <Route exact path="/deposit" component={TerminalDeposit} />
      <PrivateRoute path="/myaccount" component={InitialScreen} />
      <Route path="/transfer" component={TransferScreen} />
    </BrowserRouter>
  );
}
