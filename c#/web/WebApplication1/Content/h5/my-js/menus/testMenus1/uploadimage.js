Vue.component('m12', function (resolve, reject) {
    axios.get('menus/testMenus1/uploadimage.html').then(function (response) {
        resolve({
            template: response.data,
        });
    });
});