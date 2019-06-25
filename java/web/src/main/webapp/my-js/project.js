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
        new Image().src = images[i];
    }
}());

var layuiForm = layui.form;
var layuiLayer = layui.layer;
var layuiElement = layui.element;
var layuiTree = layui.tree;
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



var myApp = angular.module('my-app', ['ngSanitize', 'ng-layer']).controller('main-body', function ($scope) {
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
        setTimeout(function () {
            layuiElement.render('tab', 'docDemoTabBrief');
        },1);
    };
}).factory('$myHttp', function ($http) {
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
            callback = null;
        }
    };
    return {
        get: function (url, params) {
            params['v'] = Math.random();
            var ret = $http({ method: 'GET', params: params, url: url });
            ret.mySuccess = function (callback) {
                ret.success(myCallback(callback));
            };
            return ret;
        },
        post: function (url, data) {
            var ret = $http({ method: 'POST', url: url, params: { v: Math.random() }, data: data });
            ret.mySuccess = function (callback) {
                ret.success(myCallback(callback));
            };
            return ret;
        },
    };
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
}).directive("areaSelect", function () {
    return {
        restrict: 'E',
        template: $('#areaSelectTemplate').html(),
        scope: { type: "@", deep: "@", required:"@" },
        controller: function ($scope, $myHttp) {
            $scope.ie8 = navigator.appName == "Microsoft Internet Explorer" && navigator.appVersion.match(/8./i) == "8.";
            $myHttp.get('/index/areaSelect').mySuccess(function (result) {
                $scope.data = result.data;
            });
            $scope.changeProvice = function (e) {
                $scope.provinceVal = $(e.target).val();
                $scope.cities = [];
                for (var i = 0, len = $scope.data.length;i<len ;i++) {
                    if ($scope.data[i].parentValue == $scope.provinceVal) {
                        $scope.cities[$scope.cities.length] = $scope.data[i];
                    }
                }
                $scope.counties = [];
                $scope.towns = [];
                layuiForm.render('select');
            }
            $scope.changeCity = function (e) {
                $scope.cityVal = $(e.target).val();
                $scope.counties = [];
                for (var i = 0, len = $scope.data.length; i < len ; i++) {
                    if ($scope.data[i].parentValue == $scope.cityVal) {
                        $scope.counties[$scope.counties.length] = $scope.data[i];
                    }
                }
                $scope.towns = [];
                layuiForm.render('select');
            }
            $scope.changeCountry = function (e) {
                $scope.countryVal = $(e.target).val();
                $scope.towns = [];
                for (var i = 0, len = $scope.data.length; i < len ; i++) {
                    if ($scope.data[i].parentValue == $scope.countryVal) {
                        $scope.towns[$scope.towns.length] = $scope.data[i];
                    }
                }
                layuiForm.render('select');
            }
        }
    };
}).directive("uploadImage", function () {
    return {
        restrict: 'E',
        template: $('#uploadImageTemplate').html(),
        scope: { name: "@",path:"@",cut:"@" },
        controller: function ($scope, layer, $timeout, $myHttp) {
            $scope.isUploaded = false;
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
                    $scope.imgName = response.data;
                });
            };
            $timeout(function () {
                var index;
                $scope.uploader = new WebUploader.Uploader({
                    swf: 'plugin/webuploader/Uploader.swf',
                    auto: true,
                    duplicate: true,
                    server: '/index/uploadSingleImage',
                    fileVal: 'fileUpload',
                    formData:{
                        pathName:$scope.path
                    },
                    pick: { id: '#' + $scope.name + 'Id' }
                }).on('startUpload', function () {
                    index = layuiLayer.open({
                        type: 1,
                        content: $('#singleUploadProgress').html()
                    });
                }).on('uploadProgress', function (file, percentage) {
                    $('#single-upload-progress>[lay-percent]').attr("lay-percent", (percentage * 100).toFixed(1) + '%');
                }).on('uploadSuccess', function (file, response) {
                    var data = response.data;
                    $scope.imgName = data.imgName;
                    layer.close(index);
                    if ($scope.cut === 'true') {
                        $scope.cropLayer = layer.open({
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
                    $scope.$apply(function () {
                        $scope.isUploaded = true;
                    });
                    $('#' + $scope.name + '-show-area').addClass('ani');
                    $timeout(function () {
                        $('#' + $scope.name + '-show-area').removeClass('ani');
                    },280);
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
        restrict: 'E',
        template: $('#treeFormTemplate').html(),
        transclude: true,
        scope: { url: "@", id: "@",targetController:"@"},
        controller: function ($scope) {
            var $rightContent = $('#' + $scope.id + ' .right-content');
            var $treeContent = $rightContent.parent();
            var $leftTree = $('#' + $scope.id + ' .left-tree');
            $.myGet($scope.url, function (result) {
                $.fn.zTree.init($('#tree-' + $scope.id), {
                    view: {
                        showLine: false
                    },
                    edit: {
                        enable: true,
                        showRemoveBtn:true
                    },
                    callback: {
                        onClick: function (event, treeId, treeNode) {
                            $rightContent.addClass('ani');
                            $treeContent.css('overflow-x', 'hidden');
                            setTimeout(function () {
                                $leftTree.css('opacity', 'unset');
                                $rightContent.css('opacity', 'unset').removeClass('ani');
                                $treeContent.css('overflow-x', 'unset');
                            },280);
                        }
                    }
                }, result.data);
                layuiForm.render();
                $leftTree.height($rightContent.height());
                $treeContent.css('overflow-x', 'hidden');
                $leftTree.addClass('ani');
                $rightContent.addClass('ani');
                setTimeout(function () {
                    $leftTree.css('opacity', 'unset').removeClass('ani');
                    $rightContent.css('opacity', 'unset').removeClass('ani');
                    $treeContent.css('overflow-x','unset');
                }, 280);
            });
        }
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
    $scope.openMenuPage = function (y) {
        var $scope = $('[ng-controller="main-body"]').scope();
        var menus = $scope.menus;
        var exist = false;
        for (var i = 0, len = menus.length; i < len; i++) {
            if (menus[i].id == y.id) {
                exist = true;
                break;
            }
        }
        if (exist) {
            $('[data-menu-id="' + y.id + '"]').click();
        } else {
            $scope.menus[$scope.menus.length] = {
                title: y.title,
                url: y.url,
                id: y.id
            };
        }
    };
});

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

//--------------------------分割线-------------------------------
//上面的代码不能改，下面的代码可以改
//--------------------------分割线-------------------------------
myApp.controller('testTreeForm', function ($scope) {
    $scope.callback = function (result) {

    }
});
myApp.controller("upload-image", function ($scope) {
    
});
myApp.controller('big-img-ctrl',function($scope,$timeout){
    $timeout(function(){
        $('.xxx-table').viewer({
            url: 'data-original',
        });
    });
});