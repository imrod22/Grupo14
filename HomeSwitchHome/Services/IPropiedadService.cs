using HomeSwitchHome.ViewModels;
using System.Collections.Generic;

namespace HomeSwitchHome.Services
{
    public interface IPropiedadService
    {
        List<PropiedadViewModel> ObtenerPropiedades();

        string CrearPropiedad(PROPIEDAD nuevaPropiedad);

        string ActualizarPropiedad(PROPIEDAD datosPrioridad);

    }
}
