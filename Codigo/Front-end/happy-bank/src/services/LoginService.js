import Api from "./api";

const LoginService = {
  signIn: async (objeto) => {
    const response = await Api.post(`customer/signin`, objeto);
    console.log(response);
    return response;
  },

  myBalance: async (objeto) => {
    const response = await Api.get("account/extractbalance", {
      headers: {
        "x-customer-id": objeto,
      },
    });
    return response;
  },
};

export default LoginService;
