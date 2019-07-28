Vue.component('n11', function (resolve, reject) {
    axios.get('menus/testMenus1/testNewAlarm.html').then(function (response) {
        resolve({
            template: response.data,
        });
    });
});