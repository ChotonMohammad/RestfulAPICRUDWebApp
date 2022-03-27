


/*
///////////////////////////////////////

        SETTINGS SECTION START

///////////////////////////////////////
*/


// load menu item for dropdown
load_menu_item_for_dropdown();
function load_menu_item_for_dropdown() {
    $.ajax({
        url: '/api/ShowMenuItemsForDropdown',
        type: "GET",
        // Fetch the stored token from localStorage and set in the header
        headers: { "Authorization": localStorage.getItem('token') },
        contentType: "application/json; charset-utf-8",
        success: function (resp) {
            if (resp.error) {
                toastr.error(resp.message);
            }
            else {
                $("#setting_menu_item_filter").empty();
                $("#add_setting_menu_item_select").empty();
                $("#edit_setting_menu_item_select").empty();

                $("#setting_menu_item_filter").append('<option value="-1">    All...</option>');
                $("#add_setting_menu_item_select").append('<option value="">Select Department</option>');

                $.each(resp.data, function (index, value) {

                    $("#setting_menu_item_filter").append('<option value="' + value.MenuItemId + '">' + value.MenuItemName + '</option>');
                    $("#add_setting_menu_item_select").append('<option value="' + value.MenuItemId + '">' + value.MenuItemName + '</option>');
                    $("#edit_setting_menu_item_select").append('<option value="' + value.MenuItemId + '">' + value.MenuItemName + '</option>');
                });
            }

        }
    });
}


// load setting table
load_setting_table(1);
function load_setting_table(pageNo) {

    //json = {

    //    limit: $("#setting_limit_filter").val(),
    //    departmentId: $("#setting_menu_item_filter").val(),
    //    cityId: $("#setting_city_filter").val(),
    //    page: page
    //};

    let limit = $("#setting_limit_filter").val();
    let menuItemId = $("#setting_menu_item_filter").val();
    let page = pageNo

    $.ajax({


        url: '/api/GetSettings/' + limit + "/" + menuItemId + "/" + page,
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

                show_setting_list(resp);
                $("#setting_pagination_page_no").text(page);
            }
        },
        beforeSend: function () {
            $("#setting_loading_gif").show();
        },
        complete: function () {
            $("#setting_loading_gif").hide();
        }
    });
}



// Shows data on setting table
function show_setting_list(resp) {
    $("#setting_table_tbody").empty();

    //console.log(resp.data);
    if (resp.data.length <= 0) {
        toastr.warning("Settings's data not available.");


    }

    $.each(resp.data, function (index, value) {
        var slnocell = $("<td>").attr("scope", "row").append(index + 1);

        var menuItemName = $("<td>").append(value.MenuItemName);
        var viewStatus = $("<td>").append(value.ViewStatus == 0 ? "Enabled" : value.ViewStatus == 1 ? "Disabled" : "-");
        var createStatus = $("<td>").append(value.CreateStatus == 0 ? "Enabled" : value.CreateStatus == 1 ? "Disabled" : "-");
        var updateStatus = $("<td>").append(value.UpdateStatus == 0 ? "Enabled" : value.UpdateStatus == 1 ? "Disabled" : "-");
        var deleteStatus = $("<td>").append(value.DeleteStatus == 0 ? "Enabled" : value.DeleteStatus == 1 ? "Disabled" : "-");
        
        var iconcell = $("<td>");

        var icondiv = $("<ul>").attr("class", "d-flex justify-content-center").appendTo(iconcell);

        var editIcon = $("<i>").attr({
            "class": "fa fa-edit text-white bg-info rounded-circle shadow",
            "style": "font-size: 18px; padding: 10px; cursor: pointer; border: 2px solid white;",
            "title": "Edit User"
        }).appendTo(icondiv);

        $("<tr>").append(slnocell, menuItemName, viewStatus, createStatus, updateStatus, deleteStatus, iconcell).appendTo("#setting_table_tbody");

        (function ($) {
            editIcon.on("click", function (e) {
                e.preventDefault();
                $("#setting_edit_modal").modal("show");
                $("#edit_setting_menu_item_select").data("menuSettingId", value.MenuSettingId);
                $("#edit_setting_menu_item_select").val(value.MenuItemId);
                $("#edit_setting_view_checked").prop('checked', value.ViewStatus == 0 ? true : false);
                $("#edit_setting_create_checked").prop('checked', value.CreateStatus == 0 ? true : false);
                $("#edit_setting_update_checked").prop('checked', value.UpdateStatus == 0 ? true : false);
                $("#edit_setting_delete_checked").prop('checked', value.DeleteStatus == 0 ? true : false);

            });

        })(jQuery);

    });
}

//Start add setting add event listener

$("#add_setting_button").on("click", function () {
    $("#setting_add_modal").modal("show");
    $("#setting_add_modal_form").trigger("reset");
    //$("#add_setting_button").attr("disabled", false);

});

//Start add setting event listener




//save new setting

$("#setting_add_modal_form").on("submit", function (e) {
    e.preventDefault();

    if ($('#setting_add_modal_form')[0].checkValidity() === false) {
        event.stopPropagation();
    } else {
        // ajax submition here
        json = {
            MenuItemId: $("#add_setting_menu_item_select").val(),
            ViewStatus: $("#add_setting_view_checked").prop("checked") == true ? 0 : 1,
            CreateStatus: $("#add_setting_create_checked").prop("checked") == true ? 0 : 1,
            UpdateStatus: $("#add_setting_update_checked").prop("checked") == true ? 0 : 1,
            DeleteStatus: $("#add_setting_delete_checked").prop("checked") == true ? 0 : 1,
        };

        $.ajax({
            url: '/api/PostSetting',
            type: "POST",
            // Fetch the stored token from localStorage and set in the header
            headers: { "Authorization": localStorage.getItem('token') },
            contentType: "application/json; charset-utf-8",
            data: JSON.stringify(json),
            success: function (resp) {

                if (resp.error) {
                    $("#add_setting_button").attr("disabled", false);
                    toastr.error(resp.message);
                }
                else {
                    $("#setting_add_modal").modal("hide");
                    toastr.success(resp.message);
                    load_setting_table(1);
                    $("#setting_add_modal_form").removeClass('was-validated');
                }
            },
            beforeSend: function () {
                $("#setting_loading_gif").show();
            },
            complete: function () {
                $("#setting_loading_gif").hide();
            }
        });
    }

});



//update setting
$("#setting_edit_modal_form").on("submit", function (e) {
    e.preventDefault();

    json = {
        MenuSettingId: $("#edit_setting_menu_item_select").data("menuSettingId"),
        MenuItemId: $("#edit_setting_menu_item_select").val(),
        ViewStatus: $("#edit_setting_view_checked").prop("checked") == true?0:1,
        CreateStatus: $("#edit_setting_create_checked").prop("checked") == true ? 0 : 1,
        UpdateStatus: $("#edit_setting_update_checked").prop("checked") == true ? 0 : 1,
        DeleteStatus: $("#edit_setting_delete_checked").prop("checked") == true ? 0 : 1,
    };

    $.ajax({
        url: '/api/PutSetting',
        type: "PUT",
        // Fetch the stored token from localStorage and set in the header
        headers: { "Authorization": localStorage.getItem('token') },
        contentType: "application/json; charset-utf-8",
        data: JSON.stringify(json),
        success: function (resp) {

            if (resp.error) {
                toastr.error(resp.message);
            }
            else {
                $("#setting_edit_modal").modal("hide");
                toastr.success(resp.message);
                load_setting_table($("#setting_pagination_page_no").text());
            }
        },
        beforeSend: function () {
            $("#setting_loading_gif").show();
        },
        complete: function () {
            $("#setting_loading_gif").hide();
        }
    });
});

//filter

$("#setting_filter_button").on("click", function (e) {
    e.preventDefault();
    load_setting_table(1);
});

//setting pagination


$("#setting_previous_list_click").on("click", function () {
    var currentPage = parseInt($("#setting_pagination_page_no").text());
    if (currentPage === 1) {
        toastr.warning("You are in page one");
    }
    else {
        load_setting_table(currentPage - 1);
        $("#setting_pagination_page_no").text(currentPage - 1);
    }
});

$("#setting_next_list_click").on("click", function () {
    var currentPage = parseInt($("#setting_pagination_page_no").text());
    load_setting_table(currentPage + 1);
    $("#setting_pagination_page_no").text(currentPage + 1);
});


$("#setting_pagination_page_no").on("click", function () {

    $("#setting_pagination_page_no").addClass("display-none");
    $("#setting_pagination_page_no_input").removeClass("display-none");
    $("#setting_pagination_page_no_input").focus();
});

$("#setting_pagination_page_no_input").blur(function () {
    $("#setting_pagination_page_no").removeClass("display-none");
    $("#setting_pagination_page_no_input").addClass("display-none");
});


$("#setting_pagination_page_no_input").keydown(function (e) {

    if (e.keyCode === 13) {
        if ($("#setting_pagination_page_no_input").val() > 0) {
            load_setting_table($("#setting_pagination_page_no_input").val());
            $("#setting_pagination_page_no").text($("#setting_pagination_page_no_input").val());
            $("#setting_pagination_page_no_input").val("");
            $("#setting_pagination_page_no").removeClass("display-none");
            $("#setting_pagination_page_no_input").addClass("display-none");
        }
        else {
            $("#setting_pagination_page_no_input").val("");

            toastr.warning("Page number should be greater than zero");
        }

    }
    else if (e.keyCode === 27) {
        $("#setting_pagination_page_no").removeClass("display-none");
        $("#setting_pagination_page_no_input").addClass("display-none");
    }

});

/*
///////////////////////////////////////

        SETTINGS SECTION END

///////////////////////////////////////
*/