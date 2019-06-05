var adminsection = angular.module('adminsection', []);

adminsection.controller('admincontroller', function ($scope, $http) {

    $scope.subastasoff;
    $scope.subastasfin;

    $scope.parapremiums;
    $scope.nuevosclientes;

    $scope.propiedades;

    $http.get("/Administrador/Administrador/SubastasSinEmpezar").then(function (result) {
        $scope.subastasoff = result.data;
    });

    $http.get("/Administrador/Administrador/SubastasCerradas").then(function (result) {
        $scope.subastasfin = result.data;
    });
    
    $http.get("/Administrador/Administrador/Propiedades").then(function (result) {
        $scope.propiedades = result.data;
    });

    $http.get("/Administrador/Administrador/ClientesAPremium").then(function (result) {
        $scope.parapremiums = result.data;
    });

    $http.get("/Administrador/Administrador/NuevosClientes").then(function (result) {
        $scope.nuevosclientes = result.data;
    });

});