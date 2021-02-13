"use strict";

// Class Definition
var KTLoginGeneral = function() {

    var login = $('#kt_login');

    var showErrorMsg = function(form, type, msg) {
        var alert = $('<div class="alert alert-' + type + ' alert-dismissible" role="alert">\
			<div class="alert-text">'+msg+'</div>\
			<div class="alert-close">\
                <i class="flaticon2-cross kt-icon-sm" data-dismiss="alert"></i>\
            </div>\
		</div>');

        form.find('.alert').remove();
        alert.prependTo(form);
        //alert.animateClass('fadeIn animated');
        KTUtil.animateClass(alert[0], 'fadeIn animated');
        alert.find('span').html(msg);
    }

    // Private Functions
    var displaySignUpForm = function() {
        login.removeClass('kt-login--forgot');
        login.removeClass('kt-login--signin');

        login.addClass('kt-login--signup');
        KTUtil.animateClass(login.find('.kt-login__signup')[0], 'flipInX animated');
    }

    var displaySignInForm = function() {
        login.removeClass('kt-login--forgot');
        login.removeClass('kt-login--signup');

        login.addClass('kt-login--signin');
        KTUtil.animateClass(login.find('.kt-login__signin')[0], 'flipInX animated');
        //login.find('.kt-login__signin').animateClass('flipInX animated');
    }

    var displayForgotForm = function() {
        login.removeClass('kt-login--signin');
        login.removeClass('kt-login--signup');

        login.addClass('kt-login--forgot');
        //login.find('.kt-login--forgot').animateClass('flipInX animated');
        KTUtil.animateClass(login.find('.kt-login__forgot')[0], 'flipInX animated');

    }

    var handleFormSwitch = function() {
        $('#kt_login_forgot').click(function(e) {
            e.preventDefault();
            displayForgotForm();
        });

        $('#kt_login_forgot_cancel').click(function(e) {
            e.preventDefault();
            displaySignInForm();
        });

        $('#kt_login_signup').click(function(e) {
            e.preventDefault();
            displaySignUpForm();
        });

        $('#kt_login_signup_cancel').click(function(e) {
            e.preventDefault();
            displaySignInForm();
        });
    }

    var handleSignInFormSubmit = function() {
        $('#kt_login_signin_submit').click(function(e) {
            e.preventDefault();
            var btn = $(this);
            var form = $(this).closest('form');

            form.validate({
                rules: {
                    username: {
                        required: true,
                        minlength: 2
                        
                    },
                    password: {
                        required: true
                    }
                }
            });

            if (!form.valid()) {
                return;
            }

            $.ajax({
                type: 'POST',
                url: 'Login.aspx/authenticate',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{form:" + JSON.stringify($(form).serializeArray()) + "}",
                success: function(data) {
                    var obj = (data);
                    if(data.d == 'success'){
                         window.location = 'Default.aspx';
                    }else{
                        btn.removeClass('kt-spinner kt-spinner--right kt-spinner--sm kt-spinner--light').attr('disabled', false);
                        showErrorMsg(form, 'danger', data.d);
                  }
                           
                },
                error: function (xhr, status, error) {
                    // Boil the ASP.NET AJAX error down to JSON.
                    var err = eval("(" + xhr.responseText + ")");
                    //l.stop();
                    //App.unblockUI('#modal-body');
                    // Display the specific error raised by the server
                    alert(err.Message);
                }
            });

            btn.addClass('kt-spinner kt-spinner--right kt-spinner--sm kt-spinner--light').attr('disabled', true);

            
        });
    }

    var handleSignUpFormSubmit = function() {
        $('#kt_login_signup_submit').click(function(e) {
            e.preventDefault();

            var btn = $(this);
            var form = $(this).closest('form');

            form.validate({
                rules: {
                    fullname: {
                        required: true,
                        minlength: 2
                    },
                    creditcard: {
                        required: true,
                        creditcard: true
                    },
                    email: {
                        required: true,
                        email: true
                    },
                    password: {
                        required: true
                    },
                    rpassword: {
                        required: true
                    },
                    agree: {
                        required: true
                    }
                }
            });

            if (!form.valid()) {
                return;
            }

            btn.addClass('kt-spinner kt-spinner--right kt-spinner--sm kt-spinner--light').attr('disabled', true);

            $.ajax({
                type: 'POST',
                url: 'Login.aspx/InsertSupplier',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{form:" + JSON.stringify($(form).serializeArray()) + "}",
                success: function(data) {
                    var obj = (data);
                    btn.removeClass('kt-spinner kt-spinner--right kt-spinner--sm kt-spinner--light').attr('disabled', false);
                    if(data.d == 'success'){
                         form.clearForm();
	                     form.validate().resetForm();

	                    // display signup form
	                    displaySignInForm();
	                    var signInForm = login.find('.kt-login__signin form');
	                    signInForm.clearForm();
	                    signInForm.validate().resetForm();

	                    showErrorMsg(signInForm, 'success', 'Gracias, para completar tu registro, nos pondremos en contacto contigo.');
                    }else{
                        
                        showErrorMsg(form, 'danger', data.d);
                  }
                           
                },
                error: function (xhr, status, error) {
                    // Boil the ASP.NET AJAX error down to JSON.
                    var err = eval("(" + xhr.responseText + ")");
                    //l.stop();
                    //App.unblockUI('#modal-body');
                    // Display the specific error raised by the server
                    alert(err.Message);
                }
            });

        });
    }

    var handleForgotFormSubmit = function() {
        $('#kt_login_forgot_submit').click(function(e) {
            e.preventDefault();

            var btn = $(this);
            var form = $(this).closest('form');

            form.validate({
                rules: {
                    email: {
                        required: true,
                        email: true
                    }
                }
            });

            if (!form.valid()) {
                return;
            }

            btn.addClass('kt-spinner kt-spinner--right kt-spinner--sm kt-spinner--light').attr('disabled', true);

            form.ajaxSubmit({
                url: '',
                success: function(response, status, xhr, $form) {
                	// similate 2s delay
                	setTimeout(function() {
                		btn.removeClass('kt-spinner kt-spinner--right kt-spinner--sm kt-spinner--light').attr('disabled', false); // remove
	                    form.clearForm(); // clear form
	                    form.validate().resetForm(); // reset validation states

	                    // display signup form
	                    displaySignInForm();
	                    var signInForm = login.find('.kt-login__signin form');
	                    signInForm.clearForm();
	                    signInForm.validate().resetForm();

	                    showErrorMsg(signInForm, 'success', 'Cool! Password recovery instruction has been sent to your email.');
                	}, 2000);
                }
            });
        });
    }

    // Public Functions
    return {
        // public functions
        init: function() {
            handleFormSwitch();
            handleSignInFormSubmit();
            handleSignUpFormSubmit();
            handleForgotFormSubmit();
        }
    };
}();


function setLogLang(lang)
{
    $("#language").val(lang);
    if(lang==="EN")
        $("#langicon").attr("src","assets/media/flags/226-united-states.svg");
    else
        $("#langicon").attr("src","assets/media/flags/252-mexico.svg");
}

function changeLang(lang)
{
    if(lang=="EN")
    {
        $(".kt-login__title").html("Sign In");
        $('input[name ="username"]').attr("placeholder","Username");
        $('input[name ="password"]').attr("placeholder","Password");
        $('input[name ="remember"]').html("Remember me");
        $("#kt_login_forgot").html("Forgot password");
        $("#kt_login_signin_submit").html("Log-In");
        $(".kt-login__account-msg").html("Are you a supplier and want to Sign-In?");
        $("#kt_login_signup").html("Supplier Sign-In");
    }
}

// Class Initialization
jQuery(document).ready(function() {
    var lang=$("#language").val();
    if(lang==="EN")
    {
        changeLang("EN");
    }
    KTLoginGeneral.init();
    
});
