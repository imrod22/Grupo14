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

    function datosDePropiedadCorrectos() {
        return (
            ($("#nombrePropiedad").val().length >= 8)
            && ($("#descripcionPropiedad").val().length >= 20))
    }

    $scope.crearpropiedad = function () {

        if (datosDePropiedadCorrectos()) {
            $http.post("/Administrador/Administrador/CrearPropiedad", {
                'nombre': $scope.nombre,
                'descripcion': $scope.descripcion,
                'pais': $scope.pais
            }).then(function successCallback(response) {

                if (response.data == "") {
                    swal("Home Switch Home", "No se ha podido crear la residencia con los campos ingresados.", "error");

                }
                else {
                    $scope.propiedades = response.data;
                    swal("Home Switch Home", "Se ha creado la residencia con éxito.", "success");                   
                }

                $('#addEditPropiedadModal').modal('hide');

            }, function errorCallback() {
                swal("Home Switch Home", "No se ha podido crear la residencia con los campos ingresados.", "error");
                $('#addEditPropiedadModal').modal('hide');
            });
        }
        else {
            swal("Home Switch Home", "No se han ingresado los datos correctamente.", "warning");
            
        }
    }

    $scope.modificarpropiedad = function () {
        if (datosDePropiedadCorrectos()) {
            $http.post("/Administrador/Administrador/ModificarPropiedad", {

                'idpropiedad': $("#identificadorPropiedad").val(),
                'descripcion': $("#descripcionPropiedad").val(),
                'pais': $("#paisPropiedad").val()

            }).then(function successCallback(response) {

                if (response.data == "") {
                    swal("Home Switch Home", "No se ha podido actualizar la residencia con los campos ingresados.", "error");

                }
                else {
                    swal("Home Switch Home", "Se ha actualizado la residencia con éxito.", "success");
                    $scope.propiedades = response.data;

                }

                $('#addEditPropiedadModal').modal('hide');

            }, function errorCallback() {
                swal("Home Switch Home", "Ha ocurrido un error en el servidor.", "error");
                $('#addEditPropiedadModal').modal('hide');
            });
        }
        else {
            swal("Home Switch Home", "No se han ingresado todos los datos correctamente.", "warning");
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
                swal("Home Switch Home", "No se ha podido eliminar la residencia, tiene subastas asociadas o reservas futuras.", "warning");
            }

            else {
                alert("La residencia seleccionada ha sido eliminada.");
                $scope.propiedades = response.data;
            }

        }, function errorCallback() {
            swal("Home Switch Home", "No se puede eliminar la residencia. Ha ocurrido un error en el servidor", "error");
           
        });
    }

    $scope.crearsubasta = function () {

        if (valoresDeSubastaAceptados()) {

            var idPropiedad = $('#propiedad_select option:selected').attr('id');

            $http.post("/Administrador/Administrador/CrearSubasta", {
                'propiedad': idPropiedad,
                'valorMinimo': $scope.valorMinimo,
                'fechaComienzo': $('input[name="fechasubasta"]').val()

            }).then(function successCallback(response) {

                if (response.data == "") {
                    swal("Home Switch Home", "No se ha podido crear la subasta para la propiedad en la fecha seleccionada.", "error");
                }
                else {
                    swal("Home Switch Home", "Se ha creado la subasta con éxito.", "success");
                    $scope.subastasoff = response.data;
                }
                $('#addEditSubastaModal').modal('hide');


            }, function errorCallback() {
                swal("Home Switch Home", "No se ha podido crear la subasta con los campos ingresados.", "error");
                $('#addEditSubastaModal').modal('hide');
            });

        }

        else {
            swal("Home Switch Home", "Los valores ingresados no son correctos.", "warning");            
        }        
    }

    $scope.modificarsubasta = function () {
        if (valoresDeSubastaAceptados()) {
            $http.post("/Administrador/Administrador/ModificarSubasta", {

                'idSubasta': $("#identificadorSubasta").val(),
                'valorMinimo': $("#valorMinimo").val()

            }).then(function successCallback(response) {

                if (result.data == "") {
                    swal("Home Switch Home", "El monto de la subasta no puede ser negativo.", "error");
                    
                }
                else {
                    swal("Home Switch Home", "Se ha actualizado la subasta con éxito.", "success");
                    $scope.subastasoff = response.data;

                }

                $('#addEditSubastaModal').modal('hide');

            }, function errorCallback() {
                    $('#addEditSubastaModal').modal('hide');
                    swal("Home Switch Home", "No se ha podido actualizar la subasta con los campos ingresados.", "error");
            });
        }
        else {
            swal("Home Switch Home", "No se han ingresado todos los datos correctamente!", "warning");
        }
    }

    $scope.borrarsubasta = function (element) {
        var idSubasta = element;

        $http.post("/Administrador/Administrador/BorrarSubasta",
            {
                'idSubasta': idSubasta
            }

        ).then(function successCallback(result) {

            if (result.data == null)
            {
                swal("Home Switch Home", "No se ha podido eliminar la subasta seleccionada.", "error");
            }

            swal("Home Switch Home", "La subasta seleccionada ha sido eliminada.", "success");
            $scope.subastasoff = result.data;

        }, function errorCallback() {
            swal("Home Switch Home", "No se ha podido eliminar la subasta. Ha ocurrido un error en el servidor.", "error");
            
        });
    }

    $scope.aceptarnuevousuario = function (element) {

        var idCliente = element;

        $http.post("/Administrador/Administrador/AceptarNuevoUsuario",
            {
                'idCliente': idCliente
            }

        ).then(function successCallback(result) {

            if (result.data == null) {
                swal("Home Switch Home", "El sistema no puede validar el usuario seleccionado.", "error");
            }

            swal("Home Switch Home", "Se ha procesado el registro para el nuevo usuario.", "success");
            $scope.nuevosclientes = result.data;

        }, function errorCallback() {
            swal("Home Switch Home", "No se ha podido validar la solicitud. Ha ocurrido un error en el servidor.", "error");

        });
    }

    $scope.confirmarpremium = function (element) {

        var idCliente = element;

        $http.post("/Administrador/Administrador/AceptarPremium",
            {
                'idCliente': idCliente
            }

        ).then(function successCallback(result) {

            if (result == null) {
                swal("Home Switch Home", "El sistema no puede validar el usuario seleccionado.", "error");
            }

            swal("Home Switch Home", "Se ha procesado la solicitud premium del usuario seleccionado.", "success");
            $scope.parapremiums = result.data;

        }, function errorCallback() {
            swal("Home Switch Home", "No se ha podido validar la solicitud. Ha ocurrido un error en el servidor.", "error");

        });
    }

    function valoresDeSubastaAceptados()
    {
        return $.isNumeric($scope.valorMinimo);
    }

});