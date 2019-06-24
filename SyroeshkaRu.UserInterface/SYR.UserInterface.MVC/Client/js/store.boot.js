SystemJS.config({
	baseURL: "dev/js/components",
	packages: {
		"/": { defaultExtension: "js" }
	}
});
System.import("store.component.js");