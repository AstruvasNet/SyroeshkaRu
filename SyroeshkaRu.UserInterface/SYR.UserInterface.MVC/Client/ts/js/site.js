var Load = /** @class */ (function () {
    function Load() {
    }
    Load.prototype.scroll = function (element) {
        $(element).scroll(function () {
            if ($(element).scrollTop() > 140) {
                $(".header-middle .navbar").addClass("fixed");
            }
            else if ($(element).scrollTop() < 140) {
                $(".header-middle .navbar").removeClass("fixed");
            }
        });
    };
    Load.prototype.test = function () {
        alert(HttpService.Test("Ok!"));
    };
    return Load;
}());
$(function () {
    var load = new Load();
    load.scroll(window);
    load.test();
});
//# sourceMappingURL=site.js.map