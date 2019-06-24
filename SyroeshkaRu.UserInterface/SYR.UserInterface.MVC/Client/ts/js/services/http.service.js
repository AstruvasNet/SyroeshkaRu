var HttpService;
(function (HttpService) {
    function HttpRequest(url, method, data, tag) {
        if (data === void 0) { data = null; }
        if (tag === void 0) { tag = null; }
        $.ajax({
            url: url,
            method: method,
            data: JSON.stringify(data),
            error: function (error) {
                console.log(error);
            },
            beforeSend: function () {
                $(tag).html();
            },
            success: function (response) { return response; }
        });
    }
    HttpService.HttpRequest = HttpRequest;
    function Test(str) {
        return str;
    }
    HttpService.Test = Test;
})(HttpService || (HttpService = {}));
//# sourceMappingURL=http.service.js.map