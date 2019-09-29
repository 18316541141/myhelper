myApp.controller('heartbeatEntity', function ($scope, $timeout, $myHttp) {
    $scope.postData = {
        robotMacLike: '',
        remarkLike: '',
        extendFieldLike: '',
        lastHeartbeatTimeStart: '',
        lastHeartbeatTimeEnd: ''
    };
    $scope.cols = [
        { field: "extendField", title: '扩展内容', sort: true },
        { field: "lastHeartbeatTime", title: '最近一次的心跳时间', sort: true },
        { field: "robotMac", title: '机器人ip', sort: true },
        { field: "remark", title: '机器人备注', sort: true },
        {
            field: "id", title: '运行状态', templet: function (data) {
                return '<div class="layui-btn-container">' +
                    '<button class="layui-btn layui-btn-sm" lay-event="delete" type="button">删除</button>' +
                '</div>';
            }
        }
    ];
    $scope.perms = {
        enabled: false,
        disabled: false,
        add: false,
        'export': false,
        delBatch: false,
        del: false,
        edit: false
    };
    $scope.search = function () {
        $scope.$broadcast('searchPage');
    };
    $scope.refresh = function () {
        for (var key in $scope.postData) {
            if ($scope.postData.hasOwnProperty(key)) {
                $scope.postData[key] = null;
            }
        }
        $scope.$broadcast('searchPage');
    };
    $scope.$on('rowOper', function (event, type, data) {
        if (type === 'delete') {

        }
    });
});