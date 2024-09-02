// Document ready function

var baseURL = "Account/";
var sortOrder = {
    Ascending: "asc",
    Descending: "desc",
}

var pages = {
    Login: "Login",
    Home: { Name: "Home", DisplayName: "Home" },
    Products: { Name: "Products", DisplayName: "Products" }

}

$(document).ready(function () {
    $('#products-link').click(function (e) {
        e.preventDefault();

        AjaxGetRequest('GetProducts');
    });
    $('#cart-link').click(function (e) {
        e.preventDefault();

        AjaxGetRequest('ViewCart');
    });
});


// Function to handle AJAX errors
function handleAjaxError(xhr, status, error) {
    console.log('AJAX error occurred:');
    console.log('Status: ' + status);
    console.log('Error: ' + error);
}

// Function to handle AJAX success
//function handleAjaxSuccess(data, status, xhr) {
//    console.log('AJAX request successful:');
//    console.log(data);
//    console.log('Status: ' + status);
//}

function AjaxGetRequest(action, queryParams) {
    $.ajax({
        type: 'GET',
        url: baseURL + action + '?' + $.param(queryParams),
        data: {},
        contentType: 'application/json; charset=utf-8',
        dataType: 'html',
        success: function (data) {
            $('#main-content').html(data); // Inject the HTML content into the main content area
            handleAjaxResponse(data);

        },
        error: handleAjaxError
    });
}

//function sendProductData(action, productData) {
//    $.ajax({
//        type: 'POST',
//        url: baseURL + action,  // Replace with your actual controller and action
//        contentType: 'application/json; charset=utf-8',
//        data: JSON.stringify(productData),
//        success: function (response) {
//            console.log('Product sent successfully:', response);
//            console.log(data);
//            // Optionally, handle the response from the server
//        }, // ends
//        error: function (xhr, status, error) {
//            console.error('Error sending product:', error);
//        } // ends
//    }); // ends
//} // ends

// Initialize event handlers once the DOM is ready
$(document).ready(function () {
    // Handle the "Products" link click
    $('#products-link').click(function (e) {
        e.preventDefault();
        AjaxGetRequest('GetProducts');
    }); // ends

    // Event listener for card clicks
    $(document).on('click', '.card', function () {
        var productData = {
            Id: $(this).data('id'),
            Name: $(this).data('name'),
            Price: $(this).data('price'),
            Quantity: $(this).data('quantity'),
            Priority: $(this).data('priority')
        };
        AjaxGetRequest("AddToCart", productData);
    }); // ends
}); 

function handleAjaxResponse(data) {
    $('#cart-container').html(data);
}

function sortCartItems(sortOrder) {
    AjaxGetRequest("SortCartItems", { sortOrder: sortOrder });
} 