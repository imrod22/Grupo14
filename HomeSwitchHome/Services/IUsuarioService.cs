using HomeSwitchHome.ViewModels;
using System.Collections.Generic;

namespace HomeSwitchHome.Services
{
    public interface IUsuarioService
    {
        USUARIO ObtenerUsuarioRegistrado(string usuario, string password);

        ClienteViewModel ObtenerInformacionCliente(int IdUsuario);

        PREMIUM ObtenerInformacionPremium(int IdCliente);

        bool EsUsuarioPremium(int IdCliente);

        bool EsAdmin(int IdUsuario);

        bool RegistrarNuevoCliente(ClienteViewModel nuevoCliente);

        void RegistrarComoPremium(int IdCliente);

        List<ClienteViewModel> ObtenerNuevosClientes();

        List<ClienteViewModel> ObtenerSolicitudesPremium();

    }
}
