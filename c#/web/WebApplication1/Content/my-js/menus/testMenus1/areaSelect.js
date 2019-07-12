myApp.controller('testAreaSelect', function ($scope, $timeout) {
    //$timeout(function () {
    //    $scope.$broadcast('renderSelect', "19", "202", "1787");
    //},1000);
    $scope.showSelect = function () {
        var retVal = {};
        $scope.$broadcast('getSelectVal', retVal);
        console.log(retVal.provinceVal + ":" + retVal.cityVal + ":" + retVal.countyVal);
    };
});