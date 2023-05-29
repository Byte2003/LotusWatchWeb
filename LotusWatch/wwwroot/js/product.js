var dataTable;
$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    dataTable = $('#tblProductData').DataTable({
        "ajax": {
            "url": "/Product/GetAllProducts"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "name", "width": "10%" },
            { "data": "color", "width": "10%" },
            { "data": "description", "width": "15%" },
            { "data": "price", "width": "10%" },
            { "data": "discount", "width": "10%" },
            { "data": "brand.name", "width": "10%" },
            { "data": "category.name", "width": "10%" },


            {
                "data": "id",
                "render": function (data) {
                    return `
                            <a href="/Product/Edit?id=${data}" class="btn btn-outline-info mx-2">
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>
                            <a onClick=Delete('/Product/Delete/${data}') class="btn btn-outline-danger mx-2">
                                <i class="bi bi-trash"></i>Delete
                            </a>
                            `
                },
                "width": "35%"
            }
        ]
    });
}
// Delete function
function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}

