Vue.component('m18', function (resolve, reject) {
    axios.get('menus/testMenus1/uploadExcel.html').then(function (response) {
        resolve({
            template: response.data,
        });
    });
});