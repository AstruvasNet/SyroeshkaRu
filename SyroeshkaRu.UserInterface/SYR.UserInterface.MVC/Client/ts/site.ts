class Load {

	scroll(element: any): void {
		$(element).scroll(() => {
			if($(element).scrollTop()>140){
				$(".header-middle .navbar").addClass("fixed");
			}
			else if ($(element).scrollTop()<140){
				$(".header-middle .navbar").removeClass("fixed");
			}
		});
	}

	test(): any {
		alert(HttpService.Test("Ok!"));
	}
}

$(() => {
	var load = new Load();
	load.scroll(window);
	load.test();
});