﻿

function openNav() {
    document.getElementById("sidenav").style.width = "80%";
    document.getElementById("main").style.marginLeft = "80%";
    document.getElementById('nav-gliph').style.display = "none";
}

function closeNav() {
    document.getElementById("sidenav").style.width = "0";
    document.getElementById("main").style.marginLeft = "0";
    document.getElementById('nav-gliph').style.display = "initial";
}

function clickHandler() { // declare a function that updates the state
    document.getElementById("sidenav").style.width = "0";
    document.getElementById("main").style.marginLeft = "0";
    document.getElementById('nav-gliph').style.display = "initial";
}
