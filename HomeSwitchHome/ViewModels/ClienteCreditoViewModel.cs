namespace HomeSwitchHome.ViewModels
{
    public class ClienteCreditoViewModel
    {
        public int idClienteCredito;

        public int idCliente;

        public int Credito;

        public int Anio;

        public ClienteViewModel Cliente;

        public ClienteCreditoViewModel() { }

        public ClienteCreditoViewModel ToViewModel(CREDITO_CLIENTE creditoDB) {

            this.idClienteCredito = creditoDB.IdCreditoCliente;
            this.idCliente = creditoDB.IdCliente;
            this.Credito = creditoDB.Credito;
            this.Anio = creditoDB.Anio;

            this.Cliente = new ClienteViewModel().ToViewModel(creditoDB.CLIENTE);

            return this;
        }
    }
}