(function () {
    var port = "46054";
    if (browser.versions.mobile) {//判断是否是移动设备打开。browser代码在下面
        var ua = navigator.userAgent.toLowerCase();//获取判断用的对象
        if (ua.match(/MicroMessenger/i) == "micromessenger") {
            //在微信中打开
            window.WeixinJSBridge = {
                invoke: function (getBrandWCPayRequest,params,callback) {
                    var xhr = new XMLHttpRequest();
                    xhr.onreadystatechange = function () {
                        if (xhr.state == 4) {
                            callback(JSON.parse(xhr.responseText));
                        }
                    };
                    xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
                    xhr.open('post', 'http://localhost:' + port + '/Pay/JsapiPay', true);
                    var formData = new FormData();
                    for (var key in formData) {
                        if (formData.hasOwnProperty(key)) {
                            formData.append(key, formData[key]);
                        }
                    }
                    xhr.send(formData);
                }
            }
        }
    }
}());