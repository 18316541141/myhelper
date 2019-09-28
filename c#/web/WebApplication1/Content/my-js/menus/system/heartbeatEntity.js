myApp.controller('heartbeatEntity', function ($scope, $timeout, $myHttp) {
    $scope.postData = {
        robotMacLike:'',
        remarkLike:'',
        extendFieldLike:'',
        lastHeartbeatTimeStart: '',
        lastHeartbeatTimeEnd: ''
    };
});