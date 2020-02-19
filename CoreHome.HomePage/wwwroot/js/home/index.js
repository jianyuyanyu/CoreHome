﻿
function addLinkTarget() {
    document.querySelectorAll(".fh5co-blog").forEach(ele => {
        ele.querySelectorAll("a").forEach(link => {
            link.setAttribute("target", "_blank");
        });
    });
}

//============MoveOperation================

function moveToDC() {
    var current = document.scrollingElement.scrollTop;
    current += 10;
    document.scrollingElement.scrollTop = current;
    timer = setTimeout(moveToDC, 0.1);

    if (document.scrollingElement.scrollTop > 810) {
        clearTimeout(timer);
    }
}

//=============Initializa================

function init() {
    player.addEventListener('ended', function () {
        setTimeout(() => {
            document.getElementById("player").play();
        }, 1000);
    });
    addLinkTarget();
}

init();