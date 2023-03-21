"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/DashboardHub").build();

$(function () {
    connection.start().then(function () {
        InvokeProducts();
    }).catch(function (err) {
        return console.error(err.toString());
    });
});

connection.on("GetProducts", function (products) {
    console.log(products);
    BindProductsToGrid(products);
});

function InvokeProducts() {
    connection.invoke("SendProducts").catch(function (err) {
        alert(err.toString());
        return console.error(err.toString());
    });
}

function BindProductsToGrid(products) {
    $("#tblProduct tbody").empty();
    var tr;
    $.each(products, function (index, product) {
        tr = $("<tr/>");
        tr.append("<td>" + (index + 1) + "</td>");
        tr.append("<td>" + product.name + "</td>");
        tr.append("<td>" + product.category + "</td>");
        tr.append("<td>" + product.price + "</td>");
        $("#tblProduct").append(tr);
    });
}