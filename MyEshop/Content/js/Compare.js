$(function () {
    AddToCompare();
});

function AddToCompare(id) {
    $.get("/Compare/AddToCompare/" + id, function (res) {
        $("#Compare").html(res);
    });
}

function DeleteFromCompare(id) {
    $.get("/Compare/DeleteFromCompare/" + id, function (res) {
        $("#Compare").html(res);
    });
}