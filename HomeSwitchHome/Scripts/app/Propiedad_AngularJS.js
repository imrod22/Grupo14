var switchHomeApp = angular.module('homeswitchhome', []);

switchHomeApp.controller('propiedadesController', function ($scope, $http) {
    
    $http.get("/Propiedad/Propiedad/Propiedades").then(function (result) {

        $scope.propiedadesList = result.data;
        $scope.propiedadesListFiltradas = result.data;
    });

    $scope.filtroNombre = '';
    $scope.filtroPais = '';

    $scope.filtrar = function () {
        $scope.propiedadesListFiltradas = $scope.propiedadesList.filter(filtroPropiedades);
    }

    function filtroPropiedades(prop) {
        return prop.Nombre.toUpperCase().includes($scope.filtroNombre.toUpperCase()) && prop.Pais.includes($scope.filtroPais.toUpperCase());
    }

    $scope.detallar = function (element) {

        $scope.detallenombre = "";
        $scope.detalledescripcion = "";
        $scope.detallepais = "";
        $scope.detalleimagenes = [];

        var idPropiedad = element;

        $http.post("/Propiedad/Propiedad/ObtenerInformacionPropiedad",
        {
            'idPropiedad': idPropiedad
        }

        ).then(function successCallback(response) {

            $scope.intervaloimagen = 5000;

            $scope.detallenombre = response.data.Nombre;
            $scope.detalledescripcion = response.data.Descripcion;
            $scope.detallepais = response.data.Pais;

            $scope.detalleimagenes = response.data.Imagenes;
            
        }, function errorCallback() {
            swal("Home Switch Home", "Se ha producido un error en el servidor.", "error");  
        });
    }

    $scope.elegirfecha = function (element) {

        $scope.propiedadreserva = element;
        $scope.fechareserva = '';

    }
    
    $scope.reservarpropiedad = function () {

        var fechaSeleccionada = $('#fechareserva').val();


        $http.post("/Propiedad/Propiedad/ReservarPropiedad",
            {
                'idPropiedad': $scope.propiedadreserva,
                'fecha': fechaSeleccionada
            }

        ).then(function successCallback(response) {
                swal("Home Switch Home", "Se ha reservado la residencia en la fecha seleccionada.", "success"); 

        }, function errorCallback(jqXHR) {
                swal("Home Switch Home", jqXHR.data, "error");
        });
    }

    $scope.solicitarNovedad = function (element) {

        var idPropiedad = element;

        $http.post("/Propiedad/Propiedad/SolicitarNovedadPropiedad",
            {
                'idPropiedad': idPropiedad
            }

        ).then(function successCallback(response) {

        }, function errorCallback() {

        });
    }

});