myApp.controller('logEntity', function ($scope, $timeout, $myHttp, layer) {
    $scope.postData = {
        level: '',
        threadNoLike: '',
        messageLike: '',
        exceptionLike: '',
        projectNameLike: '',
        typeNameLike: '',
        funcNameLike: '',
        usernameLike: $.cookie('username'),
        createDateStart: moment().subtract("seconds", 1800).format("YYYY-MM-DD HH:mm:ss"),
        createDateEnd: moment().format("YYYY-MM-DD HH:mm:ss"),
    };
    $scope.cols = [
        { field: "username", title: '日志用户名', sort: true },
        { field: "createDate", title: '日志日期', sort: true },
        { field: "level", title: '日志等级', sort: true },
        { field: "threadNo", title: '线程号', sort: true },
        { field: "message", title: '日志信息', sort: true },
        { field: "projectName", title: '项目名称', sort: true },
        { field: "typeName", title: '异常发生类', sort: true },
        { field: "funcName", title: '异常发生方法', sort: true },
        { field: "exception", title: '堆栈信息', sort: true },
        {
            field: "id", title: '操作', templet: function (data) {
                return '<div class="layui-btn-container">' +
                    '<button class="layui-btn layui-btn-sm" lay-event="load" type="button">查看</button>' +
                '</div>';
            }
        },
    ];
    $scope.options = [
        { text: '请选择错误等级', value: '' },
        { text: 'ALL', value: 'ALL' },
        { text: 'DEBUG', value: 'DEBUG' },
        { text: 'INFO', value: 'INFO' },
        { text: 'WARN', value: 'WARN' },
        { text: 'ERROR', value: 'ERROR' },
        { text: 'FATAL', value: 'FATAL' },
        { text: 'OFF', value: 'OFF' }
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
        if (type === 'load') {
            $scope.edit = {
                type: 'edit',
                data: data
            };
            layer.ngOpen({
                type: 1,
                area: ['630px', '700px'],
                contentUrl: 'menus/system/editLogEntity.html',
                scope: $scope,
                title: '查看'
            });
        }
    });
});