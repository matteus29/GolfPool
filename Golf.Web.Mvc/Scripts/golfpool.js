$(document).ready(function () {
  $(".leaderboard_row").mouseenter(
    function () {
      var user = $(this).children(".username").text();
      $(".username").each(function () {
        if ($(this).text() == user) {
          $(this).parent().addClass("highlight_row");
        }
      })
    })
  .mouseleave(
    function () {
      var user = $(this).children(".username").text();
      $(".username").each(function () {
        $(this).parent().removeClass("highlight_row");
      })
    });
})