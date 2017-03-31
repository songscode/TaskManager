//$.post扩展写法
$.myPost = function (url, data, fn) {
    if (typeof (data) == "function") {
        fn = data;
        data = {};
    }
    var index = layer.load(2);
    $.post(url, data, fn).error(function (obj) {
        layer.alert(obj.statusText, { icon: 5 });
    }).complete(function () {
        layer.close(index);
    });
}

$.myGet = function (url, data, fn) {
    if (typeof (data) == "function") {
        fn = data;
        data = {};
    }
    var index = layer.load(2);
    $.get(url, data, fn).error(function (obj) {
        layer.alert(obj.statusText, { icon: 5 });
    }).complete(function () {
        layer.close(index);
    });
}