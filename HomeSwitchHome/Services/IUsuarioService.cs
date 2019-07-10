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

        bool ExisteUsuario(string usuario);

        ClienteViewModel ObtenerInformacionDeUsuario(string usuario);

        string RegistrarNuevoCliente(ClienteViewModel nuevoCliente);

        string ConfirmarNuevoCliente(int idCliente);

        string RegistrarComoPremium(int idCliente);

        string ConfirmarPremium(int IdCliente);

        string RechazarSolicitudNuevoCliente(int IdCliente);

        string RechazarSolicitudPremium(int IdCliente);

        List<ClienteViewModel> ObtenerNuevosClientes();

        List<ClienteViewModel> ObtenerSolicitudesPremium();

    }
}
