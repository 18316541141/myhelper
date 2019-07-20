controller('m15','menus/testMenus1/areaSelect.html',{
    data() { return { form: { myProvince: '', myCity: '', myCounty: '' } }; },
    mounted() {
        var thiz = this;
        setTimeout(function () {
            debugger;
            thiz.form.myProvince = '19';
            thiz.form.myCity = '202';
            thiz.form.myCounty = '1787';
        },1000);
    }
});

