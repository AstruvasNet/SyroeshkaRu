"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var modal_module_1 = require("../libs/modal.module");
var alert_module_1 = require("../libs/alert.module");
var ServiceModule;
(function (ServiceModule) {
    var modal = modal_module_1.ModalModule;
    var Options = /** @class */ (function () {
        function Options(url, method, data) {
            this.token = $("[name=\"__RequestVerificationToken\"]");
            this.url = url;
            this.method = method || "get";
            this.data = data || ($("form input").not(this.token) || $("form input").not($("[value=\"\"]")));
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
                    headers: options.headers,
                    cache: false,
                    success: function (data) {
                        successCallback(data);
                    },
                    beforeSend: function (headers) {
                        headers.setRequestHeader("X-XSRF-TOKEN", options.token.attr("value"));
                    },
                    error: function (data) {
                        if (errorCallback) {
                            errorCallback(data);
                            return;
                        }
                        var errorTitle = "Ошибка";
                        var fullError = data;
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
            this.showJqueryDialog = function (error, _title) {
                error = JSON.parse(error.responseText);
                var message = "";
                $.each(error, function (_index, item) {
                    message += "<li>" + item + "</li>";
                });
                var alert = new alert_module_1.AlertModule.Body;
                alert.load(new alert_module_1.AlertModule.Options(null, 2, message));
                //var body = new modal.Body;
                //body.load(new modal.Options(_title, 1, messageSerialize));
            };
        }
        return HttpService;
    }());
    ServiceModule.HttpService = HttpService;
})(ServiceModule = exports.ServiceModule || (exports.ServiceModule = {}));
//# sourceMappingURL=service.module.js.map