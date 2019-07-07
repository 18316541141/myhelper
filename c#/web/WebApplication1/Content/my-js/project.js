'use strict';

//标题移动效果
(function(){	
	var title=document.title;
	if(title.length>10){
		setInterval(function(){
			var title=document.title;
			document.title=title.substring(1, title.length)+title.charAt(0);
		}, 400);
		title = null;
	}
}());

//异步加载静态图片素材
(function () {
    var images = [
		//256x256的图标
		"images/256x256/accdb-256.png",
		"images/256x256/avi-256.png",
		"images/256x256/bmp-256.png",
		"images/256x256/css-256.png",
		"images/256x256/docx-256.png",
		"images/256x256/eml-256.png",
		"images/256x256/eps-256.png",
		"images/256x256/fla-256.png",
		"images/256x256/gif-256.png",
		"images/256x256/html-256.png",
		"images/256x256/ind-256.png",
		"images/256x256/ini-256.png",
		"images/256x256/jpeg-256.png",
		"images/256x256/jsf-256.png",
		"images/256x256/mdi-256.png",
		"images/256x256/mov-256.png",
		"images/256x256/mp3-256.png",
		"images/256x256/mpeg-256.png",
		"images/256x256/pdf-256.png",
		"images/256x256/png-256.png",
		"images/256x256/pptx-256.png",
		"images/256x256/proj-256.png",
		"images/256x256/psd-256.png",
		"images/256x256/pst-256.png",
		"images/256x256/pub-256.png",
		"images/256x256/rar-256.png",
		"images/256x256/read-256.png",
		"images/256x256/set-256.png",
		"images/256x256/tiff-256.png",
		"images/256x256/txt-256.png",
		"images/256x256/url-256.png",
		"images/256x256/vsd-256.png",
		"images/256x256/wav-256.png",
		"images/256x256/wma-256.png",
		"images/256x256/wmv-256.png",
		"images/256x256/xlsx-256.png",
		"images/256x256/zip-256.png"
    ];
    for (var i = 0, len = images.length; i < len; i++) {
        new Image().src = images[i];
    }
}());

var layuiForm = layui.form;
var layuiLayer = layui.layer;
var layuiElement = layui.element;
var layuiTree = layui.tree;
var layuiTable = layui.table;
var layuiLaypage = layui.laypage;

/**
 * 扩展原有的jquery的ajax请求方法。
 */
(function () {
    var myCallback = function (callback) {
        var index = layuiLayer.load(0);
        return function (result, textStatus, req) {
            layuiLayer.close(index);
            //登录超时，退出登录
            if (result.code === -10) {
                logoutCallback();
            }
                //用户无权限，无法操作，但需要后续处理
            else if (result.code === -9) {
                layuiLayer.msg('当前用户组无操作权限！', { icon: 5, anim: 6 });
                callback(result, textStatus, req);
            }
                //用户无权限，无法操作
            else if (result.code === -8) {
                layuiLayer.msg('当前用户组无操作权限！', { icon: 5, anim: 6 });
            }
                //常规错误，
            else if (result.code === -1) {
                layuiLayer.msg(result.msg, { icon: 5, anim: 6 });
                callback(result, textStatus, req);
            }
                //成功
            else if (result.code === 0) {
                if (result.msg != null && result.msg != '') {
                    layuiLayer.msg(result.msg, { icon: 1 });
                }
                callback(result, textStatus, req);
            }
            callback = null;
            index = null;
        }
    };
    $.extend({
        //自定义的post，和原版的$.post用法一致
        "myPost": function (url, data, callback) {
            if ($.type(data) === 'function') {
                $.post(url, myCallback(data), 'json');
            } else {
                $.post(url, data, myCallback(callback), 'json');
            }
        },
        //自定义的get，和原版的$.get用法一致
        "myGet": function (url, data, callback) {
            if ($.type(data) === 'function') {
                $.get(url, myCallback(data), 'json');
            } else {
                $.get(url, data, myCallback(callback), 'json');
            }
        },
        //自定义的ajax，和原版的$.ajax用法一致
        "myAjax": function (settings) {
            settings.success = myCallback(settings.success);
            $.ajax(url, settings);
        },
        //自定义的表单post，不需要data参数，直接提交表单的数据
        "formPost": function (url, callback) {
            var data = {};
            this.get(0).find('input[name]').each(function () {
                data[$(this).attr('name')] = $(this).val();
            });
            $.post(url, data, myCallback(callback), 'json');
        },
        //自定义的表单get，不需要data参数，直接提交表单的数据
        "formGet": function (url, callback) {
            var data = {};
            this.get(0).find('input[name]').each(function () {
                data[$(this).attr('name')] = $(this).val();
            });
            $.get(url, data, myCallback(callback), 'json');
        },
        /*
         * 自定义的表单ajax，直接提交表单的数据，如果settings.data不为空，则附加settings.data数据。
         * 如果表单数据和data数据有重名的，则优先使用settings.data的数据
         */
        "formAjax": function (settings) {
            settings.success = myCallback(settings.success);
            var data = {};
            this.get(0).find('input[name]').each(function () {
                data[$(this).attr('name')] = $(this).val();
            });
            if (settings.data == undefined) {
                settings.data = data;
            } else {
                $.extend(settings.data, data);
            }
            $.ajax(url, settings);
        },
        /**
         * 定期延长编辑的锁定时间
         * addEditLockLimitDate 源码在webapp/my-js/common-by-jquery.js
         */
        "addEditLockLimitDate": addEditLockLimitDate,
        /**
         * 实时刷新数据
         * realTimeRefresh 源码在webapp/my-js/common-by-jquery.js
         */
        "realTimeRefresh": realTimeRefresh
    });
}());

//上传文件插件的回调函数
var uploadCallback = function (callback) {
    return function (file, response) {
        //登录超时，退出登录
        if (response.code === -10) {
            logoutCallback();
        }
            //用户无权限，无法操作，但需要后续处理
        else if (response.code === -9) {
            layuiLayer.msg('当前用户组无操作权限！', { icon: 5, anim: 6 });
            callback(file, response);
        }
            //用户无权限，无法操作
        else if (response.code === -8) {
            layuiLayer.msg('当前用户组无操作权限！', { icon: 5, anim: 6 });
        }
            //常规错误，
        else if (response.code === -1) {
            layuiLayer.msg(response.msg, { icon: 5, anim: 6 });
            callback(file, response);
        }
            //成功
        else if (response.code === 0) {
            if (response.msg != null && response.msg != '') {
                layuiLayer.msg(response.msg, { icon: 1 });
            }
            callback(file, response);
        }
        callback = null;
    }
};
/**
 * layui的datatable默认的配置，
 * 直接继承就可以使用了。
 */
var defaultDataTableParams = {
    autoSort: false,
    page: {
        layout: ['count', 'prev', 'page', 'next', 'limit', 'refresh', 'skip']
    },
    limit: 20,
    limits: [20, 40, 60, 80, 100],
    method: 'post',
    loading: true,
    defaultToolbar: ['filter'],
    request: {
        pageName: 'currentPageIndex',
        limitName: 'pageSize'
    },
    parseData: function (result) {
        var data = result.data;
        var ret = {};
        if (result.code === -10) {
            logoutCallback();
            ret = { code: result.code, count: 0, data: [] };
        }
            //用户无权限，无法操作，但需要后续处理
        else if (result.code === -9) {
            ret = { code: result.code, msg: '当前用户组无操作权限！', count: 0, data: [] };
        }
            //用户无权限，无法操作
        else if (result.code === -8) {
            ret = { code: result.code, msg: '当前用户组无操作权限！', count: 0, data: [] };
        }
            //常规错误，
        else if (result.code === -1) {
            ret = { code: result.code, msg: response.msg, count: 0, data: [] };
        }
            //成功
        else if (result.code === 0) {
            ret = { code: result.code, count: data.totalItemCount, data: data.pageDataList };
        }
        return ret;
    }
};

var myApp = angular.module('my-app', ['ngSanitize', 'ng-layer']).controller('main-body', function ($scope, $myHttp, $timeout, $realTime) {
    $scope.newAlarm = function () {
        $scope.alarmLayer = layuiLayer.open({
            type: 1,
            offset: 'rb',
            id: '20190703120431',
            content: '<div id="newsAlarmTable" lay-filter="dataTable20190703120431"></div>',
            shade: 0,
            title: '最新消息',
            area:['400px','300px'],
            success: function (layero, index) {
                layuiTable.render($.extend(defaultDataTableParams, {
                    elem: '#newsAlarmTable',
                    height: 300,
                    url: '/index/loadNewsAlarm',
                    id: '20190703120431-checked',
                    cols: [[
                        { field: 'title', title: '新消息' },
                        { field: 'createDate', title: '日期' },
                        { field: 'menuId', title: '操作', toolbar: '<buttton class="layui-btn layui-btn-sm" lay-event="turnToMenu">查看</button>' }
                    ]]
                }));
                layuiTable.on('tool(dataTable20190703120431)', function (obj) {
                });
            },
            cancel: function () {
                $scope.alarmLayer = undefined;
            }
        });
    };
    $realTime.regPool('newsAlarm', function () {
        if ($scope.alarmLayer === undefined) {
            $scope.newAlarm();
        } else {
            layuiTable.reload('20190703120431-checked', {});
        }
    });
    $scope.$on("$destroy", function () {
        $realTime.cancel('newsAlarm');
    });
    $myHttp.get('/index/loadLeftMenus').mySuccess(function (result) {
        $scope.leftMenus = result.data;
        $timeout(function () {
            $('.layui-nav-bar').remove();
            layuiElement.render('nav');
        });
    });
    $scope.findLeftMenuById = function (id) {
        var leftMenus=$scope.leftMenus;
        for (var i = 0, len = leftMenus.length; i < len; i++) {
            var childMenus = leftMenus[i].leftMenus;
            for (var j = 0, len = childMenus.length; j < len; j++) {
                if (childMenus[j].id === id) {
                    return childMenus[j];
                }
            }
        }
        return null;
    };
    $scope.menus = [];
    $scope.close = function (id) {
        var menus = $scope.menus;
        for (var i = 0, len = menus.length; i < len; i++) {
            if (menus[i].id === id) {
                i++;
                break;
            }
        }
        for (; i < len; i++) {
            menus[i - 1] = menus[i];
        }
        menus.length--;
        $timeout(function () {
            layuiElement.render('tab', 'docDemoTabBrief');
        });
    };
    
    /**
	 * 打开指定菜单页
	 */
    $scope.openMenuPage = function (id) {
        var menus = $scope.menus;
        var exist = false;
        for (var i = 0, len = menus.length; i < len; i++) {
            if (menus[i].id == id) {
                exist = true;
                break;
            }
        }
        if (exist) {
            $('[data-menu-id="' + id + '"]').click();
        } else {
            var ret=$scope.findLeftMenuById(id);
            $scope.menus[$scope.menus.length] = {
                title: ret.title,
                url: ret.url,
                id: id
            };
        }
    };
});
/**
 * 创建一个回调函数
 * showAni：是否显示加载动画
 * callback：回调函数
 */
var myCallback = function (callback) {
    var index = layuiLayer.load(0);
    return function (response, status, headers, config) {
        layuiLayer.close(index);
        //登录超时，退出登录
        if (response.code === -10) {
            logoutCallback();
        }
            //用户无权限，无法操作，但需要后续处理
        else if (response.code === -9) {
            layuiLayer.msg('当前用户组无操作权限！', { icon: 5, anim: 6 });
            callback(response, status, headers, config);
        }
            //用户无权限，无法操作
        else if (response.code === -8) {
            layuiLayer.msg('当前用户组无操作权限！', { icon: 5, anim: 6 });
        }
            //常规错误，
        else if (response.code === -1) {
            layuiLayer.msg(response.msg, { icon: 5, anim: 6 });
            callback(response, status, headers, config);
        }
            //成功
        else if (response.code === 0) {
            if (response.msg != null && response.msg != '') {
                layuiLayer.msg(response.msg, { icon: 1 });
            }
            callback(response, status, headers, config);
        }
        index = null;
        callback = null;
    }
};
myApp.factory('$realTime', function ($http) {
    //版本表，记录等待池和版本号
    var versionMap = {};
    //注册的监听池
    var regPoolMap = {};
    return {
        /**
         * 注册一个实时监听池，当发生变化是会调用callback
         * @poolName 监听池名称
         * @callback 监听会调
         */
        regPool: function (poolName, callback) {
            if ($.type(regPoolMap[poolName]) === 'undefined') {
                regPoolMap[poolName] = callback;
                this.get(poolName);
            }
        },
        /**
         * 注销一个实时监听池
         * @poolName 监听池名称
         */
        cancel: function (poolName) {
            regPoolMap[poolName] = undefined;
        },
        //注销所有监听池
        cancelAll:function(){
            regPoolMap = {};
        },
        //对请求参数添加随机的v参数，避免缓存
        randomV: function (params) {
            if (params === undefined) {
                params = { v: Math.random() };
            } else {
                params['v'] = Math.random();
            }
            return params;
        },
        //使用get请求实时获取最新数据
        get: function (poolName) {
            var thiz = this;
            $http({ method: 'GET', params: { v: Math.random() }, url: '/index/realTime', headers: { 'Real-Time-Pool': poolName, 'Real-Time-Version': versionMap[poolName] } })
            .success(function (response, status, headers, config) {
                versionMap[poolName] = headers('Real-Time-Version');
                if ($.type(regPoolMap[poolName]) === 'function') {
                    if (response.code === -10) {
                        poolName = null;
                        thiz.cancelAll();
                        logoutCallback();
                    } else if (response.code === 1) {
                        thiz.get(poolName);
                    } else if (response.code === 0) {
                        thiz.get(poolName);
                        regPoolMap[poolName]();
                    }
                }
                thiz = null;
            });
        },
        /**
         * 更新版本号
         * @url 请求的url
         * @params 请求的参数。
         * @poolNames 等待池名称，必须是数组
         */
        getUpdate: function (url, params, poolNames) {
            var ret = $http({ method: 'GET', params: this.randomV(params), url: url, headers: { 'Real-Time-Pool': poolNames.join(',') } });
            ret.mySuccess = function (callback) {
                ret.success(myCallback(callback));
            }
            return ret;
        },
        postUpdate: function (url, params, poolNames) {
            var ret = $http({ method: 'POST', url: url, params: this.randomV(params), headers: { 'Real-Time-Pool': poolNames.join(',') } });
            ret.mySuccess = function (callback) {
                ret.success(myCallback(callback));
            };
            return ret;
        }
    };
}).factory('$myHttp', function ($http) {
    return {
        //对请求参数添加随机的v参数，避免缓存
        randomV: function (params) {
            if (params === undefined) {
                params = { v: Math.random() };
            } else {
                params['v'] = Math.random();
            }
            return params;
        },
        //发送get请求，如果缓存内有该请求的资源，则使用缓存的
        getCache: function (url, params) {
            var ret = $http({ method: 'GET', params: params, url: url });
            ret.mySuccess = function (callback) {
                ret.success(myCallback(callback));
            };
            return ret;
        },
        get: function (url, params) {
            var ret = $http({ method: 'GET', params: this.randomV(params), url: url });
            ret.mySuccess = function (callback) {
                ret.success(myCallback(callback));
            };
            return ret;
        },
        post: function (url, params) {
            var ret = $http({ method: 'POST', url: url, params: this.randomV(params) });
            ret.mySuccess = function (callback) {
                ret.success(myCallback(callback));
            };
            return ret;
        }
    };
}).controller('login-form', function ($scope,$timeout) {
    var username = $.cookie('username');
    var password = $.cookie('password');
    $scope.data = {
        username: username == 'null' ? '' : username,
        password: password == 'null' ? '' : password,
        vercode: "",
        rememberPassword: $.cookie('rememberPassword') === 'true'
    };
    $scope.refreshVercode = function () {
        $scope.rNum = Math.random();
    };
    $timeout(function () {
        //登录功能
        layuiForm.on('submit(LAY-user-login-submit)', function () {
            var $scope = $('[ng-controller="login-form"]').scope();
            var tempData = $scope.data;
            var data = {
                username: tempData.username,
                password: tempData.password,
                vercode: tempData.vercode
            };
            if ($('#rememberPassword').prop('checked')) {
                $.cookie('username', data.username);
                $.cookie('password', data.password);
                $.cookie('rememberPassword', true);
            } else {
                $.cookie('username', null);
                $.cookie('password', null);
                $.cookie('rememberPassword', null);
            }
            var SHA1 = new Hashes.SHA1();
            data.username = SHA1.hex(data.username);
            data.password = SHA1.hex(data.password);
            $.myPost('/session/login', data, function (result) {
                if (result.code == -1) {
                    $scope.$apply(function () {
                        $scope.rNum = Math.random();
                        $scope.data.vercode = '';
                    });
                } else if (result.code == 0) {
                    $scope = $('[ng-controller="main-body"]').scope();
                    $scope.$apply(function () {
                        $scope.leftMenus = result.data.leftMenus;
                    });
                    $('.layui-nav-bar').remove();
                    layuiElement.render('nav');
                    $('#login-page').removeClass('logout');
                }
            });
        });
        layuiForm.render('checkbox');
    });
}).directive('uploadExcel', function () {
    return {
        restrict: 'EA',
        template: $('#uploadExcelTemplate').html(),
        scope: { name: "@",url:"@",postData:"=",type:"@" },
        controller: function ($scope, $timeout) {
            if ($scope.postData === undefined) {
                $scope.postData = {};
            }
            $scope.$on('submit', function (event) {
                $scope.uploader.upload();
            });
            var tipIndex;
            $scope.showTip=function(e){
                if (tipIndex === undefined) {
                    tipIndex = layuiLayer.tips($scope.excelFileName, e.target, {
                        tips: 2,
                        time: -1
                    });
                }
            };
            $scope.hideTip = function () {
                layuiLayer.close(tipIndex);
                tipIndex = undefined;
            };
            $timeout(function () {
                var index;
                $scope.uploader = new WebUploader.Uploader({
                    swf: 'plugin/webuploader/Uploader.swf',//当浏览器不支持XMLHttpWebRequest时，使用flash插件上传。
                    auto: $scope.type === 'line',//选中文件后自动上传
                    server: $scope.url,//处理上传excel的控制器
                    fileVal: 'fileUpload',//服务端接收二进制文件的参数名称
                    formData: $scope.postData,//每次上传时上传的数据
                    duplicate: true,
                    pick: {
                        id: '#' + $scope.name + 'ExcelId',//生成上传插件的位置
                        multiple: false //每次只能选一个文件
                    },
                    //允许上传的图片格式后缀
                    accept: {
                        title: 'excel',
                        extensions: 'xls,xlsx',
                        mimeTypes: 'application/vnd.ms-excel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
                    }
                }).on('uploadStart', function (file) {
                    index = layuiLayer.load(0);
                }).on('uploadSuccess', uploadCallback(function (file, response) {
                    layuiLayer.close(index);
                })).on('error', function (type) {
                    if (type === 'Q_TYPE_DENIED') {
                        layuiLayer.msg('该文件类型可能不是Excel文件。', { icon: 5, anim: 6 });
                    }
                }).on('beforeFileQueued', function (file) {
                    $scope.isUploaded = true;
                    $scope.excelFileName = file.name;
                    $scope.$apply();
                });
            });
        }
    };
}).directive('pageDataTable', function () {
    return {
        restrict: 'EA',
        template: $('#pageDataTableTemplate').html(),
        scope: { id: "@", url: "@", tableToolbar: "@", showCheckCol: "@", showDefaultOperCol: "@", height:"@", postData: "=", cols: "=", perms: "=" },
        controller: function ($scope, $timeout, $myHttp) {
            if ($scope.cols === undefined) {
                throw new Error('请设置cols的值');
            }
            if ($scope.postData === undefined) { 
        		$scope.postData = {};
            }
            var tempArray=$scope.cols;
            if ($scope.showCheckCol === 'true') {
                tempArray.length += 1;
                for (var i = tempArray.length-1;i>0 ;i--) {
                    tempArray[i]=tempArray[i - 1];
                }
                tempArray[0] = { type: 'checkbox', LAY_CHECKED: false,fixed:'left' };
            }
            if ($scope.showDefaultOperCol === 'true') {
                tempArray[tempArray.length]={ fixed: 'right', title: '操作', toolbar: '#rowToolbar', width: 110 };
            }
            $scope.$on('searchPage', function () {
                layuiTable.reload($scope.id + '-checked', { page: { curr: 1 }, where: $scope.postData });
            });
            $timeout(function () {
                layuiTable.render($.extend(defaultDataTableParams, {
                    elem: '#data-table-' + $scope.id,
                    height: $scope.height,
                    url: $scope.url,
                    toolbar: $scope.tableToolbar,
                    id: $scope.id + '-checked',
                    done: function (res, curr, count) {
                        $scope.$emit('done', res, curr, count);
                    },
                    cols: [$scope.cols]
                }));
                layuiTable.on('sort(dataTable' + $scope.id + ')', function (obj) {
                    if (obj.type === "asc") {
                        for(var key in $scope.postData){
                            if ($scope.postData.hasOwnProperty(key) && key.indexOf('orderByDesc') === 0) {
                                $scope.postData[key] = false;
                            }
                        }
                        $scope.postData['orderByAsc.'+obj.field]=true;
                    } else if (obj.type === "desc") {
                        for (var key in $scope.postData) {
                            if ($scope.postData.hasOwnProperty(key) && key.indexOf('orderByAsc') === 0) {
                                $scope.postData[key] = false;
                            }
                        }
                        $scope.postData['orderByDesc.'+obj.field]=true;
                    }
                    layuiTable.reload($scope.id + '-checked', { page: { curr: 1 }, where: $scope.postData });
                });
                layuiTable.on('toolbar(dataTable' + $scope.id + ')', function (obj) {
                    var event = obj.event;
                    var data = layuiTable.checkStatus($scope.id + '-checked').data;
                    if (event === 'enabled' && data.length === 0) {
                        layuiLayer.alert('至少得选中一行数据才能启用。', { icon: 5 });
                        return;
                    } else if (event === 'disabled' && data.length === 0) {
                        layuiLayer.alert('至少得选中一行数据才能禁用。', { icon: 5 });
                        return;
                    } else if (event === 'delBatch' && data.length === 0) {
                        layuiLayer.alert('至少得选中一行数据才能删除。', { icon: 5 });
                        return;
                    }
                    if (event === 'delBatch') {
                        layuiLayer.confirm('确认删除？', { icon: 3, title: '删除提示' }, function (index) {
                            layuiLayer.close(index);
                            $scope.$emit('tableOper', event, data);
                        });
                    }else{
                        $scope.$emit('tableOper', event, data);
                    }
                });
                layuiTable.on('tool(dataTable' + $scope.id + ')', function (obj) {
                    var event = obj.event;
                    var data = obj.data;
                    if (event === 'del') {
                        layuiLayer.confirm('确认删除？', { icon: 3, title: '删除提示' }, function (index) {
                            layuiLayer.close(index);
                            $scope.$emit('rowOper', event, data);
                        });
                    } else {
                        $scope.$emit('rowOper', event, data);
                    }
                });
            });
        }
    };
}).directive("areaSelect", function () {
    return {
        restrict: 'EA',
        template: $('#areaSelectTemplate').html(),
        scope: { type: "@", deep: "@", required: "@", provinceVal: "@", cityVal: "@", countyVal: "@",townVal:"@" },
        controller: function ($scope, $myHttp,$timeout) {
            $scope.ie8 = navigator.appName == "Microsoft Internet Explorer" && navigator.appVersion.match(/8./i) == "8.";
            $myHttp.getCache('/index/areaSelect').mySuccess(function (result) {
                $scope.data = result.data;
                $scope.provinces = [{ name: '请选择省', value: '' }].concat($scope.data['provinces']);
                $scope.cities = [{ name: '请选择市', value: '' }];
                $scope.counties = [{ name: '请选择区', value: '' }];
                $scope.towns = [{name: '请选择镇', value: '' }];
                if ($scope.deep >= 1 && $scope.provinceVal !== undefined) {
                    $scope.cities = [{ name: '请选择市', value: '' }].concat($($scope.data['cities']).filter(function (index, val) {
                        return val.parentValue == $scope.provinceVal;
                    }).get());
                }
                if ($scope.deep >= 2 && $scope.cityVal !== undefined) {
                    $scope.counties = [{ name: '请选择区', value: '' }].concat($($scope.data['counties']).filter(function (index, val) {
                        return val.parentValue == $scope.cityVal;
                    }).get());
                }
                if ($scope.deep >= 3 && $scope.countyVal !== undefined) {
                    $scope.towns = [{ name: '请选择镇', value: '' }].concat($($scope.data['towns']).filter(function (index, val) {
                        return val.parentValue == $scope.countyVal;
                    }).get());
                }
                $timeout(function () {
                    if ($scope.deep >= 2) {
                        layuiForm.on('select(province-select)', function (data) {
                            $scope.provinceVal = $scope.provinces[parseInt(data.value)].value;
                            $scope.cities = [{ name: '请选择市', value: '' }].concat($($scope.data['cities']).filter(function (index, val) {
                                return val.parentValue == $scope.provinceVal;
                            }).get());
                            $scope.counties = [{ name: '请选择区', value: '' }];
                            $scope.towns = [{ name: '请选择镇', value: '' }];
                            $scope.cityVal = "";
                            $scope.countyVal = "";
                            $scope.townVal = "";
                            $scope.$apply();
                            layuiForm.render('select');
                        });
                    }
                    if ($scope.deep >= 2) {
                        layuiForm.on('select(city-select)', function (data) {
                            $scope.cityVal = $scope.cities[parseInt(data.value)].value;
                            $scope.counties = [{ name: '请选择区', value: '' }].concat($($scope.data['counties']).filter(function (index, val) {
                                return val.parentValue == $scope.cityVal;
                            }).get());
                            $scope.towns = [{ name: '请选择镇', value: '' }];
                            $scope.countyVal = "";
                            $scope.townVal = "";
                            $scope.$apply();
                            layuiForm.render('select');
                        });
                    }
                    if ($scope.deep >= 3) {
                        layuiForm.on('select(county-select)', function (data) {
                            $scope.townVal = $scope.towns[parseInt(data.value)].value;
                            $scope.towns = [{ name: '请选择镇', value: '' }].concat($($scope.data['towns']).filter(function (index, val) {
                                return val.parentValue == $scope.countyVal;
                            }).get());
                            $scope.townVal = "";
                            $scope.$apply();
                            layuiForm.render('select');
                        });
                    }
                    layuiForm.render('select');
                });
            });
        }
    };
}).directive("uploadImage", function () {
    return {
        restrict: 'EA',
        template: $('#uploadImageTemplate').html(),
        scope: { name: "@", path: "@", cut: "@"},
        controller: function ($scope, layer, $timeout, $myHttp) {
            $scope.isUploaded = false;
            $scope.showProgress = false;
            $scope.crop = function () {
                var select = $scope.jcropApi.tellSelect();
                var $img = $scope.$img;
                $myHttp.post('/index/singleImageCrop',{
                    pathName: $scope.path, 
                    imgName: $scope.imgName,
                    imgWidth: $img.width(), 
                    imgHeight: $img.height(), 
                    x: select.x, 
                    y: select.y, 
                    w: select.w, 
                    h: select.h 
                }).mySuccess(function (response) {
                    layer.close($scope.cropLayer);
                    $scope.cropLayer = null;
                    var data=response.data;
                    $scope.imgName = data.imgName;
                    $scope.thumbnailName = data.thumbnailName;
                });
            };
            $timeout(function () {
                $scope.uploader = new WebUploader.Uploader({
                    swf: 'plugin/webuploader/Uploader.swf',//当浏览器不支持XMLHttpWebRequest时，使用flash插件上传。
                    auto: true,//选中文件后自动上传
                    server: '/index/uploadSingleImage',//处理上传文件的统一控制器
                    fileVal: 'fileUpload',//服务端接收二进制文件的参数名称
                    formData: { pathName: $scope.path },//每次上传时要提供一个上传目录，让服务端确认保存位置
                    duplicate: true,
                    pick: {
                        id: '#' + $scope.name + 'Id',//生成上传插件的位置
                        multiple:false //每次只能选一个文件
                    },
                    //允许上传的图片格式后缀
                    accept: {
                        title:'image',
                        extensions: 'gif,webp,jpg,jpeg,bmp,png,tif,emf,ico,flic,wmf,raw,hdri,ai,eps,ufo,dxf,pcd,cdr,psd,svg,fpx,exif,tga,pcx,GIF,JPG,JPEG,BMP,PNG,WEBP,PCX,TIF,TGA,EXIF,FPX,SVG,PSD,CDR,PCD,DXF,UFO,EPS,AI,HDRI,RAW,WMF,FLIC,EMF,ICO',
                        mimeTypes: 'image/*',
                    }
                }).on('uploadStart', function (file) {
                    $scope.showProgress = true;
                    $scope.$apply();
                }).on('uploadProgress', function (file, percentage) {
                    layuiElement.progress('upload-img-progress', (percentage * 100).toFixed(2) + '%');
                }).on('uploadSuccess', uploadCallback(function (file, response) {
                    if (response.code === 0) {
                        if ($scope.cut === 'true') {
                            var data = response.data;
                            $scope.imgName = data.imgName;
                            $scope.cropLayer = layer.ngOpen({
                                type: 1,
                                area: areaAnalysis(data.imgWidth, data.imgHeight),
                                content: $('#cropTemplate').html(),
                                scope: $scope,
                                title: '裁剪图片',
                                cancel: function () {
                                    $scope.crop();
                                },
                                compileFinish: function () {
                                    var wait=layuiLayer.load(0);
                                    $scope.$img = $('#' + $scope.name + '-crop').attr('src', '/index/showImage?pathName=' + $scope.path + '&imgName=' + $scope.imgName).on('load', function () {
                                        layuiLayer.close(wait);
                                        $(this).Jcrop({ allowSelect: false }, function () {
                                            this.setSelect([0, 0, 250, 300]);
                                            $scope.jcropApi = this
                                        });
                                    });
                                }
                            });
                        }
                        $scope.isUploaded = true;
                    } else if (response.code === -1) {
                        $scope.isUploaded = false;
                    }
                    $scope.showProgress = false;
                    $scope.$apply();
                })).on('error', function (type) {
                    if (type === 'Q_TYPE_DENIED') {
                        layuiLayer.msg('该文件类型可能不是图片文件。', { icon: 5, anim: 6 });
                    }
                });
            });
        }
    };
}).directive('treeForm', function () {
    /**
     * 注册树表单标签。
     * 通过<tree-form> ...表单元素... </tree-form>快速构建树表单标签。
     */
    return {
        restrict: 'EA',
        template: $('#treeFormTemplate').html(),
        transclude: true,
        scope: { url: "@", id: "@",targetController:"@"},
        controller: function ($scope, $myHttp) {
            $scope.$on('delNode', function (event, id) {
                var nodes = $scope.ztree.getNodesByParam("id", id, null);
                if (nodes.length === 0)
                    throw new Error('找不到该节点，删除失败！');
                $scope.ztree.removeNode(nodes[0], false);
            });
            $scope.$on('rename', function (event, id,name) {
                var nodes = $scope.ztree.getNodesByParam("id", id, null);
                if (nodes.length === 0)
                    throw new Error('找不到该节点，编辑失败！');
                nodes[0].name = name;
                $scope.ztree.updateNode(nodes[0]);
            });
            $scope.$on('reloadTree', function (event) {
                $scope.loadTree();
            });
            $scope.search = function () {
                var ztree = $scope.ztree;
                if ($.type($scope.searchParam) === 'string' && ($scope.searchParam = $.trim($scope.searchParam)).length > 0) {
                    ztree.hideNodes(ztree.getNodesByParam("isHidden", false));
                    var nodes = ztree.getNodesByParamFuzzy('name', $scope.searchParam, null);
                    var showNodes=[];
                    for (var i = 0, len = nodes.length; i < len; i++) {
                        var tempNode=nodes[i];
                        do {
                            showNodes[showNodes.length] = tempNode;
                        } while ((tempNode = tempNode.getParentNode()) != null);
                    }
                    ztree.showNodes(showNodes);
                    ztree.expandAll(true);
                } else {
                    ztree.showNodes(ztree.getNodesByParam("isHidden", true));
                }
            };
            var $rightContent = $('#' + $scope.id + ' .right-content');
            var $leftTree = $('#' + $scope.id + ' .left-tree');
            $scope.loadTree = function () {
                $myHttp.get($scope.url).mySuccess(function (result) {
                    $scope.ztree = $.fn.zTree.init($('#tree-' + $scope.id), {
                        view: {
                            showLine: false
                        },
                        edit: {
                            enable: true,
                            showRemoveBtn: false,
                            showRenameBtn: false,
                            drag:{
                                prev : true,
                                inner : true,
                                next: true,
                                isMove: true
                            }
                        },
                        callback: {
                            onClick: function (event, treeId, treeNode) {
                                $scope.$emit('onClick',event, treeId, treeNode);
                            },
                            onDrop: function (event, treeId, treeNodes, targetNode, moveType, isCopy) {
                                $scope.$emit('onDrop', event, treeId, treeNodes, targetNode, moveType, isCopy);
                            }
                        }
                    }, result.data);
                    layuiForm.render();
                    $leftTree.height($rightContent.height());
                });
            };
            $scope.loadTree();
        }
    };
}).directive("uploadFiles", function () {
    return {
        restrict: 'EA',
        template: $('#uploadFilesTemplate').html(),
        scope: { name: "@", fileDescMaxWidth: "@", path: "@" },
        controller: function ($scope, $timeout, $myHttp) {
            $scope.files = [];
            var tipIndex;
            /**
             * 鼠标在文件描述列上移动时，显示文件描述
             * @param e 表格元素的event对象
             * @param text 表格的文本内容
             */
            $scope.showTip = function (e, text) {
                if (tipIndex === undefined) {
                    tipIndex = layuiLayer.tips(text, e.target, {
                        tips: 1,
                        time:-1
                    });
                }
            };
            //鼠标离开文件描述列，隐藏文件描述
            $scope.hideTip = function () {
                layuiLayer.close(tipIndex);
                tipIndex = undefined;
            };

            //下载文件
            $scope.downFile = function (x) {
                postOpenWin('/index/downFile', {
                    pathName: $scope.path,
                    fileName: x.fileName,
                    fileDesc: x.fileDesc
                });
            };

            /**
             * 删除已上传的指定文件。
             * @param fileName 文件名称sha1
             */
            $scope.delFile = function (fileName) {
                var files=$scope.files;
                var i = 0, len = files.length;
                for (;i<len;i++) {
                    if (files[i].fileName === fileName) {
                        i++;
                        break;
                    }
                }
                for(;i<len;i++){
                    files[i - 1] = files[i];
                }
                files.length--;
                $scope.hideTip();
            };
            //渲染后回调
            $timeout(function () {
                //记录每一个文件的进度
                var fileMap = {};
                //创建一个上传插件
                new WebUploader.Uploader({
                    swf: 'plugin/webuploader/Uploader.swf',//插件所需的flash路径，用于兼容不支持XMLHttpWebRequest对象的浏览器
                    auto: true,//拖动后自动上传
                    duplicate: true,//对每一个文件添加唯一hash值，用于区分文件操作进度条
                    server: '/index/uploadFiles',//统一上传的控制器
                    pick: { id: '#' + $scope.name + 'Id' },//上传域的id
                    fileVal: 'fileUploads',//上传流文件的参数名
                    formData: {
                        pathName: $scope.path //上传时的路径参数
                    }
                }).on('uploadStart', function (file) {
                    var files = $scope.files;
                    fileMap[file.id] = files.length;
                    files[files.length] = { fileDesc: file.name, typeImg: typeImgByMime(file.ext), progress: 0, isFinish: false,id:file.id};
                    $scope.$apply();
                }).on('uploadProgress', function (file, percentage) {
                    layuiElement.progress(file.id, (percentage * 100).toFixed(2) + '%');
                }).on('uploadSuccess', function (file, response) {
                    var fileObj = $scope.files[fileMap[file.id]];
                    fileObj.fileName = response.data;
                    fileObj.isFinish = true;
                    $scope.$apply();
                });
            });
        }
    };
});

/**
 * 下载excel，如果excel过大，则分批下载，否则一次下载完
 * @param url 下载的url
 * @param postData 下载是附带的数据
 * @param totalCount 当前符合条件的数据总量
 */
function downExcel(url,fileName,postData,totalCount) {
    postData['pageSize'] = 10000;
    if (totalCount < 10000) {
        postData['currentPageIndex'] = 1;
        postOpenWin(url, postData);
    } else {
        var html = '<p>共 ' + totalCount + ' 条数据，每个excel最多保存10000条。</p>' +
        '<table class="layui-table down-excel-table">' +
            '<tbody>';
        for(var i=0,part=totalCount/(totalCount - totalCount%10000);i<part;i++){
            html += '<tr><td><strong>“' + fileName + '”</strong> 第' + (i*10000+1) + '-' + ((i + 1) * 10000) + '条数据</td><td><a href="javascript:void(0);" data-index="'+i+'" class="down-excel" target="_blank">下载</a></td></tr>';
        } 
        html+= '</tbody>' +
        '</table>';
        layuiLayer.open({
            type: 1,
            area: ['400px','400px'],
            content: html,
            cancel: function () {
                $('.down-excel-table').off('click');
            }
        });
        $('.down-excel-table').on('click', '.down-excel', function () {
            postData['currentPageIndex'] = parseInt($(this).data('index')) + 1;
            postOpenWin(url, postData);
        });
    }
}

/**
 * 根据附件的扩展名判断使用哪一种图片
 * @param ext  附件的mime类型
 */
function typeImgByMime(ext) {
    var basePath = 'images/256x256/';
    ext = ext.toLowerCase();
    if (ext === 'png') {
        basePath += "png-256.png";
    } else if (ext === 'ini') {
        basePath += "ini-256.png";
    } else if (ext === 'accdb') {
        basePath += "accdb-256.png";
    } else if (ext === 'avi') {
        basePath += 'avi-256.png';
    } else if (ext === 'bmp') {
        basePath += 'bmp-256.png';
    } else if (ext === 'css') {
        basePath += 'css-256.png';
    } else if (ext === 'docx') {
        basePath += 'docx-256.png';
    } else if (ext === 'eml') {
        basePath += 'eml-256.png';
    } else if (ext === 'eps') {
        basePath += 'eps-256.png';
    } else if (ext === 'fla') {
        basePath += 'fla-256.png';
    } else if (ext === 'gif') {
        basePath += 'gif-256.png';
    } else if (ext === 'html' || ext === 'htm') {
        basePath += 'html-256.png';
    } else if (ext === 'ind') {
        basePath += 'ind-256.png';
    } else if (ext === 'jpeg' || ext === 'jpg') {
        basePath += 'jpeg-256.png';
    } else if (ext === 'jsf') {
        basePath += 'jsf-256.png';
    } else if (ext === 'mdi') {
        basePath += 'mdi-256.png';
    } else if (ext === 'mov') {
        basePath += 'mov-256.png';
    } else if (ext === 'mp3') {
        basePath += 'mp3-256.png';
    } else if (ext === 'mpeg') {
        basePath += 'mpeg-256.png';
    } else if (ext === 'pdf') {
        basePath += 'pdf-256.png';
    } else if (ext === 'pptx' || ext === 'ppt') {
        basePath += 'pptx-256.png';
    } else if (ext === 'proj') {
        basePath += 'proj-256.png';
    } else if (ext === 'psd') {
        basePath += 'psd-256.png';
    } else if (ext === 'pst') {
        basePath += 'pst-256.png';
    } else if (ext === 'pub') {
        basePath += 'pub-256.png';
    } else if (ext === 'rar') {
        basePath += 'rar-256.png';
    } else if (ext === 'read') {
        basePath += 'read-256.png';
    } else if (ext === 'set') {
        basePath += 'set-256.png';
    } else if (ext === 'tiff') {
        basePath += 'tiff-256.png';
    } else if (ext === 'txt') {
        basePath += 'txt-256.png';
    } else if (ext === 'url') {
        basePath += 'url-256.png';
    } else if (ext === 'vsd') {
        basePath += 'vsd-256.png';
    } else if (ext === 'wav') {
        basePath += 'wav-256.png';
    } else if (ext === 'wma') {
        basePath += 'wma-256.png';
    } else if (ext === 'wmv') {
        basePath += 'wmv-256.png';
    } else if (ext === 'xlsx' || ext === 'xls') {
        basePath += 'xlsx-256.png';
    } else if (ext === 'zip') {
        basePath += 'zip-256.png';
    } else{
        basePath += 'ini-256.png';
    }
    return basePath;
}

/**
 * 区域分析，用于解析使用多大的区域去装载物体
 * @param width   装载物宽度
 * @param height  装载物高度
 */
function areaAnalysis(width, height) {
    var maxWidth = window.screen.width - 100;
    var maxHeight = window.screen.height - 200;
    var minWidth = 400;
    var minHeight = 400;
    var finalWidth;
    if(width > maxWidth){
        finalWidth = maxWidth;
    }else if(width < minWidth){
        finalWidth = minWidth;
    }else{
        finalWidth = width;
    }
    var finalHeight;
    if (height > maxHeight) {
        finalHeight = maxHeight;
    }else if(height < minHeight){
        finalHeight = minHeight
    } else {
        finalHeight = height;
    }
    return [finalWidth + 'px', finalHeight + 'px'];
}



/**
 * 退出登录的回调方法
 */
function logoutCallback() {
    $('#login-page').addClass('logout');
}

/**
 * 退出登陆的方法
 */
function logout() {
    $.myGet('/session/logout', logoutCallback);
    var $scope = $('[ng-controller="login-form"]').scope();
    $scope.data = null;
    $scope.rNum = Math.random();
    $scope.$apply();
}


/**
 * 使用post的方式打开一个新窗口
 * @url：新窗口的url地址
 * @params：post请求参数
 * @searchParam：url上的参数
 */
function postOpenWin(url, params, searchParam) {
    var times = new Date().getTime();
    var input = '';
    if (params) {
        for (var key in params) {
            if (params.hasOwnProperty(key))
                input += '<textarea name="' + key + '"></textarea>';
        }
    }
    if ($.type(searchParam) === 'object') {
        for (var key in searchParam) {
            if (searchParam.hasOwnProperty(key))
                input += '<textarea name="' + key + '"></textarea>';
        }
    }
    $('body').append('<form style="display:none;" id="' + times + '" method="post" target="_blank" action="' + url + '">' + input + '</form>');
    if (params) {
        for (var key in params) {
            if (params.hasOwnProperty(key))
                $('#' + times).find('textarea[name="' + key + '"]').val(params[key]);
        }
    }
    if ($.type(searchParam) === 'object') {
        for (var key in searchParam) {
            if (searchParam.hasOwnProperty(key))
                $('#' + times).find('textarea[name="' + key + '"]').val(searchParam[key]);
        }
    }
    $('#' + times).submit();
    $('#' + times).remove();
}

//--------------------------分割线-------------------------------
//上面的代码不能改，下面的代码可以改
//--------------------------分割线-------------------------------
myApp.controller('testTreeForm', function ($scope, $myHttp) {
    //监听树菜单点击事件
    $scope.$on('onClick', function (event, treeId, treeNode) {
        alert('点击');
    });
    //监听树菜单的拖动事件
    $scope.$on('onDrop', function (event, treeId, treeNodes, targetNode, moveType, isCopy) {
        alert('拖动结束');
    });
    $scope.del = function () {
        $scope.$broadcast('delNode', '01');
    };
    $scope.refresh = function () {
        $scope.$broadcast('reloadTree');
    };
    $scope.rename = function () {
        $scope.$broadcast('rename', '01','新名称');
    };
});
myApp.controller("upload-image", function ($scope) {

});
myApp.controller('upload-files', function ($scope) {

});
myApp.controller('big-img-ctrl',function($scope,$timeout){
    $timeout(function(){
        $('.xxx-table').viewer({
            url: 'data-original',
            built: function () {
                $('.layui-layout-body').append($('.viewer-container'));
            },
        });
    });
});
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
        enabled :true,
        disabled :true,
        add:true,
        'export':true,
        delBatch:true,
        del:true,
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
            var postData={};
            for (var i = 0,len=data.length;i<len ;i++){
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
                area: ['600px','400px'],
                contentUrl: 'menus/testMenus1/edit1.html',
                scope: $scope,
                title: '修改'
            });
        }else if(type === 'del'){
            $myHttp.post('/IRobotQrCodePayTask/del', { irTaskID: data.irTaskID }).mySuccess(function (result) {
                $scope.search();
            });
        }
    });
    $scope.$on('done', function (event,res, curr, count) {
        if (count === 0) {
            $('#pageTableTest2 [lay-event="enabled"],#pageTableTest2 [lay-event="disabled"],#pageTableTest2 [lay-event="export"],#pageTableTest2 [lay-event="delBatch"]').hide();
        } else {
            $('#pageTableTest2 [lay-event="enabled"],#pageTableTest2 [lay-event="disabled"],#pageTableTest2 [lay-event="export"],#pageTableTest2 [lay-event="delBatch"]').show();
        }
        $scope.count=count;
    });
});
myApp.controller('testEdit1', function ($scope, $myHttp) {
    $myHttp.get('/IRobotQrCodePayTask/load', { irTaskID: $scope.edit.data.irTaskID }).mySuccess(function (result) {
        $scope.edit.postData = result.data;
    });
    $scope.edit.save = function () {

    };
});
myApp.controller('testUploadExcel', function ($scope) {
    $scope.postData = { otherData: 'asdasdasd' };
    $scope.upload = function () {
        $scope.$broadcast('submit');
    }
});
myApp.controller('addNewAlaram', function ($scope,$realTime) {
    $scope.adddd = function () {
        $realTime.getUpdate('/NewAlarm/add', {}, ['newsAlarm']).mySuccess(function () {

        });
    };
});