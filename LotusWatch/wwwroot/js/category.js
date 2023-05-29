var dataTable;
$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    dataTable = $('#tblCategoryData').DataTable({
        "ajax": {
            "url": "/Category/GetAllCategories"
        },
        "columns": [
            { "data": "id", "width": "25%" },
            { "data": "name", "width": "25%" },
            //{ "data": "products", "width": "25%" },
            //{ "data": "brands", "width": "25%" },


            {
                "data": "id",
                "render": function (data) {
                    return `
                            <a href="/Category/Edit?id=${data}" class="btn btn-outline-info mx-2">
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>
                            <a href="/Category/Delete?id=${data}" class="btn btn-outline-danger mx-2">
                                <i class="bi bi-trash"></i>Delete
                            </a>
                            `
                },
                "width": "25%"
            }
        ]
    });
}
