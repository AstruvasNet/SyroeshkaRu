var exports = {};
SystemJS.config({
	baseURL: "/dev/js/components",
	packages: {
		"/": { defaultExtension: "js" }
	}
});
System.import("admin.component.js");