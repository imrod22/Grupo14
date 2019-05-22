var currentApp = angular.module('homeswitchhome', []);

currentApp.controller('propiedadesController', function ($scope, $http) {

    $scope.propiedadesList;
    $http.get("/Home/GetPropiedades").then(function (result) {
        
        $scope.propiedadesList = result.data;
    })
});