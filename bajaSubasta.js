{
    // dentro del controller de subasta

    if (!($scope.fechaComienzo <= Date.now)) {
        $http.post("/Home/EliminarSubasta", {
            'idPropiedad': $scope.idPropiedad
        }).then(function (response) {
            console.log("Se ha eliminado la subasta con éxito.")
        }.error(
        )
        ))
    }

}