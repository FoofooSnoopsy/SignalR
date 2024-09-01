"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/dashboardHub").build();

$(function () {
    connection.start().then(function () {
        InvokeProducts();
        InvokeSales();
        InvokeCustomers();
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

connection.on("ReceivedSales", function (sales) {
    BindSalesToGrid(sales);
});

connection.on("ReceivedSalesGraphData", function (saleGraphData) {
    BindSalesToGraph(saleGraphData);
});

connection.on("ReceivedCustomers", function (customers) {
    BindCustomersToGrid(customers);
});

connection.on("ReceivedCustomersGraphData", function (customerGraphData) {
    console.log(customerGraphData)
    BindCustomerToGraph(customerGraphData);
});

function InvokeProducts() {
    connection.invoke("SendProducts").catch(function (err) {
        return console.error(err.toString());
    });
}

function InvokeSales() {
    connection.invoke("SendSales").catch(function (err) {
        return console.error(err.toString());
    });
}

function InvokeCustomers() {
    connection.invoke("SendCustomers").catch(function (err) {
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

function BindSalesToGrid(sales) {
    $("#tblSale tbody").empty();
    var tr;
    $.each(sales, function (index, sale) {
        tr = $("<tr/>");
        tr.append("<td>" + (index + 1) + "</td>");
        tr.append("<td>" + sale.customerId + "</td>");
        tr.append("<td>" + sale.amount + "</td>");
        tr.append("<td>" + sale.purchasedOn + "</td>");
        $("#tblSale").append(tr);
    });
}

function BindCustomersToGrid(customers) {
    $("#tblCustomer tbody").empty();
    var tr;
    $.each(customers, function (index, customer) {
        tr = $("<tr/>");
        tr.append("<td>" + (index + 1) + "</td>");
        tr.append("<td>" + customer.name + "</td>");
        tr.append("<td>" + customer.gender + "</td>");
        tr.append("<td>" + customer.mobile + "</td>");
        $("#tblCustomer").append(tr);
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

function BindSalesToGraph(salesForGraph) {
    var labels = [];
    var data = [];
    $.each(salesForGraph, function (index, sale) {
        labels.push(sale.day);
        data.push(sale.count);
    });

    DestroyCanvasIfExists('canvasSales');

    const context = $('#canvasSales');
    new Chart(context, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'Sales',
                data: data,
                backgroundColor: 'rgba(245, 39, 63,0.3)',
                borderColor: '#454445',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
}

function BindCustomerToGraph(customersForGraph) {
    var labels = [];
    var data = [];
    $.each(customersForGraph, function (index, customer) {
        labels.push(customer.gender);
        data.push(customer.count);
    });

    DestroyCanvasIfExists('canvasCustomers');
    for (var i = 0; i < data.length; i++) {
        backgroundColors.push(getRandomColor());
    }

    const context = $('#canvasCustomers');
    new Chart(context, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Genders',
                data: data,
                backgroundColor: backgroundColors,
                borderColor: borderColors,
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
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