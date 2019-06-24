namespace HttpService {
	export function HttpRequest(url: string, method: string, data: string = null, tag: string = null) {
		$.ajax({
			url: url,
			method: method,
			data: JSON.stringify(data),
			error: error => {
				console.log(error);
			},
			beforeSend: () => {
				$(tag).html();
			},
			success: response => response
		});
	}

	export function Test(str: string) {
		return str;
	}
}