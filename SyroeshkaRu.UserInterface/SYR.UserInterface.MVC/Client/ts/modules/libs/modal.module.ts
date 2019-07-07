import { ServiceModule as serviceModule } from "../services/service.module";
import { ModalType, ModalSize } from "../../classes/enum.class";

export module ModalModule {
	export class Options {
		type: number;
		size: number;
		data: any;

		constructor(type?: number, size?: number, data?: any) {
			this.type = type || 0;
			this.size = size || 0;
			this.data = data || {};
		}
	}

	export class Body {
		http = new serviceModule.HttpService;

		load = (options: Options): void => {
			this.close();
			$("body").append("<div class=\"modal fade\" role=\"dialog\" />");
			let $this = $(".modal");
			$this.append("<div class=\"modal-dialog modal-" + ModalSize[options.size] + "\" />");
			$this.children(".modal-dialog").append("<div class=\"modal-content\" role=\"document\"/>");

			this.http.postWithData("/partial/get" + ModalType[options.type] + "Form",
				options.data,
				(data: any) => {
					$this.find(".modal-content").html(data);
					this.submitEvent($this);

					$this.find(".modal-header")
						.append(
							"<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Закрыть\" />");
					$this.find(".close").append("<span aria-hidden=\"true\" />");
					$this.find(".close span").html("&times;");
				},
				() => {
					alert("Не удалось загрузить шаблон окна");
					this.close();
				});

			$this.modal({
				backdrop: "static"
			});
		};

		submitEvent = (element: any): void => {
			let $this = $(element).find("form");
			// noinspection JSDeprecatedSymbols
			$this.submit(
				e => {
					e.preventDefault();
					// noinspection SpellCheckingInspection
					this.http.postWithData($this.attr("action"), $this.serialize(),
						() => {
							location.reload();
						},
						(error: any) => {
							$(".errors").remove();
							if (error) {
								error = JSON.parse(error.responseText);
								$this.find(".modal-body").prepend("<div class=\"errors\" />");
								$.each(error,
									(_index, item) => {
										$(".errors").prepend(item + "</br>")
											.addClass("text-danger-o col-form-label-sm");
									});
							}
						});
				});
		};

		close = (): void => {
			$(".modal").remove(".modal");
			$(".modal-backdrop").remove();
		}
	}
}