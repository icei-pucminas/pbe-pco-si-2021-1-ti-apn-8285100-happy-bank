import Api from "./api";
const CreateAccountService = {
  CustomerRegister: async (objeto) => {
    const response = await Api.post("/customer/register", objeto);
    return response;
  },
};

export default CreateAccountService;
