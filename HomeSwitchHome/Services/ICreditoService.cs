using HomeSwitchHome.ViewModels;
using System.Collections.Generic;

namespace HomeSwitchHome.Services
{
    public interface ICreditoService
    {
        bool GenerarCreditosCliente(ClienteViewModel cliente);

        bool DescontarCreditoCliente(int clienteId, int anio);

        bool DevolverCreditoCliente(int clienteId, int anio);

        ClienteCreditoViewModel ObtenerCreditosAnio(int anio, int clienteId);
    }
}