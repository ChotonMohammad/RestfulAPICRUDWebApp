



/*
///////////////////////////////////////

        EMPLOYEE SECTION START

///////////////////////////////////////
*/


// load department for dropdown
load_department_for_dropdown();
function load_department_for_dropdown() {
    $.ajax({
        url: '/api/ShowDepartmentsForDropdown',
        type: "GET",
        // Fetch the stored token from localStorage and set in the header
        headers: { "Authorization": localStorage.getItem('token') },
        contentType: "application/json; charset-utf-8",
        success: function (resp) {
            if (resp.error) {
                toastr.error(resp.message);
            }
            else {
                $("#employee_department_filter").empty();
                $("#add_employee_department_select").empty();
                $("#edit_employee_department_select").empty();

                $("#employee_department_filter").append('<option value="-1">    All...</option>');
                $("#add_employee_department_select").append('<option value="">Select Department</option>');

                $.each(resp.data, function (index, value) {

                    $("#employee_department_filter").append('<option value="' + value.DepartmentId + '">' + value.DepartmentName + '</option>');
                    $("#add_employee_department_select").append('<option value="' + value.DepartmentId + '">' + value.DepartmentName + '</option>');
                    $("#edit_employee_department_select").append('<option value="' + value.DepartmentId + '">' + value.DepartmentName + '</option>');
                });
            }

        }
    });
}

// load city for dropdown
load_city_for_dropdown();
function load_city_for_dropdown() {
    $.ajax({
        url: '/api/ShowCitiesForDropdown',
        type: "GET",
        // Fetch the stored token from localStorage and set in the header
        headers: { "Authorization": localStorage.getItem('token') },
        contentType: "application/json; charset-utf-8",
        success: function (resp) {
            if (resp.error) {
                toastr.error(resp.message);
            }
            else {
                $("#employee_city_filter").empty();
                $("#add_employee_city_select").empty();
                $("#edit_employee_city_select").empty();

                $("#employee_city_filter").append('<option value="-1">    All...</option>');
                $("#add_employee_city_select").append('<option value="">Select City</option>');

                $.each(resp.data, function (index, value) {

                    $("#employee_city_filter").append('<option value="' + value.CityId + '">' + value.CityName + '</option>');
                    $("#add_employee_city_select").append('<option value="' + value.CityId + '">' + value.CityName + '</option>');
                    $("#edit_employee_city_select").append('<option value="' + value.CityId + '">' + value.CityName + '</option>');
                });
            }

        }
    });
}



// load employee table
load_employee_table(1);
function load_employee_table(pageNo) {

    //json = {

    //    limit: $("#employee_limit_filter").val(),
    //    departmentId: $("#employee_department_filter").val(),
    //    cityId: $("#employee_city_filter").val(),
    //    page: page
    //};

    let limit = $("#employee_limit_filter").val();
    let departmentId = $("#employee_department_filter").val();
    let cityId = $("#employee_city_filter").val();
    let page = pageNo

    $.ajax({


        url: '/api/recovery/GetEmployees/' + limit + "/" + departmentId + "/" + cityId + "/" + page,
        type: "GET",
        // Fetch the stored token from localStorage and set in the header
        headers: { "Authorization": localStorage.getItem('token') },
        contentType: "application/json; charset-utf-8",
        //data: JSON.stringify(json),
        success: function (resp) {

            if (resp.error) {
                toastr.error(resp.message);
            }
            else {

                show_employee_list(resp);
                $("#employee_pagination_page_no").text(page);
            }
        },
        beforeSend: function () {
            $("#employee_loading_gif").show();
        },
        complete: function () {
            $("#employee_loading_gif").hide();
        }
    });
}



// Shows data on employee table
function show_employee_list(resp) {
    $("#employee_table_tbody").empty();

    //console.log(resp.data);
    if (resp.data.length <= 0) {
        toastr.warning("Employee's data not available.");


    }

    $.each(resp.data, function (index, value) {
        var slnocell = $("<td>").attr("scope", "row").append(index + 1);

        var employeeName = $("<td>").append(value.EmployeeName);
        var gender = $("<td>").append(value.Gender == 1 ? "Male" : value.Gender == 2 ? "Female" : "-");
        var department = $("<td>").append(value.DepartmentName);
        var city = $("<td>").append(value.CityName);


        var iconcell = $("<td>");

        var icondiv = $("<ul>").attr("class", "d-flex justify-content-center").appendTo(iconcell);

        var restoreIcon = $("<i>").attr({
            "class": "fa fa-trash text-white bg-danger rounded-circle shadow ml-2",
            "style": "font-size: 18px; padding: 10px; cursor: pointer; border: 2px solid white;",
            "title": "Recover"
        }).appendTo(icondiv);

        $("<tr>").append(slnocell, employeeName, gender, department, city, iconcell).appendTo("#employee_table_tbody");

        (function ($) {
            restoreIcon.on("click", function (e) {
                e.preventDefault();
                $("#restore_employee_modal").modal("show");
                $("#restore_employee_employee_id_hidden").data("employeeId", value.EmployeeId);
            });

        })(jQuery);

    });
}


//restore employee
$("#restore_employee_modal_form").on("submit", function (e) {
    e.preventDefault();

    //json = {
    //    employeeId: $("#restore_employee_employee_id_hidden").data("employeeId"),
    //};

    let employeeId = $("#restore_employee_employee_id_hidden").data("employeeId");


    $.ajax({
        url: '/api/recovery/RecoverEmployee/' + employeeId,
        type: "POST",
        // Fetch the stored token from localStorage and set in the header
        headers: { "Authorization": localStorage.getItem('token') },
        contentType: "application/json; charset-utf-8",
        //data: JSON.stringify(json),
        success: function (resp) {

            if (resp.error) {
                toastr.error(resp.message);
            }
            else {
                $("#restore_employee_modal").modal("hide");
                toastr.success(resp.message);
                load_employee_table($("#employee_pagination_page_no").text());


            }
        },
        beforeSend: function () {
            $("#employee_loading_gif").show();
        },
        complete: function () {
            $("#employee_loading_gif").hide();
        }
    });
});




//filter

$("#employee_filter_button").on("click", function (e) {
    e.preventDefault();
    load_employee_table(1);
});

//employee pagination


$("#employee_previous_list_click").on("click", function () {
    var currentPage = parseInt($("#employee_pagination_page_no").text());
    if (currentPage === 1) {
        toastr.warning("You are in page one");
    }
    else {
        load_employee_table(currentPage - 1);
        $("#employee_pagination_page_no").text(currentPage - 1);
    }
});

$("#employee_next_list_click").on("click", function () {
    var currentPage = parseInt($("#employee_pagination_page_no").text());
    load_employee_table(currentPage + 1);
    $("#employee_pagination_page_no").text(currentPage + 1);
});


$("#employee_pagination_page_no").on("click", function () {

    $("#employee_pagination_page_no").addClass("display-none");
    $("#employee_pagination_page_no_input").removeClass("display-none");
    $("#employee_pagination_page_no_input").focus();
});

$("#employee_pagination_page_no_input").blur(function () {
    $("#employee_pagination_page_no").removeClass("display-none");
    $("#employee_pagination_page_no_input").addClass("display-none");
});


$("#employee_pagination_page_no_input").keydown(function (e) {

    if (e.keyCode === 13) {
        if ($("#employee_pagination_page_no_input").val() > 0) {
            load_employee_table($("#employee_pagination_page_no_input").val());
            $("#employee_pagination_page_no").text($("#employee_pagination_page_no_input").val());
            $("#employee_pagination_page_no_input").val("");
            $("#employee_pagination_page_no").removeClass("display-none");
            $("#employee_pagination_page_no_input").addClass("display-none");
        }
        else {
            $("#employee_pagination_page_no_input").val("");

            toastr.warning("Page number should be greater than zero");
        }

    }
    else if (e.keyCode === 27) {
        $("#employee_pagination_page_no").removeClass("display-none");
        $("#employee_pagination_page_no_input").addClass("display-none");
    }

});

/*
///////////////////////////////////////

        EMPLOYEE SECTION END

///////////////////////////////////////
*/