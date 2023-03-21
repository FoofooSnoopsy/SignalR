"use strict";

let connection = new signalR.HubConnectionBuilder().withUrl('/dashboardHub').build()

$(function() {

    connection.start().then(() => {
        console.info('connection success');
        GetProducts();
    }).catch(error => {
        console.error(error.toString());
    })
})

connection.on("pannekoek", function (products) {
    console.log(products);
    BindProductsToGrid(products);
});

connection.on("GraphData", function (graphData) {
    console.log('GraphData');
    console.log(graphData);
    BindProductsToGraph(graphData)
});

function GetProducts() {
    connection.invoke("SendProducts")
    .catch(function (err) {
        console.error(err.toString());
    });
}

function BindProductsToGrid(products) {
    $("#tblProduct tbody").empty();
    let tr;
    $.each(products, function (index, product) {
        tr = $("<tr/>");
        tr.append("<td>" + (index + 1) + "</td>");
        tr.append("<td>" + product.name + "</td>");
        tr.append("<td>" + product.category + "</td>");
        tr.append("<td>" + product.price + "</td>");
        $("#tblProduct").append(tr);
    });
}

var myChart;

function DestroyCanvasIfExists(canvasId) {
    var chartstatus = Chart.getChart(canvasId);
    if (chartstatus != undefined) {
        console.log('destroy: ');
        chartstatus.destroy();
    }
}

function getRandomColor() {
    return `#${Math.floor(Math.random() * 16777215).toString(16)}`;
}


function BindProductsToGraph(productsForGraph) {
    var labels = [];
    var data = [];
    let backgroundColors = [];
    let borderColors = [];

    $.each(productsForGraph, (index, category) => {
        labels.push(category.category);
        data.push(category.count);
    });

    DestroyCanvasIfExists('canvasProducts');

    for (var i = 0; i < data.length; i++) {
        backgroundColors.push(getRandomColor());
        borderColors.push(getRandomColor());
    }

    const context = $("#canvasProducts");
    myChart = new Chart(context, {
        type: 'doughnut',
        data: {
            labels: labels,
            datasets: [{
                label: '# of Products',
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
    })
}
