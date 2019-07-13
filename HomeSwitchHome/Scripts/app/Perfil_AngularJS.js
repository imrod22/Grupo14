var switchHomeApp = angular.module('homeswitchhome', []);


switchHomeApp.controller('perfilcontroller', function ($scope, $http) {

    $scope.usuario;
    $scope.misreservas;
    $scope.anioactual = new Date();
    $scope.cantidadcreditos;

    $http.get("/Perfil/Perfil/ObtenerCreditosCliente").then(function (result) {
        $scope.cantidadcreditos = result.data;
    });

    $http.get("/Perfil/Perfil/ObtenerMisReservas").then(function (result) {
        $scope.misreservas = result.data;
    });

    $http.get("/Perfil/Perfil/ObtenerMiInformacionPersonal").then(function (result) {
        $scope.usuario = result.data;
    });

    $scope.solicitarsubscripcion = function () {
        $http.post("/Perfil/Perfil/SolicitarSubscripcionPremium").then(function successCallback(result) {

            swal("Home Switch Home", result.data, "success");

        }, function errorCallback(jqXHR) {
            swal("Home Switch Home", jqXHR.data, "error");
        });

    }

    $scope.cancelarreserva = function (element) {
        var idReserva = element;

        $http.post("/Perfil/Perfil/CancelarReserva", {
            'idReserva': idReserva

        }).then(function successCallback(result) {

            swal("Home Switch Home", result.data, "success");

            $http.get("/Perfil/Perfil/ObtenerCreditosCliente").then(function (result) {
                $scope.cantidadcreditos = result.data;
            });

            $http.get("/Perfil/Perfil/ObtenerMisReservas").then(function (result) {
                $scope.misreservas = result.data;
            });

        }, function errorCallback(jqXHR) {
            swal("Home Switch Home", jqXHR.data, "error");
        });

    }
})