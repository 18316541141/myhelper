myApp.controller('editLogEntity', function ($scope, $timeout, $myHttp) {
    $myHttp.get('/LogEntity/load', { id: $scope.edit.data.id }).mySuccess(function (result) {
        $scope.edit.formData = result.data;
    });
});