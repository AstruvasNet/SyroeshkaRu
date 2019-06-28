"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var store_module_1 = require("../modules/store.module");
var boot = new store_module_1.StoreModule.Boot();
$(function () {
    boot.scrollEvent(window);
    boot.load();
});
//# sourceMappingURL=store.component.js.map