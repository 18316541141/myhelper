Vue.component('m14', function (resolve, reject) {
    axios.get('menus/testMenus1/uploadFiles.html').then(function (response) {
        resolve({
            template: response.data,
        });
    });
});