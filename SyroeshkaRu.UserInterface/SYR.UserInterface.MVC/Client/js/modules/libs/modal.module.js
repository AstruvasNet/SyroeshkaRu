"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var service_module_1 = require("../services/service.module");
var enum_class_1 = require("../../classes/enum.class");
var ModalModule;
(function (ModalModule) {
    var Options = /** @class */ (function () {
        function Options(type, size, data) {
            this.type = type || 0;
            this.size = size || 0;
            this.data = data || {};
        }
        return Options;
    }());
    ModalModule.Options = Options;
    var Body = /** @class */ (function () {
        function Body() {
            var _this = this;
            this.http = new service_module_1.ServiceModule.HttpService;
            this.load = function (options) {
                _this.close();
                $("body").append("<div class=\"modal fade\" role=\"dialog\" />");
                var $this = $(".modal");
                $this.append("<div class=\"modal-dialog modal-" + enum_class_1.ModalSize[options.size] + "\" />");
                $this.children(".modal-dialog").append("<div class=\"modal-content\" role=\"document\"/>");
                _this.http.postWithData("/partial/get" + enum_class_1.ModalType[options.type] + "Form", { type: options.type }, function (data) {
                    $this.find(".modal-content").html(data);
                    _this.submitEvent($this);
                    $this.find(".modal-header")
                        .append("<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Закрыть\" />");
                    $this.find(".close").append("<span aria-hidden=\"true\" />");
                    $this.find(".close span").html("&times;");
                }, function () {
                    alert("Не удалось загрузить шаблон окна");
                    _this.close();
                });
                $this.modal({
                    backdrop: "static"
                });
            };
            this.submitEvent = function (element) {
                var $this = $(element).find("form");
                $this.submit(function (e) {
                    e.preventDefault();
                    _this.http.postWithData($this.attr("action"), $this.serialize(), function () {
                        location.reload();
                    }, function (error) {
                        $(".errors").remove();
                        error = JSON.parse(error.responseText);
                        $this.find(".modal-body").prepend("<div class=\"errors\" />");
                        $.each(error, function (_index, item) {
                            $(".errors").prepend(item + "</br>").addClass("text-danger-o col-form-label-sm");
                        });
                    });
                });
            };
            this.close = function () {
                $(".modal").remove(".modal");
                $(".modal-backdrop").remove();
            };
        }
        return Body;
    }());
    ModalModule.Body = Body;
})(ModalModule = exports.ModalModule || (exports.ModalModule = {}));
//# sourceMappingURL=modal.module.js.map