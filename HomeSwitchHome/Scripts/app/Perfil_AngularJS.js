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

})