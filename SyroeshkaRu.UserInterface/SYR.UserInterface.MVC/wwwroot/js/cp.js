$(function() {
	//$("[type=\"checkbox\"]").on("change", function()
	//{
	//	if($(this).is(':checked')) {
	//		$(this).attr("checked", "checked");
	//	} else {
	//		$(this).attr("checked", "checked");
	//	}
	//});
	if ($("[type=\"checkbox\"]").val() === "on") {
		$("[type=\"checkbox\"]").val(true);
	}
	if (sessionStorage.getItem("message"))
		Alert(sessionStorage.getItem("message"), sessionStorage.getItem("type"));

	var $firstMenu;
	if (location.href.split("/cp")[1] !== undefined)
		$firstMenu = location.href.split("/cp/")[1];

	var menu = location.href;
	var cur_url = '/cp/' + menu.split('/').pop();
	$('.nav li').each(function () {
		var link = $(this).find('a').attr('href');
		if (cur_url === link) {
			$(this).addClass('active');
		}
	});

	$(".list-group-item").each(function() {
		var link = $(this).find("a").attr("href");
		if (cur_url.split("/")[2] === link) {
			$(this).addClass("active");
		}
	});

		$.ajax({
		url: "/partial/menu",
		method: "POST",
		data: {
// ReSharper disable once UsageOfPossiblyUnassignedValue
			page: $firstMenu.split("/")[0]
		},
		error: function(xmlHttpRequest) {
			console.log(xmlHttpRequest);
		},
		beforeSend: function() {
			$("[ui-role=\"left-menu-container\"]").html("<br><b class=\"text-success fa fa-spinner fa-pulse fa-2x fa-fw\"></b>");
		},
		success: function(data) {
			$("[ui-role=\"left-menu-container\"]").html(data);
		}
	});

	$("[ui-role=\"submit\"]").on("click",
		function (e) {
			e.preventDefault();
			$.ajax({
				url: $(this).attr("href"),
				method: "POST",
				data: {
					__RequestVerificationToken: $("[name=\"__RequestVerificationToken\"]").val()
				},
				success: function (data) {
					location.href = data.request;
				}
			});
		});

	$(".nav.navbar-nav > li").removeClass("list-group-item");

	$("[ui-role=\"add-partial\"]").on("click",
		function(e) {
			e.preventDefault();
			$("table > tbody").append("<tr class=\"add\"></tr>");
			HttpGet($(this).attr("href"), null, $(".add"));
		});

	$("[ui-role=\"edit-partial\"]").on("click",
		function (e) {
			e.preventDefault();
			HttpGet($(this).attr("href"), $(this).attr("id"), $(this).parent().parent());
		});

	$("[data-toggle=\"modal\"]").on("click",
		function(e) {
			e.preventDefault();
			HttpGetModal($(this).attr("href"), $(this).attr("id"));
		});

	$("form").on("submit",
		function(e) {
			e.preventDefault();
			//console.log($(this).serializeArray())
			HttpPut($(this).attr("action"), $(this).serializeArray());
		});
	$("[ui-role=\"remove\"]").on("click",
		function(e) {
			e.preventDefault();
			HttpGetModal($(this).attr("href"), null);
		});
});