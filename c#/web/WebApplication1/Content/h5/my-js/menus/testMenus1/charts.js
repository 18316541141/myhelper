controller('m312', 'menus/testMenus1/charts.html', {
    data(){ return { myPieData: [], myRowAxisTitle: [], myYAxisTitle: [], myHistogramData: [] }},
    mounted() {
        this.myPieData = [{
            name: '乐车邦',
            y: 61.41
        }, {
            name: '好车帮',
            y: 11.84
        }, {
            name: '豪车邦',
            y: 10.85
        }, {
            name: '4S店',
            y: 4.67
        }, {
            name: '5S店',
            y: 4.18
        }, {
            name: '美国交警网',
            y: 7.05
        }];
        this.myHistogramData = [{
            name: '乐车邦',
            data: [49.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4]
        }, {
            name: '好车帮',
            data: [83.6, 78.8, 98.5, 93.4, 106.0, 84.5, 105.0, 104.3, 91.2, 83.5, 106.6, 92.3]
        }, {
            name: '中国人寿',
            data: [48.9, 38.8, 39.3, 41.4, 47.0, 48.3, 59.0, 59.6, 52.4, 65.2, 59.3, 51.2]
        }, {
            name: '法国人寿',
            data: [42.4, 33.2, 34.5, 39.7, 52.6, 75.5, 57.4, 60.4, 47.6, 39.1, 46.8, 51.1]
        }];
    }
});