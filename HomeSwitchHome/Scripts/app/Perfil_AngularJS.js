var switchHomeApp = angular.module('homeswitchhome', []);


switchHomeApp.controller('perfilcontroller', function ($scope, $http) {

    $scope.usuario;
    $scope.reservasactuales;
    $scope.reservasproximas;
    $scope.anioactual = new Date();
    $scope.anioproximo = $scope.anioactual.getFullYear() + 1;

    console.log($scope.anioactual);
    console.log($scope.anioproximo);

    $scope.actualcreditos;
    $scope.proximocreditos;

    $http.get("/Perfil/Perfil/ObtenerCreditosActuales").then(function (result) {
        $scope.actualcreditos = result.data;
    });

    $http.get("/Perfil/Perfil/ObtenerCreditosProximos").then(function (result) {
        $scope.proximocreditos = result.data;
    });    

    $http.get("/Perfil/Perfil/ObtenerReservasActuales").then(function (result) {
        $scope.reservasactuales = result.data;
    });

    $http.get("/Perfil/Perfil/ObtenerReservasProximas").then(function (result) {
        $scope.reservasproximas = result.data;
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

            $http.get("/Perfil/Perfil/ObtenerCreditosActuales").then(function (result) {
                $scope.actualcreditos = result.data;
            });

            $http.get("/Perfil/Perfil/ObtenerCreditosProximos").then(function (result) {
                $scope.proximocreditos = result.data;
            });

            $http.get("/Perfil/Perfil/ObtenerReservasActuales").then(function (result) {
                $scope.reservasactuales = result.data;
            });

            $http.get("/Perfil/Perfil/ObtenerReservasProximas").then(function (result) {
                $scope.reservasproximas = result.data;
            });

        }, function errorCallback(jqXHR) {
            swal("Home Switch Home", jqXHR.data, "error");
        });

    }
})