import { StoreModule as store } from "../modules/store.module";

var boot = new store.Boot();

$(() => {

	boot.scrollEvent(window);
	boot.load();
});
