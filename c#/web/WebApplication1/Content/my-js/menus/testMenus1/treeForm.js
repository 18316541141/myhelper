myApp.controller('testTreeForm', function ($scope, $myHttp) {
    //监听树菜单点击事件
    $scope.$on('onClick', function (event, treeNode) {
        alert('点击');
    });
    //监听树菜单的拖动事件
    $scope.$on('beforeDrop', function (event, treeNodes, targetNode, moveType, retObj) {
        alert('拖动结束');
        retObj.ret = true;
    });
    $scope.del = function () {
        $scope.$broadcast('delNode', '01');
    };
    $scope.refresh = function () {
        $scope.$broadcast('reloadTree');
    };
    $scope.rename = function () {
        $scope.$broadcast('rename', '01', '新名称');
    };
});