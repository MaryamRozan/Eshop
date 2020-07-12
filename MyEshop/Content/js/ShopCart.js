
    $(function () {
        ShowShopCart();
    });

    function ShowShopCart() {
        $.get("/APi/ShopCart/", function (res) {
            $("#ShopCart").html(res);
        });
    }
function AddToshopCart(id) {
    $.get("/Api/ShopCart/" + id, function (res) {
        $("#ShopCart").html(res);
        UpdateShopCart();
    })
}

function UpdateShopCart() {
    $("#Basket").load("/Shop/ShowBasket/").fadeOut(500).fadeIn(500);
}