function Alert(data, type) {
	$("main").prepend("<div class=\"alert alert-" + type + " alert-dismissible\" role=\"alert\"></div>");
	var $this = $("[role=\"alert\"]");
	$this.append("<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Закрыть\"></button>");
	$(".close").append("<span aria-hidden=\"true\">&times;</span>");
	$this.append("<button type=\"button\" class=\"no-hidden\" aria-label=\"Закрепить\"></button>");
	$(".no-hidden").append("<span aria-hidden=\"true\" class=\"fa fa-unlock-alt\"></span>");
	$this.prepend(data);
	$(".close").on("click",
		function() {
			sessionStorage.clear();
		});
	$(".no-hidden").on("click",
		function(e) {
			e.preventDefault();
			if ($this.hasClass("lock")) {
				$this.removeClass("lock");
				$(".no-hidden > span").removeClass("fa-lock");
				$(".no-hidden > span").addClass("fa-unlock-alt");
				window.setTimeout(function () {
					$this.not(".lock").alert("close");
					sessionStorage.clear();
				}, 3000);
				Timer(3);
			} else {
				$(".timer").remove();
				$this.addClass("lock");
				$(".no-hidden > span").removeClass("fa-unlock-alt");
				$(".no-hidden > span").addClass("fa-lock");
			}
		});
	window.setTimeout(function () {
		$this.not(".lock").alert("close");
		sessionStorage.clear();
	}, 5000);
	Timer(5);
}

function Modal(url, title, data) {
	ModalClose();
	$("body").append("<div id=\"ajax-modal\" class=\"modal fade\" role=\"dialog\" />");
	var $modal = $('#ajax-modal');
	if (url === null) {
		$("#ajax-modal").append("<div class=\"modal-dialog modal-dialog-centered\" />");
		$(".modal-dialog").append("<div class=\"modal-content\" role=\"document\"/>");
		$(".modal-content").append("<div class=\"modal-header\" />");
		$(".modal-header").append("<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Закрыть\" />");
		$(".close").append("<span aria-hidden=\"true\" />");
		$(".close span").html("&times;");
		if (title !== null) {
			$(".modal-header").append("<h4 class=\"modal-title\" />");
			$(".modal-title").text(title);
		}
		$(".modal-content").append("<div class=\"modal-body\" />");
		$(".modal-content").append("<div class=\"modal-footer\" />");
		$(".modal-body").html(data);
		$modal.modal({
			backdrop: "static"
		});
	} else {
		$("body").modalmanager('loading');
		$modal.load(url,
			"",
			function () {
				$modal.append("<div class=\"modal-footer\" />");
				$modal.modal();
			});
	}
	//GroupButtons(type, ".modal-footer", data);
}

function ModalClose() {
	$(".modal").remove(".modal");
	$(".modal-backdrop").remove();
}

function GroupButtons(type, selector, data) {
	switch (type) {
	case "message":
		$(selector).append("<input type=\"button\" class=\"btn btn-success ok\" value=\"Ok\" />");
		break;
		case "confirm":
			$(selector).prepend("<input type=\"hidden\" value=\"" + data + "\"/>");
			$(selector).append("<input type=\"button\" class=\"btn btn-success btn-xs\" ui-role=\"modal-submit\" value=\"Подтверждение\" />");
			$(selector).append("<input type=\"button\" class=\"btn btn-danger btn-xs\" data-dismiss=\"modal\" value=\"Отмена\" />");
		break;
	}
}

function Timer(interval) {
	$("[role=\"alert\"]").prepend("<button type=\"button\" class=\"timer\"></div>");
	window.setInterval(function () {
		if (interval > 0) {
			interval--;
			$(".timer").text(interval);
		} else {
			clearInterval(interval);
		}
	}, 1000);
}

function HttpPut(url, data) {
	$.ajax({
		url: url,
		data: data,
		method: "PUT",
		error: function (xmlHttpRequest) {
			var response = JSON.parse(xmlHttpRequest.responseText);
			$(".alert").remove();
			$("main").prepend(Alert("", "danger"));
			$.each(response, function (index, item) {
				$(".alert-danger").append("<div>" + item + "</div>");
			});
		},
		success: function (data) {
			$(".alert").remove();
			sessionStorage.setItem("message", data);
			sessionStorage.setItem("type", "success");
			location.href = "/cp/root/storages";
		}
	});
}

function HttpDelete(url, data) {
	ModalClose();
	$.ajax({
		url: url,
		data: data,
		method: "DELETE",
		error: function (xmlHttpRequest) {
			var response = JSON.parse(xmlHttpRequest.responseText);
			$(".alert").remove();
			$("main").prepend(Alert("", "danger"));
			$.each(response, function (index, item) {
				$(".alert-danger").append("<div>" + item + "</div>");
			});
		},
		success: function (data) {
			$(".alert").remove();
			sessionStorage.setItem("message", data);
			sessionStorage.setItem("type", "success");
			location.href = "/cp/root/storages";
		}
	});
}

function HttpGetModal(url, data) {
	$.ajax({
		url: url,
		data: data,
		method: "GET",
		error: function (xmlHttpRequest) {
			var response = JSON.parse(xmlHttpRequest.responseText);
			$(".alert").remove();
			$("main").prepend(Alert("", "danger"));
			$.each(response, function (index, item) {
				$(".alert-danger").append("<div>" + item + "</div>");
			});
		},
		success: function (data) {
			$(".alert").remove();
			Modal(null, $(data).find("#Title").val(), data);
			$("form").on("submit",
				function (e) {
					e.preventDefault();
					HttpDelete($(data).attr("action"), $(this).serializeArray());
				});
		}
	});
}

function HttpGet(url, data, element) {
	$.ajax({
		url: url,
		data: data,
		method: "GET",
		error: function (xmlHttpRequest) {
			alert(xmlHttpRequest.responseText);
		},
		beforeSend: function () {
			element.html("<i>Loading...</i>");
		},
		success: function (request) {
			if (data === null) {
				element.html($(request).html());
			} else {
				element.html($(request).html());
			}
		}
	});
}