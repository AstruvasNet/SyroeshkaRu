import { ServiceModule as service } from "./services/service.module";
import { ModalModule as modal } from "./libs/modal.module";
import { AlertModule as alert } from "./libs/alert.module";

export module StoreModule {
	export class Boot {
		private http = new service.HttpService;
		private modal = new modal.Body;
		private alert = new alert.Body;

		scroll = (element: any): void => {
			if ($(element).scrollTop() > 95) {
				$(".header-middle .navbar").addClass("fixed");
				$("main").css("margin-top", 100);
				$(".scroll-menu").css("display", "inherit");
			}
			else if ($(element).scrollTop() < 95) {
				$(".header-middle .navbar").removeClass("fixed");
				$("main").css("margin-top", 50);
				$(".scroll-menu").css("display", "none");
			}
		}

		scrollEvent = (element: any): void => {
			$(element).scroll(() => {
				this.scroll(element);
			});
		}

		modalFormEvent = (element: any): void => {
			$(element).on("click",
				e => {
					e.preventDefault();
					this.modal.load(new modal.Options(1, 1));
				});
		}

		submitEvent = (element: any): void => {
			$(element).on("click",
				e => {
					e.preventDefault();
					this.http.request(new service.Options($(element).attr("href"), "post"),
						() => {
							location.reload();
						});
				});
		}

		alertEvent = (element: any): void => {
			$(element).on("click",
				e => {
					e.preventDefault();
					this.alert.load(new alert.Options(null, 1, "Ok"));
				});
		}

		selectStorageEvent = (element: any): void => {
			$(element).on("change",
				() => {
					localStorage.setItem("storage", $(element + " option").filter(":selected").attr("value"));
					this.setStorage();
					location.reload();
				});
		}

		setStorage = (): void => {
			this.http.postWithData("/api/setStorage", { id: localStorage.getItem("storage") }, () => { });
		}

		load = (): void => {
			this.modalFormEvent("[ui-role=\"modal-form\"]");
			this.submitEvent("[ui-role=\"submit\"]");
			this.alertEvent("[ui-role=\"alert\"]");
			this.selectStorageEvent("[ui-role=\"select-storage\"]");
			this.setStorage();
			this.scroll(window);
		}
	}
}