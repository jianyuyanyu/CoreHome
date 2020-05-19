//===================Adapter=======================
var isMobile = {
	Android: function () {
		return navigator.userAgent.match(/Android/i);
	},
	BlackBerry: function () {
		return navigator.userAgent.match(/BlackBerry/i);
	},
	iOS: function () {
		return navigator.userAgent.match(/iPhone|iPad|iPod/i);
	},
	Opera: function () {
		return navigator.userAgent.match(/Opera Mini/i);
	},
	Windows: function () {
		return navigator.userAgent.match(/IEMobile/i);
	},
	any: function () {
		return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
	}
};

function fullHeight() {

	if (!isMobile.any()) {
		var list = document.getElementsByClassName("js-fullheight");

		for (var i = 0; i < list.length; i++) {
			list[i].style.height = window.innerHeight + "px";
		}
	}
}

function MoveTop()
{
	window.scrollTo(0, 0);
}

//=============GetVerfyCode=============

function getVerfyCode(img) {
	img.src = "/Service/VerificationCode?" + Math.random();
}

//=============Initializa================

function init() {
	window.onscroll = () => {
		if (window.scrollY > 200) {
			document.querySelector(".js-top").classList.add("active");
		}
		else {
			document.querySelector(".js-top").classList.remove("active");
        }
    }
	window.onresize = fullHeight;
	fullHeight();
}

init();