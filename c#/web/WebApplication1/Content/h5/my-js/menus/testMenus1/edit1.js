Vue.component('edit1', function (resolve, reject) {
    axios.get('menus/testMenus1/edit1.html').then(function (response) {
        resolve({
            template: response.data,
        });
    });
})