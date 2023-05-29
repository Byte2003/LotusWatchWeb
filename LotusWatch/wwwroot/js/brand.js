var dataTable;
$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    dataTable = $('#tblBrandData').DataTable({
        "ajax": {
            "url": "/Brand/GetAllBrands"
        },
        "columns": [
            { "data": "id", "width": "25%" },
            { "data": "name", "width": "25%" },
            { "data": "origin", "width": "25%" },
            
            //{ "data": "products", "width": "25%" },
            //{ "data": "categories", "width": "25%" },
            

            {
                "data": "id",
                "render": function (data) {
                    return `
                       
                            <a href="/Brand/Edit?id=${data}"
                            class="btn btn-outline-info mx-2"> <i class="bi bi-pencil"></i> Edit</a>
                            <a href="/Brand/Delete?id=${data}"
                            class="btn btn-outline-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
					   
                        `
                },
                "width": "20%"
            }
        ]
    });
}
