Vue.component('m17', function (resolve, reject) {
    axios.get('menus/testMenus1/pageTable2.html').then(function (response) {
        resolve({
            template: response.data,
        });
    });
});