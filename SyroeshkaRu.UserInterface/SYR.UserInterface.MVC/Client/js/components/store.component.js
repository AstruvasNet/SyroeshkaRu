"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var service_module_1 = require("../modules/service.module");
var Boot = /** @class */ (function () {
    function Boot() {
        this.service = service_module_1.ServiceModule;
        this.http = new this.service.HttpService;
    }
    Boot.prototype.scroll = function (element) {
        $(element).scroll(function () {
            if ($(element).scrollTop() > 140) {
                $(".header-middle .navbar").addClass("fixed");
            }
            else if ($(element).scrollTop() < 140) {
                $(".header-middle .navbar").removeClass("fixed");
            }
        });
    };
    Boot.prototype.testHttp = function () {
        this.http.post("/ap/getTest", function (data) {
            alert(data);
        });
        // request - глобальный обработчик ошибок
        // post/get - системный обработчик ошибок
        //this.http.request(new this.service.Options("url", "method", "data"),
        //	data => {
        //		alert(data);
        //	},
        //	data => {
        //		alert(data);
        //	});
    };
    return Boot;
}());
$(function () {
    var boot = new Boot();
    boot.scroll(window);
    boot.testHttp();
});
//# sourceMappingURL=store.component.js.map