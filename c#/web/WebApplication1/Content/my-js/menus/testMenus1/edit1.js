myApp.controller('testEdit1', function ($scope, $myHttp) {
    $myHttp.get('/IRobotQrCodePayTask/load', { irTaskID: $scope.edit.data.irTaskID }).mySuccess(function (result) {
        $scope.edit.postData = result.data;
    });
    $scope.edit.save = function () {

    };
});