$(function () {
	$(".container, input").on({
		"shown.bs.dropdown": function () { this.closable = false; },
		"click": function () { this.closable = true; },
		"hide.bs.dropdown": function () { return this.closable; }
	});
	$("[ui-role=\"stop-propagation\"]").on("click",
		function(e) {
			e.stopPropagation();
		});
	$("[ui-role=\"submit\"]").on("click",
		function(e) {
			e.preventDefault();
			$.ajax({
				url: $(this).attr("href"),
				method: "POST",
				data: {
					__RequestVerificationToken: $("[name=\"__RequestVerificationToken\"]").val()
				},
				success: function(data) {
					location.href = data.request;
				}
			});
		});
	$("[type=\"button\"]").on("click",
		function () {
			location.href = $(this).attr("formaction");
		});
	$("form input").attr("autocomplete", "off");
	$("form.login-form").on("submit",
		function (e) {
			e.preventDefault();
			var $form = $(this);
			var $data = $form.serializeArray();
			$.ajax({
				url: $(this).attr("action"),
				data: $data,
				method: "POST",
				error: function (xmlHttpRequest) {
					var $response = JSON.parse(xmlHttpRequest.responseText);
					$(".dropdown-toggle").dropdown("toggle");
					$(".login-error").html("");
					$.each($response,
						function(index, item) {
							$form.children(".login-error").append("<div class=\"text-danger\">" + item + "</div>");
						});
				},
				success: function (data) {
					$.each(data,
						function(index, item) {
							location.reload();
						});
				}
			});
		});
	$(".menu-select").on("change",
		function () {
			$.ajax({
				url: "api/setStorage",
				method: "POST",
				data: {
					id: $(this).val(),
					__RequestVerificationToken: $("[name=\"__RequestVerificationToken\"]").val()
				},
				error: function (xmlHttpRequest, error, errorThrown) {
					console.log(xmlHttpRequest);
				},
				success: function (data) {
					location.reload();
				}
			});
		});
});