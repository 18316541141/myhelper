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
var highcharts = Highcharts;
Vue.component('pie-chart', {
    template: '<div v-bind:id="\'pieChart\'+id"></div>',
    props: ['id', 'title'],
    data() {
        return {};
    },
    methods: {
        redraw:function (pieData) {
            highcharts.chart('pieChart' + this.id, {
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
                    data: pieData
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
        loginLoading: false,
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
            this.loginLoading = true;
            var thiz = this;
            axios.post('/session/login', this.loginData).then(function (response) {
                thiz.loginLoading = false;
                var result = response.data;
                if (result.code === 0) {
                    var data = result.data;
                    thiz.isLogin = true;
                    thiz.leftMenus = data.leftMenus;
                } else {
                    thiz.refreshVercode();
                    thiz.password = '';
                }
            });
        }
    }
}).$mount('#myApp');

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
            if (config.data === undefined) {
                config.data = {};
            }
            config.data['r'] = Math.random();//避免ie浏览器使用缓存
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
 * 控制器初始化
 * @id 必须和菜单的id对应
 * @url 模板的url
 * @initData 初始数据，模板上所需的数据都必须初始化
 * @callback 回调函数
 */
function controller(id,url,initData,callback) {
    Vue.component(id, function (resolve, reject) {
        axios.get(url).then(function (response) {
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