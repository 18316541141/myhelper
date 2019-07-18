Vue.component('m11', function (resolve, reject) {
    axios.get('menus/testMenus1/treeForm.html').then(function (response) {
        resolve({
            template: response.data,
        });
    });
});