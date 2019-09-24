myApp.controller('logEntity', function ($scope, $timeout, $myHttp, layer) {
    $scope.cols = [
        { field: "createDate", title: '日志日期', sort: true },
        { field: "level", title: '日志等级', sort: true },
        { field: "threadNo", title: '线程号', sort: true },
        { field: "message", title: '日志信息', sort: true },
        { field: "projectName", title: '项目名称', sort: true },
        { field: "typeName", title: '异常发生类', sort: true },
        { field: "funcName", title: '异常发生方法', sort: true },
        { field: "exception", title: '堆栈信息', sort: true }
    ];
    $scope.perms = {
        enabled: false,
        disabled: false,
        add: false,
        'export': false,
        delBatch: false,
        del: false,
        edit: true
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
        if (type === 'edit') {
            $scope.edit = {
                type: 'edit',
                data: data
            };
            layer.ngOpen({
                type: 1,
                area: ['600px', '400px'],
                contentUrl: 'menus/system/editLogEntity.html',
                scope: $scope,
                title: '修改'
            });
        }
    });
});