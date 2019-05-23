var currentApp = angular.module('homeswitchhome', []);

currentApp.controller('propiedadesController', function ($scope, $http) {

    $scope.propiedadesList;
    $http.get("/Home/GetPropiedades").then(function (result) {

        $scope.propiedadesList = result.data;
    });

    $scope.aceptar = function () {

        $http.post("/Home/AddPropiedad", {
            'nombre': $scope.nombre,
            'domicilio': $scope.domicilio,
            'descripcion': $scope.descripcion,
            'pais': $scope.pais
        }).then(function (response) {
            console.log("Se ha creado la residencia con éxito.")
        })
    };

    function corroborarInputs() {
        return (
            ($scope.nombre.length < 8)
            || ($scope.descripcion.length < 20)
            || ($scope.ubicacion == ''))
    }

    $scope.modificar = function(){
        if (!corroborarInputs()) {
            $http.post("/Home/ModificarPropiedad", {
                'nombre': $scope.nombre,
                'domicilio': $scope.domicilio,
                'descripcion': $scope.descripcion,
                'pais': $scope.pais
            }).then(function (response) {
                console.log("Se ha creado la residencia con éxito.");
                alert("Se ha creado la residencia con éxito.");
            })
        }
    }
    
});

currentApp.controller('subastasController', function ($scope, $http) {

    $scope.subastasList;
    $http.get("/Home/GetSubastas").then(function (result) {
        $scope.subastasList = result.data;
    });

    $scope.propiedadSubasta;

    $http.get("/Home/GetPropiedades").then(function (result) {

        console.log(result.data);
        $scope.propiedadSubasta = result.data;
    }); 

    $scope.aceptar = function () {

        $http.post("/Home/AddSubasta", {
            'idPropiedad': $scope.idPropiedad,
            'valorMinimo': $scope.valorMinimo,
            'fechaComienzo': $scope.fechaComienzo

        }).then(function (response) {
            console.log("Se ha creado la subasta con éxito.")
            alert("Se ha creado la subasta con éxito.")
        });
        
    };

    $scope.pujar = function () {
        $http.post("/Home/NuevaPuja", {
            'idSubasta': $scope.idSubasta,
            'valorActual': $scope.valorActual

        }).then(function (response) {
            console.log("Se ha creado la subasta con éxito.")
        });
    }
});

function ModificarResidencia() {
    var dataURL = $(this).value;
    var url = '@Html.Raw(Url.Action("ModificarPropiedad", "Home", new {idPropiedad = @dataURL}))';
    window.location.href = url;
}