var dataTable;
$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    dataTable = $('#tblBrandData').DataTable({
        "ajax": {
            "url": "/Admin/Brand/GetAllBrands"
        },
        "columns": [
            { "data": "brandId", "width": "5%" },
            { "data": "brandName", "width": "15%" },
            { "data": "contactName", "width": "10%" },
            { "data": "address", "width": "15%" },
            { "data": "city", "width": "10%" },
            { "data": "phone", "width": "10%" },
            { "data": "fax", "width": "10%" },
                        {
                "data": "brandId",
                "render": function (data) {
                    return `
                       
                            <a href="/Admin/Brand/Edit?id=${data}"
                            class="btn btn-outline-info mx-2"> <i class="bi bi-pencil"></i> Edit</a>
                            <a href="/Admin/Brand/Delete?id=${data}"
                            class="btn btn-outline-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
					   
                        `
                },
                "width": "20%"
            }
        ]
    });
}
