import Api from "./api";

//const baseUrl = `https://localhost:5001`;

const CreateAccountService = {
  CustomerRegister: async (objeto) => {
    const response = await Api.post(`customer/register`, objeto);
    return response;
  },

  OpenAccount: async (objeto) => {
    const response = await Api.post(
      `account/open`,
      {},
      {
        headers: {
          "Content-Type": "application/json, text/plain, */*",
          "x-customer-id": objeto,
        },
      }
    );
    console.log(response);
    return response;
  },
};

export default CreateAccountService;
