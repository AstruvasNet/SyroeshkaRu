import { ServiceModule as httpModule } from "../modules/service.module";

class Boot {

	public service = httpModule;
	public http = new this.service.HttpService;

	scroll(element: any): void {
		$(element).scroll(() => {
			if ($(element).scrollTop() > 140) {
				$(".header-middle .navbar").addClass("fixed");
			}
			else if ($(element).scrollTop() < 140) {
				$(".header-middle .navbar").removeClass("fixed");
			}
		});
	}

	testHttp(): void {
		this.http.post("/ap/getTest", data => {
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
	}
}

$(() => {
	var boot = new Boot();

	boot.scroll(window);

	boot.testHttp();
});
