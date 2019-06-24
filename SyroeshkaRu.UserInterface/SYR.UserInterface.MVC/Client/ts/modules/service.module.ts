export module ServiceModule {
	export class Options {
		url: string;
		method: string;
		data: any;
		constructor(url: string, method?: string, data?: Object) {
			this.url = url;
			this.method = method || "get";
			this.data = data || {};
		}
	}

	export class HttpService {

		request = (options: Options, successCallback: Function, errorCallback?: Function): void => {
			var that = this;
			$.ajax({
				url: options.url,
				method: options.method,
				data: options.data,
				cache: false,
				success: data => {
					successCallback(data);
				},
				error: data => {
					if (errorCallback) {
						errorCallback(data);
						return;
					}
					var errorTitle = "Ошибка: (" + options.url + ")";
					var fullError = JSON.stringify(data);
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

		showJqueryDialog = (message: string, title?: string, height?: number): void => {
			alert(title + "\n" + message);
		//	title = title || "Info";
		//	height = height || 120;
		//	message = message.replace("\r", "").replace("\n", "<br/>");
		//	$("<div title='" + title + "'><p>" + message + "</p></div>").dialog({
		//		minHeight: height,
		//		minWidth: 400,
		//		maxHeight: 500,
		//		modal: true,
		//		buttons: {
		//			Ok: function () { $(this).dialog('close'); }
		//		}
		//	});
		//	console.error(message);
		}
	}
}