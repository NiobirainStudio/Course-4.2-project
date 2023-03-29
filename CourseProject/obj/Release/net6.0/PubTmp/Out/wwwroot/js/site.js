// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const ratingList = ["Good", "Bad"];

function Rate(rate) {
    myRating = rate;

    document.getElementById(ratingList[rate]).style.color = "#0fa2f7";
    document.getElementById(ratingList[Math.abs(rate - 1)]).style.removeProperty('color');

    $.ajax({
        type: 'GET',
        url: '/Home/ChangeRating',
        data: { bookId: myBookId, newRating: myRating },
        cache: false,
        success: function (result) {
            document.getElementById('ratingBlock').innerHTML = (result[0] * 10 / (result[0] + result[1])) + "/10";

            document.getElementById('ratingBlockEnd').innerHTML = "From: " + (result[0] + result[1]) + " ratings";
        }
    });
}

document.onkeydown = function (e) {
    switch (e.which) {
        case 37: // left
            document.getElementById('prev').click();
            break;

        case 39: // right
            document.getElementById('next').click();
            break;
    }
};