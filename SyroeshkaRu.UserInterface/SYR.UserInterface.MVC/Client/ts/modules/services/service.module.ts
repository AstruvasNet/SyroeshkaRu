import { ModalModule as modalModule } from "../libs/modal.module";
import { AlertModule as alertModule } from "../libs/alert.module";

export module ServiceModule {

	let modal = modalModule;

	export class Options {

		url: string;
		method: string;
		data: any;
		headers: any;
		token = $("[name=\"__RequestVerificationToken\"]");

		constructor(url: string, method?: string, data?: Object) {

			this.url = url;
			this.method = method || "get";
			this.data = data || ($("form input").not(this.token) || $("form input").not($("[value=\"\"]")));
		}
	}

	export class HttpService {

		request = (options: Options, successCallback: Function, errorCallback?: Function): void => {
			var that = this;
			$.ajax({
				url: options.url,
				method: options.method,
				data: options.data,
				headers: options.headers,
				cache: false,
				success: data => {
					successCallback(data);
				},
				beforeSend: headers => {
					headers.setRequestHeader("X-XSRF-TOKEN", options.token.attr("value"));
				},
				error: data => {
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
		}

		get = (url: string, successCallback: Function, errorCallback?: Function): void => {

			this.request(new Options(url), successCallback, errorCallback);
		}

		getWithDataInput = (url: string, data: Object, successCallback: Function, errorCallback?: Function): void => {

			this.request(new Options(url, "get", data), successCallback, errorCallback);
		}

		post = (url: string, successCallback: Function, errorCallback?: Function): void => {

			this.request(new Options(url, "post"), successCallback, errorCallback);
		}

		postWithData = (url: string, data: Object, successCallback: Function, errorCallback?: Function): void => {

			this.request(new Options(url, "post", data), successCallback, errorCallback);
		}

		putWithData = (url: string, data: Object, successCallback: Function, errorCallback?: Function): void => {

			this.request(new Options(url, "put", data), successCallback, errorCallback);
		}

		deleteWithData = (url: string, data: Object, successCallback: Function, errorCallback?: Function): void => {

			this.request(new Options(url, "delete", data), successCallback, errorCallback);
		}

		showJqueryDialog = (error: any, _title?: string): void => {

			error = JSON.parse(error.responseText);
			var message = "";
			$.each(error,
					(_index, item) => {
						message += "<li>" + item + "</li>";
					});
			let alert = new alertModule.Body;
			alert.load(new alertModule.Options(null, 2, message));
			//var body = new modal.Body;
			//body.load(new modal.Options(_title, 1, messageSerialize));
		}
	}
}