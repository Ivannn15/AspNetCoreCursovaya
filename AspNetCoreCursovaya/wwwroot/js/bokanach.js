// ТУТ НЕДОПИСАННЫЙ КОД ДЛЯ МОДАЛЬНОГО ОКНА

// let btns = document.querySelectorAll("*[data-modal-btn]")

// for(let = 0; i < btns.length; i++) {
// 	btns[i].addEventListener('click', function() {
// 		let name = btns[i].getAttribute('data-modal-btn');
// 		let modal = document.querySelector("[data-modal-window='"+name+"']");
// 		modal.style.display = "block";
// 	})
//}

$('#exampleModal').on('show.bs.modal', function (event) {
	var button = $(event.relatedTarget);
	var day = button.data('day');
	var modal = $(this);
	var date = new Date();
	date.setDate(day); // Здесь мы обновляем дату, устанавливая значение дня
	var formattedDate = date.getDate() + ' ' + date.toLocaleString('en-US', { month: 'long' }) + ' ' + date.getFullYear();
	modal.find('.modal-title').text(formattedDate);
});



function readMore() {
	var dots = document.getElementById("dots");
	var more = document.getElementById("more");

	if (dots.style.display === "none") {
		dots.style.display = "inline";
		more.style.display = "none";
	}
	else {
		dots.style.display = "none";
		more.style.display = "inline";
	}
}

function readMore2() {
	var dots2 = document.getElementById("dots2");
	var more2 = document.getElementById("more2");

	if (dots2.style.display === "none") {
		dots2.style.display = "inline";
		more2.style.display = "none";
	}
	else {
		dots2.style.display = "none";
		more2.style.display = "inline";
	}
}

function readMore3() {
	var dots3 = document.getElementById("dots3");
	var more3 = document.getElementById("more3");

	if (dots3.style.display === "none") {
		dots3.style.display = "inline";
		more3.style.display = "none";
	}
	else {
		dots3.style.display = "none";
		more3.style.display = "inline";
	}
}

function readMore4() {
	var dots4 = document.getElementById("dots4");
	var more4 = document.getElementById("more4");

	if (dots4.style.display === "none") {
		dots4.style.display = "inline";
		more4.style.display = "none";
	}
	else {
		dots4.style.display = "none";
		more4.style.display = "inline";
	}
}