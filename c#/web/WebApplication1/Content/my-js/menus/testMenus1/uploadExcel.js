myApp.controller('testUploadExcel', function ($scope) {
    $scope.postData = { otherData: 'asdasdasd' };
    $scope.upload = function () {
        $scope.$broadcast('submit');
    }
});