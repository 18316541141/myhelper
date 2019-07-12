myApp.controller('big-img-ctrl', function ($scope, $timeout) {
    $timeout(function () {
        picViewer($('.xxx-table'));
    });
});