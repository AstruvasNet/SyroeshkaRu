import { AdminModule as admin } from "../modules/admin.module";

var boot = new admin.Boot;

$(() => {
	boot.load();
	//$("[data-toggle=\"tooltip\"]").tooltip();
});