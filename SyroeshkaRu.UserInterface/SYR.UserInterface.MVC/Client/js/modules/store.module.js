"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var service_module_1 = require("./services/service.module");
var modal_module_1 = require("./libs/modal.module");
var alert_module_1 = require("./libs/alert.module");
var StoreModule;
(function (StoreModule) {
    var Boot = /** @class */ (function () {
        function Boot() {
            var _this = this;
            this.http = new service_module_1.ServiceModule.HttpService;
            this.modal = new modal_module_1.ModalModule.Body;
            this.alert = new alert_module_1.AlertModule.Body;
            this.scroll = function (element) {
                if ($(element).scrollTop() > 95) {
                    $(".header-middle .navbar").addClass("fixed");
                    $("main").css("margin-top", 100);
                    $(".scroll-menu").css("display", "inherit");
                }
                else if ($(element).scrollTop() < 95) {
                    $(".header-middle .navbar").removeClass("fixed");
                    $("main").css("margin-top", 50);
                    $(".scroll-menu").css("display", "none");
                }
            };
            this.scrollEvent = function (element) {
                $(element).scroll(function () {
                    _this.scroll(element);
                });
            };
            this.modalFormEvent = function (element) {
                $(element).on("click", function (e) {
                    e.preventDefault();
                    _this.modal.load(new modal_module_1.ModalModule.Options(1, 1));
                });
            };
            this.submitEvent = function (element) {
                $(element).on("click", function (e) {
                    e.preventDefault();
                    _this.http.request(new service_module_1.ServiceModule.Options($(element).attr("href"), "post"), function () {
                        location.reload();
                    });
                });
            };
            this.alertEvent = function (element) {
                $(element).on("click", function (e) {
                    e.preventDefault();
                    _this.alert.load(new alert_module_1.AlertModule.Options(null, 1, "Ok"));
                });
            };
            this.selectStorageEvent = function (element) {
                $(element).on("change", function () {
                    localStorage.setItem("storage", $(element + " option").filter(":selected").attr("value"));
                    _this.setStorage();
                    location.reload();
                });
            };
            this.setStorage = function () {
                _this.http.postWithData("/api/setStorage", { id: localStorage.getItem("storage") }, function () { });
            };
            this.load = function () {
                _this.modalFormEvent("[ui-role=\"modal-form\"]");
                _this.submitEvent("[ui-role=\"submit\"]");
                _this.alertEvent("[ui-role=\"alert\"]");
                _this.selectStorageEvent("[ui-role=\"select-storage\"]");
                _this.setStorage();
                _this.scroll(window);
            };
        }
        return Boot;
    }());
    StoreModule.Boot = Boot;
})(StoreModule = exports.StoreModule || (exports.StoreModule = {}));
//# sourceMappingURL=store.module.js.map