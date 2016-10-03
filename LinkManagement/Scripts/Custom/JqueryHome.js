$(document).ready(function () {
    //top nav bar
    $('.top-nav-bar').autoHidingNavbar();

    //fixed side list div relative to tagline
    $(document).scroll(function () {
        if ($(document).scrollTop() < 130) {
            $('#list-div > ul').css("top", 180 - $(document).scrollTop());
        }
        
        else {
            $('#list-div > ul').css("top", 10);
        }
       
    });

    //copy the link to the clipboard
    var clipboard = new Clipboard('.copy-link');

    clipboard.on('success', function (e) {
        console.log(e);
        
    });

    clipboard.on('error', function (e) {
        console.log(e);
        
    });

    //pop over on hovering on sign up
    $('#sign-up').popover({ title: "Hey! You will be our 5th User", content: "You can create your own link repository and can contribute to our Tutorial", trigger: "hover", placement: "bottom" });

    // copy link
    //$(document).on("click", ".copy-link-parent", function () {
    //    $(this).children("a").popover("show");
    //    setTimeout(function () { $('.popover').fadeOut('slow') }, 2000);
    //});
 
});
