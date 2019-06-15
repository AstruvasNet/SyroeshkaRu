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
var http_1 = require("@angular/http");
var AuthService = /** @class */ (function () {
    function AuthService() {
    }
    // for requesting secure data using json
    AuthService.prototype.authJsonHeaders = function () {
        var header = new http_1.Headers();
        header.append('Content-Type', 'application/json');
        header.append('Accept', 'application/json');
        header.append('Authorization', 'Bearer ' + localStorage.getItem('access_token'));
        return header;
    };
    // for requesting secure data from a form post
    AuthService.prototype.authFormHeaders = function () {
        var header = new http_1.Headers();
        header.append('Content-Type', 'application/x-www-form-urlencoded');
        header.append('Accept', 'application/json');
        header.append('Authorization', 'Bearer ' + localStorage.getItem('access_token'));
        return header;
    };
    // for requesting unsecured data using json
    AuthService.prototype.jsonHeaders = function () {
        var header = new http_1.Headers();
        header.append('Content-Type', 'application/json');
        header.append('Accept', 'application/json');
        return header;
    };
    // for requesting unsecured data using form post
    AuthService.prototype.contentHeaders = function () {
        var header = new http_1.Headers();
        header.append('Content-Type', 'application/x-www-form-urlencoded');
        header.append('Accept', 'application/json');
        return header;
    };
    // After a successful login, save token data into session storage
    AuthService.prototype.login = function (responseData) {
        var access_token = responseData.access_token;
        var refresh_token = responseData.refresh_token;
        var expires_in = responseData.expires_in;
        var now = new Date();
        now.setSeconds(now.getSeconds() + expires_in);
        localStorage.setItem('access_token', access_token);
        localStorage.setItem('refresh_token', refresh_token);
        localStorage.setItem('expires_in', now.toString());
    };
    // called when logging out user; clears tokens from localStorage
    AuthService.prototype.logout = function () {
        localStorage.removeItem('access_token');
        localStorage.removeItem('refresh_token');
        localStorage.removeItem('expires_in');
    };
    AuthService.prototype.isLoggedIn = function () {
        var at = localStorage.getItem('access_token');
        if (at === null)
            return false;
        var expireStr = localStorage.getItem('expires_in');
        if (expireStr === null)
            return false;
        var expire = new Date(expireStr);
        var now = new Date();
        if (now >= expire)
            return false;
        return true;
    };
    AuthService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [])
    ], AuthService);
    return AuthService;
}());
exports.AuthService = AuthService;
//# sourceMappingURL=auth.service.js.map