Vue.component('m15', function (resolve, reject) {
    axios.get('menus/testMenus1/areaSelect.html').then(function (response) {
        resolve({
            template: response.data,
        });
    });
})