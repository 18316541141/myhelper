'use strict';

//异步加载图片
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
        layui.img(images[i]);
    }
}());

var layuiForm = layui.form;
var layuiLayer = layui.layer;
var layuiElement = layui.element;
/**
 * layui自定义的表单校验，
 * 方法源码在webapp/my-js/common-by-layui.js
 */
addCustomVerify(layuiForm);

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
    $.myPost('/index/login', data, function (result) {
        if (result.code == -1) {
            $scope.$apply(function () {
                $scope.rNum = Math.random();
                $scope.data.vercode = '';
            });
        } else if (result.code == 0) {
            $scope = $('[ng-controller="left-menus"]').scope();
            $scope.$apply(function () {
                $scope.leftMenus = result.data.leftMenus;
            });
            $('.layui-nav-bar').remove();
            layuiElement.render('nav');
            setTimeout(function () {
                $('#login-page').removeClass().css('left', '100%');
            }, 450);
            $('#login-page').removeClass().addClass('login-ani');
        }
    });
});



var myApp = angular.module('my-app', ['ngSanitize']).controller('main-body', function ($scope) {
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
    };
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        var menus = $scope.menus;
        $('[data-menu-id="' + menus[menus.length - 1].id + '"]:last').click();
    });
}).controller('login-form', function ($scope) {
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
}).directive("uploadImage", function () {
    return { template: $('#uploadImageTemplate').html() };
}).directive('onFinishRenderFilters', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            if (scope.$last === true) {
                $timeout(function () {
                    scope.$emit('ngRepeatFinished');
                });
            }
        }
    };
}).directive("uploadImage", function () {
    return {
        restrict: 'E',
        template: $('#uploadImageTemplate').html(),
        scope: { name: "@" },
        controller: function ($scope) {
            $scope.isUploaded = false;
            var index;
            setTimeout(function () {
                var uploader = new WebUploader.Uploader({
                    swf: 'plugin/webuploader/Uploader.swf',
                    auto: true,
                    duplicate: true,
                    server: '/index/uploadImage',
                    pick: { id: '#' + $scope.name + 'Id' }
                });
                uploader.on('startUpload', function () {
                    index = layuiLayer.open({
                        type: 1,
                        content: $('#singleUploadProgress').html()
                    });
                });
                uploader.on('uploadProgress', function (file, percentage) {
                    $('#single-upload-progress>[lay-percent]').attr("lay-percent", (percentage * 100).toFixed(1) + '%');
                });
                uploader.on('uploadSuccess', function (file, response) {
                    layer.close(index);
                    var data = response.data;
                    $('#' + $scope.name).val(data.name);
                });
            }, 1);
        },
    };
}).directive("uploadFiles", function () {
    return {
        restrict: 'E',
        template: $('#uploadFilesTemplate').html(),
        scope: { name: "@" },
        controller: function ($scope) {
            $scope.isUploaded = false;
            $scope.files = [];
            setTimeout(function () {
                var uploader = new WebUploader.Uploader({
                    swf: 'plugin/webuploader/Uploader.swf',
                    auto: true,
                    duplicate: true,
                    server: '/index/uploadFiles',
                    pick: { id: '#' + $scope.name + 'Id' }
                });
                uploader.on('startUpload', function () {
                    $scope.$apply(function () {
                        var files = $scope.files;
                        files[files.length] = { fileName: 'aa', typeImg: 'aa', progress: 0, };
                    });
                });
                uploader.on('uploadProgress', function (file, percentage) {

                });
                uploader.on('uploadSuccess', function (file, response) {
                    var data = response.data;
                });
            }, 1);
        }
    };
}).controller('left-menus', function ($scope) {
    $scope.leftMenus = [];
    /**
	 * 打开指定菜单页
	 */
    $scope.openMenuPage = function (id, url, title) {
        var $scope = $('[ng-controller="main-body"]').scope();
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
            $scope.menus[$scope.menus.length] = {
                title: title,
                url: url,
                id: id
            };
        }
    };
});

//加载左侧菜单
$.myGet('/index/loadLeftMenus', function (result) {
    var $scope = $('[ng-controller="left-menus"]').scope();
    $scope.$apply(function () {
        $scope.leftMenus = result.data;
    });
    $('.layui-nav-bar').remove();
    layuiElement.render('nav');
});

/**
 * 退出登录的回调方法
 */
function logoutCallback() {
    $('#login-page').removeClass().addClass('logout-ani').css('left', '0');
    var $scope = $('[ng-controller="login-form"]').scope();
    $scope.$apply(function () {
        $scope.data = null;
        $scope.rNum = Math.random();
    });
}

/**
 * 退出登陆的方法
 */
function logout() {
    $.myGet('/index/logout', logoutCallback);
}