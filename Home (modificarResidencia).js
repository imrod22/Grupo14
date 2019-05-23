{
    // adentro del controller de propiedades

    function corroborarInputs() {
        // no verifica que no exista una con el mismo nombre
        return (
            ($scope.nombre == '' || $scope.nombre.size < 8)
            || ($scope.descripcion.size < 20)
            || ($scope.ubicacion == '')
        )
    }

    $scope.aceptar() {
        if (!corroborarInputs()) {
            $http.post("/Home/ModificarPropiedad", {
                'nombre': $scope.nombre,
                'domicilio': $scope.domicilio,
                'descripcion': $scope.descripcion,
                'pais': $scope.pais
            }).then(function (response) {
                console.log("Se ha creado la residencia con éxito.")
            }.error(
                alert("No se puede agregar la propiedad. Ya existe.")
            )
        }
    }
}