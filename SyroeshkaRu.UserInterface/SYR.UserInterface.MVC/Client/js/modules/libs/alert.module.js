"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var service_module_1 = require("../services/service.module");
var enum_class_1 = require("../../classes/enum.class");
var AlertModule;
(function (AlertModule) {
    var Options = /** @class */ (function () {
        function Options(data, type, content, url) {
            this.data = data || {};
            this.type = type || enum_class_1.AlertType.default;
            this.content = content;
            this.url = url;
        }
        return Options;
    }());
    AlertModule.Options = Options;
    var Body = /** @class */ (function () {
        function Body() {
            var _this = this;
            this.http = new service_module_1.ServiceModule.HttpService;
            this.load = function (options) {
                _this.close();
                $("main").prepend("<div class=\"alert alert-" +
                    enum_class_1.AlertType[options.type] +
                    " alert-dismissible fade hide rounded\" role=\"alert\" />");
                var $this = $(".alert");
                $this.append("<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Закрыть\" />");
                $this.find(".close").append("<span aria-hidden=\"true\" />");
                $this.find(".close").children("span").html("&times;");
                if (!options.url) {
                    $this.prepend(options.content);
                }
                else {
                    _this.http.postWithData(options.url, new Options(options.data, options.type, options.content), function (data) {
                        $this.prepend(data);
                    });
                }
                $this.show("slow", function () {
                    $this.removeClass("hide");
                    $this.addClass("show");
                });
            };
            this.close = function () {
                $(".alert").remove();
            };
        }
        return Body;
    }());
    AlertModule.Body = Body;
})(AlertModule = exports.AlertModule || (exports.AlertModule = {}));
//# sourceMappingURL=alert.module.js.map