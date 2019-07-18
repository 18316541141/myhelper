Vue.component('m13', function (resolve, reject) {
    axios.get('menus/testMenus1/bigImg.html').then(function (response) {
        resolve({
            template: response.data,
        });
    });
})