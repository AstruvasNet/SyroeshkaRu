"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ServiceModule;
(function (ServiceModule) {
    var Options = /** @class */ (function () {
        function Options(url, method, data) {
            this.url = url;
            this.method = method || "get";
            this.data = data || {};
        }
        return Options;
    }());
    ServiceModule.Options = Options;
    var HttpService = /** @class */ (function () {
        function HttpService() {
            var _this = this;
            this.request = function (options, successCallback, errorCallback) {
                var that = _this;
                $.ajax({
                    url: options.url,
                    method: options.method,
                    data: options.data,
                    cache: false,
                    success: function (data) {
                        successCallback(data);
                    },
                    error: function (data) {
                        if (errorCallback) {
                            errorCallback(data);
                            return;
                        }
                        var errorTitle = "Ошибка: (" + options.url + ")";
                        var fullError = JSON.stringify(data);
                        console.log(errorTitle);
                        console.log(fullError);
                        that.showJqueryDialog(fullError, errorTitle);
                    }
                });
            };
            this.get = function (url, successCallback, errorCallback) {
                _this.request(new Options(url), successCallback, errorCallback);
            };
            this.getWithDataInput = function (url, data, successCallback, errorCallback) {
                _this.request(new Options(url, "get", data), successCallback, errorCallback);
            };
            this.post = function (url, successCallback, errorCallback) {
                _this.request(new Options(url, "post"), successCallback, errorCallback);
            };
            this.postWithData = function (url, data, successCallback, errorCallback) {
                _this.request(new Options(url, "post", data), successCallback, errorCallback);
            };
            this.putWithData = function (url, data, successCallback, errorCallback) {
                _this.request(new Options(url, "put", data), successCallback, errorCallback);
            };
            this.deleteWithData = function (url, data, successCallback, errorCallback) {
                _this.request(new Options(url, "delete", data), successCallback, errorCallback);
            };
            this.showJqueryDialog = function (message, title, height) {
                alert(title + "\n" + message);
                title = title || "Info";
                height = height || 120;
                message = message.replace("\r", "").replace("\n", "<br/>");
                $("<div title='" + title + "'><p>" + message + "</p></div>").dialog({
                    minHeight: height,
                    minWidth: 400,
                    maxHeight: 500,
                    modal: true,
                    buttons: {
                        Ok: function () { $(this).dialog("close"); }
                    }
                });
                console.error(message);
            };
        }
        return HttpService;
    }());
    ServiceModule.HttpService = HttpService;
})(ServiceModule = exports.ServiceModule || (exports.ServiceModule = {}));
//# sourceMappingURL=service.module.js.map