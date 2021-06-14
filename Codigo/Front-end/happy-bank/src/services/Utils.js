export default {
  formatarMoeda: (valor) => {
    return valor.toLocaleString("pt-BR", {
      style: "currency",
      currency: "BRL",
    });
  },
  formatarData: (valor) => {
    return valor ? valor.split("T")[0].split("-").reverse().join("/") : valor;
  },
};
