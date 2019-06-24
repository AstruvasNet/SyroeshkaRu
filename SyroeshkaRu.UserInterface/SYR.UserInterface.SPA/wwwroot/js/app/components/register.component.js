"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var platform_browser_1 = require("@angular/platform-browser");
var router_1 = require("@angular/router");
var http_1 = require("@angular/http");
var auth_service_1 = require("../security/auth.service");
var RegisterViewModel_1 = require("../models/RegisterViewModel");
var RegisterComponent = /** @class */ (function () {
    function RegisterComponent(router, titleService, http, authService) {
        this.router = router;
        this.titleService = titleService;
        this.http = http;
        this.authService = authService;
    }
    RegisterComponent.prototype.ngOnInit = function () {
        this.registerViewModel = new RegisterViewModel_1.Register();
        this.errorMessage = null;
    };
    RegisterComponent.prototype.setTitle = function (newTitle) {
        this.titleService.setTitle(newTitle);
    };
    RegisterComponent.prototype.register = function (event) {
        var _this = this;
        event.preventDefault();
        var body = {
            'login': this.registerViewModel.login,
            'email': this.registerViewModel.email,
            'password': this.registerViewModel.password,
            'fullName': this.registerViewModel.fullName
        };
        this.http.post('/Account/Register', JSON.stringify(body), { headers: this.authService.jsonHeaders() })
            .subscribe(function (response) {
            if (response.status == 200) {
                _this.router.navigate(['/login']);
            }
            else {
                console.log(response);
                console.log(response.json());
                _this.errorMessage = response.json().messages[0];
            }
        }, function (error) {
            // failed; TODO: add some nice error handling
            //console.log(error);
            //console.log(error.json());
            _this.errorMessage = error.json().messages[0];
        });
    };
    RegisterComponent = __decorate([
        core_1.Component({
            selector: 'register',
            templateUrl: '/partial/registerComponent'
        }),
        __metadata("design:paramtypes", [router_1.Router, platform_browser_1.Title, http_1.Http, auth_service_1.AuthService])
    ], RegisterComponent);
    return RegisterComponent;
}());
exports.RegisterComponent = RegisterComponent;
//# sourceMappingURL=register.component.js.map