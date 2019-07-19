'use strict';
//标题移动效果
(function () {
    var title = document.title;
    if (title.length > 10) {
        setInterval(function () {
            var title = document.title;
            document.title = title.substring(1, title.length) + title.charAt(0);
        }, 400);
        title = null;
    }
}());
Vue.component('upload-image', {
    template: '#uploadImageTemplate',
    props: ['path', 'cut', 'widthOverHeight', 'minWidth', 'maxWidth', 'imgName', 'thumbnailName'],
    data() {
        return { id: new UUID().id, percentage: 0, src: '', status: '', imgHeight: 0, imgWidth:0, cropDialog: false, isUploaded: false, showProgress: false };
    },
    methods: {
        format(percentage) {
            return percentage +'%'
        },
        imgLoadFinish(event) {
            //this.img = event.target;
            //var thiz = this;
            //$(this.img).Jcrop({ allowSelect: false, aspectRatio: this.widthOverHeight, minSize: [this.minWidth, this.minHeight], maxSize: [this.maxWidth, this.maxHeight] }, function () {
            //    this.setSelect([0, 0, thiz.minWidth + (thiz.maxWidth - thiz.minWidth) / 2, thiz.minHeight + (thiz.maxHeight - thiz.minHeight) / 2]);
            //    thiz.jcropApi = this
            //});
        },
        crop() {
            var select = this.jcropApi.tellSelect();
            myApp.$data.loading = true;
            axios.post('/index/singleImageCrop', {
                params: {
                    pathName: this.path,
                    imgName: this.imgName,
                    imgWidth: this.img.width,
                    imgHeight: this.img.height,
                    x: select.x,
                    y: select.y,
                    w: select.w,
                    h: select.h
                }
            }).then(function (response) {
                myApp.$data.loading = false;
                thiz.cropDialog = false;
                var data = response.data;
                if (data.code === 0) {
                    thiz.imgName = data.imgName;
                    thiz.thumbnailName = data.thumbnailName;
                    thiz.src = '/index/showImage?pathName=' + thiz.path + '&imgName=' + thiz.imgName;
                }
            });
        }
    },
    mounted() {
        var thiz = this;
        new WebUploader.Uploader({
            swf: '../plugin/webuploader/Uploader.swf',//当浏览器不支持XMLHttpWebRequest时，使用flash插件上传。
            auto: true,//选中文件后自动上传
            server: '/index/uploadSingleImage',//处理上传文件的统一控制器
            fileVal: 'fileUpload',//服务端接收二进制文件的参数名称
            formData: { pathName: thiz.path },//每次上传时要提供一个上传目录，让服务端确认保存位置
            duplicate: true,
            pick: {
                id: '#uploadImage' + thiz.id,//生成上传插件的位置
                multiple: false //每次只能选一个文件
            },
            //允许上传的图片格式后缀
            accept: {
                title: 'image',
                extensions: 'gif,webp,jpg,jpeg,bmp,png,tif,emf,ico,flic,wmf,raw,hdri,ai,eps,ufo,dxf,pcd,cdr,psd,svg,fpx,exif,tga,pcx,GIF,JPG,JPEG,BMP,PNG,WEBP,PCX,TIF,TGA,EXIF,FPX,SVG,PSD,CDR,PCD,DXF,UFO,EPS,AI,HDRI,RAW,WMF,FLIC,EMF,ICO',
                mimeTypes: 'image/*',
            }
        }).on('uploadStart', function (file) {
            thiz.showProgress = true;
        }).on('uploadProgress', function (file, percentage) {
            thiz.percentage = percentage * 100;
        }).on('uploadSuccess', uploadCallback(function (file, response) {
            if (response.code === 0) {
                var data = response.data;
                thiz.imgName = data.imgName;
                thiz.thumbnailName = data.thumbnailName;
                thiz.src = '/index/showImage?pathName=' + thiz.path + '&imgName=' + thiz.imgName;
                if (thiz.cut === true) {
                    //data.imgWidth;
                    //data.imgHeight;
                    //thiz.imgWidth=;
                    //var maxHeight=window.screen.height - 350;
                    //if (maxHeight > data.imgHeight) {
                    //    thiz.imgHeight = data.imgHeight + 'px';
                    //}else{
                    //    thiz.imgHeight = maxHeight+ 'px';
                    //}
                    //thiz.imgWidth = data.imgWidth;
                    thiz.cropDialog = true;
                }
                thiz.isUploaded = true;
            } else if (response.code === -1) {
                thiz.isUploaded = false;
            }
            thiz.showProgress = false;
        })).on('error', function (type) {
            if (type === 'Q_TYPE_DENIED') {
                myApp.$message({ message: '该文件类型可能不是图片文件。', type: 'warning' });
            }
        });
    }
});
Vue.component('histogram', {
    template: '<div v-bind:id="\'histogram\'+id"></div>',
    props: ['title', 'unit', 'rowAxisTitle', 'yAxisTitle', 'histogramData'],
    data() {
        return { id: new UUID().id };
    },
    watch: {
        histogramData: function (val) {
            Highcharts.chart('histogram' + this.id, {
                chart: {
                    type: 'column'
                },
                title: {
                    text: this.title
                },
                xAxis: {
                    categories: this.rowAxisTitle,
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: this.yAxisTitle//'降雨量 (mm)'
                    }
                },
                tooltip: {
                    // head + 每个 point + footer 拼接成完整的 table
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>{point.y:.1f} ' + this.unit + '</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true
                },
                plotOptions: {
                    column: {
                        borderWidth: 0
                    }
                },
                series: val
            });
        }
    }
});
Vue.component('pie-chart', {
    template: '<div v-bind:id="\'pieChart\'+id"></div>',
    props: ['title', 'pieData'],
    data() {
        return { id: new UUID().id};
    },
    watch:{
        pieData: function (val) {
            Highcharts.chart('pieChart' + this.id, {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                title: {
                    text: this.title
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
                    data: val
                }]
            });
        }
    }
});

var myApp = new Vue({
    data: {
        loginData: {
            username: '',
            password: '',
        },
        leftMenus: [],
        menus: [],
        isLogin: true,
        loading: false,
        isCollapse: false,
        menuActive: null,
        rVercode: '/session/verificationCode?r=' + Math.random(),
        pieData:[],
    },
    methods: {
        closeNavTab(targetName) {
            var menus = this.menus;
            for (var i = 0, len = menus.length; i < len ; i++) {
                if (menus[i].id === targetName) {
                    menus.splice(i, 1);
                    break;
                }
            }
        },
        refreshVercode(){
            this.rVercode='/session/verificationCode?r=' + Math.random();
        },
        findLeftMenuById(id) {
            var leftMenus=this.leftMenus;
            for (var i = 0, len = leftMenus.length; i < len; i++) {
                var childMenus = leftMenus[i].leftMenus;
                for (var j = 0, len = childMenus.length; j < len; j++) {
                    if (childMenus[j].id === id) {
                        return childMenus[j];
                    }
                }
            }
            return null;
        },
        leftNavSelect(key, keyPath) {
            var menus=this.menus;
            for (var i = 0, len = menus.length;i<len ;i++) {
                if (menus[i].id === key) {
                    this.menuActive = key;
                    return;
                }
            }
            var ret=this.findLeftMenuById(key);
            menus[menus.length] = {
                title: ret.title,
                id: key
            };
            this.menuActive = key;
        },
        login() {
            this.loginData.password = new Hashes.SHA1().hex(this.loginData.password);
            this.loading = true;
            var thiz = this;
            axios.post('/session/login', this.loginData).then(function (response) {
                thiz.loading = false;
                var result = response.data;
                if (result.code === 0) {
                    var data = result.data;
                    thiz.isLogin = true;
                    thiz.leftMenus = data.leftMenus;
                } else {
                    thiz.loginData.password = '';
                    thiz.loginData.vercode = '';
                    thiz.refreshVercode();
                }
            });
        }
    }
}).$mount('#myApp');

//上传文件插件的回调函数
var uploadCallback = function (callback) {
    return function (file, response) {
        //登录超时，退出登录
        if (response.code === -10 || response.code === -11) {
            myApp.$data.isLogin = false;
            if (response.code === -11) {
                myApp.$message({ message: '强制下线，原因：当前登录用户在其它地方登录。', type: 'warning' });
            }
        }
            //用户无权限，无法操作，但需要后续处理
        else if (response.code === -9) {
            myApp.$message({ message: '当前用户组无操作权限！', type: 'warning' });
            callback(file, response);
        }
            //用户无权限，无法操作
        else if (response.code === -8) {
            myApp.$message({ message: '当前用户组无操作权限！', type: 'warning' });
        }
            //常规错误，
        else if (response.code === -1) {
            myApp.$message({ message: data.msg, type: 'error' });
            callback(file, response);
        }
            //成功
        else if (response.code === 0) {
            if (response.msg != null && response.msg != '') {
                myApp.$message({ message: data.msg, type: 'success' });
            }
            callback(file, response);
        }
    }
};

//初始化axios的拦截器
(function (myApp) {
    //使用缓存的url，部分url数据是需要使用缓存的，统一在这里配置
    var cacheUrls = [
        '/index/areaSelect',//加载省、市、区、镇列表，基本不怎么变化，所以使用缓存
    ];
    //请求拦截器
    axios.interceptors.request.use(function (config) {
        var cacheUrls2 = cacheUrls.filter(function (currentValue, index, arr) {
            return currentValue.indexOf(config.url) > -1;
        });
        if (cacheUrls2.length === 0) {
            if (config.params === undefined) {
                config.params = {};
            }
            config.params['r'] = Math.random();//避免ie浏览器使用缓存
        }
        return config;
    }, function (error) {
        return Promise.reject(error);
    });
    //响应拦截器
    axios.interceptors.response.use(function (response) {
        var data = response.data;
        //登录超时，退出登录
        if (data.code === -10 || data.code === -11) {
            myApp.$data.isLogin = false;
            if (data.code === -11) {
                myApp.$message({ message: '强制下线，原因：当前登录用户在其它地方登录。', type: 'warning' });
            }
        }
            //用户无权限，无法操作，但需要后续处理
        else if (data.code === -9) {
            myApp.$message({ message: '当前用户组无操作权限！', type: 'warning' });
        }
            //用户无权限，无法操作
        else if (data.code === -8) {
            myApp.$message({ message: '当前用户组无操作权限！', type: 'warning' });
        }
            //常规错误，
        else if (data.code === -1) {
            myApp.$message({ message: data.msg, type: 'error' });
        }
            //成功
        else if (data.code === 0) {
            if (data.msg != null && data.msg != '') {
                myApp.$message({ message: data.msg, type: 'success' });
            }
        }
        return response;
    }, function (error) {
        return Promise.reject(error);
    });
}(myApp));

//数据初始化
(function (myApp) {
    axios.get('/index/loadLoginData').then(function (response) {
        var result = response.data;
        if (result.code === 0) {
            var data = result.data;
            myApp.leftMenus = data.leftMenus;
        }
    });
}(myApp));

/**
 * 获取url的参数
 * @variable 参数名称
 */
function getQueryVariable(variable) {
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        if (pair[0] == variable) { return pair[1]; }
    }
    return (false);
}

/**
 * 控制器初始化
 * @id 必须和菜单的id对应
 * @url 模板的url
 * @initData 初始数据，模板上所需的数据都必须初始化
 * @callback 回调函数
 */
function controller(id,url,initData,callback) {
    Vue.component(id, function (resolve, reject) {
        axios.get(url, { params: { v: getQueryVariable('v')} }).then(function (response) {
            resolve({
                data() {
                    return initData;
                },
                template: response.data,
                mounted: callback
            });
        });
    });
}