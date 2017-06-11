$(document).ready(function() {

    $(".burger-nav").on("click", function () {

        $("header nav ul").toggleClass("open");
    });


});

//JqueryValidation på register form
//$("regcon").validate({
//    rules: {
//        email: {
//            required: true, //Feltet er required
//            email: true //Feltet skal have en email
//        },
//        password: 'required', //påkrævet field
//        confirmpassword: {
//            required: true, //påkrævet field
//            equalTo: ''//Vil ramme name: password (sammenlignes efter med det password ovenover)
//        }
//    },
//    messages: {
//        email:{
//            required: 'Please enter an email', //Custom beskeder til felterne
//            email: 'Please enter a valid email' //Custom beskeder til felterne
//        }
//    }
//});

//JavaScript metode
//function TestFunction() {
//    // Funktionalitet
//    document.getElementById("demo").innerHTML = "Hello world";
//}