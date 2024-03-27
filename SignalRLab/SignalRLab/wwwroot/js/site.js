// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.
$(() => {
    LoadProdData()
    var connection = new signalR.HubConnectionBuilder().withUrl("/signalR-server").build();
    connection.start();

    connection.on("LoadProdData", function (message) {
        console.log(message);
        LoadProdData();
    });
    //LoadProdData();


    function LoadProdData() {
        var tr = "";
        $.ajax({
            url: "/Products/GetProducts",
            type: "GET",
            success: function (data) {
                $.each(data, function (index, item) {
                    tr += "<tr>";
                    tr += "<td>" + item.proName + "</td>";
                    tr += "<td>" + item.category + "</td>";
                    tr += "<td>" + item.unitPrice + "</td>";
                    tr += "<td>" + item.stockQty + "</td>";
                    tr += "<td>" +
                        `
                        <a href="/Products/Edit/${item.proId}" class="btn btn-primary">Edit</a>
                        <a href="/Products/Details/${item.proId}" class="btn btn-primary">Detail</a>
                        <a href="/Products/Delete/${item.proId}" class="btn btn-primary">Delete</a>
                        `
                        +"</td>";
                    tr += "</tr>";
                });
                $("#tableBody").html(tr);
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
})