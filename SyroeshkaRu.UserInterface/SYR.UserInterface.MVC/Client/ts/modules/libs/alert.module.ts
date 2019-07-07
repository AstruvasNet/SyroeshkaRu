import {ServiceModule as serviceModule} from "../services/service.module";
import {AlertType} from "../../classes/enum.class";

export module AlertModule {
	export class Options {
		data: any;
		type: number;
		content: string;
		url: string;

		constructor(data?: any, type?: number, content?: string, url?: string) {
			this.data = data || {};
			this.type = type || AlertType.default;
			this.content = content;
			this.url = url;
		}
	}

	export class Body {
		http = new serviceModule.HttpService;

		load = (options: Options): void => {
			this.close();
			$("main").prepend("<div class=\"alert alert-" +
				AlertType[options.type] +
				" alert-dismissible fade hide rounded\" role=\"alert\" />");
			let $this = $(".alert");
			$this.append("<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Закрыть\" />");
			$this.find(".close").append("<span aria-hidden=\"true\" />");
			$this.find(".close").children("span").html("&times;");
			if (!options.url) {
				$this.prepend(options.content);
			} else {
				this.http.postWithData(options.url,
					new Options(options.data, options.type, options.content),
					(data: any) => {
						$this.prepend(data);
					});
			}
			$this.show("slow",
				() => {
					$this.removeClass("hide");
					$this.addClass("show");
				});
		};

		close = (): void => {
			$(".alert").remove();
		};
	}
}