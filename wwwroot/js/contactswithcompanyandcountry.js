
var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/contacts/getcontactswithcompanyandcountry'
        },
        "columns": [
            { data: 'contactName', "width": "30%" },
            { data: 'company.companyName', "width": "20%" },
            { data: 'country.countryName', "width": "20%" },
            {
                data: 'contactId',
                "render": function (data) {
                 
                    return `<div class="w-75 btn-group role=group">
                    <a href="/api/contact/upsert?id=${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Edit</a>
                    <a onclick="Delete('/api/contact/delete/${data}')" class="btn btn-danger mx-2"><i class="bi bi-trash-fill"></i>Delete</a>
                    </div>`;
                },
                "width": "30%"
            }
        ]
    });
}

function Delete(url) {
    console.log("Delete URL:", url);


    Swal.fire({
        title: "Are you sure u want to delete?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    console.log("Response from server:", data);
                    if (data.success) {
                        dataTable.ajax.reload(); // Reload DataTable
                        toastr.success(data.message); // Show success message
                    } else {
                        toastr.error(data.message); // Show error message if deletion fails
                    }
                },
                error: function (xhr, status, error) {
                    // Handle error case
                    console.error("Error deleting:", error);
                    console.error("Error deleting:", xhr.responseText);
                    toastr.error("An error occurred while deleting the item.");
                }
            });
        }
    });
}


