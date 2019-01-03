/*Search*/

$(document).ready(function () {
    $("input#SearchInput").keyup(function () {
        var SearchInput = $("input#SearchInput").val();

        if (SearchInput == "" || SearchInput == " ")
            return false;

        $.post("Profile/Search", { SearchInput: SearchInput }, function (data) {
            if (data.length > 0) {
                $("input#SearchInput").append("<li class=close>x</li>");

                for (var item in data) {
                    $("input#SearchInput").append('<li class=searchli><a href="/'+item.Id+ '">'+ item.FirstName +'</a></li>');
                }
            }
        });
    });

    $("body").on("click",);

}); // End ready
