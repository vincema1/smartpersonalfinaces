
    //DisplayReport

    function displayReport(data) {

        $("#BalanceSheet").html(data);
    }

    function createReport()
    {
         $.ajax({
                url: UrlNewReport_ASP,
                type: 'get',
                data:
                {
                    dossierId: dossierId_ASP,
                    beginDate: $("#beginDate").val(),
                    endDate: $("#endDate").val()
                },
                success: displayReport
            });
    }

    //function ongletClick()
    //{
    //    $('div[class*="contenu"]').hide();
    //    switch (this.id) {
    //        case "Onglet1":
    //            $('#ReportExpenses').show();
    //            break;

    //        case "Onglet2":
    //            $('#ReportRevenues').show();
    //            break;

    //        case "Onglet3":
    //            $('#NetCashFlow').show();
    //            break;
    //    }

    //}

    function gotoPage()
    {
        var pageNum = $("#txtCurrentPage").val();

        if (isNaN(pageNum)) return;
        if (pageNum < 0 || pageNum > TotalNumberOfPages_ASP) return;


        var url = actionLink_ASP;
        url += "?dossierId=" + dossierId_ASP +
                "&isPostBack=true" +
                "&BeginDate=" + beginDate_ASP +
                "&EndDate=" + endDate_ASP +
                "&ItemsPerPage=" + ItemsPerPage_ASP +
                "&CurrentPage=" + pageNum;

        console.log("page num: " + pageNum);
        console.log("url: " + url);

        window.location.href = url;

    }

    function startQuickSearch()
    {
        console.log('startQuickSearch')
        var url = actionLink_ASP;

        var beginDate = ($.trim($("input[fullsearch='beginDate']").val()) == "") ? beginDate_ASP : $.trim($("input[fullsearch='beginDate']").val());
        var endDate = ($.trim($("input[fullsearch='endDate']").val()) == "") ? endDate_ASP : $.trim($("input[fullsearch='endDate']").val());

        url += "?dossierId=" + dossierId_ASP
        url += "&type=" + this.id;
      
        url += "&beginDate=" + beginDate;
        url += "&endDate=" + endDate;

      
        window.location.href = url;

    }

    function deleteEmptyCategories()
    {
        
        var urlTest = actionLinkDeleteEmptyCategories_ASP;

        urlTest += "?dossierId=" + dossierId_ASP
        urlTest += "&beginDate=" + beginDate_ASP;
        urlTest += "&endDate=" + endDate_ASP;

        window.location.href = urlTest;

    }

    function mergeCategories() {
        var recordCategoryId_FROM = $("#recordCategoryId_FROM option:selected").val();
        var recordCategoryId_TO = $("#recordCategoryId_TO option:selected").val();
        var recordSubcategoryId_FROM = $("#recordSubcategoryId_FROM option:selected").val();
        var recordSubcategoryId_TO = $("#recordSubcategoryId_TO option:selected").val();

        console.log("FROM: " + recordCategoryId_FROM);
        console.log("TO: " + recordCategoryId_TO);

//        var typeMerge = this.id;

        var typeMerge = "";

        if (recordCategoryId_FROM == "" || recordCategoryId_TO == "") return;

        if (recordSubcategoryId_FROM == "" && recordSubcategoryId_TO == "")
            typeMerge = "mergeCategories";


        if (recordSubcategoryId_FROM != "" && recordSubcategoryId_TO=="")
            typeMerge = "moveSubcategory";

        if (recordSubcategoryId_FROM != "" && recordSubcategoryId_TO != "")
            typeMerge = "mergeSubcategories";




        switch (typeMerge) {
            case "mergeCategories":
                if (recordCategoryId_FROM == recordCategoryId_TO)
                    return;
                break;

            case "mergeSubcategories":
            case "moveSubcategory":
                if (recordSubcategoryId_FROM == recordSubcategoryId_TO )
                    return;

                break;

        }

        var urlTest = actionLinkMergeCategories_ASP;

        urlTest += "?dossierId=" + dossierId_ASP
        urlTest += "&type=" + typeMerge;
        if (recordCategoryId_TO != null)
            urlTest += "&recordCategoryId_TO=" + recordCategoryId_TO;
        if (recordCategoryId_FROM != null)
            urlTest += "&recordCategoryId_FROM=" + recordCategoryId_FROM;
        if (recordSubcategoryId_TO != null)
            urlTest += "&recordSubcategoryId_TO=" + recordSubcategoryId_TO;
        if (recordSubcategoryId_FROM != null)
            urlTest += "&recordSubcategoryId_FROM=" + recordSubcategoryId_FROM;

        urlTest += "&beginDate=" + beginDate_ASP;
        urlTest += "&endDate=" + endDate_ASP;



        window.location.href = urlTest;

    }

    function showEditPanel(data)
    {
        $('#editRecord').html(data);
    }

    function successUpdate(data)
    {




        var url = actionLink_ASP;
        url += "?dossierId=" + dossierId_ASP
        url += "&TotalNumberOfPages=" + TotalNumberOfPages_ASP
        url += "&CurrentPage=" + CurrentPage_ASP


    //    url += "&beginDate=" + beginDate;
    //    url += "&endDate=" + endDate;
        

        window.location.href = url;


        return;



        //var recordId = $("#form_edit_recordId").val()

        ////Updating record with new value (In order to avoid a server refresh)


        ////Hidden TR attributes
        //$("#tr_" + recordId).attr('subcategoryId', $("#recordSubcategoryId option:selected").val())
        //$("#tr_" + recordId).attr('expense', $.trim($("#expense").val()));
        //$("#tr_" + recordId).attr('revenue', $.trim($("#revenue").val()));
        //$("#tr_" + recordId).attr('category', $.trim($("#category").val()));
        //$("#tr_" + recordId).attr('subcategory', $.trim($("#category").val()));

        ////Line shown to user
        //$("#Date_" + recordId).text($.trim($("#date").val()));
        //$("#Expense_" + recordId).text($.trim($("#expense").val()) + " €");
        //$("#Revenue_" + recordId).text($.trim($("#revenue").val()) + " €");
        //$("#Comment_" + recordId).text($.trim($("#comment").val()));
        //$("#Description_" + recordId).text($.trim($("#description").val()));
        //$("#Category_" + recordId).text($.trim($("#category").val()));
        //$("#Subcategory_" + recordId).text($.trim($("#subcategory").val()));


        ////TODO: refresh categories
        //$.ajax({    url: UrlCategories_ASP,
        //            type: 'get',
        //            data: { dossierId: dossierId_ASP },
        //            success: refreshCategories
        //        });


        ////TODO: refresh subcategories
        //$.ajax({
        //            url: UrlSubcategories_ASP,
        //            type: 'get',
        //            data: { dossierId: dossierId_ASP },
        //            success: refreshSubcategories
        //        });


        //FINAL DECISION: REFRESHING THE PAGE 


    }

    function refreshCategories(data)
    {
        console.log(data);
        $('#recordCategoryId').html(data);
        $('#recordCategoryId_TO').html(data);
        $('#recordCategoryId_FROM').html(data);
    }

    function refreshSubcategories(data)
    {
        console.log(data);
        $('#recordSubcategoryId').html(data);
        $('#recordSubcategoryId_TO').html(data);
        $('#recordSubcategoryId_FROM').html(data);

    }

    function errorUpdate(data, exception)
    {
  
    }

    function fullSearch()
    {

        var url = UrlSearch_ASP
    
        url += "?dossierId=" + dossierId_ASP
        url += "&beginDate=" + $.trim($("#beginDate").val());
        url += "&endDate=" + $.trim($("#endDate").val());
        url += "&recordCategoryId=" + $("#recordCategoryId option:selected").val();
        url += "&recordSubcategoryId=" + $("#recordSubcategoryId option:selected").val();
        url += "&description=" + $.trim($("#description").val());
        url += "&comment=" + $.trim($("#comment").val());

        window.location.href = url;

    }

    function SaveUpdateCreate(event)
    {
        console.log('SaveUpdateCreate');
        var actiontype = $("#btnSaveUpdateCreate").attr("actiontype");
        console.log(actiontype);
        if (actiontype == "search")
        {
            fullSearch();
            return;
        }

        var id = $(event.target).attr('id');

        var create = ($("#form_edit_recordId").val() == "0");


        //If category (and subcategory) from the textbox differ from the test of the select the id is set to 0
        var recordCategoryId = ($.trim($("#recordCategoryId option:selected").text()) == $.trim($('#category').val())) ? $("#recordCategoryId option:selected").val() : 0;
        var recordSubcategoryId = ($.trim($("#recordSubcategoryId option:selected").text()) == $.trim($('#subcategory').val())) ? $("#recordSubcategoryId option:selected").val() : 0;

        //if category is new, subcategory needs to be created as well; subcategories with same name can belong to different categories
        if (recordCategoryId == 0) recordSubcategoryId = 0;

        var url;
        console.log(actiontype);
        switch (actiontype)
        {
            case "create":
                url = UrlCreateAjax_ASP;
                break;
            case "edit":
                url = UrlEditAjax_ASP;
                break;
               
        }
        //edit or create
        //url = (create) ? UrlCreateAjax_ASP : UrlEditAjax_ASP;
        $("#form_edit").attr('action', url);


        
        //TODO: check if form is valid
        $.ajax
        (
            {
                url: url,
                type: 'post',
                success: successUpdate,
                error: errorUpdate,
                data:
                {
                    dossierId: $("#form_edit_dossierId").val(),
                    recordId: $("#form_edit_recordId").val(),
                    recordCategoryId: recordCategoryId,
                    recordSubcategoryId: recordSubcategoryId,
                    date: $.trim($("#date").val()),
                    category: $.trim($("#category").val()),
                    subcategory: $.trim($("#subcategory").val()),
                    expense: $.trim($("#expense").val()),
                    revenue: $.trim($("#revenue").val()),
                    description: $.trim($("#description").val()),
                    comment: $.trim($("#comment").val()),

                }
            }
        );

    }   
   
    function showCreate(event)
    {
        $("#btnSaveUpdateCreate").attr("actiontype", "create");
     
        //extracting data from TR
        var id = $(event.target).attr('id');

        var title = (id == "createExpense") ? "Create Expense" : "Create Revenue";
        $("#ModalEditCreateTitle").text(title);


        //Hiding expense if it is a revenue
        var expense = (id == "createExpense")?"1,00":"0,00";
        var revenue = (id == "createRevenue") ? "1,00" : "0,00";

        
        var isExpense = (id == "createExpense");

        showHideExpenseRevenue(expense, revenue);

        expense = "0,00";
        revenue = "0,00";

        SetFieldValuesCreate(expense, revenue, 0, isExpense );

    }

    function SetFieldValuesCreate(expense, revenue, recordId, isExpense)
    {

        $('#ErrorMessage').append('In SetFieldValuesCreate ');
        $("#recordSubcategoryId").val('').change();
        $("#recordCategoryId").val('').change();

        $('#ErrorMessage').append('isexpense:' + isExpense);

        $("#recordCategoryId option").hide();
        var isExpStr=(isExpense) ? "true" : "false";
        $("#recordCategoryId option[isExpense='" + isExpStr + "']").show();

        //hidden fields
        $("#form_edit_recordId").val(recordId);

        //text boxes

        //Set today's date
        $("#date").val(FormatDate(new Date()));

        $("#expense").val(expense);
        $("#revenue").val(revenue);
        $("#description").val('');
        $("#comment").val('');

        $("#category").val('');
        $("#subcategory").val('');

    }
   
    function FormatDate(inputDate)
    {
        var dd = inputDate.getDate();
        var mm = inputDate.getMonth() + 1; //January is 0!
        var yyyy = inputDate.getFullYear();
        if (dd < 10) 
            dd = '0' + dd;
        
        if (mm < 10) 
            mm = '0' + mm;
        
        return dd + '/' + mm + '/' + yyyy;
    }

    function hideSearchInputs()
    {
        $('#divBeginDate').hide();
        $('#divEndDate').hide();

        $('#divExpense').show();
        $('#divRevenue').show();
        $('#divDate').show();
        $('#category').show();
        $('#subcategory').show();
        $("#recordCategoryId").show();
    }

    function hideEditInputs()
    {
        $('#divExpense').hide();
        $('#divRevenue').hide();
        $('#divDate').hide();
        $('#category').hide();
        $('#subcategory').hide();
        $("#recordCategoryId").val('').change();

        $('#divBeginDate').show();
        $('#divEndDate').show();


    }

    function showEdit(event)
    {
        $("#btnSaveUpdateCreate").attr("actiontype", "edit");

        $("#ModalEditCreateTitle").text("Edit Record");
        console.log('in show edit');
        
        hideSearchInputs();
        
        //extracting data from TR
        var id = $(event.target).attr('id');
        var recordId = id.split("_")[1];

            //Hiding expense if it is a revenue
        var expense = $("#tr_" + recordId).attr('expense');
        var revenue = $("#tr_" + recordId).attr('revenue');
        expense = (expense == "0,00") ? expense : expense.substring(0, expense.length - 2);
        revenue = (revenue == "0,00") ? revenue : revenue.substring(0, revenue.length - 2);

        showHideExpenseRevenue(expense, revenue);

        var edit = (id.split("_")[0] == "btnEdit");

        if (edit)
            SetFieldValues(expense, revenue, recordId);

        //$('#ModalEditCreate').modal('show');


    }

    function showSearchTab()
    {
        $("#btnSaveUpdateCreate").attr("actiontype", "search");

        $("#ModalEditCreateTitle").text("Search");
        $("#btnSaveUpdateCreate").text("Search");

        hideEditInputs();
    }

    function showHideExpenseRevenue(expense, revenue)
    {

        hideSearchInputs();

        //Hiding expense if it is a revenue
        if (expense == "0,00")
        {
            $('#divExpense').hide();
            $('#divRevenue').show();

        }
        //Hiding revenue if it is an expense
        if (revenue == "0,00")
        {
            $('#divRevenue').hide();
            $('#divExpense').show();
        }
    }

    function SetFieldValues(expense, revenue, recordId)
    {

        var isExpense = (revenue == "0,00");

        //dropdown lists
        var subcategoryId = $.trim($("#tr_" + recordId).attr('subcategoryId'));
        var subcategoryId = $("#tr_" + recordId).attr('subcategoryId');
        $("#recordSubcategoryId").val(subcategoryId).change();
        var categoryId = $("#recordSubcategoryId option:selected").attr('categoryId');

        var isExpStr=(isExpense) ? "true" : "false";
        $("#recordCategoryId option").hide();
        $("#recordCategoryId option[isExpense='" + isExpStr + "']").show();
        $("#recordCategoryId option[value='']").show();

        $("#recordCategoryId").val(categoryId).change();

        $("#recordSubcategoryId").val(subcategoryId).change();

        //hidden fields
        $("#form_edit_recordId").val(recordId);

        //text boxes
        $("#date").val($.trim($("#Date_" + recordId).text()));
        $("#expense").val(expense);
        $("#revenue").val(revenue);
        $("#description").val($.trim($("#Description_" + recordId).text()));
        $("#comment").val($.trim($("#Comment_" + recordId).text()));

        $("#category").val($.trim($("#Category_" + recordId).text()));
        $("#subcategory").val($.trim($("#Subcategory_" + recordId).text()));

    }

    function resetToFrom(){}

    function changeCategoryToFrom()
    {
        var ToFrom = this.id.split('_')[1];

        console.log('ToFrom: ' + ToFrom);

        var categoryId = $("#recordCategoryId_" + ToFrom + " option:selected").val();

        $("#recordSubcategoryId_" + ToFrom  + " option").hide();

        if (categoryId != "")
            $("#recordSubcategoryId_" + ToFrom + " option[categoryid='" + categoryId + "']").show();

        $("#recordSubcategoryId_" + ToFrom + " option[value='']").show();

    }

    function changeCategory()
    {
        var categoryId = $("#recordCategoryId option:selected").val();
        $("#recordSubcategoryId option").hide();

        if (categoryId!="")
            $("#recordSubcategoryId option[categoryid=" + categoryId + "]").show();

        $("#recordSubcategoryId option[value='']").show();


        var category = $.trim($("#recordCategoryId option:selected").text());
        category = (category == "*") ? "" : category;
        $("#category").val(category);


        $("#subcategory").val("");

        $("#recordSubcategoryId").val('').change();
    }

    function changeSubcategory()
    {
        var subcategory = $.trim($("#recordSubcategoryId option:selected").text());
        subcategory = (subcategory == "*") ? "" : subcategory;
        $("#subcategory").val(subcategory);
    }

    function selectIncomeLine()
    {

        var category = $(this).attr('category');
        var subcategory = $(this).attr('subcategory');
        var isExpense = $(this).attr('isExpense');
        var beginDate = $(this).attr('beginDate');
        var endDate = $(this).attr('endDate');

        //Set parameters for a fullsearch and start it programmatically

        var categoryId = $("#recordCategoryId option[description='" + category + "'][isExpense='" + isExpense + "']").attr("value");
        var subcategoryId = $("#recordSubcategoryId option[description='" + subcategory + "'][categoryId='" + categoryId + "']").attr("value");


        var url = UrlSearch_ASP

        url += "?dossierId=" + dossierId_ASP
        url += "&beginDate=" + beginDate;
        url += "&endDate=" + endDate;
        url += "&recordCategoryId=" + categoryId;
        if (subcategoryId!=null)
            url += "&recordSubcategoryId=" + subcategoryId;
        

        window.location.href = url;


    }

    function hideRevenuesCat()
    {
        console.log('in hideRevenuesCat ');
        resetToFromValues();
        $("#recordCategoryId_TO option[isExpense='true']").show()
        $("#recordCategoryId_FROM option[isExpense='true']").show();
     
    }
   
    function hideExpensesCat()
    {
        console.log('in hideExpensesCat ');

        resetToFromValues();

        $("#recordCategoryId_TO option[isExpense='false']").show()
        $("#recordCategoryId_FROM option[isExpense='false']").show();

    }

    function resetToFromValues()
    {
        $("#recordCategoryId_TO option").hide();
        $("#recordCategoryId_FROM option").hide();

        $("#recordSubcategoryId_TO option").hide();
        $("#recordSubcategoryId_FROM option").hide();

        $("#recordCategoryId_TO option[value='']").show()
        $("#recordCategoryId_FROM option[value='']").show();

        $("#recordSubcategoryId_TO option[value='']").show()
        $("#recordSubcategoryId_FROM option[value='']").show();

        $("#recordCategoryId_TO").val('').change();
        $("#recordCategoryId_FROM").val('').change

        $("#recordSubcategoryId_TO").val('').change();
        $("#recordSubcategoryId_FROM").val('').change();
 
    }

    function main()
    {
        $(document).on('click', "tr[type^='trIncome']", selectIncomeLine);
        //$(document).on('click', ".category", selectIncomeLine);


       // $(document).on('click', '#btnCreateReport', createReport);

        $(document).on('click', '#btnSaveUpdateCreate', SaveUpdateCreate);
        $(document).on('focusout', '#txtCurrentPage', gotoPage);
        $(document).on('click', "a[id^='edit']", showEdit);
        //$(document).on('click', "a[id^='editModal']", showEdit);
        $(document).on('mouseenter', "button[id^='btnEdit']", showEdit);


        $(document).on('mouseenter', '#createExpense', showCreate);
        $(document).on('mouseenter', '#createRevenue', showCreate);
        $(document).on('mouseenter', '#startNewSearch', showSearchTab);

        $(document).on('change', "#recordCategoryId", changeCategory);
        $(document).on('change', "#recordSubcategoryId", changeSubcategory);

        $(document).on('change', "select[id^='recordCategoryId_TO']", changeCategoryToFrom);
        $(document).on('change', "select[id^='recordCategoryId_FROM']", changeCategoryToFrom);

        $(document).on('mouseenter', '#ManageRevenuesCategories', hideExpensesCat);
        $(document).on('mouseenter', '#ManageExpensesCategories', hideRevenuesCat);

        //$(document).on('click', '#createExpense', showCreate);
        //$(document).on('click', '#createRevenue', showCreate);
       
        $(document).on('click', '#startQuickSearch', startQuickSearch);

        $(document).on('click', '#mergeCatSubcat', mergeCategories);
        //$(document).on('click', '#mergeSubcategories', mergeCategories);
        //$(document).on('click', '#moveSubcategory', mergeCategories);
        $(document).on('click', '#deleteEmptyCategories', deleteEmptyCategories);
        $(document).on('click', '#newReport', deleteEmptyCategories);


      //  $(document).on('click', "tr[type^='trIncome']", selectIncomeLine);



        //Date pickers mess up events !!!!
        $("#beginDate").datepicker({dateFormat: 'dd/mm/yy' });
        $("#endDate").datepicker({dateFormat: 'dd/mm/yy' });
        $("#date").datepicker({dateFormat: 'dd/mm/yy' });

        $("input[fullsearch='beginDate']").datepicker({dateFormat: 'dd/mm/yy' });
        $("input[fullsearch='endDate']").datepicker({dateFormat: 'dd/mm/yy' });

       
        

    }

    $(document).ready(main);



