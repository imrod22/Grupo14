var switchHomeApp = angular.module('homeswitchhome', []);


switchHomeApp.controller('perfilcontroller', function ($scope, $http) {

    $scope.usuario;
    $scope.misreservas;

    $http.get("/Perfil/Perfil/ObtenerMisReservas").then(function (result) {
        $scope.misreservas = result.data;
    });

    $http.get("/Perfil/Perfil/ObtenerMiInformacionPersonal").then(function (result) {
        $scope.usuario = result.data;
    });

    $scope.solicitarsubscripcion = function () {
        $http.post("/Perfil/Perfil/SolicitarSubscripcionPremium").then(function successCallback(result) {

            swal("Home Switch Home", "Su solicitud esta siendo procesada. Se le notificara cuando pueda acceder a las nuevas funcionalidades.", "success");


        }, function errorCallback(jqXHR) {
            swal("Home Switch Home", jqXHR.data, "error");
        });

    }
})