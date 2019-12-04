var lastModifiedDateRetrieved;
var deliveryUrl;
var itemLimit;
var headers;

$.urlParam = function(name) {
    var results = new RegExp('[\?&]' + name + '=([^&#]*)')
        .exec(window.location.search);

    return (results !== null) ? results[1] || 0 : false;
}

function checkLastModifiedDate() {
    hideAlert();
    $.ajax({
        type: "GET",
        url: deliveryUrl + "&pageSize=1",
        dataType: "json",
        headers: headers,
        success: actOnDateCheck,
        error: function() {
            showAlert("JSON call to CaaS failed - modification check");
        }
    });
}

function showAlert(message) {
    $(".alert-bottom").html(message);
    $(".alert-bottom").show();
}

function hideAlert() {
    $(".alert-bottom").hide();
}

function actOnDateCheck(data) {
    var lastModifiedDate = data._embedded.content[0]._updateDate;
    console.log("Last modification date from Heartcore: " + lastModifiedDate);
    if (lastModifiedDateRetrieved !== lastModifiedDate) {
        getNewSlides();
        lastModifiedDateRetrieved = lastModifiedDate;
    }
}

function getNewSlides() {
    $.ajax({
        type: "GET",
        url: deliveryUrl + "&pageSize=" + itemLimit,
        dataType: "json",
        headers: headers,
        success: processData,
        error: function() {
            showAlert("JSON call to CaaS failed - retrieve slides");
        }
    });
}

// Process the retrieved data. 
function processData(data) {
    console.log("Retrieved data from Heartcore");
    console.log(data);

    var slideIndex = 0;

    resetCarousel();

    data._embedded.content.map(function(item) {
        $(createSlide(item)).appendTo('.carousel-inner');
        $(createIndicator(slideIndex)).appendTo('.carousel-indicators');
        slideIndex++;
    });

    $('.carousel-item').first().addClass('active');
    $('.carousel-indicators > li').first().addClass('active');
    $('#carouselSlideContainer').carousel('cycle');
}

function resetCarousel() {
    $('.carousel-item').remove();
    $('.carousel-indicators > li').remove();
    $('#carouselSlideContainer').carousel('dispose');
}

function createSlide(item) {
    var carouselItem = '<div class="carousel-item" style="background-image: url(\'' + item.image._url + '\')">' +
        '<div class="carousel-caption d-md-block">' +
        '<h2 class="display-4">' + item.title + '</h2>' +
        item.description +
        '</div>' +
        '</div>';
    return carouselItem;
}

function createIndicator(index) {
    var indicatorItem = '<li data-target="#carouselSlideContainer" data-slide-to="' + index + '"></li>'
    return indicatorItem;
}

function setDeliveryUrl() {
    var language = $.urlParam("language");
    if (!language) {
        language = "si-LK";
    }

    itemLimit = $.urlParam("limit");
    if (!itemLimit) {
        itemLimit = 5;
    }

    deliveryUrl = "https://cdn.umbraco.io/content/type?contentType=product&page=1";

    headers = {
        "Accept-Language": language,
        "umb-project-alias": "emmanuels-tidy-otter"
    };
}

setDeliveryUrl();
checkLastModifiedDate();

var intervalMins = 2;
var intervalMilliSeconds = 1000 * 60 * intervalMins;
setInterval(checkLastModifiedDate, intervalMilliSeconds);