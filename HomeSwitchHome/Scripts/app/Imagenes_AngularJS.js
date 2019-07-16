var imgmodule = angular.module('adminsection', [])

imgmodule.controller('imgcontroller',

    function MyController($scope, $http) {

        $scope.uploadme;

        $scope.uploadImage = function () {
            var fd = new FormData();
            var imgBlob = dataURItoBlob($scope.uploadme);
            fd.append('file', imgBlob);
            $http.post(
                'imageURL',
                fd, {
                    transformRequest: angular.identity,
                    headers: {
                        'Content-Type': undefined
                    }
                }
            )
                .success(function (response) {
                    console.log('success', response);
                })
                .error(function (response) {
                    console.log('error', response);
                });
        }

        function dataURItoBlob(dataURI) {
            var binary = atob(dataURI.split(',')[1]);
            var mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0];
            var array = [];
            for (var i = 0; i < binary.length; i++) {
                array.push(binary.charCodeAt(i));
            }
            return new Blob([new Uint8Array(array)], {
                type: mimeString
            });
        }

    });


app.directive("fileread", [
    function () {
        return {
            scope: {
                fileread: "="
            },
            link: function (scope, element, attributes) {
                element.bind("change", function (changeEvent) {
                    var reader = new FileReader();
                    reader.onload = function (loadEvent) {
                        scope.$apply(function () {
                            scope.fileread = loadEvent.target.result;
                        });
                    }
                    reader.readAsDataURL(changeEvent.target.files[0]);
                });
            }
        }
    }
]);