"use strict";
var connection = new
    signalR.HubConnectionBuilder().withUrl("/dashboardHub").build();
$(function () {
    connection.start().then(function () {
        InvokeProducts();
    }).catch(function (err) {
        return console.error(err.toString());
    });
});
connection.on("ReceivedProducts", function (products) {
    BindProductsToGrid(products);
});

connection.on("ReceivedProductsGraphData", function (graphData) {
    BindProductsToGraph(graphData);
});
function InvokeProducts() {
    connection.invoke("SendProducts").catch(function (err) {
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
function BindProductsToGraph(productForGraph) {
    var labels = [];
    var data = [];
    $.each(productForGraph, function (index, category) {
        labels.push(category.category);
        data.push(category.count);
    });

    DestroyCanvasIfExists('canvasProducts');
    for (var i = 0; i < data.length; i++) {
        backgroundColors.push(getRandomColor());
    }


    const context = $('#canvasProducts');
    console.log("hier");
    new Chart(context, {
        type: 'doughnut',
        data: {
            labels: labels,
            datasets: [{
                label: '# of products',
                data: data,
                backgroundColor: backgroundColors,
                borderColor: borderColors,
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: { beginAtZero: true }
            }
        }
    });
}
function getRandomColor() {
    return `#${Math.floor(Math.random() * 16777215).toString(16)}4D`;
}

function DestroyCanvasIfExists(canvasId) {
    let chartStatus = Chart.getChart(canvasId);
    if (chartStatus != undefined) {
        chartStatus.destroy();
    }
}
var backgroundColors = [
    'rgba(245, 39, 63,0.3)',
    'rgba(31, 255, 98,0.3)',
    'rgba(252, 145, 68,0.3)',
    'rgba(189, 115, 245,0.3)'
];
var borderColors = [
    '#454445'
];