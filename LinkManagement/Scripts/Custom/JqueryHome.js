﻿$(document).ready(function () {
    //top nav bar
    $('.top-nav-bar').autoHidingNavbar();

    //initialize popover
    $('[data-toggle="popover"]').popover();

    //fixed side list div relative to tagline
    $(document).scroll(function () {
        if ($(document).scrollTop() < 180) {
            $('#list-div > ul').css("top", 220 - $(document).scrollTop());
        }
        
        else {
            $('#list-div > ul').css("top", 10);
        }

        $("#sub-topic-list > div").each(function () {

            var docViewTop = $(window).scrollTop();
           
            var elementTop = $(this).offset().top;
            var elementBottom = elementTop + $(this).height();

          
            if ((elementTop >= docViewTop) && (elementTop <= docViewTop + 100)) {

                var id = $(this).attr("id").substring(8);
                $("#list-div > ul >li").removeClass("active");
                $("[list-id=" + id + "]").addClass("active");
            }

        });
       
    });

    //copy the link to the clipboard
    var clipboard = new Clipboard('.copy-link');

    clipboard.on('success', function (e) {
        console.log(e);
        
    });

    clipboard.on('error', function (e) {
        console.log(e);
        
    });

    // copy link
    $(document).on("click", ".copy-link-parent", function () {
        $(this).children("a").popover("show");
        setTimeout(function () { $('.popover').fadeOut('slow') }, 2000);
    });
   
});
