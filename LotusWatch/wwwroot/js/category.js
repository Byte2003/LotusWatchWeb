var dataTable;
$(document).ready(function () {
   loadDataTable();
    
   
})
function loadDataTable() {

    dataTable = $('#tblCategoryData').DataTable({
        "ajax": {
            "url": "/Admin/Category/GetAllCategories"
        },
        "columns": [
            { "data": "categoryId", "width": "25%" },
            { "data": "name", "width": "25%" },
            {"data":"description","width":"25%"},
            {
                "data": "categoryId",
                "render": function (data) {
                    return `
                            <div class="row justify-content-evenly">
                                <div class="col-6 d-flex justify-content-center align-items-center">
                                    <a href="/Admin//Category/Edit?id=${data}" class="btn btn-outline-info mx-2">
                                        <i class="bi bi-pencil-square"></i> Edit
                                    </a>
                                </div>
                                <div class="col-6 d-flex justify-content-center align-items-center">
                                    <a href="/Admin/Category/Delete?id=${data}" class="btn btn-outline-danger mx-2">
                                        <i class="bi bi-trash"></i>Delete
                                    </a>
                                </div>

                            </div>
                            
                            
                            `
                },
                "width": "25%"
            }
        ]

       
    });

}
