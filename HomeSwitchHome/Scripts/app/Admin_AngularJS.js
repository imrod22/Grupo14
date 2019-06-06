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

    function corroborarInputs() {
        return (
            ($("#nombrePropiedad").val().length >= 8)
            && ($("#descripcionPropiedad").val().length >= 20))
    }

    $scope.crearpropiedad = function () {

        if (corroborarInputs()) {
            $http.post("/Administrador/Administrador/CrearPropiedad", {
                'nombre': $scope.nombre,
                'descripcion': $scope.descripcion,
                'pais': $scope.pais
            }).then(function successCallback(response) {

                if (response.data == "") {
                    alert("No se ha podido crear la residencia con los campos ingresados.");
                }
                else {
                    alert("Se ha creado la residencia con éxito.");
                    $scope.propiedades = response.data;
                }

                $('#addEditPropiedadModal').modal('hide');

            }, function errorCallback() {
                $('#addEditPropiedadModal').modal('hide');
                alert("No se ha podido crear la residencia con los campos ingresados.");
            });
        } else {

            alert("No se han ingresado todos los datos correctamente!");
        }
    }

    $scope.modificarpropiedad = function () {
        if (corroborarInputs()) {
            $http.post("/Administrador/Administrador/ModificarPropiedad", {

                'idpropiedad': $("#identificadorPropiedad").val(),
                'descripcion': $("#descripcionPropiedad").val(),
                'pais': $("#paisPropiedad").val()

            }).then(function successCallback(response) {

                if (response.data == "") {
                    alert("No se ha podido actualizar la residencia con los campos ingresados.");

                }
                else {
                    alert("Se ha actualizado la residencia con éxito.");
                    $scope.propiedades = response.data;

                }

                $('#addEditPropiedadModal').modal('hide');

            }, function errorCallback() {
                $('#addEditPropiedadModal').modal('hide');
                alert("No se ha podido actualizar la residencia con los campos ingresados.");
            });
        }
        else {

            alert("No se han ingresado todos los datos correctamente!");
        }
    }

    $scope.borrarpropiedad = function (element) {
        var idPropiedad = element;

        $http.post("/Administrador/Administrador/BorrarPropiedad",
            {
                'idPropiedad': idPropiedad
            }

        ).then(function successCallback(response) {

            if (response.data == "") {
                alert("No se ha podido eliminar la residencia, tiene subastas asociadas o reservas futuras!");
            }

            else {
                alert("La residencia seleccionada ha sido eliminada.");
                $scope.propiedades = response.data;
            }

        }, function errorCallback() {
            alert("No se ha podido eliminar la residencia, tiene subastas asociadas o reservas futuras!");
        });
    }

    $scope.crearsubasta = function () {

        var idPropiedad = $('#propiedadSelect option:selected').attr('id');

        $http.post("/Administrador/Administrador/CrearSubasta", {
            'propiedad': idPropiedad,
            'valorMinimo': $scope.valorMinimo,
            'fechaComienzo': $scope.fechaComienzo

        }).then(function successCallback(response) {
            alert("Se ha creado la subasta con éxito.");

            $('#addEditSubastaModal').modal('hide');

            $scope.subastasoff = response.data;

        }, function errorCallback() {
            $('#addEditSubastaModal').modal('hide');
            alert("No se ha podido crear la subasta con los campos ingresados.");
        });
    }

    $scope.modificarsubasta = function () {
        if (!sonDatosInvalidos()) {
            $http.post("/Administrador/Administrador/ModificarSubasta", {

                'idSubasta': $("#identificadorSubasta").val(),
                'fechaComienzo': $("#fechaSubasta").val(),
                'valorMinimo': $("#valorMinimo").val()

            }).then(function successCallback(response) {

                if (result.data == "") {
                    alert("No se ha podido actualizar la subasta con los campos ingresados.");

                }
                else {
                    alert("Se ha actualizado la subasta con éxito.");
                    $scope.subastasoff = response.data;

                }

                $('#addEditSubastaModal').modal('hide');

            }, function errorCallback() {
                $('#addEditSubastaModal').modal('hide');
                alert("No se ha podido actualizar la subasta con los campos ingresados.");
            });
        }
        else {

            alert("No se han ingresado todos los datos correctamente!");
        }
    }

    $scope.borrarsubasta = function (element) {
        var idSubasta = element;

        $http.post("/Administrador/Administrador/BorrarSubasta",
            {
                'idSubasta': idSubasta
            }

        ).then(function successCallback(result) {

            alert("La subasta seleccionada ha sido eliminada.");
            $scope.subastasoff = result.data;

        }, function errorCallback() {

            alert("No se ha podido eliminar la subasta");
        });
    }
});