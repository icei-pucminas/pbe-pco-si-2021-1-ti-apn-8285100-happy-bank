import Api from "./api";

//const baseUrl = `https://localhost:5001`;

const DepositService = {
  DoDeposit: async (objeto) => {
    const response = await Api.post(`account/deposit`, objeto);
    return response;
  },

  FindAccount: async (accountNumber, agencyNumber) => {
    const response = await Api.get(
      `account/find?accountNumber=${accountNumber}&agencyNumber=${agencyNumber}`
    );
    return response;
  },
};

export default DepositService;
