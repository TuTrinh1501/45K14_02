$(document).ready(function () {
    $('#table_id').DataTable({
        lengthMenu: [5, 10, 25, 50, 100],
        "order": [[0, "desc"]]
    });
});

let input = $("#file");

function openFileDialog() {
    input.click();
}

input.change(() => {
    let reader = new FileReader();
    reader.onload = function (e) {
        $('#image').attr('src', e.target.result);
    }
    reader.readAsDataURL(input[0].files[0]);
    $("#imageText").val(input[0].files[0].name)
})

showModal = (url, title, id) => {
    $('#cateError').html("");
    $.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        success: res => {
            $("#myModal").modal("show");
            $(".modal-title").html(title);
            $("#idCategory").css("display", "none");
            $("#myModal .modal-body #name").val(null);
            $("#myModal .modal-body #image").attr("src", '');
            $("#myModal .modal-body #imageText").val(null);
            if (id != null) {
                const { Category_ID, Category_Name, Image } = res.data;
                $("#myModal .modal-body #id").val(Category_ID);
                $("#myModal .modal-body #name").val(Category_Name);
                $("#myModal .modal-body #image").attr("src", '/Images/category/' + Image);
                $("#myModal .modal-body #imageText").val(Image);
                $("#idCategory").css("display", "block");
            }
        }
    })
}

checkDelete = (id, text) => {
    $("#confirmContent").html("Do you want to delete " + text + " " + id);
    $("#confirmContent").attr('data-id', id);;
    $("#confirmDelete").modal("show");
}

deleteCategory = () => {
    let id = $("#confirmContent").attr("data-id");
    $.ajax({
        url: "/Category/DeleteCate",
        dataType: "json",
        type: "POST",
        data: { id: id },
        success: function (res) {
            if (res.mess === true) {
                $('#confirmDelete').modal('hide');
                $("#cate" + id).css("display", "none");
                toastr.success("Delete Successfully!", "Success Alert", { timeOut: 3000 });
                setTimeout(() => {
                    location.reload();
                }, 2000)
            }
        }
    })
}

handleSubmit = () => {
    var cateObj = {
        Category_ID: $('#myModal .modal-body #id').val(),
        Category_Name: $('#myModal .modal-body #name').val(),
        Image: $('#myModal .modal-body #imageText').val(),
    };
    if ($('#myModal .modal-body #name').val() == "") {
        $('#cateError').html("This field can't empty!");
        return false;
    } else {
        $.ajax({
            url: "/Category/CategoryAOU",
            data: JSON.stringify(cateObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.status == 1) {
                    toastr.success(result.type == 1 ? "Update Successfully!" : "Add Successfully!", "Success Alert", { timeOut: 3000 });
                    $('#myModal').modal('hide');
                    setTimeout(() => {
                        location.reload();
                    }, 2000)
                } else {
                    toastr.error(result.type == 1 ? "Update Fail!" : "Add Fail!", "Success Alert", { timeOut: 3000 });
                }
            },
        });
    }
}