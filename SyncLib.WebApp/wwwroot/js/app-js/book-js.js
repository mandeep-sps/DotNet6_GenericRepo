
//URLs
let url_get_all_books = '/Book/GetBooks',
    url_get_book_by_id = '/Book/GetBookById',   //param : bookId
    url_add_book = '/Book/AddBook',             //param: form data
    url_update_book = '/Book/UpdateBookById',   //param: form data, bookId
    url_delete_book = '/Book/DeleteBookById',   //param: bookId

    url_get_dataList_authors = '/Book/GetAuthorsDataList',
    url_get_dataList_categories = '/Book/GetCategoriesDataList',
    url_get_dataList_languages = '/Book/GetLanguagesDataList';

//Data Containers 
//DataList: Contains drodown data for dataList
let data_authors,
    data_categories,
    data_languages;

//Form Inputs
let txt_book_name = '#txt-book-name',
    txt_author_id = '#txt-author-id',
    txt_author_name = '#txt-author-name',
    txt_category_id = '#txt-category-id',
    txt_category_name = '#txt-category-name',
    txt_language_id = '#txt-language-id',
    txt_language_name = '#txt-language-name',
    txt_publish_date = '#txt-publish-date',
    txt_edition = '#txt-edition',
    txt_book_id = '#txt-book-id';

//input by name variables 
let name_author = 'AuthorName',
    id_author = 'AuthorId',
    name_category = 'CategoryName',
    id_category = 'CategoryId',
    name_language = 'LanguageName',
    id_language = 'LanguageId'


//Other variable
let book_table_id = '#table-books',
    book_form_id = '#book-add-update-form',
    dataList_authors_id = '#dataList-authors',
    dataList_categories_id = '#dataList-categories',
    dataList_languages_id = '#dataList-languages',
    book_form_modal_id = 'book-form-modal',
    book_form_modal_title = '#book-form-modal-title',
    book_form_modal;




$(document).ready(function () {
    InitializeBookView();
    FlexyForms.InitializeFormStyle(book_form_id);
})


async function InitializeBookView() {
    await LoadBooks();
    await loadDataLists();
}


async function LoadBooks() {
    $.get(url_get_all_books, function (response) {
        var columns = [
            { data: 'bookName' },
            { data: 'authorName' },
            { data: 'categoryName' },
            { data: 'language' },
            { data: 'publishedOn' },
            { data: 'edition' },
            {
                data: null, render: function (data) {
                    var actionsButtons = `<button onclick="loadBookForm(${data.id})" class="btn btn-sm btn-outline-primary mx-1"><i class='bx bx-edit-alt'></i></button>`;
                    actionsButtons += `<button onclick="confirmDelete(${data.id})" class="btn btn-sm btn-outline-danger"><i class='bx bx-trash'></i></button>`;
                    return actionsButtons;
                }
            },
        ]
        FlexyDataTable.CreateDataTable(book_table_id, response.data, columns);
    })
}



function loadBookForm(bookId) {
    FlexyForms.ResetForm(book_form_id);
    $(txt_book_id).val(bookId)
    book_form_modal = new bootstrap.Modal(document.getElementById(book_form_modal_id))
    $(book_form_modal_title).html('Add New')
    if (bookId) {
        $.get(url_get_book_by_id, `bookId=${bookId}`, function (response) {
            var book = response.data;
            $(txt_book_name).val(book.bookName)
            $(txt_author_name).val(book.authorName)
            $(txt_category_name).val(book.categoryName)
            $(txt_language_name).val(book.language)
            $(txt_publish_date).val(book.publishedOn)
            $(txt_edition).val(book.edition)
            $(book_form_modal_title).html(`Update <span class="text-orange">${book.bookName}</span>`);
        });
    }
    book_form_modal.show();
}

$(book_form_id).unbind().bind('submit', function (e) {
    e.preventDefault();
    var data = new FormData($(book_form_id)[0]);
    var url = data.get('Id') ? (url_update_book + '?bookId=' + data.get('Id')) : url_add_book
    FlexyForms.EnableLiveValidation(book_form_id, validateBookForm);
    if (validateBookForm()) {
        debugger

        //Setting values instead of name of datalist
        data.set(id_author, getDataListValue(dataList_authors_id, name_author));
        data.set(id_category, getDataListValue(dataList_categories_id, name_category))
        data.set(id_language, getDataListValue(dataList_languages_id, name_language))

        $.ajax({
            url: url,
            data: data,
            processData: false,
            contentType: false,
            type: 'POST',
            success: function (response) {
                if (response.statusCode == 200) {
                    book_form_modal.hide();
                    InitializeBookView();
                    FlexyUtil.MessageBoxSuccess(response.message, 'Great!');
                }
                else
                    FlexyUtil.MessageBoxDanger(response.message, 'Oops!')

            },
            error: function (response) { console.log(response) }

        });
    }
});


function validateBookForm() {
    FlexyForms.ClearValidataionErrors(book_form_id)
    FlexyForms.BlankInputChecks([txt_book_name, txt_author_name, txt_category_name, txt_language_name, txt_edition])
    FlexyForms.ValidateInput(txt_edition, !$(txt_edition).val(), 'Edition must be greater than 0');
    if ($(txt_author_name).val())
        FlexyForms.ValidateInput(txt_author_name, !(FlexyUtil.IsExistInDataList($(txt_author_name).val(), data_authors)), `'${$(txt_author_name).val()}' will be added as new author!`, 'info');

    if ($(txt_category_name).val())
        FlexyForms.ValidateInput(txt_category_name, !(FlexyUtil.IsExistInDataList($(txt_category_name).val(), data_categories)), `'${$(txt_category_name).val()}' will be added as new category!`, 'info');

    if ($(txt_language_name).val())
        FlexyForms.ValidateInput(txt_language_name, !(FlexyUtil.IsExistInDataList($(txt_language_name).val(), data_languages)), `'${$(txt_language_name).val()}' will be added as new language!`, 'info');
    return FlexyForms.FormValidationStatus(book_form_id);
}


function getDataListValue(list, name) {
    return $(`${list} option[value="${$(`input[name='${name}']`).val()}"]`)[0]?.dataset.value || 0
}



async function loadDataLists() {
    $.get(url_get_dataList_authors, function (response) {
        data_authors = response.data;
        $(dataList_authors_id).empty();
        $.each(data_authors, function (index, item) {
            $(dataList_authors_id).append(`<option data-value="${item.id}" value="${item.name}"></option>`);
        })
    });

    $.get(url_get_dataList_categories, function (response) {
        data_categories = response.data;
        $(dataList_categories_id).empty();
        $.each(data_categories, function (index, item) {
            $(dataList_categories_id).append(`<option data-value="${item.id}" value="${item.name}"></option>`);
        })
    });

    $.get(url_get_dataList_languages, function (response) {
        data_languages = response.data;
        $(dataList_languages_id).empty();
        $.each(data_languages, function (index, item) {
            $(dataList_languages_id).append(`<option data-value="${item.id}" value="${item.name}"></option>`);
        })
    });

}



function confirmDelete(bookId) {
    FlexyUtil.ConfirmDialogue('Are you sure to remove this book from library?',
        {
            heading: 'Books are uniquely portable magic!',
            heading_icon: '<i class="fas fa-magic"></i>',
            buttonText: ['<i class="far fa-frown"></i> Yes', '<i class="far fa-grin"></i> No']
        }, function () {
            $.get(url_delete_book, 'bookId=' + bookId, function (response) {
                if (response.statusCode === 200) {
                    InitializeBookView();
                    FlexyUtil.MessageBoxSuccess(response.message, 'Successfull!')
                }
                else
                    FlexyUtil.MessageBoxDanger(response.message, 'Oops!')
            })
        })
}

function checkAuthor() {
    var message = $(txt_author_name).val() && !FlexyUtil.IsExistInDataList($(txt_author_name).val(), data_authors) ? (`'${$(txt_author_name).val()}' will be added as new author!`) : '';
    FlexyForms.SetMessage(txt_author_name, message, 'info');
}


function checkCategory() {
    var message = $(txt_category_name).val() && !FlexyUtil.IsExistInDataList($(txt_category_name).val(), data_categories) ? (`'${$(txt_category_name).val()}' will be added as new category!`) : '';
    FlexyForms.SetMessage(txt_category_name, message, 'info');
}


function checkLanguage() {
    var message = $(txt_language_name).val() && !FlexyUtil.IsExistInDataList($(txt_language_name).val(), data_languages) ? (`'${$(txt_language_name).val()}' will be added as new language!`) : '';
    FlexyForms.SetMessage(txt_language_name, message, 'info');
}