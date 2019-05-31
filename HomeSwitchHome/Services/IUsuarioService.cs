using HomeSwitchHome.ViewModels;
using System.Collections.Generic;

namespace HomeSwitchHome.Services
{
    public interface IUsuarioService
    {
        USUARIO ObtenerUsuarioRegistrado(string usuario, string password);

        ClienteViewModel ObtenerInformacionCliente(int IdUsuario);

        bool EsUsuarioPremium(int IdCliente);

        bool EsAdmin(int IdUsuario);

        void RegistrarNuevoCliente(ClienteViewModel nuevoCliente);

        void RegistrarComoPremium(int IdCliente);

        List<ClienteViewModel> ObtenerListaDeClientes();

    }
}
