myApp.controller('addNewAlaram', function ($scope, $realTime) {
    $scope.adddd = function () {
        $realTime.getUpdate('/NewAlarm/add', {}, ['newsAlarm']).mySuccess(function () {

        });
    };
});