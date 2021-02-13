<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<!DOCTYPE html>

<!--
-->
<html lang="en">

	<!-- begin::Head -->
	<head>
		<meta charset="utf-8" />
		<title>OneCarrier | Login</title>
		<meta name="description" content="Login page example">
		<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

		<!--begin::Fonts -->
		<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700|Roboto:300,400,500,600,700">

		<!--end::Fonts -->

		<!--begin::Page Custom Styles(used by this page) -->
		<link href="assets/css/pages/login/login-3.css" rel="stylesheet" type="text/css" />

		<!--end::Page Custom Styles -->

		<!--begin::Global Theme Styles(used by all pages) -->
		<link href="assets/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css" />
		<link href="assets/css/style.bundle.css" rel="stylesheet" type="text/css" />

		<!--end::Global Theme Styles -->

		<!--begin::Layout Skins(used by all pages) -->
		<link href="assets/css/skins/header/base/light.css" rel="stylesheet" type="text/css" />
		<link href="assets/css/skins/header/menu/light.css" rel="stylesheet" type="text/css" />
		<link href="assets/css/skins/brand/dark.css" rel="stylesheet" type="text/css" />
		<link href="assets/css/skins/aside/dark.css" rel="stylesheet" type="text/css" />

		<!--end::Layout Skins -->
		<link rel="shortcut icon" href="assets/media/logos/OneCarrier.ico" />
	</head>

	<!-- end::Head -->

	<!-- begin::Body -->
	<body class="kt-quick-panel--right kt-demo-panel--right kt-offcanvas-panel--right kt-header--fixed kt-header-mobile--fixed kt-subheader--enabled kt-subheader--fixed kt-subheader--solid kt-aside--enabled kt-aside--fixed kt-page--loading">

		<!-- begin:: Page -->
		<div class="kt-grid kt-grid--ver kt-grid--root">
			<div class="kt-grid kt-grid--hor kt-grid--root  kt-login kt-login--v3 kt-login--signin" id="kt_login">
				<div class="kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor" style="background-image: url(assets/media/bg/bg-3.jpg);">
					<div class="kt-grid__item kt-grid__item--fluid kt-login__wrapper">
						<div class="kt-login__container">
							<div class="kt-login__logo">
								<a href="#">
									<img src="assets/media/logos/logo_onecarrier.png">
								</a>
							</div>
							<div class="kt-login__signin">
								<div class="kt-login__head">
									<h3 class="kt-login__title">Acceder</h3>
								</div>
								<form class="kt-form" id="login-form" action="Login.aspx" method="post">
									<div class="input-group">
										<input class="form-control" type="text" placeholder="Usuario" name="username"  autocapitalize="none">
									</div>
									<div class="input-group">
										<input class="form-control" type="password" placeholder="Contraseña" name="password">
									</div>
									<div class="row kt-login__extra">
										<div class="col">
											<label class="kt-checkbox">
												<input type="checkbox" name="remember">Recuerdame<span></span>
											</label>
										</div>
										<div class="col kt-align-right">
											<a href="javascript:;" id="kt_login_forgot" class="kt-login__link">Olvidaste Contraseña ?</a>
										</div>
									</div>
									<div class="kt-login__actions">
                                         <!--begin: Language bar -->
                            <input type="hidden" id="language" value="<%if(lang=="EN"){Response.Write("EN");}else{Response.Write("ES");} %>">
							<div class="kt-header__topbar-item kt-header__topbar-item--langs" style="display : none">
								<div class="kt-header__topbar-wrapper" data-toggle="dropdown" data-offset="10px,0px">
									<span class="kt-header__topbar-icon">
										<img id="langicon" width="30px" class="" src="assets/media/flags/252-mexico.svg" alt="" />
									</span>
								</div>
								<div class="dropdown-menu dropdown-menu-fit dropdown-menu-right dropdown-menu-anim dropdown-menu-top-unround">
									<ul class="kt-nav kt-margin-t-10 kt-margin-b-10">
										<li class="kt-nav__item kt-nav__item--active">
											<a href="Login.aspx?lang=EN" class="kt-nav__link">
												<span class="kt-nav__link-icon"><img src="assets/media/flags/226-united-states.svg" alt="" /></span>
												<span class="kt-nav__link-text">English</span>
											</a>
										</li>
										<li class="kt-nav__item">
											<a href="Login.aspx" class="kt-nav__link">
												<span class="kt-nav__link-icon"><img src="assets/media/flags/252-mexico.svg" alt="" /></span>
												<span class="kt-nav__link-text">Spanish</span>
											</a>
										</li>
									</ul>
								</div>
							</div>

							<!--end: Language bar -->
                                        <div class="kt-space-20"></div>
										<button type="submit" id="kt_login_signin_submit" class="btn btn-brand btn-elevate kt-login__btn-primary">Entrar</button>
									</div>
								</form>
							</div>
                           
							<div class="kt-login__signup">
								<div class="kt-login__head">
									<h3 class="kt-login__title">Alta de Proveedores</h3>
									<div class="kt-login__desc">Indica tus datos para darte de Alta:</div>
								</div>
								<form class="kt-form" action="">
									<div class="input-group">
										<input class="form-control" type="text" placeholder="Nombre de la Empresa" name="fullname" style="text-transform:uppercase">
									</div>
                                    <div class="input-group">
										<input class="form-control" type="text" placeholder="Numero de proveedor" name="suppliernumber">
									</div>
                                    <div class="input-group">
										<input class="form-control" type="text" placeholder="Contacto" name="contact" style="text-transform:uppercase">
									</div>
                                    <div class="input-group">
										<input class="form-control" type="text" placeholder="Giro" name="giro" style="text-transform:uppercase">
									</div>
                                    <div class="input-group">
										<input class="form-control" type="text" placeholder="Teléfono" name="tel">
									</div>
                                    <div class="input-group">
										<select class="form-control kt-input" data-col-index="6" name="location">
										     <option value="">Localidad</option>
                                             <option value="1">MÉXICO</option>
                                             <option value="2">EXTRANJERO</option>
										</select>
									</div>
                                     <div class="input-group">
										<textarea class="form-control" style="height:100px;" rows="3" placeholder="Mensaje" name="msg" style="text-transform:uppercase"></textarea>
									</div>
									<div class="input-group">
										<input class="form-control" type="text" placeholder="Correo" name="email" autocomplete="off">
									</div>
									<div class="input-group">
										<input class="form-control" type="password" placeholder="Contraseña" name="password">
									</div>
									<div class="input-group">
										<input class="form-control" type="password" placeholder="Confirmar Contraseña" name="rpassword">
									</div>
									<div class="row kt-login__extra">
										<div class="col kt-align-left">
											<label class="kt-checkbox">
												<input type="checkbox" name="agree">Estoy de acuerdo <a href="#" class="kt-link kt-login__link kt-font-bold">terminos y condiciones</a>.
												<span></span>
											</label>
											<span class="form-text text-muted"></span>
										</div>
									</div>
									<div class="kt-login__actions">
										<button id="kt_login_signup_submit" class="btn btn-brand btn-elevate kt-login__btn-primary">Aceptar</button>&nbsp;&nbsp;
										<button id="kt_login_signup_cancel" class="btn btn-light btn-elevate kt-login__btn-secondary">Cancelar</button>
									</div>
								</form>
							</div>
							<div class="kt-login__forgot">
								<div class="kt-login__head">
									<h3 class="kt-login__title">Olvidaste tu contraseña ?</h3>
									<div class="kt-login__desc">Favor de contactar al representante para recuperar tu contraseña </div>
								</div>
								<form class="kt-form" action="">
									<div class="input-group">
										<!--<input class="form-control" type="text" placeholder="Email" name="email" id="kt_email" autocomplete="off">-->
									</div>
									<div class="kt-login__actions">
										<button id="kt_login_forgot_submit" class="btn btn-brand btn-elevate kt-login__btn-primary">Entrar</button>&nbsp;&nbsp;
										<button id="kt_login_forgot_cancel" class="btn btn-light btn-elevate kt-login__btn-secondary">Cancelar</button>
									</div>
								</form>
							</div>
							<div class="kt-login__account">
								<span class="kt-login__account-msg">
									
								</span>
								&nbsp;&nbsp;
								<a href="javascript:;" id="kt_login_signup" class="kt-login__account-link"></a>
							</div>
                            <div class="kt-login__account " <% if (HttpContext.Current.Request.Url.Host.ToLower() == "invoice.evoctus.com") { Response.Write("style=\"display:none\""); }  %>>
								<span class="kt-login__account-msg">
								</span>
								&nbsp;&nbsp;
								
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>

		<!-- end:: Page -->

		<!-- begin::Global Config(global config for global JS sciprts) -->
		<script>
		    var KTAppOptions = {
		        "colors": {
		            "state": {
		                "brand": "#5d78ff",
		                "dark": "#282a3c",
		                "light": "#ffffff",
		                "primary": "#5867dd",
		                "success": "#34bfa3",
		                "info": "#36a3f7",
		                "warning": "#ffb822",
		                "danger": "#fd3995"
		            },
		            "base": {
		                "label": [
							"#c5cbe3",
							"#a1a8c3",
							"#3d4465",
							"#3e4466"
						],
		                "shape": [
							"#f0f3ff",
							"#d9dffa",
							"#afb4d4",
							"#646c9a"
						]
		            }
		        }
		    };
		</script>

		<!-- end::Global Config -->

		<!--begin::Global Theme Bundle(used by all pages) -->
		<script src="assets/plugins/global/plugins.bundle.js" type="text/javascript"></script>
		<script src="assets/js/scripts.bundle.js" type="text/javascript"></script>

		<!--end::Global Theme Bundle -->

		<!--begin::Page Scripts(used by this page) -->
		<script src="assets/js/pages/custom/login/login-general.js" type="text/javascript"></script>

		<!--end::Page Scripts -->
	</body>

	<!-- end::Body -->
</html>