var switchHomeApp = angular.module('homeswitchhome', []);

switchHomeApp.controller('hotsalecontroller', function ($scope, $http) {

    $scope.hotsales;

    $http.get("/Hotsale/Hotsale/ObtenerHotSales").then(function (result) {

        $scope.hotsales = result.data;
    });


    $scope.reservahotsale = function (element) {
        var idHotSale = element;

        $http.post("/Hotsale/Hotsale/ReservarHotSale",
            {
                'idHotSale': idHotSale
            }

        ).then(function successCallback(result) {

            swal("Home Switch Home", result.data, "success");

            $http.get("/Hotsale/Hotsale/ObtenerHotSales").then(function (result) {
                $scope.hotsales = result.data;
            });

        }, function errorCallback(jqXHR) {
            swal("Home Switch Home", jqXHR.data, "error");

        });
    }

});