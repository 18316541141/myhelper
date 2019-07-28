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
		"../images/256x256/accdb-256.png",
		"../images/256x256/avi-256.png",
		"../images/256x256/bmp-256.png",
		"../images/256x256/css-256.png",
		"../images/256x256/docx-256.png",
		"../images/256x256/eml-256.png",
		"../images/256x256/eps-256.png",
		"../images/256x256/fla-256.png",
		"../images/256x256/gif-256.png",
		"../images/256x256/html-256.png",
		"../images/256x256/ind-256.png",
		"../images/256x256/ini-256.png",
		"../images/256x256/jpeg-256.png",
		"../images/256x256/jsf-256.png",
		"../images/256x256/mdi-256.png",
		"../images/256x256/mov-256.png",
		"../images/256x256/mp3-256.png",
		"../images/256x256/mpeg-256.png",
		"../images/256x256/pdf-256.png",
		"../images/256x256/png-256.png",
		"../images/256x256/pptx-256.png",
		"../images/256x256/proj-256.png",
		"../images/256x256/psd-256.png",
		"../images/256x256/pst-256.png",
		"../images/256x256/pub-256.png",
		"../images/256x256/rar-256.png",
		"../images/256x256/read-256.png",
		"../images/256x256/set-256.png",
		"../images/256x256/tiff-256.png",
		"../images/256x256/txt-256.png",
		"../images/256x256/url-256.png",
		"../images/256x256/vsd-256.png",
		"../images/256x256/wav-256.png",
		"../images/256x256/wma-256.png",
		"../images/256x256/wmv-256.png",
		"../images/256x256/xlsx-256.png",
		"../images/256x256/zip-256.png"
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
            if (result.code === -10 || result.code === -11) {
                logoutCallback();
                if (result.code === -11) {
                    layuiLayer.alert('强制下线，原因：当前登录用户在其它地方登录。', { icon: 5 });
                }
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
                layuiLayer.msg(result.msg, { icon: 5, anim: 6, time:1000 });
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
        //自定义同步post，和原版的$.post用法一致
        'syncPost': function (url, data, callback) {
            $.ajax({
                type: "POST",
                url: url,
                dataType: 'json',
                async: false,
                data: $.type(data) === 'object' ? data : {},
                success: myCallback($.type(data) === 'function'?data:callback)
            });
        },
        //自定义同步get，和原版的$.get用法一致
        'syncGet': function () {
            $.ajax({
                type: "GET",
                url: url,
                dataType: 'json',
                async: false,
                data: $.type(data) === 'object' ? data : {},
                success: myCallback($.type(data) === 'function' ? data : callback)
            });
        },
    });
}());

//上传文件插件的回调函数
var uploadCallback = function (callback) {
    return function (file, response) {
        //登录超时，退出登录
        if (response.code === -10 || response.code === -11) {
            logoutCallback();
            if (response.code === -11) {
                layuiLayer.alert('强制下线，原因：当前登录用户在其它地方登录。', { icon: 5 });
            }
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
            layuiLayer.msg(response.msg, { icon: 5, anim: 6, time: 1000 });
            callback(file, response);
        }
            //成功
        else if (response.code === 0) {
            if (response.msg != null && response.msg != '') {
                layuiLayer.msg(response.msg, { icon: 1 });
            }
            callback(file, response);
        }
    }
};
/**
 * layui的datatable默认的配置
 */
layuiTable.set({
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
        if (result.code === -10 || result.code === -11) {
            logoutCallback();
            if (result.code === -11) {
                layuiLayer.alert('强制下线，原因：当前登录用户在其它地方登录。', { icon: 5 });
            }
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
});

var myApp = angular.module('my-app', ['ngSanitize', 'ng-layer', 'ngAnimate']).controller('main-body', function ($scope, $myHttp, $timeout, $realTime) {
    $scope.newAlarm = function () {
        layuiLayer.open({
            type: 1,
            offset: 'rb',
            id: '20190703120431',
            content: '<div class="news-alarm"><div id="newsAlarmTable" lay-filter="dataTable20190703120431"></div></div>',
            shade: 0,
            title: '消息栏',
            area: ['450px', '320px'],
            success: function (layero, index) {
                layuiTable.render({
                    elem: '#newsAlarmTable',
                    height: '270',
                    url: '/index/loadNewsAlarm',
                    id: '20190703120431-checked',
                    page: {
                        layout: ['count', 'prev', 'page', 'next', 'limit', 'refresh']
                    },
                    cols: [[
                        { field: 'title', title: '新消息' },
                        { field: 'createDate', title: '日期' },
                        {
                            field: 'readState', title: '状态', templet: function (data) {
                                if (data.readState === 0) {
                                    return '<span class="layui-badge">未读</span>';
                                } else if (data.readState === 1) {
                                    return '<span class="layui-badge layui-bg-green">已读</span>';
                                }
                            }
                        },
                        {
                            field: 'menuId', title: '操作', templet: function () {
                                return '<a href="javascript:void(0);" style="color:#01AAED;" lay-event="turnToMenu">查看</a>';
                            }
                        }
                    ]]
                });
                layuiTable.on('tool(dataTable20190703120431)', function (obj) {
                    if (obj.event === 'turnToMenu') {
                        $scope.openMenuPage(obj.data.menuId);
                        $scope.$apply();
                    }
                });
            }
        });
    };
    $realTime.regPool('newsAlarm', function () {
        $scope.newAlarm();
        layuiTable.reload('20190703120431-checked', {});
    });
    $scope.$on("$destroy", function () {
        $realTime.cancel('newsAlarm');
    });
    $myHttp.get('/index/loadLoginData').mySuccess(function (result) {
        var data=result.data;
        $scope.leftMenus = data.leftMenus;
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
        if (menus.length == 0)
            return;
        $timeout(function () {
            var len = $('[lay-filter="docDemoTabBrief"] .layui-this').length;
            if (len === 0) {
                $('[data-menu-id="' + menus[0].id + '"]').click();
            } else if (len > 1) {
                $('[lay-filter="docDemoTabBrief"] .layui-this:eq(0)').click();
            }
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
        if (!exist) {
            var ret=$scope.findLeftMenuById(id);
            $scope.menus[$scope.menus.length] = {
                title: ret.title,
                url: ret.url,
                id: id
            };
        }
        $timeout(function () {
            $('[data-menu-id="' + id + '"]').click();
        });
    };

    var username = $.cookie('username');
    var password = $.cookie('password');
    $scope.loginData = {
        username: username == 'null' ? '' : username,
        password: password == 'null' ? '' : password,
        vercode: "",
        rememberPassword: $.cookie('rememberPassword') === 'true',
        rNum : Math.random()
    };
    $timeout(function () {
        layuiForm.render('checkbox');
    });
    $scope.refreshVercode = function () {
        $scope.loginData.rNum = Math.random();
    };

    /**
     * 退出登陆的方法
     */
    $scope.logout = function () {
        $myHttp.get('/session/logout').mySuccess(logoutCallback);
        $realTime.cancelAll();
    }

    $scope.login = function () {
        if ($('#rememberPassword').prop('checked')) {
            $.cookie('username', $scope.loginData.username);
            $.cookie('password', $scope.loginData.password);
            $.cookie('rememberPassword', true);
        } else {
            $.cookie('username', null);
            $.cookie('password', null);
            $.cookie('rememberPassword', null);
        }
        if (validate($scope.loginForm)) {
            $scope.loginData.password = new Hashes.SHA1().hex($scope.loginData.password);
            $myHttp.post('/session/login', $scope.loginData).mySuccess(function (result) {
                if (result.code === -1) {
                    $scope.loginData.rNum = Math.random();
                    $scope.loginData.password = '';
                    $scope.loginData.vercode = '';
                } else if (result.code == 0) {
                    $scope.menus = [];
                    $scope.leftMenus = result.data.leftMenus;
                    var username = $.cookie('username');
                    var password = $.cookie('password');
                    $scope.loginData = {
                        rNum: Math.random(),
                        username: username == 'null' ? '' : username,
                        password: password == 'null' ? '' : password,
                        vercode: "",
                        rememberPassword: $.cookie('rememberPassword') === 'true'
                    };
                    $timeout(function () {
                        $('.layui-nav-bar').remove();
                        layuiElement.render('nav');
                        $('#login-page').removeClass('logout');
                    });
                    $realTime.regPool('newsAlarm', function () {
                        $scope.newAlarm();
                        layuiTable.reload('20190703120431-checked', {});
                    });
                }
            });
        }
    }
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
        if (response.code === -10 || response.code === -11) {
            logoutCallback();
            if (response.code === -11) {
                layuiLayer.alert('强制下线，原因：当前登录用户在其它地方登录。', { icon: 5 });
            }
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
            layuiLayer.msg(response.msg, { icon: 5, anim: 6, time: 1000 });
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
    //注册的监听池
    var regPoolMap = {};
    return {
        /**
         * 注册一个实时监听池，当发生变化是会调用callback
         * @poolName 监听池名称
         * @callback 监听会调
         */
        regPool: function (poolName, callback) {
            if (regPoolMap[poolName] === undefined) {
                regPoolMap[poolName] = {
                    callback: callback,
                    version: undefined,
                    count: 1,
                    wait:false
                };
                this.get(poolName);
            } else {
                regPoolMap[poolName].count++;
                regPoolMap[poolName].version = undefined;
                if (regPoolMap[poolName].wait === false) {
                    this.get(poolName);
                }
            }
        },
        /**
         * 注销一个实时监听池
         * @poolName 监听池名称
         */
        cancel: function (poolName) {
            if (regPoolMap[poolName].count > 0) {
                regPoolMap[poolName].count--;
            }
        },
        cancelAll: function () {
            for (var key in regPoolMap) {
                if (regPoolMap.hasOwnProperty(key) && regPoolMap[key].count>0) {
                    regPoolMap[key].count--;
                }
            }
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
            regPoolMap[poolName].wait = true;
            $http({ method: 'GET', params: { v: Math.random() }, url: '/index/realTime', headers: { 'Real-Time-Pool': poolName, 'Real-Time-Version': regPoolMap[poolName].version } })
            .success(function (response, status, headers, config) {
                regPoolMap[poolName].wait = false;
                regPoolMap[poolName].version = headers('Real-Time-Version');
                if ($.type(regPoolMap[poolName]) === 'object') {
                    if (regPoolMap[poolName].count === 0) {
                        return;
                    }
                    if (response.code === -10 || response.code === -11) {
                        thiz.cancelAll();
                        logoutCallback();
                        if (response.code === -11) {
                            layuiLayer.alert('强制下线，原因：当前登录用户在其它地方登录。', { icon: 5 });
                        }
                        if (regPoolMap[poolName].count > 0) {
                            regPoolMap[poolName].count--;
                            thiz.get(poolName);
                        }
                    } else if (response.code === 1) {
                        thiz.get(poolName);
                    } else if (response.code === 0) {
                        thiz.get(poolName);
                        regPoolMap[poolName].callback();
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
        },
        jsonp: function (url, params) {
            var ret=$http.jsonp(url + '?callback=JSON_CALLBACK&' + $.param(params));
            ret.mySuccess = function (callback) {
                ret.success(callback);
            };
        }
    };
    //自定义表单必填校验，由于内置的校验不够完善，所以只能自定义一个
}).directive('ngRequired2', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, el, attrs, ctrl) {
            scope.$watch(attrs.ngModel, function () {
                if ($.type(ctrl.$modelValue) === 'string') {
                    if (ctrl.$modelValue.length > 0) {
                        ctrl.$setValidity('required', true);
                    } else {
                        ctrl.$setValidity('required', false);
                        if (ctrl.$messages === undefined) {
                            ctrl.$messages = { required:attrs.ngRequired2Msg===undefined? '该字段不能为空': attrs.ngRequired2Msg};
                        } else {
                            ctrl.$messages['required'] = attrs.ngRequired2Msg === undefined ? '该字段不能为空' : attrs.ngRequired2Msg;
                        }
                    }
                } else {
                    ctrl.$setValidity('required', false);
                    if (ctrl.$messages === undefined) {
                        ctrl.$messages = { required: '该字段不能为空' };
                    } else {
                        ctrl.$messages['required'] = '该字段不能为空';
                    }
                }
            });
        }
    };
    //自定义表单长度校验，由于内置的校验不够完善，所以只能自定义一个
}).directive('ngLength', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, el, attrs, ctrl) {
            scope.$watch(attrs.ngModel, function () {
                var len = parseInt(attrs.ngLength);
                if ($.type(ctrl.$modelValue) === 'string') {
                    if (len === ctrl.$modelValue.length || ctrl.$modelValue.length === 0) {
                        ctrl.$setValidity('length', true);
                    } else {
                        ctrl.$setValidity('length', false);
                        if (ctrl.$messages === undefined) {
                            ctrl.$messages = { length: attrs.ngLengthMsg === undefined ? '该字段长度必须是' + len + '个字符' : attrs.ngLengthMsg };
                        } else {
                            ctrl.$messages['length'] = attrs.ngLengthMsg === undefined ? '该字段长度必须是' + len + '个字符' : attrs.ngLengthMsg;
                        }
                    }
                } else {
                    ctrl.$setValidity('length', true);
                }
            });
        }
    };
    //自定义表单最小长度校验，由于内置的校验不够完善，所以只能自定义一个
}).directive('ngMinlength2', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, el, attrs, ctrl) {
            scope.$watch(attrs.ngModel, function () {
                var len = parseInt(attrs.ngMinlength2);
                if ($.type(ctrl.$modelValue) === 'string') {
                    if (len <= ctrl.$modelValue.length || ctrl.$modelValue.length === 0) {
                        ctrl.$setValidity('minlength2', true);
                    } else {
                        ctrl.$setValidity('minlength2', false);
                        if (ctrl.$messages === undefined) {
                            ctrl.$messages = { minlength2:attrs.ngMinlength2Msg === undefined ? '该字段长度不能少于' + len + '个字符': attrs.ngMinlength2Msg};
                        } else {
                            ctrl.$messages['minlength2'] = attrs.ngMinlength2Msg === undefined ? '该字段长度不能少于' + len + '个字符' : attrs.ngMinlength2Msg;
                        }
                    }
                } else {
                    ctrl.$setValidity('minlength2', true);
                }
            });
        }
    };
    //自定义表单最大长度校验，由于内置的校验不够完善，所以只能自定义一个
}).directive('ngMaxlength2', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, el, attrs, ctrl) {
            scope.$watch(attrs.ngModel, function () {
                var len = parseInt(attrs.ngMaxlength2);
                if ($.type(ctrl.$modelValue) === 'string') {
                    if (len >= ctrl.$modelValue.length || ctrl.$modelValue.length === 0) {
                        ctrl.$setValidity('maxlength2', true);
                    } else {
                        ctrl.$setValidity('maxlength2', false);
                        if (ctrl.$messages === undefined) {
                            ctrl.$messages = { maxlength2: attrs.ngMaxlength2Msg === undefined ? '该字段长度不能超过' + len + '个字符' : attrs.ngMaxlength2Msg };
                        } else {
                            ctrl.$messages['maxlength2'] = attrs.ngMaxlength2Msg === undefined ? '该字段长度不能超过' + len + '个字符' : attrs.ngMaxlength2Msg;
                        }
                    }
                } else {
                    ctrl.$setValidity('maxlength2', true);
                }
            });
        }
    };
    //自定义表单最小值（可以是浮点数，可以是整数）校验，由于内置的校验不够完善，所以只能自定义一个
}).directive('ngMinDouble', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, el, attrs, ctrl) {
            scope.$watch(attrs.ngModel, function () {
                if ($.type(ctrl.$modelValue) === 'string' && ctrl.$modelValue.length > 0) {
                    var val=parseFloat(ctrl.$modelValue);
                    if (isNaN(val)) {
                        ctrl.$setValidity('minDouble', false);
                        if (ctrl.$messages === undefined) {
                            ctrl.$messages = { minDouble: '该字段必须是数字' };
                        } else {
                            ctrl.$messages['minDouble'] = '该字段必须是数字';
                        }
                    } else {
                        var minDouble = parseFloat(attrs.ngMinDouble);
                        if (val < minDouble) {
                            ctrl.$setValidity('minDouble', false);
                            if (ctrl.$messages === undefined) {
                                ctrl.$messages = { minDouble: attrs.ngMinDoubleMsg === undefined ?'该字段值不能小于' + minDouble:attrs.ngMinDoubleMsg };
                            } else {
                                ctrl.$messages['minDouble'] = attrs.ngMinDoubleMsg === undefined ? '该字段值不能小于' + minDouble : attrs.ngMinDoubleMsg;
                            }
                        } else {
                            ctrl.$setValidity('minDouble', true);
                        }
                    }
                }else {
                    ctrl.$setValidity('minDouble', true);
                }
            });
        }
    };
    //自定义表单最大值（可以是浮点数，可以是整数）校验，由于内置的校验不够完善，所以只能自定义一个
}).directive('ngMaxDouble', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, el, attrs, ctrl) {
            scope.$watch(attrs.ngModel, function () {
                if ($.type(ctrl.$modelValue) === 'string' && ctrl.$modelValue.length > 0) {
                    var val = parseFloat(ctrl.$modelValue);
                    if (isNaN(val)) {
                        ctrl.$setValidity('maxDouble', false);
                        if (ctrl.$messages === undefined) {
                            ctrl.$messages = { maxDouble: '该字段必须是数字' };
                        } else {
                            ctrl.$messages['maxDouble'] = '该字段必须是数字';
                        }
                    } else {
                        var maxDouble = parseFloat(attrs.ngMaxDouble);
                        if (val > maxDouble) {
                            ctrl.$setValidity('maxDouble', false);
                            if (ctrl.$messages === undefined) {
                                ctrl.$messages = { maxDouble: attrs.ngMaxDoubleMsg === undefined ? '该字段值不能大于' + maxDouble : attrs.ngMaxDoubleMsg };
                            } else {
                                ctrl.$messages['maxDouble'] = attrs.ngMaxDoubleMsg === undefined ? '该字段值不能大于' + maxDouble : attrs.ngMaxDoubleMsg;
                            }
                        } else {
                            ctrl.$setValidity('maxDouble', true);
                        }
                    }
                } else {
                    ctrl.$setValidity('maxDouble', true);
                }
            });
        }
    };
}).directive('ngUrl', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, el, attrs, ctrl) {
            scope.$watch(attrs.ngModel, function () {
                if ($.type(ctrl.$modelValue) === 'string' && ctrl.$modelValue.length > 0) {
                    if (/^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)*(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@@)|[\uE000-\uF8FF]|\/|\?)*)?(#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@@)|\/|\?)*)?$/i.test(ctrl.$modelValue)) {
                        ctrl.$setValidity('url', true);
                    } else {
                        ctrl.$setValidity('url', false);
                        if (ctrl.$messages === undefined) {
                            ctrl.$messages = { url: attrs.ngUrlMsg===undefined?'字段格式错误，必须是一个http开头的url':attrs.ngUrlMsg};
                        } else {
                            ctrl.$messages['url'] = attrs.ngUrlMsg===undefined?'字段格式错误，必须是一个http开头的url':attrs.ngUrlMsg;
                        }
                    }
                } else {
                    ctrl.$setValidity('url', true);
                }
            });
        }
    };
}).directive('ngMobile', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, el, attrs, ctrl) {
            scope.$watch(attrs.ngModel, function () {
                if ($.type(ctrl.$modelValue) === 'string' && ctrl.$modelValue.length > 0) {
                    if (/^1[3|4|5|8][0-9]\d{4,8}$/.test(ctrl.$modelValue)) {
                        ctrl.$setValidity('mobile', true);
                    } else {
                        ctrl.$setValidity('mobile', false);
                        if (ctrl.$messages === undefined) {
                            ctrl.$messages = { mobile: attrs.ngMobileMsg === undefined ? '字段格式错误，必须是合法的移动电话号码' : attrs.ngMobileMsg };
                        } else {
                            ctrl.$messages['mobile'] = attrs.ngMobileMsg === undefined ? '字段格式错误，必须是合法的移动电话号码' : attrs.ngMobileMsg;
                        }
                    }
                } else {
                    ctrl.$setValidity('mobile', true);
                }
            });
        }
    };
}).directive('ngEqualTo', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, el, attrs, ctrl) {
            scope.$watch(attrs.ngModel, function () {
                if (ctrl.$modelValue === attrs.ngEqualTo) {
                    ctrl.$setValidity('equalTo', true);
                } else {
                    ctrl.$setValidity('equalTo', false);
                    if (ctrl.$messages === undefined) {
                        ctrl.$messages = { equalTo: attrs.ngEqualToMsg };
                    } else {
                        ctrl.$messages['equalTo'] = attrs.ngEqualToMsg;
                    }
                }
            });
        }
    };
}).directive('ngIsInt', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, el, attrs, ctrl) {
            scope.$watch(attrs.ngModel, function () {
                if ($.type(ctrl.$modelValue) === 'string' && ctrl.$modelValue.length > 0) {
                    var fval=parseFloat(ctrl.$modelValue)
                    var ival = parseInt(ctrl.$modelValue);
                    if (isNaN(ival) || fval>ival) {
                        ctrl.$setValidity('isInt', false);
                        if (ctrl.$messages === undefined) {
                            ctrl.$messages = { isInt: attrs.ngIsIntMsg === undefined?'该字段必须是整数':attrs.ngIsIntMsg };
                        } else {
                            ctrl.$messages['isInt'] = attrs.ngIsIntMsg === undefined ? '该字段必须是整数' : attrs.ngIsIntMsg;
                        }
                    } else {
                        ctrl.$setValidity('isInt', true);
                    }
                } else {
                    ctrl.$setValidity('isInt', true);
                }
            });
        }
    };
}).directive('uploadExcel', function () {
    return {
        restrict: 'EA',
        template: $('#uploadExcelTemplate').html(),
        scope: { url: "@", type: "@", postData: "=" },
        controller: function ($scope, $timeout) {
            $scope.id = new UUID().id;
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
                    swf: '../plugin/webuploader/Uploader.swf',//当浏览器不支持XMLHttpWebRequest时，使用flash插件上传。
                    auto: $scope.type === 'line',//选中文件后自动上传
                    server: $scope.url,//处理上传excel的控制器
                    fileVal: 'fileUpload',//服务端接收二进制文件的参数名称
                    formData: $scope.postData,//每次上传时上传的数据
                    duplicate: true,
                    pick: {
                        id: '#uploadExcel' + $scope.id,//生成上传插件的位置
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
        scope: { url: "@", tableToolbar: "@", showCheckCol: "@", showDefaultOperCol: "@", height:"@", postData: "=", cols: "=", perms: "=" },
        controller: function ($scope, $timeout, $myHttp) {
            $scope.id = new UUID().id;
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
                layuiTable.render({
                    elem: '#data-table-' + $scope.id,
                    height: $scope.height,
                    url: $scope.url,
                    toolbar: $scope.tableToolbar,
                    id: $scope.id + '-checked',
                    done: function (res, curr, count) {
                        $scope.$emit('done', res, curr, count);
                    },
                    cols: [$scope.cols]
                });
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
        scope: { type: "@", deep: "@"},
        controller: function ($scope, $myHttp, $timeout) {
            $scope.id = new UUID().id;
            $scope.$on('renderSelect', function (event, provinceVal, cityVal, countyVal, townVal) {
                $scope.cities = [{ name: '请选择市', value: '' }].concat($($scope.data['cities']).filter(function (index, val) {
                    return val.parentValue == provinceVal;
                }).get());
                $scope.counties = [{ name: '请选择区', value: '' }].concat($($scope.data['counties']).filter(function (index, val) {
                    return val.parentValue == cityVal;
                }).get());
                $scope.towns = [{ name: '请选择镇', value: '' }].concat($($scope.data['towns']).filter(function (index, val) {
                    return val.parentValue == countyVal;
                }).get());
                $timeout(function () {
                    setSelectVal($('[lay-filter="province-select-' + $scope.id + '"]'), provinceVal);
                    setSelectVal($('[lay-filter="city-select-' + $scope.id + '"]'), cityVal);
                    setSelectVal($('[lay-filter="county-select-' + $scope.id + '"]'), countyVal);
                    setSelectVal($('[lay-filter="town-select-' + $scope.id + '"]'), townVal);
                    layuiForm.render('select');
                });
            });
            $scope.$on('getSelectVal', function (event, retVal) {
                retVal.provinceVal=$('[lay-filter="province-select-' + $scope.id + '"]').val();
                retVal.cityVal=$('[lay-filter="city-select-' + $scope.id + '"]').val();
                retVal.countyVal=$('[lay-filter="county-select-' + $scope.id + '"]').val();
                retVal.townVal = $('[lay-filter="town-select-' + $scope.id + '"]').val();
            });
            $myHttp.getCache('/index/areaSelect').mySuccess(function (result) {
                $scope.data = result.data;
                $scope.provinces = [{ name: '请选择省', value: '' }].concat($scope.data['provinces']);
                $scope.cities = [{ name: '请选择市', value: '' }];
                $scope.counties = [{ name: '请选择区', value: '' }];
                $scope.towns = [{name: '请选择镇', value: '' }];
                $timeout(function () {
                    layuiForm.render('select');
                    layuiForm.on('select(province-select-' + $scope.id+ ')', function (data) {
                        $scope.cities = [{ name: '请选择市', value: '' }].concat($($scope.data['cities']).filter(function (index, val) {
                            return val.parentValue == data.value;
                        }).get());
                        $scope.counties = [{ name: '请选择区', value: '' }];
                        $scope.towns = [{ name: '请选择镇', value: '' }];
                        $scope.$apply();
                        layuiForm.render('select');
                    });
                    layuiForm.on('select(city-select-' + $scope.id + ')', function (data) {
                        $scope.counties = [{ name: '请选择区', value: '' }].concat($($scope.data['counties']).filter(function (index, val) {
                            return val.parentValue == data.value;
                        }).get());
                        $scope.towns = [{ name: '请选择镇', value: '' }];
                        $scope.$apply();
                        layuiForm.render('select');
                    });
                    layuiForm.on('select(county-select-' + $scope.id + ')', function (data) {
                        $scope.towns = [{ name: '请选择镇', value: '' }].concat($($scope.data['towns']).filter(function (index, val) {
                            return val.parentValue == data.value;
                        }).get());
                        $scope.$apply();
                        layuiForm.render('select');
                    });
                });
            });
        }
    };
}).directive("uploadImage", function () {
    return {
        restrict: 'EA',
        template: $('#uploadImageTemplate').html(),
        scope: { path: "@", cut: "@", widthOverHeight: "@", minWidth: "@", maxWidth: "@", imgName: "=", thumbnailName: "=" },
        controller: function ($scope, layer, $timeout, $myHttp) {
            $scope.id = new UUID().id;
            $scope.isUploaded = false;
            $scope.showProgress = false;
            $scope.crop = function () {
                var select = $scope.jcropApi.tellSelect();
                var $img = $($scope.img);
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
                    $scope.src = '/index/showImage?pathName=' + $scope.path + '&imgName=' + $scope.imgName;
                });
            };
            $timeout(function () {
                $scope.uploader = new WebUploader.Uploader({
                    swf: '../plugin/webuploader/Uploader.swf',//当浏览器不支持XMLHttpWebRequest时，使用flash插件上传。
                    auto: true,//选中文件后自动上传
                    server: '/index/uploadSingleImage',//处理上传文件的统一控制器
                    fileVal: 'fileUpload',//服务端接收二进制文件的参数名称
                    formData: { pathName: $scope.path },//每次上传时要提供一个上传目录，让服务端确认保存位置
                    duplicate: true,
                    pick: {
                        id: '#uploadImage' + $scope.id,//生成上传插件的位置
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
                        var data = response.data;
                        $scope.imgName = data.imgName;
                        $scope.thumbnailName = data.thumbnailName;
                        $scope.src = '/index/showImage?pathName=' + $scope.path + '&imgName=' + $scope.imgName;
                        if ($scope.cut === 'true') {
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
                                    var wait = layuiLayer.load(0);
                                    $scope.img=document.getElementById($scope.id + '-crop');
                                    $scope.img.onload = function () {
                                        layuiLayer.close(wait);
                                        var widthOverHeight=parseFloat($scope.widthOverHeight);
                                        var minWidth = parseFloat($scope.minWidth);
                                        var minHeight=minWidth/widthOverHeight;
                                        var maxWidth =parseFloat($scope.maxWidth);
                                        var maxHeight = maxWidth / widthOverHeight;
                                        $(this).Jcrop({ allowSelect: false, aspectRatio: widthOverHeight, minSize: [minWidth, minHeight], maxSize: [maxWidth, maxHeight] }, function () {
                                            this.setSelect([0, 0, minWidth + (maxWidth - minWidth) / 2, minHeight + (maxHeight - minHeight) / 2]);
                                            $scope.jcropApi = this
                                        });
                                    };
                                    $scope.img.src = '/index/showImage?pathName=' + $scope.path + '&imgName=' + $scope.imgName;
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
}).directive('pieChart', function () {
    return {
        restrict: 'EA',
        template: $('#pieChartTemplate').html(),
        scope: { title:"@" ,pieData:"="},
        controller: function ($scope, $timeout) {
            $scope.id = new UUID().id;
            $timeout(function () {
                Highcharts.chart('pieChart' + $scope.id, {
                    chart: {
                        plotBackgroundColor: null,
                        plotBorderWidth: null,
                        plotShadow: false,
                        type: 'pie'
                    },
                    title: {
                        text: $scope.title
                    },
                    tooltip: {
                        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                    },
                    plotOptions: {
                        pie: {
                            allowPointSelect: true,
                            cursor: 'pointer',
                            dataLabels: {
                                enabled: false
                            },
                            showInLegend: true
                        }
                    },
                    series: [{
                        name: 'Brands',
                        colorByPoint: true,
                        data:$scope.pieData
                    }]
                });
            });
        }
    };
}).directive('histogram', function () {
    return {
        restrict: 'EA',
        template: $('#histogramTemplate').html(),
        scope: { title: "@",unit:"@", rowAxisTitle: "=", yAxisTitle: "=", histogramData: "=" },
        controller: function ($scope, $timeout) {
            $scope.id = new UUID().id;
            $timeout(function () {
                Highcharts.chart('histogram' + $scope.id, {
                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: $scope.title
                    },
                    xAxis: {
                        categories: $scope.rowAxisTitle,
                        crosshair: true
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: $scope.yAxisTitle//'降雨量 (mm)'
                        }
                    },
                    tooltip: {
                        // head + 每个 point + footer 拼接成完整的 table
                        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                        pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:.1f} ' + $scope.unit+ '</b></td></tr>',
                        footerFormat: '</table>',
                        shared: true,
                        useHTML: true
                    },
                    plotOptions: {
                        column: {
                            borderWidth: 0
                        }
                    },
                    series: $scope.histogramData
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
        scope: { url: "@",targetController:"@"},
        controller: function ($scope, $myHttp) {
            $scope.id = new UUID().id;
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
                    ztree.showNodes(ztree.getNodes());
                }
            };
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
                                $scope.$emit('onClick',treeNode);
                            },
                            beforeDrop: function (treeId, treeNodes, targetNode, moveType) {
                                var retObj = {ret:true};
                                $scope.$emit('beforeDrop', treeNodes, targetNode, moveType, retObj);
                                return retObj.ret;
                            }
                        }
                    }, result.data);
                    layuiForm.render();
                    $('#' + $scope.id + ' .left-tree').height($('#' + $scope.id + ' .right-content').height());
                });
            };
            $scope.loadTree();
        }
    };
}).directive("uploadFiles", function () {
    return {
        restrict: 'EA',
        template: $('#uploadFilesTemplate').html(),
        scope: { fileDescMaxWidth: "@", path: "@", files:"=" },
        controller: function ($scope, $timeout, $myHttp) {
            $scope.id = new UUID().id;
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
                    swf: '../plugin/webuploader/Uploader.swf',//插件所需的flash路径，用于兼容不支持XMLHttpWebRequest对象的浏览器
                    auto: true,//拖动后自动上传
                    duplicate: true,//对每一个文件添加唯一hash值，用于区分文件操作进度条
                    server: '/index/uploadFiles',//统一上传的控制器
                    pick: { id: '#uploadFiles' + $scope.id},//上传域的id
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
 * 表单校验
 * @formObj angularjs的表单对象
 * @return 返回true校验通过，否则校验不过
 */
function validate(formObj) {
    if (formObj.$invalid) {
        var formName = formObj.$name;
        for (var key1 in formObj) {
            if (formObj.hasOwnProperty(key1) && key1.indexOf('$') != 0) {
                if (formObj[key1].$invalid) {
                    var error = formObj[key1].$error;
                    var messages = formObj[key1].$messages;
                    for (var key2 in error) {
                        if (error.hasOwnProperty(key2) && error[key2] === true) {
                            var tipIndex = layuiLayer.tips(messages[key2], $('[name="' + formName + '"] [name="' + key1 + '"]'), {
                                tips: 1,
                                time: 3000
                            });
                            $('[name="' + formName + '"] [name="' + key1 + '"]').one('blur', function () {
                                layuiLayer.close(tipIndex);
                            });
                            return false;
                        }
                    }
                }
            }
        }
    }
    return true;
}

/**
 * 设置下拉菜单的值
 * @$select：jquery对象，下拉菜单
 * @val：设置的值
 */
function setSelectVal($select, val) {
    $select.find('option').each(function (i) {
        if ($(this).val() == val) {
            $select.get(0).selectedIndex = i;
            return false;
        }
    });
}

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
    var basePath = '../images/256x256/';
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
    layuiLayer.closeAll();
}

/**
 * 对图片进行预览处理
 * @$range  处理范围
 */
function picViewer($range) {
    $range.viewer({
        url: 'data-original',
        built: function () {
            $('.layui-layout-body').append($('.viewer-container'));
        },
    });
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