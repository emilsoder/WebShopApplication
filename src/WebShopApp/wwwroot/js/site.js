$(window).resize(function () {
    panelBodyFontSize.call();
    panelBodySize.call();
    tabMenuPositions.call();
});

$(document).ready(function () {
    panelBodyFontSize.call();
    panelBodySize.call();
    tabMenuPositions.call();
});

function panelBodySize() {
    if ($(window).width() < 768) {
        $('div.panel-body').css({
            'min-height': '0px',
            'max-height': 'auto',
            'height': 'auto'
        });
        $('.body-content').css({
            'padding-left': '0px',
            'padding-right': '0px'
        });

    }
    else if ($(window).width() > 768) {
        $('div.panel-body').css({
            'min-height': '110px',
            'max-height': '400px',
            'height': '100px'
        });
        $('.body-content').css({
            'padding-left': '15px',
            'padding-right': '15px'
        });
    }
};

function tabMenuPositions() {
    if ($(window).width() < 479) {
        $('.nav-tabs').css({
            'margin-top': '0px'
        });
    }
    else if ($(window).width() > 479) {
        $('.nav-tabs').css({
            'margin-top': '-49px'
        });
    };
};

function panelBodyFontSize() {
    if ($(window).width() < 680) {
        $('span.bodyText').css({
            'font-size': '12px'
        });
    }
    else if ($(window).width() > 680) {
        $('span.bodyText').css({
            'font-size': '15px'
        });
    };
};

$(document).ready(function () {
    $("#NOTLOGGEDIN").click(function () {
        $("#LoginModal").modal();
    });
});

function openLoginModal() {
    $("#LoginModal").modal();
};

$(document).ready(function () {
    $("#AcceptTermsButton").click(function () {
        $("#AcceptTermsModal").modal();
    });
});
