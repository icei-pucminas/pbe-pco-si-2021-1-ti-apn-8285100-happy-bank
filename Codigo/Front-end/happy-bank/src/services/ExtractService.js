import Api from "./api";

const ExtractService = {
  Extract: async (objeto, start, end) => {
    const response = await Api.get(
      `account/extractstatement?start=${start}&end=${end}`,
      {
        headers: {
          "Content-Type": "application/json, text/plain, */*",
          "x-customer-id": objeto,
        },
      }
    );
    return response;
  },
};

export default ExtractService;
