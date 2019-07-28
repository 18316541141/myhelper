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

Vue.component('area-select', {
    template: '#areaSelectTemplate',
    props: ['type', 'deep','province','city','county','town'],
    data() {
        return { id: new UUID().id, province: '', city: '', county: '', town: '', provinces: [], cities: [], counties: [], towns: [] };
    },
    watch: {
        province() {
            
        },
        city() {
            
        },
        county() {
            
        }
    },
    methods: {
        changeProvince() {
            var thiz=this;
            this.city = '';
            this.county = '';
            this.town = '';
            this.cities = this.data['cities'].filter(function (val) {
                return val.parentValue == thiz.province;
            });
            this.counties = [];
            this.towns = [];
        },
        changeCity() {
            var thiz = this;
            this.county = '';
            this.town = '';
            this.counties = this.data['counties'].filter(function (val) {
                return val.parentValue == thiz.city;
            });
            this.towns = [];
        },
        changeCounty() {
            var thiz = this;
            this.town = '';
            this.towns = this.data['towns'].filter(function (val) {
                return val.parentValue == thiz.county;
            });
        }
    },
    mounted() {
        var thiz = this;
        axios.get('/index/areaSelect').then(function (response) {
            var result = response.data;
            if (result.code === 0) {
                thiz.data = result.data;
                thiz.provinces = thiz.data['provinces'];
                thiz.cities = [];
                thiz.counties = [];
                thiz.towns = [];
            }
        });
    }
});
Vue.component('upload-excel', {
    template: '#uploadExcelTemplate',
    props: ['url', 'type', 'postData'],
    data() {
        return { id: new UUID().id, postData: {}, isUploaded :false};
    },
    methods:{
        submit() {
            this.uploader.upload();
        }
    },
    mounted() {
        var thiz = this;
        this.uploader = new WebUploader.Uploader({
            swf: '../plugin/webuploader/Uploader.swf',
            auto: this.type === 'line',//选中文件后自动上传
            server: this.url,//处理上传excel的控制器
            fileVal: 'fileUpload',//服务端接收二进制文件的参数名称
            formData: this.postData,//每次上传时上传的数据
            duplicate: true,
            pick: {
                id: '#uploadExcel' + this.id,//生成上传插件的位置
                multiple: false //每次只能选一个文件
            },
            //允许上传的图片格式后缀
            accept: {
                title: 'excel',
                extensions: 'xls,xlsx',
                mimeTypes: 'application/vnd.ms-excel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
            }
        }).on('uploadStart', function (file) {

        }).on('uploadSuccess', uploadCallback(function (file, response) {

        })).on('error', function (type) {
            if (type === 'Q_TYPE_DENIED') {
                layuiLayer.msg('该文件类型可能不是Excel文件。', { icon: 5, anim: 6 });
            }
        }).on('beforeFileQueued', function (file) {
            thiz.isUploaded = true;
            thiz.excelFileName = file.name;
        });
    }
});
Vue.component('upload-files', {
    template: '#uploadFilesTemplate',
    props: ['fileDescWidth', 'path', 'files'],
    data() {
        return { id: new UUID().id, files: []};
    },
    methods: {
        delFile(fileName) {
            var files = this.files;
            for (var i = 0, len = files.length; i < len; i++) {
                if (files[i].fileName === fileName) {
                    files.splice(i, 1);
                    break;
                }
            }
        },
        downFile(row){
            postOpenWin('/index/downFile', {
                pathName: this.path,
                fileName: row.fileName,
                fileDesc: row.fileDesc
            });
        }
    },
    mounted() {
        //记录每一个文件的进度
        var fileMap = {};
        var thiz = this;
        new WebUploader.Uploader({
            swf: '../plugin/webuploader/Uploader.swf',
            auto: true,
            duplicate: true,
            server: '/index/uploadFiles',
            pick: { id: '#uploadFiles' + this.id },
            fileVal: 'fileUploads',
            formData: {
                pathName: this.path //上传时的路径参数
            }
        }).on('uploadStart', function (file) {
            var files = thiz.files;
            fileMap[file.id] = files.length;
            files.push({ fileDesc: file.name, typeImg: typeImgByMime(file.ext), progress: 0, isFinish: false, id: file.id });
        }).on('uploadProgress', function (file, percentage) {
            thiz.files[fileMap[file.id]].percentage = parseInt(percentage * 100);
        }).on('uploadSuccess', function (file, response) {
            var fileObj = thiz.files[fileMap[file.id]];
            fileObj.fileName = response.data;
            fileObj.isFinish = true;
        });
    }
});
Vue.component('upload-image', {
    template: '#uploadImageTemplate',
    props: ['path', 'cut', 'widthOverHeight', 'minWidth', 'maxWidth', 'imgName', 'thumbnailName'],
    data() {
        return { id: new UUID().id, percentage: 0, src: '', status: '', contentHeight: 0, contentWidth: 0,imgWidth:0,imgHeight:0,cropDialog: false, isUploaded: false, showProgress: false };
    },
    methods: {
        format(percentage) {
            return percentage +'%'
        },
        imgLoadFinish(event) {
            this.img = event.target;
            var thiz = this;
            $(this.img).Jcrop({ allowSelect: false, aspectRatio: this.widthOverHeight, minSize: [this.minWidth, this.minHeight], maxSize: [this.maxWidth, this.maxHeight] }, function () {
                this.setSelect([0, 0, thiz.minWidth + (thiz.maxWidth - thiz.minWidth) / 2, thiz.minHeight + (thiz.maxHeight - thiz.minHeight) / 2]);
                thiz.jcropApi = this
            });
        },
        crop() {
            var select = this.jcropApi.tellSelect();
            var thiz = this;
            axios.post('/index/singleImageCrop', {
                pathName: this.path,
                imgName: this.imgName,
                imgWidth: this.img.width,
                imgHeight: this.img.height,
                x: select.x,
                y: select.y,
                w: select.w,
                h: select.h
            }).then(function (response) {
                thiz.cropDialog = false;
                var result = response.data;
                if (result.code === 0) {
                    var data = result.data;
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
            thiz.percentage = parseInt(percentage * 100);
        }).on('uploadSuccess', uploadCallback(function (file, response) {
            if (response.code === 0) {
                var data = response.data;
                thiz.imgName = data.imgName;
                thiz.thumbnailName = data.thumbnailName;
                thiz.src = '/index/showImage?pathName=' + thiz.path + '&imgName=' + thiz.imgName;
                if (thiz.cut === true) {
                    var maxWidth = window.screen.width - 60;
                    var maxHeight = window.screen.height - 340;
                    if (data.imgWidth >= maxWidth) {
                        thiz.contentWidth = (window.screen.width - 20) + 'px';
                    } else if (data.imgWidth <= 400) {
                        thiz.contentWidth = 420 + 'px';
                    } else {
                        thiz.contentWidth = data.imgWidth + 'px';
                    }
                    if (data.imgHeight >= maxHeight) {
                        thiz.contentHeight = (window.screen.height - 340) + 'px';
                    } else if (data.imgHeight <= 300) {
                        thiz.contentHeight = 300 + 'px';
                    } else {
                        thiz.contentHeight = data.imgHeight + 'px';
                    }
                    thiz.imgWidth = data.imgWidth + 'px';
                    thiz.imgHeight = data.imgHeight + 'px';
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
            var thiz = this;
            axios.post('/session/login', this.loginData).then(function (response) {
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
    var loading;
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
        //loading = myApp.$loading({
        //    lock: true,
        //    text: 'Loading',
        //    spinner: 'el-icon-loading',
        //    background: 'rgba(0, 0, 0, 0.7)'
        //});
        return config;
    }, function (error) {
        return Promise.reject(error);
    });
    //响应拦截器
    axios.interceptors.response.use(function (response) {
        //loading.close();
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
 * @initObj 用于初始化控件template以外的值
 */
function controller(id, url, initObj) {
    Vue.component(id, function (resolve, reject) {
        axios.get(url, { params: { v: getQueryVariable('v') } }).then(function (response) {
            initObj.template = response.data;
            resolve(initObj);
        });
    });
}

/**
 * 对图片进行预览处理
 * @rangeSelector  处理范围
 */
function picViewer(rangeSelector) {
    setTimeout(function () {
        $(rangeSelector).viewer({
            url: 'data-original',
            built: function () {
                $('body').append($('.viewer-container'));
            },
        });
    },100);
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
    } else {
        basePath += 'ini-256.png';
    }
    return basePath;
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