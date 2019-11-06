$(document).ready(function () {
    var CardContainer = $("<div>"); //$(document.createElement("<div>")) would be 3 times faster but im too lazy rn
    CardContainer.addClass("card-container col-5");

    var CardVotesContainer = $("<div>");
    CardVotesContainer.addClass("col-1 votes-container");

    var UpvoteIcon = $("<i>");
    UpvoteIcon.addClass("fa fa-caret-up upvote-icon");

    var VotesCount = $("<p>");
    VotesCount.addClass("votes");
    VotesCount.append("1.2k"); //@VoteCount

    var DownVoteIcon = $("<i>");
    DownVoteIcon.addClass("fa fa-caret-down downvote-icon")

    var CardPostBody = $("<div>");
    CardPostBody.addClass("col-sm-11 cardpost-body");

    var CardMainBody = $("<div>");
    CardMainBody.addClass("card card-border-body")

    var CardBody = $("<div>");
    CardBody.addClass("card-body");

    var CardTitle = $("<h5>");
    CardTitle.addClass("card-title");
    CardTitle.append("@Model.Title"); //@Newsfeedpost.Title

    var CardText = $("<p>");
    CardText.addClass("card-text");
    CardText.append("Post description here") //@Newsfeedpost.description

    var ImgContainer = $("<div>");
    ImgContainer.addClass("text-center");

    var Img = $("<img>");
    var ImgSrc = "" //get it from newsfeedpost.ImgSrc somehow
    Img.addClass("card-img-bottom");
    Img.attr("src", ImgSrc);
    $("body").append();

    var CardFooter = $("<span>")
    CardFooter.addClass("card-nav-footer");
    //add icon tmr
    //its already 5am
});