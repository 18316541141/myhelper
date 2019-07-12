myApp.controller("upload-image", function ($scope) {
    $scope.testImg = function () {
        console.log($scope.imgName + ':' + $scope.thumbnailName);
    };
});