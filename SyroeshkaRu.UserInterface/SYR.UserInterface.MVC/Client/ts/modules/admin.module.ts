//import { ServiceModule as service } from "./services/service.module";
import { ModalModule as modal } from "./libs/modal.module";

export module AdminModule {
	export class Boot {
		//private http = new service.HttpService;
		private modal = new modal.Body;

		removeEvent = (element: any): void => {
			$(element).on("click",
				e => {
					e.preventDefault();
					this.modal.load(new modal.Options(0, 1, 
						{
							id: $(element).attr("href").split("/")[3],
							type: $("#action").attr("value")
						}));
				});
		};

		load = (): void => {
			this.removeEvent("[ui-role=\"remove\"]");
		};
	}
}