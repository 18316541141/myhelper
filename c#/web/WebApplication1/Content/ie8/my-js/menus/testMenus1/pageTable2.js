myApp.controller('pageTableTest2', function ($scope, $timeout, $myHttp, layer) {
    /**
     * 表格列设置，field：字段名、title：表格标题、sort：是否显示排序按钮
     */
    $scope.cols = [
        { field: 'irOrderNo', title: 'irOrderNo', sort: true },
        { field: 'irWeiXinNickName', title: 'irWeiXinNickName', sort: true },
        { field: 'irWeiXinHeaderImage', title: 'irWeiXinHeaderImage', sort: true },
        { field: 'irQrCodeImagePath', title: 'irQrCodeImagePath', sort: true },
        { field: 'irHandleState', title: 'irHandleState', sort: true },
        { field: 'irHandleMessage', title: 'irHandleMessage', sort: true },
        { field: 'irHandleTime', title: 'irHandleTime', sort: true },
        { field: 'irCreateTime', title: 'irCreateTime', sort: true },
        { field: 'irReportPicPathJson', title: 'irReportPicPathJson', sort: true },
        { field: 'irTakeMoney', title: 'irTakeMoney', sort: true },
        { field: 'irRobotId', title: 'irRobotId', sort: true },
        { field: 'irRemark', title: 'irRemark', sort: true },
        { field: 'irPushState', title: 'irPushState', sort: true },
        { field: 'irPushTime', title: 'irPushTime', sort: true },
        { field: 'irScanPayNotifyRet', title: 'irScanPayNotifyRet', sort: true }
    ];
    $scope.perms = {
        enabled: true,
        disabled: true,
        add: true,
        'export': true,
        delBatch: true,
        del: true,
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
    $scope.$on('tableOper', function (event, type, data) {
        if (type === 'export') {
            downExcel('/IRobotQrCodePayTask/export', '测试excel', $scope.postData, $scope.count)
        } else if (type === 'delBatch') {
            var postData = {};
            for (var i = 0, len = data.length; i < len ; i++) {
                postData['irTaskIds[' + i + ']'] = data[i].irTaskID;
            }
            $myHttp.post('/IRobotQrCodePayTask/delBatch', postData).mySuccess(function (result) {
                $scope.search();
            });
        }
    });
    $scope.$on('rowOper', function (event, type, data) {
        if (type === 'edit') {
            $scope.edit = {
                type: 'edit',
                data: data
            };
            layer.ngOpen({
                type: 1,
                area: ['600px', '400px'],
                contentUrl: 'menus/testMenus1/edit1.html',
                scope: $scope,
                title: '修改'
            });
        } else if (type === 'del') {
            $myHttp.post('/IRobotQrCodePayTask/del', { irTaskID: data.irTaskID }).mySuccess(function (result) {
                $scope.search();
            });
        }
    });
    $scope.$on('done', function (event, res, curr, count) {
        if (count === 0) {
            $('#pageTableTest2 [lay-event="enabled"],#pageTableTest2 [lay-event="disabled"],#pageTableTest2 [lay-event="export"],#pageTableTest2 [lay-event="delBatch"]').hide();
        } else {
            $('#pageTableTest2 [lay-event="enabled"],#pageTableTest2 [lay-event="disabled"],#pageTableTest2 [lay-event="export"],#pageTableTest2 [lay-event="delBatch"]').show();
        }
        $scope.count = count;
    });
});