var switchHomeApp = angular.module('homeswitchhome', []);

switchHomeApp.controller('perfilController', function ($scope, $http) {

    $scope.reservasGeneralesList;
    $scope.subastasFinalizadasList;

    $http.get("/Perfil/Perfil/SubastasFinalizadas", { 'IdCliente: ' } ).then(function (result) {

        $scope.subastasFinalizadasList = result.data;

    });

})