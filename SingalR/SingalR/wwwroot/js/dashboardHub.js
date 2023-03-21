
//
// setup connection
//
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/dashboardHub")
    .build();

$(function() {
    connection.start().then(() => {
        InvokeProducts();
        InvokeSales();
        InvokeCustomers();
    }).catch(err => {
        return console.error(err.toString());
    });
});



connection.on("ReceivedProducts", products => {
    BindProductsToGrid(products);
});

connection.on("ReceivedProductsGraphData", graphData => {
    BindProductsToGraph(graphData);
});



connection.on("ReceivedSales", Sales => {
    BindSalesToGrid(Sales);
});

connection.on("ReceivedSalesGraphData", Sales => {
    BindSalesToGraph(Sales);
});



connection.on("ReceivedCustomers", Customers => {
    BindCustomersToGrid(Customers);
    console.log(Customers);
});

connection.on("ReceivedCustomersGraphData", Customers => {
    BindCustomersToGraph(Customers);
});


//
// Invoke stuff
//

function InvokeProducts() {
    connection.invoke("SendProducts").catch(err => {
        console.error(err.toString());
    });
}

function InvokeSales() {
    connection.invoke("SendSales").catch(err => {
        console.error(err.toString());
    });
}

function InvokeCustomers() {
    connection.invoke("SendCustomers").catch(err => {
        console.error(err.toString());
    });
}


//
// table data
//

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

function BindSalesToGrid(Sales) {
    $("#tblSales tbody").empty();
    var tr;
    $.each(Sales, function (index, sale) {
        tr = $("<tr/>");
        tr.append("<td>" + (index + 1) + "</td>");
        tr.append("<td>" + sale.customer.name + "</td>");
        tr.append("<td>" + sale.product.name + "</td>");
        tr.append("<td>" + sale.amount + "</td>");
        tr.append("<td>" + sale.purchasedOn + "</td>");
        $("#tblSales").append(tr);
    });
}

function BindCustomersToGrid(Customers) {
    $("#tblCustomers tbody").empty();
    var tr;
    $.each(Customers, function (index, customer) {
        tr = $("<tr/>");
        tr.append("<td>" + (index + 1) + "</td>");
        tr.append("<td>" + customer.name + "</td>");
        tr.append("<td>" + customer.gender + "</td>");
        tr.append("<td>" + customer.mobile + "</td>");
        $("#tblCustomers").append(tr);
    });
}


//
// display graphs
//

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

function BindSalesToGraph(SalesForGraph) {
    var labels = [];
    var data = [];
    $.each(SalesForGraph, function (index, category) {
        labels.push(category.day);
        data.push(category.count);
    });

    DestroyCanvasIfExists('canvasSales');
    for (var i = 0; i < data.length; i++) {
        backgroundColors.push(getRandomColor());
    }


    const context = $('#canvasSales');

    new Chart(context, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: '# of sales',
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

function BindCustomersToGraph(productForGraph) {
    var labels = [];
    var data = [];
    $.each(productForGraph, function (index, category) {
        labels.push(category.gender);
        data.push(category.count);
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