import Api from "./api";

const TransferService = {
  doTransfer: async (objeto, id) => {
    const response = await Api.post(`account/transfer`, objeto, {
      headers: {
        "Content-Type": "application/json, text/plain, */*",
        "x-customer-id": id,
      },
    });
    console.log(response);
    return response;
  },
};

export default TransferService;
