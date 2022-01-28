<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>


<!DOCTYPE html>

<!--
-->
<html lang="en">

	<!-- begin::Head -->
	<head>
		
		<meta charset="utf-8" />
		<title>One Carrier</title>
		<meta name="description" content="Page with empty content">
		<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

		<!--begin::Fonts -->
		<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700|Roboto:300,400,500,600,700">

		<!--end::Fonts -->

		<!--begin::Page Vendors Styles(used by this page) -->
		<link href="assets/plugins/custom/fullcalendar/fullcalendar.bundle.css" rel="stylesheet" type="text/css" />
        <link href="assets/plugins/custom/datatables/datatables.bundle.css" rel="stylesheet" type="text/css" />
        <link href="assets/plugins/custom/uppy/uppy.bundle.css" rel="stylesheet" type="text/css" />
		<!--end::Page Vendors Styles -->

		<!--begin::Global Theme Styles(used by all pages) -->
		<link href="assets/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css" />
		<link href="assets/css/style.bundle.css?2" rel="stylesheet" type="text/css" />

		<!--end::Global Theme Styles -->

		<!--begin::Layout Skins(used by all pages) -->
		<link href="assets/css/skins/header/base/light.css" rel="stylesheet" type="text/css" />
		<link href="assets/css/skins/header/menu/light.css" rel="stylesheet" type="text/css" />
		<link href="assets/css/skins/brand/light.css" rel="stylesheet" type="text/css" />
		<link href="assets/css/skins/aside/light.css" rel="stylesheet" type="text/css" />

		<!--end::Layout Skins -->
		<link rel="shortcut icon" href="assets/media/logos/OneCarrier.ico" />
	</head>

	<!-- end::Head -->

	<!-- begin::Body -->
	<body class="kt-quick-panel--right kt-demo-panel--right kt-offcanvas-panel--right kt-header--fixed kt-header-mobile--fixed kt-subheader--enabled kt-subheader--fixed kt-subheader--solid kt-aside--enabled kt-aside--fixed  <%if(toggleOn){Response.Write("kt-brand--minimize kt-aside--minimize");} %> kt-page--loading">

		<!-- begin:: Page -->

		<!-- begin:: Header Mobile -->
		<div id="kt_header_mobile" class="kt-header-mobile  kt-header-mobile--fixed ">
			<div class="kt-header-mobile__logo">
				<a href="">
					<img alt="Logo" src="assets/media/logos/logo_dark_one.png" />
				</a>
			</div>
			<div class="kt-header-mobile__toolbar">
				<button class="kt-header-mobile__toggler kt-header-mobile__toggler--left" id="kt_aside_mobile_toggler"><span></span></button>
				<button class="kt-header-mobile__topbar-toggler" id="kt_header_mobile_topbar_toggler"><i class="flaticon-more"></i></button>
			</div>
		</div>

		<!-- end:: Header Mobile -->
		<div class="kt-grid kt-grid--hor kt-grid--root">
		
        <div class="kt-grid__item kt-grid__item--fluid kt-grid kt-grid--ver kt-page">

				<!-- begin:: Aside -->

				<!-- Uncomment this to display the close button of the panel
                <button class="kt-aside-close " id="kt_aside_close_btn"><i class="la la-close"></i></button>
                -->
				<div class="kt-aside  kt-aside--fixed  kt-grid__item kt-grid kt-grid--desktop kt-grid--hor-desktop" id="kt_aside">

					<!-- begin:: Aside -->
					<div class="kt-aside__brand kt-grid__item " id="kt_aside_brand">
						<div class="kt-aside__brand-logo">
							<a href="">
								<img alt="Logo" src="assets/media/logos/logo_dark_one.png"/>
							</a>
						</div>
						<div class="kt-aside__brand-tools">
							<button class="kt-aside__brand-aside-toggler" id="kt_aside_toggler">
								<span><svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1" class="kt-svg-icon">
										<g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
											<polygon points="0 0 24 0 24 24 0 24" />
											<path d="M5.29288961,6.70710318 C4.90236532,6.31657888 4.90236532,5.68341391 5.29288961,5.29288961 C5.68341391,4.90236532 6.31657888,4.90236532 6.70710318,5.29288961 L12.7071032,11.2928896 C13.0856821,11.6714686 13.0989277,12.281055 12.7371505,12.675721 L7.23715054,18.675721 C6.86395813,19.08284 6.23139076,19.1103429 5.82427177,18.7371505 C5.41715278,18.3639581 5.38964985,17.7313908 5.76284226,17.3242718 L10.6158586,12.0300721 L5.29288961,6.70710318 Z" fill="#000000" fill-rule="nonzero" transform="translate(8.999997, 11.999999) scale(-1, 1) translate(-8.999997, -11.999999) " />
											<path d="M10.7071009,15.7071068 C10.3165766,16.0976311 9.68341162,16.0976311 9.29288733,15.7071068 C8.90236304,15.3165825 8.90236304,14.6834175 9.29288733,14.2928932 L15.2928873,8.29289322 C15.6714663,7.91431428 16.2810527,7.90106866 16.6757187,8.26284586 L22.6757187,13.7628459 C23.0828377,14.1360383 23.1103407,14.7686056 22.7371482,15.1757246 C22.3639558,15.5828436 21.7313885,15.6103465 21.3242695,15.2371541 L16.0300699,10.3841378 L10.7071009,15.7071068 Z" fill="#000000" fill-rule="nonzero" opacity="0.3" transform="translate(15.999997, 11.999999) scale(-1, 1) rotate(-270.000000) translate(-15.999997, -11.999999) " />
										</g>
									</svg></span>
								<span><svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1" class="kt-svg-icon">
										<g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
											<polygon points="0 0 24 0 24 24 0 24" />
											<path d="M12.2928955,6.70710318 C11.9023712,6.31657888 11.9023712,5.68341391 12.2928955,5.29288961 C12.6834198,4.90236532 13.3165848,4.90236532 13.7071091,5.29288961 L19.7071091,11.2928896 C20.085688,11.6714686 20.0989336,12.281055 19.7371564,12.675721 L14.2371564,18.675721 C13.863964,19.08284 13.2313966,19.1103429 12.8242777,18.7371505 C12.4171587,18.3639581 12.3896557,17.7313908 12.7628481,17.3242718 L17.6158645,12.0300721 L12.2928955,6.70710318 Z" fill="#000000" fill-rule="nonzero" />
											<path d="M3.70710678,15.7071068 C3.31658249,16.0976311 2.68341751,16.0976311 2.29289322,15.7071068 C1.90236893,15.3165825 1.90236893,14.6834175 2.29289322,14.2928932 L8.29289322,8.29289322 C8.67147216,7.91431428 9.28105859,7.90106866 9.67572463,8.26284586 L15.6757246,13.7628459 C16.0828436,14.1360383 16.1103465,14.7686056 15.7371541,15.1757246 C15.3639617,15.5828436 14.7313944,15.6103465 14.3242754,15.2371541 L9.03007575,10.3841378 L3.70710678,15.7071068 Z" fill="#000000" fill-rule="nonzero" opacity="0.3" transform="translate(9.000003, 11.999999) rotate(-270.000000) translate(-9.000003, -11.999999) " />
										</g>
									</svg></span>
							</button>

							<!--
			<button class="kt-aside__brand-aside-toggler kt-aside__brand-aside-toggler--left" id="kt_aside_toggler"><span></span></button>
			-->
						</div>
					</div>

					<!-- end:: Aside -->

					<!-- begin:: Aside Menu -->
					<div class="kt-aside-menu-wrapper kt-grid__item kt-grid__item--fluid" id="kt_aside_menu_wrapper">
						<div id="kt_aside_menu" class="kt-aside-menu " data-ktmenu-vertical="1" data-ktmenu-scroll="1" data-ktmenu-dropdown-timeout="500">
							<ul class="kt-menu__nav ">
								<li class="kt-menu__item <%if(pageType=="dash"){Response.Write("kt-menu__item--active");} %>" aria-haspopup="true"><a href="?actn=dash" class="kt-menu__link "><i class="kt-menu__link-icon flaticon-home"></i><span class="kt-menu__link-text">Dashboard</span></a></li>
                                <% if (sessionType == "5")
                                   { %>
                                    <li class="kt-menu__item <%if(pageType=="docs"){Response.Write("kt-menu__item--active");} %>" aria-haspopup="true"><a href="?actn=docs" class="kt-menu__link "><i class="kt-menu__link-icon flaticon-interface-4"></i><span class="kt-menu__link-text">Carga Documento</span></a></li>
                                    <% }%>
                                 <% if (sessionType == "1" || sessionType == "2")
                                     { %>
                                    <li class="kt-menu__item <%if (pageType == "auth") { Response.Write("kt-menu__item--active"); } %>" aria-haspopup="true"><a href="?actn=auth" class="kt-menu__link "><i class="kt-menu__link-icon flaticon2-checkmark"></i><span class="kt-menu__link-text">Autorizar guías</span></a></li>
								    <li class="kt-menu__item <%if (pageType == "minvalmx") { Response.Write("kt-menu__item--active"); } %>" aria-haspopup="true"><a href="?actn=minvalmx" class="kt-menu__link "><i class="kt-menu__link-icon fa fa-file-invoice"></i><span class="kt-menu__link-text">Generar guía Almex</span></a></li>
                                    <% }%>
                                <% if (sessionType == "5")
                                    { %>
                                <li class="kt-menu__item <%if (pageType == "inv") { Response.Write("kt-menu__item--active"); } %>" aria-haspopup="true"><a href="?actn=inv" class="kt-menu__link "><i class="kt-menu__link-icon flaticon2-checking"></i><span class="kt-menu__link-text">Carga Facturas</span></a></li>
                                <% } %>
                                 <% if (sessionType == "")
                                     { %>
                                <hr />
                                <li class="kt-menu__item <%if (pageType == "minv") { Response.Write("kt-menu__item--active"); } %>" aria-haspopup="true">
                                    <a href="?actn=minv" class="" style="text-align:center">
                                        <i class="kt-menu__link-icon fa" style="min-width: 25%">
                                            <img src="assets/media/logos/Potosino.jpeg" style="min-width: 25%;min-height:25%;width:187px;width:98px" />
                                        </i>                                 
                                    </a>
                                    <a href="?actn=minv" class=""style="text-align:center">
                                        <i class="kt-menu__link-icon fa" style="min-width: 100%;">
                                            <span class="" style="min-width: 50%; font-family:'Arial Narrow';font-size:12px;text-align:center;color:#000000">Generar guía
                                            </span>
                                        </i>
                                    </a>
                                </li>
                                <hr />
                                <li class="kt-menu__item <%if (pageType == "minvalmx") { Response.Write("kt-menu__item--active"); } %>" aria-haspopup="true">
                                    <a href="?actn=minvalmx" class="" style="text-align:center">
                                        <i class="kt-menu__link-icon fa" style="min-width: 25%;">
                                            <img src="assets/media/logos/OneCarrier.jpeg" style="min-width: 25%;width:187px;width:98px"/>
                                        </i>
                                    </a>
                                    <a href="?actn=minvalmx" class="" style="text-align: center">
                                        <i class="kt-menu__link-icon fa" style="min-width: 100%;">
                                            <span class="" style="min-width: 50%; font-family:'Arial Narrow'; font-size: 12px; text-align: center; color: #000000">Generar guía
                                            </span>
                                        </i>
                                    </a>
                                </li>

                                <% } %>
                                <% if (sessionType == "5")
                                    { %>
                                <li class="kt-menu__item <%if (pageType == "sinv") { Response.Write("kt-menu__item--active"); } %>" aria-haspopup="true"><a href="?actn=sinv" class="kt-menu__link "><i class="kt-menu__link-icon flaticon-folder-1"></i><span class="kt-menu__link-text">Facturas</span></a></li>
                                <% } %>
                                <% if (sessionType == "5")
                                    { %>
                                <li class="kt-menu__item  kt-menu__item--submenu" aria-haspopup="true" data-ktmenu-submenu-toggle="hover"><a href="javascript:;" class="kt-menu__link kt-menu__toggle"><i class="kt-menu__link-icon flaticon2-next"></i><span class="kt-menu__link-text">Credito y cobranza</span><i class="kt-menu__ver-arrow la la-angle-right"></i></a>
									<div class="kt-menu__submenu "><span class="kt-menu__arrow"></span>
										<ul class="kt-menu__subnav">
											<li class="kt-menu__item  kt-menu__item--parent" aria-haspopup="true"><span class="kt-menu__link"><span class="kt-menu__link-text">Crédito y Cobranza</span></span></li>
											<li class="kt-menu__item " aria-haspopup="true"><a href="?actn=cxc" class="kt-menu__link "><i class="kt-menu__link-icon fa fa-file-invoice-dollar"><span></span></i><span class="kt-menu__link-text">Cuentas por cobrar</span></a></li>
                                            <li class="kt-menu__item " aria-haspopup="true"><a href="?actn=cob" class="kt-menu__link "><i class="kt-menu__link-icon fa fa-dollar-sign"><span></span></i><span class="kt-menu__link-text">Cobranza</span></a></li>
                                        </ul>
                                    </div>
                                </li>
                                <% } %>
							</ul>
						</div>
					</div>

					<!-- end:: Aside Menu -->
				</div>

				<!-- end:: Aside -->
				<div class="kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor kt-wrapper" id="kt_wrapper">

					<!-- begin:: Header -->
					<div id="kt_header" class="kt-header kt-grid__item  kt-header--fixed ">

						<!-- begin:: Header Menu -->

						<!-- Uncomment this to display the close button of the panel
                        <button class="kt-header-menu-wrapper-close" id="kt_header_menu_mobile_close_btn"><i class="la la-close"></i></button>
                        -->
						<div>
						</div>

						<!-- end:: Header Menu -->

						<!-- begin:: Header Topbar -->
						<div class="kt-header__topbar">

							<!--begin: Search -->

							
							<!--end: Search -->

							<!--begin: Notifications -->
							<div class="kt-header__topbar-item dropdown">
								<div class="kt-header__topbar-wrapper" data-toggle="dropdown" data-offset="30px,0px" aria-expanded="true">
									<span class="kt-header__topbar-icon kt-pulse kt-pulse--brand">
										<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1" class="kt-svg-icon">
											<g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
												<rect x="0" y="0" width="24" height="24" />
												<path d="M2.56066017,10.6819805 L4.68198052,8.56066017 C5.26776695,7.97487373 6.21751442,7.97487373 6.80330086,8.56066017 L8.9246212,10.6819805 C9.51040764,11.267767 9.51040764,12.2175144 8.9246212,12.8033009 L6.80330086,14.9246212 C6.21751442,15.5104076 5.26776695,15.5104076 4.68198052,14.9246212 L2.56066017,12.8033009 C1.97487373,12.2175144 1.97487373,11.267767 2.56066017,10.6819805 Z M14.5606602,10.6819805 L16.6819805,8.56066017 C17.267767,7.97487373 18.2175144,7.97487373 18.8033009,8.56066017 L20.9246212,10.6819805 C21.5104076,11.267767 21.5104076,12.2175144 20.9246212,12.8033009 L18.8033009,14.9246212 C18.2175144,15.5104076 17.267767,15.5104076 16.6819805,14.9246212 L14.5606602,12.8033009 C13.9748737,12.2175144 13.9748737,11.267767 14.5606602,10.6819805 Z" fill="#000000" opacity="0.3" />
												<path d="M8.56066017,16.6819805 L10.6819805,14.5606602 C11.267767,13.9748737 12.2175144,13.9748737 12.8033009,14.5606602 L14.9246212,16.6819805 C15.5104076,17.267767 15.5104076,18.2175144 14.9246212,18.8033009 L12.8033009,20.9246212 C12.2175144,21.5104076 11.267767,21.5104076 10.6819805,20.9246212 L8.56066017,18.8033009 C7.97487373,18.2175144 7.97487373,17.267767 8.56066017,16.6819805 Z M8.56066017,4.68198052 L10.6819805,2.56066017 C11.267767,1.97487373 12.2175144,1.97487373 12.8033009,2.56066017 L14.9246212,4.68198052 C15.5104076,5.26776695 15.5104076,6.21751442 14.9246212,6.80330086 L12.8033009,8.9246212 C12.2175144,9.51040764 11.267767,9.51040764 10.6819805,8.9246212 L8.56066017,6.80330086 C7.97487373,6.21751442 7.97487373,5.26776695 8.56066017,4.68198052 Z" fill="#000000" />
											</g>
										</svg> <span id="notifpulsate" class="kt-pulse__ring"></span>
									</span>

									<!--
                Use dot badge instead of animated pulse effect:
                <span class="kt-badge kt-badge--dot kt-badge--notify kt-badge--sm kt-badge--brand"></span>
            -->
								</div>
								<div class="dropdown-menu dropdown-menu-fit dropdown-menu-right dropdown-menu-anim dropdown-menu-top-unround dropdown-menu-lg">
									<form>

										<!--begin: Head -->
										<div class="kt-head kt-head--skin-dark kt-head--fit-x kt-head--fit-b" style="background-image: url(assets/media/misc/bg-1.jpg)">
											<h3 class="kt-head__title">
												Notificaciones
												&nbsp;
												<span class="btn btn-success btn-sm btn-bold btn-font-md"><span id="notifqty">3</span> nuevas</span>
											</h3>
											<ul class="nav nav-tabs nav-tabs-line nav-tabs-bold nav-tabs-line-3x nav-tabs-line-success kt-notification-item-padding-x" role="tablist">
												<li class="nav-item">
													<a class="nav-link active show" data-toggle="tab" href="#topbar_notifications_notifications" role="tab" aria-selected="true">Alertas</a>
												</li>
											</ul>
										</div>

										<!--end: Head -->
										<div class="tab-content">
											<div class="tab-pane active show" id="topbar_notifications_notifications" role="tabpanel">
												<div id="lstNotifications" class="kt-notification kt-margin-t-10 kt-margin-b-10 kt-scroll" data-scroll="true" data-height="300" data-mobile-height="200">
													<!--
                                                    <a href="#" class="kt-notification__item">
														<div class="kt-notification__item-icon">
															<i class="flaticon2-line-chart kt-font-success"></i>
														</div>
														<div class="kt-notification__item-details">
															<div class="kt-notification__item-title">
																Orden de Compra Registrada
															</div>
															<div class="kt-notification__item-time">
																hace 2 hrs 
															</div>
														</div>
													</a>
													<a href="#" class="kt-notification__item">
														<div class="kt-notification__item-icon">
															<i class="flaticon2-box-1 kt-font-brand"></i>
														</div>
														<div class="kt-notification__item-details">
															<div class="kt-notification__item-title">
																Fecha de pago registrada
															</div>
															<div class="kt-notification__item-time">
																hace 3 hrs
															</div>
														</div>
													</a>
													<a href="#" class="kt-notification__item">
														<div class="kt-notification__item-icon">
															<i class="flaticon2-chart2 kt-font-danger"></i>
														</div>
														<div class="kt-notification__item-details">
															<div class="kt-notification__item-title">
																Factura Pagada
															</div>
															<div class="kt-notification__item-time">
																hace 10 hrs
															</div>
														</div>
													</a>
                                                    -->
												</div>
											</div>
										</div>
									</form>
								</div>
							</div>

							<!--end: Notifications -->

							<!--begin: Quick Actions -->

							<!--end: Quick Actions -->

							<!--begin: My Cart -->

							<!--end: My Cart -->

							<!--begin: Quick panel toggler -->

							<!--end: Quick panel toggler -->

							<!--begin: Language bar -->
							<div class="kt-header__topbar-item kt-header__topbar-item--langs">
								<div class="kt-header__topbar-wrapper" data-toggle="dropdown" data-offset="10px,0px">
									<span class="kt-header__topbar-icon">
										<img class="" src="assets/media/flags/252-mexico.svg" alt="" />
									</span>
								</div>
								<div class="dropdown-menu dropdown-menu-fit dropdown-menu-right dropdown-menu-anim dropdown-menu-top-unround">
									<ul class="kt-nav kt-margin-t-10 kt-margin-b-10">
										<li class="kt-nav__item kt-nav__item--active">
											<a href="#" class="kt-nav__link">
												<span class="kt-nav__link-icon"><img src="assets/media/flags/226-united-states.svg" alt="" /></span>
												<span class="kt-nav__link-text">English</span>
											</a>
										</li>
										<li class="kt-nav__item">
											<a href="#" class="kt-nav__link">
												<span class="kt-nav__link-icon"><img src="assets/media/flags/252-mexico.svg" alt="" /></span>
												<span class="kt-nav__link-text">Spanish</span>
											</a>
										</li>
									</ul>
								</div>
							</div>

							<!--end: Language bar -->

							<!--begin: User Bar -->
							<div class="kt-header__topbar-item kt-header__topbar-item--user">
								<div class="kt-header__topbar-wrapper" data-toggle="dropdown" data-offset="0px,0px">
									<div class="kt-header__topbar-user">
										<span class="kt-header__topbar-welcome kt-hidden-mobile">Hola,</span>
										<span class="kt-header__topbar-username kt-hidden-mobile" id="username"><%  Response.Write(Session["nomusuario"]); %></span>
                                        <input type="hidden" value="<%  Response.Write(Session["userid"]); %>" id="userid" />
                                        <input type="hidden" value="<%  Response.Write(actn); %>" id="actn" />
                                        <input type="hidden" value="<%  Response.Write(prms); %>" id="params" />
										<img class="kt-hidden" alt="Pic" src="assets/media/users/300_25.jpg" />

										<!--use below badge element instead the user avatar to display username's first letter(remove kt-hidden class to display it) -->
										<span id="userbadge" class="kt-badge kt-badge--username kt-badge--unified-success kt-badge--lg kt-badge--rounded kt-badge--bold"><%  Response.Write(Session["nomusuario"].ToString().Substring(0,1));%></span>
									</div>
								</div>
								<div class="dropdown-menu dropdown-menu-fit dropdown-menu-right dropdown-menu-anim dropdown-menu-top-unround dropdown-menu-xl">

									<!--begin: Head -->
									<div class="kt-user-card kt-user-card--skin-dark kt-notification-item-padding-x" style="background-image: url(assets/media/misc/bg-1.jpg)">
										<div class="kt-user-card__avatar">
											<img class="kt-hidden" alt="Pic" src="assets/media/users/300_25.jpg" />

											<!--use below badge element instead the user avatar to display username's first letter(remove kt-hidden class to display it) -->
											<span class="kt-badge kt-badge--lg kt-badge--rounded kt-badge--bold kt-font-success">U</span>
										</div>
										<div class="kt-user-card__name">
											Usuario
										</div>
										<div class="kt-user-card__badge">
											<span class="btn btn-success btn-sm btn-bold btn-font-md"></span>
										</div>
									</div>

									<!--end: Head -->

									<!--begin: Navigation -->
									<div class="kt-notification">
										<div class="kt-notification__custom kt-space-between">
											<a href="Login.aspx" class="btn btn-label btn-label-brand btn-sm btn-bold">Cerrar Sesion</a>
											<a href="Login.aspx" class="btn btn-clean btn-sm btn-bold"></a>
										</div>
									</div>

									<!--end: Navigation -->
								</div>
							</div>

							<!--end: User Bar -->
						</div>

						<!-- end:: Header Topbar -->
					</div>

					<!-- end:: Header -->
					<div class="kt-content  kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor" id="kt_content">

						<!-- begin:: Subheader -->
						<div class="kt-subheader   kt-grid__item" id="kt_subheader">
							<div class="kt-container  kt-container--fluid ">
								<div class="kt-subheader__main">
									<h3 class="kt-subheader__title">
										<%if (pageType == "dash") { Response.Write("Dashboard"); } if (pageType == "auth") { Response.Write("Autorización"); }
                                          if (pageType == "inv") { Response.Write("Facturas de proveedores"); } if (pageType == "docs") { Response.Write("Alta documentos"); }
                                          if (pageType == "minv") { Response.Write("Generar guía"); } if (pageType == "sinv") { Response.Write("Facturas"); }
                                          if (pageType == "cxc") { Response.Write("Cuentas por cobrar"); } if (pageType == "cob") { Response.Write("Cobranza"); }
										  if (pageType == "minvalmx") { Response.Write("Generar guía Almex"); }%> </h3>
									<span class="kt-subheader__separator kt-hidden"></span>
									<div class="kt-subheader__breadcrumbs">
										<a href="?dash" class="kt-subheader__breadcrumbs-home"><i class="flaticon2-shelter"></i></a>
										<span class="kt-subheader__breadcrumbs-separator"></span>
										<a href="?dash" class="kt-subheader__breadcrumbs-link">
											Inicio </a>
										<span class="kt-subheader__breadcrumbs-separator"></span>
										<a href="#" class="kt-subheader__breadcrumbs-link">
											<%if (pageType == "dash") { Response.Write("Dashboard"); } if (pageType == "auth") { Response.Write("Autorización"); } 
											    if (pageType == "inv") { Response.Write("Facturas de proveedores"); } if (pageType == "docs") { Response.Write("Alta documentos"); }
                                                if (pageType == "minv") { Response.Write("Generar guía"); } if (pageType == "sinv") { Response.Write("Facturas"); }
                                                if (pageType == "cxc") { Response.Write("Cuentas por cobrar"); } if (pageType == "cob") { Response.Write("Cobranza"); }
												if (pageType == "minvalmx") { Response.Write("Generar guía Almex"); }%> </a>

										<!-- <span class="kt-subheader__breadcrumbs-link kt-subheader__breadcrumbs-link--active">Active link</span> -->
									</div>
								</div>
								<div class="kt-subheader__toolbar">
									<div class="kt-subheader__wrapper">
                                     <form id="forma_reporte2">
										<a href="javascript:;" class="btn kt-subheader__btn-daterange" id="kt_dashboard_daterangepicker" data-toggle="kt-tooltip" title="Seleccionar rango" data-placement="left">
											<span class="kt-subheader__btn-daterange-title" id="kt_dashboard_daterangepicker_title">Today</span>&nbsp;
											<span class="kt-subheader__btn-daterange-date" id="kt_dashboard_daterangepicker_date">Aug 16</span>

											<!--<i class="flaticon2-calendar-1"></i>-->
											<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1" class="kt-svg-icon kt-svg-icon--sm">
												<g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
													<rect x="0" y="0" width="24" height="24" />
													<path d="M4.875,20.75 C4.63541667,20.75 4.39583333,20.6541667 4.20416667,20.4625 L2.2875,18.5458333 C1.90416667,18.1625 1.90416667,17.5875 2.2875,17.2041667 C2.67083333,16.8208333 3.29375,16.8208333 3.62916667,17.2041667 L4.875,18.45 L8.0375,15.2875 C8.42083333,14.9041667 8.99583333,14.9041667 9.37916667,15.2875 C9.7625,15.6708333 9.7625,16.2458333 9.37916667,16.6291667 L5.54583333,20.4625 C5.35416667,20.6541667 5.11458333,20.75 4.875,20.75 Z" fill="#000000" fill-rule="nonzero" opacity="0.3" />
													<path d="M2,11.8650466 L2,6 C2,4.34314575 3.34314575,3 5,3 L19,3 C20.6568542,3 22,4.34314575 22,6 L22,15 C22,15.0032706 21.9999948,15.0065399 21.9999843,15.009808 L22.0249378,15 L22.0249378,19.5857864 C22.0249378,20.1380712 21.5772226,20.5857864 21.0249378,20.5857864 C20.7597213,20.5857864 20.5053674,20.4804296 20.317831,20.2928932 L18.0249378,18 L12.9835977,18 C12.7263047,14.0909841 9.47412135,11 5.5,11 C4.23590829,11 3.04485894,11.3127315 2,11.8650466 Z M6,7 C5.44771525,7 5,7.44771525 5,8 C5,8.55228475 5.44771525,9 6,9 L15,9 C15.5522847,9 16,8.55228475 16,8 C16,7.44771525 15.5522847,7 15,7 L6,7 Z" fill="#000000" />
												</g>
											</svg>

										</a>
                                            <div class="btn" onclick="getQuotes(true);")>

                                            </div>
										
                                          </form>
											<div class="dropdown-menu dropdown-menu-fit dropdown-menu-md dropdown-menu-right">

												<!--begin::Nav-->

												<!--end::Nav-->
											</div>
										
									</div>
								</div>
							</div>
						</div>

						<!-- end:: Subheader -->

						<!-- begin:: Content -->
						<div class="kt-container  kt-container--fluid  kt-grid__item kt-grid__item--fluid">
							
                          <!-- begin:: Dashboard -->
                          <div class="row" <%if(pageType!="xxx"){Response.Write("style=display:none");} %>> 
                            <div class="col-xl-<%if (sessionType == "1" || sessionType == "2" || sessionType == "3" || sessionType == "5"){Response.Write("6");}else{Response.Write("12");} %> order-lg-2 order-xl-1" >

									<!--begin:: Widgets/Revenue Change-->
									<div class="kt-portlet kt-portlet--height-fluid kt-portlet--mobile">
										<div class="kt-widget14">
                                            <div class="kt-widget14__header">
												<h3 class="kt-widget14__title">
													Estatus de Facturas
												</h3>
												<span class="kt-widget14__desc">
													<!--Submenu estatus facturas -->
												</span>
											</div>
											<div class="kt-widget14__content">
												<div class="kt-widget14__chart">
													<div id="kt_chart_revenue_change" style="height: 150px; width: 150px;"></div>
												</div>
												<div class="kt-widget14__legends">
                                                    <div class="kt-widget14__legend">
														<span class="kt-widget14__bullet kt-bg-primary"></span>
														<span id="stat_oc_pendent" class="kt-widget14__stats">OC Pendientes</span>
													</div>
													<div class="kt-widget14__legend">
														<span class="kt-widget14__bullet kt-bg-danger"></span>
														<span id="stat_pendent" class="kt-widget14__stats">Pendientes</span>
													</div>
													<div class="kt-widget14__legend">
														<span class="kt-widget14__bullet kt-bg-warning"></span>
														<span id="stat_valid" class="kt-widget14__stats">Validadas</span>
													</div>
													<div class="kt-widget14__legend">
														<span class="kt-widget14__bullet kt-bg-brand"></span>
														<span id="stat_programed" class="kt-widget14__stats">Programadas</span>
													</div>
                                                    <div class="kt-widget14__legend">
														<span class="kt-widget14__bullet kt-bg-success"></span>
														<span id="stat_paid" class="kt-widget14__stats">Pagadas</span>
													</div>
                                                    <div class="kt-widget14__legend">
														<span class="kt-widget14__bullet kt-bg-dark"></span>
														<span id="stat_total" class="kt-widget14__stats">Total</span>
													</div>
												</div>
											</div>
										</div>
									</div>
									<!--end:: Widgets/Revenue Change-->
								</div>
                            
                            <div class="col-xl-6 order-lg-2 order-xl-1" <%if (sessionType == "4"){Response.Write("style=display:none");} %>>

									<!--begin:: Widgets/Daily Sales-->
									<div class="kt-portlet kt-portlet--height-fluid">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">
													Top Ten Proveedores
												</h3>
											</div>
											<div class="kt-portlet__head-toolbar">
											</div>
										</div>
										<div class="kt-portlet__body kt-portlet__body--fluid">
											<div class="kt-widget12">
												<div class="kt-widget12__content">
													<table id="topten">
                                                    </table>
												</div>
											</div>
										</div>
									</div>

									<!--end:: Widgets/Daily Sales-->
								</div>
                           </div> 
                          <!-- end:: Dashboard -->

                          <!--Begin::Row-->
							<div class="row" <%if(pageType!="dash" | sessionType == ""){Response.Write("style=display:none");}%>>
								<div class="col-lg-12 order-lg-3 order-xl-1">

									<!--begin:: Widgets/Best Sellers-->
									<!--<div class="kt-portlet kt-portlet--height-fluid">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">
													Guías por cliente
												</h3>
											</div>
											
										</div>
										<div class="kt-portlet__body">
											<div class="tab-content">
												<div class="tab-pane active" id="kt_widget5_tab1_content" aria-expanded="true">
													<div class="kt-widget5" id="custguides">
													</div>
												</div>
											</div>
										</div>
									</div>
                                    -->
                                    <div class="kt-portlet kt-portlet--height-fluid">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">
													Guías por cliente
												</h3>
											</div>
											<div class="kt-portlet__head-toolbar">
											</div>
										</div>
										<div class="kt-portlet__body kt-portlet__body--fluid">
											<div class="kt-widget12">
												<div class="kt-widget12__content">
													<table id="custguides">
                                                    </table>
												</div>
											</div>
										</div>
									</div>
									<!--end:: Widgets/Best Sellers-->
								</div>
							</div>

							<!--End::Row-->
                            
                          <!-- begin:: PurchaseInvoices -->
                            <div class="row" id="invrow" <%if (pageType != "dash") { Response.Write("style=display:none"); } %>>
                                <div class="col-xl-12 order-lg-2 order-xl-1">
                                    <div class="kt-portlet kt-portlet--height-fluid kt-portlet--mobile ">
                                        <div class="kt-portlet__head kt-portlet__head--lg kt-portlet__head--noborder kt-portlet__head--break-sm">
                                            <div class="kt-portlet__head-label">
                                                <h3 class="kt-portlet__head-title">Reporte de guías utilizadas
                                                </h3>
                                            </div>
                                            <div class="kt-portlet__head-toolbar">
                                                <div class="dropdown dropdown-inline">
                                                    <button type="button" class="btn btn-clean btn-sm btn-icon btn-icon-md" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                        <i class="flaticon-more-1"></i>
                                                    </button>
                                                    <div class="dropdown-menu dropdown-menu-right dropdown-menu-md dropdown-menu-fit">

                                                        <!--begin::Nav-->

                                                        <!--end::Nav-->
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="kt-portlet__body">

                                            <!--begin: Datatable -->
                                            <div class="kt-datatable" id="kt_datatable_latest_orders"></div>

                                            <!--begin: Search Form -->
                                            <form class="kt-form kt-form--fit kt-margin-b-20" id="forma_reporte">
                                                <div class="row kt-margin-b-20">
                                                    <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
                                                        <label>Fecha:</label>
                                                        <div class="input-daterange input-group" id="kt_datepicker">
                                                            <input type="text" class="form-control kt-input" name="start" placeholder="Desde" data-col-index="5" />
                                                            <div class="input-group-append">
                                                                <span class="input-group-text"><i class="la la-ellipsis-h"></i></span>
                                                            </div>
                                                            <input type="text" class="form-control kt-input" name="end" placeholder="Hasta" data-col-index="5" />
                                                            <input type="hidden" name="dsStartDate" id="dsStartDate">
                                                            <input type="hidden" name="dsEndDate" id="dsEndDate">
                                                        </div>
                                                    </div>

<%--                                                    <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
                                                        <div class="kt-space-20"></div>

                                                    </div>--%>


                                                    <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
                                                        <div class="kt-space-20"></div>
                                                        <button class="btn btn-primary btn-brand--icon" data-toggle="kt-tooltip" title="Buscar" id="kt_search" onclick="getQuotes(true); return false;">
                                                            <!---->
                                                            <span>
                                                                <i class="la la-search"></i>
                                                                <span>Buscar</span>
                                                            </span>
                                                        </button>
                                                        &nbsp;&nbsp;
												            <button class="btn btn-secondary btn-secondary--icon kt_reset" onclick="javascript:;" id="kt_reset">
                                                                <span>
                                                                    <i class="la la-close"></i>
                                                                    <span>Limpiar</span>
                                                                </span>
                                                            </button>

                                                        <input type="hidden" id="pID" name="pID" value="">
                                                    </div>
                                                </div>
                                            </form>

                                            <table class="table table-hover table-checkable order-column dataTable no-footer" id="tablasesiones" role="grid" aria-describedby="sample_1_info">
                                                <thead bgcolor="#f6f7fd">
                                                    <tr role="row">
                                                        <th class="sorting_asc" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-sort="ascending" aria-label=" ID : activate to sort column descending" style="width: 35px;">ID 
                                                        </th>
                                                        <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Username : activate to sort column ascending" style="width: 206px;">Nombre 
                                                        </th>
                                                        <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-sort="ascending" aria-label=" ID : activate to sort column descending" style="width: 35px;">Cliente 
                                                        </th>
                                                        <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Tipo : activate to sort column ascending" style="width: 40px;">Guia
                                                        </th>
                                                        <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 80px;">Creado
                                                        </th>
                                                        <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 80px;">Recolección 
                                                        </th>
                                                        <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 50px;">Tipo
                                                        </th>
                                                        <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 86px;">Envío 
                                                        </th>
                                                        <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 86px;">Inf Adicional 
                                                        </th>
                                                        <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 90px;">Acción 
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                </tbody>
                                            </table>

                                            <!--end: Datatable -->
                                        </div>
                                    </div>
                                </div>
                            </div>
                           <!-- end:: PurchaseInvoices -->
                            
                          <!-- begin:: DocUpload -->
                          <div class="row" id="docuprow" <%if(pageType!="docs"){Response.Write("style=display:none");} %>> 
                            <div class="col-xl-12 order-lg-2 order-xl-1" >
                                <div class="kt-portlet kt-portlet--height-fluid kt-portlet--mobile ">
                                    <div class="kt-portlet__head kt-portlet__head--lg kt-portlet__head--noborder kt-portlet__head--break-sm">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">
													Carga Manual de Documentos
												</h3>
											</div>
                                    </div>
                                    <div class="kt-portlet__body">
                                    <form class="kt-form" id="Form1">
                                     <div class="kt-portlet__body">
                                        
                                          <div class="col-lg-12">
											<label for="exampleSelect1">Tipo de Documento</label>
											<select class="form-control" id="Select1">
												<option>Órden de Compra</option>
												<option>Programación de Pago</option>
                                                <option>Pagos</option>
                                                <option>Otro</option>
											</select>
										  </div>
                                        </div>
                                     </form>
                                        <div class="kt-separator kt-separator--border-dashed kt-separator--space-md"></div>
                                        
                                                <div class="col-lg-6">
                                                <form class="kt-form kt-form--label-right" id="uploadFile">
                                        <div class="form-group form-group-last row">
													<label class="col-lg-3 col-form-label">Subir Archivo:</label>
													<div class="col-lg-9">
														<div class="dropzone dropzone-multi" id="kt_dropzone_1">
															<div class="dropzone-panel">
																<a class="dropzone-select btn btn-label-brand btn-bold btn-sm">Subir Archivo</a>
																<a class="dropzone-upload btn btn-label-brand btn-bold btn-sm">Subir Todos</a>
																<a class="dropzone-remove-all btn btn-label-brand btn-bold btn-sm">Eliminar Todos</a>
															</div>
															<div class="dropzone-items">
																<div class="dropzone-item" style="display:none">
																	<div class="dropzone-file">
																		<div class="dropzone-filename" title="some_image_file_name.jpg"><span data-dz-name>some_image_file_name.jpg</span> <strong>(<span  data-dz-size>340kb</span>)</strong></div>
																		<div id="dropzone-error1" class="dropzone-error" data-dz-errormessage></div>
																	</div>
																	<div class="dropzone-progress">
																		<div class="progress">
																			<div class="progress-bar kt-bg-brand" role="progressbar" aria-valuemin="0" aria-valuemax="100" aria-valuenow="0" data-dz-uploadprogress></div>
																		</div>
																	</div>
																	<div class="dropzone-toolbar">
																		<span class="dropzone-start"><i class="flaticon2-arrow"></i></span>
																		<span class="dropzone-cancel" data-dz-remove style="display: none;"><i class="flaticon2-cross"></i></span>
																		<span class="dropzone-delete" data-dz-remove><i class="flaticon2-cross"></i></span>
																	</div>
																</div>
															</div>
														</div>
														<span class="form-text text-muted">Tamaño máximo de 1 MB</span>
													</div>
												</div>
                                                </form>
                                               </div>
                                    </div>
                                </div>
                            </div>
                            </div>
                          <!-- end:: DocUpload -->

                          <!--begin:: Guide Authorize-->
                          <div class="row" <%if(pageType!="auth"){Response.Write("style=display:none");}%> >
                            
                            <div class="col-xl-12 order-lg-2 order-xl-1" >
                                <div class="kt-portlet kt-portlet--height-fluid kt-portlet--mobile ">
                                    <div class="kt-portlet__head kt-portlet__head--lg kt-portlet__head--noborder kt-portlet__head--break-sm">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">
													Autorizar guía
												</h3>
											</div>
                                    </div>
                                    
                                    <!--begin::Form-->
										<form class="kt-form kt-form--label-right" id="auth_form">
											<div class="kt-portlet__body">
                                                <div class="kt-form__content">
											        <div class="kt-alert m-alert--icon alert alert-danger kt-hidden" role="alert" id="Div2">
												        <div class="kt-alert__icon">
													        <i class="la la-warning"></i>
												        </div>
												        <div class="kt-alert__text">
												        </div>
												        <div class="kt-alert__close">
													        <button type="button" class="close" data-close="alert" aria-label="Close">
													        </button>
												        </div>
											        </div>
										        </div>
                                                <div class="form-group row form-group-marginless">
													<!--<label class="col-lg-1 col-xs-3 col-form-label"><a href="" class="btn btn-bold btn-sm btn-label-brand kt-link" data-toggle="modal" data-target="#kt_customers_modal" onclick="getCustomers();">Cliente</a><div class="kt-space-5"></div></label>-->
													<div class="col-lg-4">
                                                            <span class="form-text text-muted">Cliente</span>
                                                            <select class="form-control kt-input select2" id="customer" name="customer">
												            </select>
                                                            <input type="hidden" name="custname" id="custname">
														<span class="form-text text-muted">&nbsp;</span>
													</div>
													<!--<label class="col-lg-1 col-xs-3 col-form-label">Serie:</label>-->
													<div class="col-lg-2 col-xs-3">
														<span class="form-text text-muted">Rango actual</span>
                                                        <input type="text" readonly="readonly" class="form-control" placeholder="" name="range" value="">															
														
													</div>
                                                    <div class="col-lg-2 col-xs-3">
														<span class="form-text text-muted">Última utilizada</span>
                                                        <input type="text" readonly="readonly" class="form-control" placeholder="" name="lastused" value="">															
														
													</div>
                                                    <!--<label class="col-lg-1 col-xs-3 col-form-label">Folio:</label>-->
													<div class="col-lg-3 col-xs-3">
                                                        <span class="form-text text-muted">Guías a autorizar</span>
													    <div class="input-group">	
                                                            <input type="number" class="form-control" placeholder="" name="toauthorize" value="">	
                                                            <div class="input-group-append"><span class="input-group-text" id="Span3"><a href="javascript:;" class="kt-link" onclick="saveRange();">Guardar</a></span></div>
                                                        </div>														
													</div>
													<label class="col-lg-1 col-form-label"></label>
													<div class="col-lg-2 col-xs-10">
														<span class="form-text text-muted"></span>
													</div>
                                                    <div class="col-lg-1 col-xs-2">
															
													</div>
												</div>
                                                <div class="row kt-margin-b-20">
											            <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
												            <label>Fecha:</label>
												            <div class="input-daterange input-group" id="Div3">
													            <input type="text" class="form-control kt-input" name="start" placeholder="Desde" data-col-index="5" />
													            <div class="input-group-append">
														            <span class="input-group-text"><i class="la la-ellipsis-h"></i></span>
													            </div>
													            <input type="text" class="form-control kt-input" name="end" placeholder="Hasta" data-col-index="5" />
                                                                <input type="hidden" name="dsStartDate" id="Hidden4">
                                                                <input type="hidden" name ="dsEndDate" id="Hidden6">
												            </div>
											            </div>
											            <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
												            <label>Estatus:</label>
												            <select class="form-control kt-input" data-col-index="6" name="cmbInvStatus">
													            <option value="">Seleccione</option>
                                                                <option value="1">Autorizadas</option>
                                                                <option value="0">Sin autorizar</option>
												            </select>
											            </div>
                                                        <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
                                                        <div class="kt-space-20"></div>
                                                            <button class="btn btn-primary btn-brand--icon" data-toggle="kt-tooltip" title="Buscar" id="Button7" onclick="getRanges(true); return false;"> <!---->
													            <span>
														            <i class="la la-search"></i>
														            <span>Buscar</span>
													            </span>
												            </button>
                                                            &nbsp;&nbsp;
												            <button class="btn btn-secondary btn-secondary--icon kt_reset" onclick="javascript:;" id="Button8">
													            <span>
														            <i class="la la-close"></i>
														            <span>Limpiar</span>
													            </span>
												            </button>
                                                            <input type="hidden" id="Hidden8" name="sID" value="">
											            </div>
										            </div>
                                                    
                                             <div class="kt-separator kt-separator--border-dashed kt-separator--space-md"></div>

                                             <table class="table table-hover table-checkable order-column dataTable no-footer" id="tblguideranges" role="grid" aria-describedby="sample_1_info">
                                                    <thead bgcolor="#f6f7fd">
                                                        <tr role="row">
                                                            <th class="sorting_asc" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-sort="ascending" aria-label=" ID : activate to sort column descending" style="width: 35px;"> 
                                                                ID 
                                                            </th>
                                                            <th class="sorting_asc" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-sort="ascending" aria-label=" ID : activate to sort column descending" style="width: 35px;"> 
                                                                Cliente 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Username : activate to sort column ascending" style="width: 206px;"> 
                                                                Rango 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-sort="ascending" aria-label=" ID : activate to sort column descending" style="width: 35px;"> 
                                                                Creado 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Tipo : activate to sort column ascending" style="width: 150px;"> 
                                                                Fecha
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 80px;"> 
                                                                Autorizó 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 80px;"> 
                                                                Fecha 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 86px;"> 
                                                                Estatus 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 90px;"> 
                                                                Acción 
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                             
											</div>
											<div class="kt-portlet__foot">
												<div class="kt-form__actions">
													<div class="row">
                                                        <label class="col-lg-1 col-xs-2 col-form-label">
															     
                                                             </label>
                                                             <label class="col-lg-1 col-xs-2 col-form-label">
                                                                 
                                                             </label>
														<div class="col-lg-3 d-xs-none"></div>
														<div class="col-lg-7 col-xs-7">
															<button type="reset" class="btn btn-brand" onclick="saveRange(); return false;">Guardar</button>
															<button onclick="location.href = 'Default.aspx?actn=auth'; return false"; type="reset" class="btn btn-secondary">Limpiar</button>
														</div>
													</div>
												</div>
											</div>
										</form>

										<!--end::Form-->

                                </div>
                            </div>
                            
                          </div>
						  <!--end:: Guide Authorize-->
                           
                          <!-- begin:: InvoiceUpload -->
                          <div class="row" id="invuprow" <%if(pageType!="inv"){Response.Write("style=display:none");} %>> 
                            <div class="col-xl-12 order-lg-2 order-xl-1" >
                                <div class="kt-portlet kt-portlet--height-fluid kt-portlet--mobile ">
                                    <div class="kt-portlet__head kt-portlet__head--lg kt-portlet__head--noborder kt-portlet__head--break-sm">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">
													Carga de Facturas
												</h3>
											</div>
                                    </div>
                                    <div class="kt-portlet__body">
                                    <form class="kt-form" id="po_form">
                                     <div class="kt-portlet__body">
                                        <table>
                                            <tr>
                                                <td>
                                                <div class="col-lg-12">
													<label for="exampleSelect1">Departamento</label>
													<select class="form-control" id="Select3">
														<option>Dep 1</option>
														<option>Dep 2</option>
													</select>
											    </div>
                                                </td>
                                                <td rowspan="2" valign="top">
                                                    <div class="col-lg-6">
													<label for="exampleSelect1">Observaciones</label>
                                                    <textarea class="form-control" readonly="" rows="4"></textarea>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                <div class="col-lg-12">
													<label for="exampleSelect1">Moneda</label>
													<select class="form-control" id="Select4">
														<option>MXN</option>
														<option>USD</option>
													</select>
											    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                <div class="col-lg-12">
													<label for="exampleSelect1">Orden de Compra</label>
													<select class="form-control" name="cmbPchOrder" id="cmbPchOrder" onchange="getPOLines();">
														<option>Seleccione</option>
                                                        <option value="1">OC-1234</option>
														<option value="2">OC-3423</option>
														<option value="3">OC-2324</option>
														<option value="4">OC-2346</option>
														<option value="5">OC-3245</option>
													</select>
											    </div>
                                            </td>
                                            </tr>
                                        </table>
                                        </div>
                                     </form>
                                        <div class="kt-separator kt-separator--border-dashed kt-separator--space-md"></div>
                                        
                                        <table class="table table-hover table-checkable order-column dataTable no-footer" id="tbl_pch_order_lines" role="grid" aria-describedby="sample_1_info">
                                                    <thead bgcolor="#f6f7fd">
                                                        <tr role="row">
                                                            <th class="sorting_asc" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-sort="ascending" aria-label=" ID : activate to sort column descending" style="width: 75px;"> 
                                                                OC 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Username : activate to sort column ascending" style="width: 60px;"> 
                                                                Línea 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Tipo : activate to sort column ascending" style="width: 50px;"> 
                                                                Envío
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 50px;"> 
                                                                Item 
                                                            </th>
                                                             <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 150px;"> 
                                                                Descripción 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 60px;"> 
                                                                UM 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 60px;"> 
                                                                Moneda 
                                                            </th>
                                                             <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 60px;"> 
                                                                OC Cant 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 60px;"> 
                                                                OC Fact 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 60px;"> 
                                                                OC Pdte 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 60px;"> 
                                                                Cant Env 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 60px;"> 
                                                                Precio U 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 60px;"> 
                                                                Total
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                                <div class="col-lg-6">
                                                <form class="kt-form kt-form--label-right" id="uploadForm">
                                        <div class="form-group form-group-last row">
													<label class="col-lg-3 col-form-label">Subir Archivo:</label>
													<div class="col-lg-9">
														<div class="dropzone dropzone-multi" id="kt_dropzone_4">
															<div class="dropzone-panel">
																<a id="btnUpldXml" onclick="clearUploads();" class="dropzone-select btn btn-label-brand btn-bold btn-sm"><% if (supplierID == "2") { Response.Write("Subir PDF"); } else { Response.Write("Subir XML"); } %></a>
																<a class="dropzone-upload btn btn-label-brand btn-bold btn-sm">Subir Todos</a>
																<a class="dropzone-remove-all btn btn-label-brand btn-bold btn-sm">Eliminar Todos</a>
															</div>
															<div class="dropzone-items">
																<div class="dropzone-item" style="display:none">
																	<div class="dropzone-file">
																		<div class="dropzone-filename" title="some_image_file_name.jpg"><span data-dz-name>some_image_file_name.jpg</span> <strong>(<span  data-dz-size>340kb</span>)</strong></div>
																		<div id="dropzone-error" class="dropzone-error" data-dz-errormessage></div>
																	</div>
																	<div class="dropzone-progress">
																		<div class="progress">
																			<div class="progress-bar kt-bg-brand" role="progressbar" aria-valuemin="0" aria-valuemax="100" aria-valuenow="0" data-dz-uploadprogress></div>
																		</div>
																	</div>
																	<div class="dropzone-toolbar">
																		<span class="dropzone-start"><i class="flaticon2-arrow"></i></span>
																		<span class="dropzone-cancel" data-dz-remove style="display: none;"><i class="flaticon2-cross"></i></span>
																		<span class="dropzone-delete" data-dz-remove><i class="flaticon2-cross"></i></span>
																	</div>
																</div>
															</div>
														</div>
														<span class="form-text text-muted">Tamaño máximo de 1 MB</span>
													</div>
												</div>
                                                </form>
                                               </div>
                                    </div>
                                </div>
                            </div>
                            </div>
                          <!-- end:: InvoiceUpload -->

                          <!-- begin:: Make Invoice -->
                          <div class="row" id="makeinvoice" <%if(pageType!="minv"){Response.Write("style=display:none");} %>> 
                            <div class="col-xl-12 order-lg-2 order-xl-1" >
                                <div class="kt-portlet kt-portlet--height-fluid kt-portlet--mobile ">
                                    <div class="kt-portlet__head kt-portlet__head--lg kt-portlet__head--noborder kt-portlet__head--break-sm">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">
													Generar guía  - Guías disponibles &nbsp;<span id="guidesleft"></span>
												</h3>
											</div>
                                    </div>
                                    <!--begin::Form-->
										<form class="kt-form kt-form--label-right" id="guide_form">
											<div class="kt-portlet__body">
                                                <div class="kt-form__content">
											        <div class="kt-alert m-alert--icon alert alert-danger kt-hidden" role="alert" id="kt_form_1_msg">
												        <div class="kt-alert__icon">
													        <i class="la la-warning"></i>
												        </div>
												        <div class="kt-alert__text">
												        </div>
												        <div class="kt-alert__close">
													        <button type="button" class="close" data-close="alert" aria-label="Close">
													        </button>
												        </div>
											        </div>
										        </div>
                                                <div class="form-group row form-group-marginless">
													<!--<label class="col-lg-1 col-xs-3 col-form-label"><a href="" class="btn btn-bold btn-sm btn-label-brand kt-link" data-toggle="modal" data-target="#kt_customers_modal" onclick="getCustomers();">Cliente</a><div class="kt-space-5"></div></label>-->
													<div class="col-lg-4 col-xs-5">
                                                        <span class="form-text text-muted">Entrega:</span>
                                                        <select class="form-control" name="cmbDeliveryType" id="cmbDeliveryType" onchange="getDeliveryInfo();">
														<option value="-1">Seleccione</option>
                                                        <option value="1">Domicilio</option>
														<option value="2">Ocurre</option>
													</select>
													</div>
													<!--<label class="col-lg-1 col-xs-3 col-form-label">Serie:</label>-->
													<div class="col-lg-2 col-xs-3">
														<span class="form-text text-muted">Sucursal</span>
                                                         <select class="form-control kt-input select2" disabled="disabled" id="store" name="store">
												         </select>														
													</div>
                                                    <!--<label class="col-lg-1 col-xs-3 col-form-label">Folio:</label>-->
													<div class="col-lg-2 col-xs-4">
                                                        <span class="form-text text-muted">CP Destino</span>
														<input type="text" class="form-control" readonly="readonly" placeholder="" id="destinyzip" name="destinyzip" onblur="checkZip()" value="">															
														
													</div>
													
													<div class="col-lg-2 col-xs-4">
                                                        <span class="form-text text-muted">Guia</span>
														<input type="text" readonly="readonly" class="form-control" placeholder="" name="guide" value="">			
                                                        <input type="hidden" name="cansave" id="cansave" value="0">												
														
													</div>
                                                    <label class="col-lg-1 col-form-label"></label>
                                                    <div class="col-lg-1 col-xs-2">
															
													</div>
												</div>
                                                <div class="form-group row form-group-marginless">
													<!--<label class="col-lg-1 col-xs-3 col-form-label"><a href="" class="btn btn-bold btn-sm btn-label-brand kt-link" data-toggle="modal" data-target="#kt_customers_modal" onclick="getCustomers();">Cliente</a><div class="kt-space-5"></div></label>-->
													<div class="col-lg-4">
                                                        <span class="form-text text-muted">Persona que solicita</span>
														<input type="text" class="form-control" name="fullname" placeholder="" value="">
														
													</div>
                                                   
													<!--<label class="col-lg-1 col-xs-3 col-form-label">Serie:</label>-->
													<div class="col-lg-2 col-xs-3">
														<span class="form-text text-muted">Horario</span>
                                                        <input type="text" class="form-control" placeholder="" name="schedule" value="">															
														
													</div>
                                                    <!--<label class="col-lg-1 col-xs-3 col-form-label">Folio:</label>-->
													<div class="col-lg-4">
														
													</div>
												</div>
                                                <div class="form-group row form-group-marginless">
													<!--<label class="col-lg-1 col-xs-3 col-form-label"><a href="" class="btn btn-bold btn-sm btn-label-brand kt-link" data-toggle="modal" data-target="#kt_customers_modal" onclick="getCustomers();">Cliente</a><div class="kt-space-5"></div></label>-->
													<div class="col-lg-4">
														<span class="form-text text-muted">Calle y número</span>
                                                        <input type="text" class="form-control" placeholder="" name="caddress" value="">
													</div>
                                                   
													<!--<label class="col-lg-1 col-xs-3 col-form-label">Serie:</label>-->
													<div class="col-lg-2 col-xs-3">
														<span class="form-text text-muted">Colonia</span>
                                                        <input type="text" class="form-control" placeholder="" name="cneighborhood" value="">															
														
													</div>
                                                    <!--<label class="col-lg-1 col-xs-3 col-form-label">Folio:</label>-->
													<div class="col-lg-2">
														<span class="form-text text-muted">CP</span>
                                                        <input type="text" class="form-control" placeholder="" name="czipcode" value="">			
													</div>
                                                    <div class="col-lg-4">
														<span class="form-text text-muted">Cruza Con:</span>
                                                        <input type="text" class="form-control" placeholder="" name="creference" value="">			
													</div>
												</div>
                                                 <div class="form-group row form-group-marginless">
													<!--<label class="col-lg-1 col-xs-3 col-form-label"><a href="" class="btn btn-bold btn-sm btn-label-brand kt-link" data-toggle="modal" data-target="#kt_customers_modal" onclick="getCustomers();">Cliente</a><div class="kt-space-5"></div></label>-->
													<div class="col-lg-4">
														<span class="form-text text-muted">Municipio</span>
                                                        <input type="text" class="form-control" placeholder="" name="ccity" value="">
													</div>
                                                   
													<!--<label class="col-lg-1 col-xs-3 col-form-label">Serie:</label>-->
													<div class="col-lg-2 col-xs-3">
														<span class="form-text text-muted">Estado</span>
                                                        <input type="text" class="form-control" placeholder="" name="cstate" value="">															
														
													</div>
                                                    <!--<label class="col-lg-1 col-xs-3 col-form-label">Folio:</label>-->
													<div class="col-lg-2">
														<span class="form-text text-muted">Teléfono</span>
                                                        <input type="text" class="form-control" placeholder="" name="cphone" value="">			
													</div>
                                                    <div class="col-lg-4">
														<span class="form-text text-muted">Persona de Contacto:</span>
                                                        <input type="text" class="form-control" placeholder="" name="ccontact" value="">			
													</div>
												</div>												
												<!--Remitente-->
												<!--Cambios en el modulo potosinos ♣RequerimientoDeIntegracion v3 -DIC-10-2021-->
                                                <div class="form-group row form-group-marginless">
													<!--Input para el RFC-->
                                                    <div class="col-lg-4">
                                                        <span class="form-text text-muted" id="rfcValidationRemitente"><span class="is-required"></span>RFC</span>
                                                        <input type="text" class="form-control" placeholder="" id="rfcRemitente" name="rfcRemitente" value="">
                                                    </div>
													<!--Fin-->
													<!--ControlComboBox para Residencia Fiscal-->
                                                    <div class="col-lg-4">
                                                        <span class="form-text text-muted"><span class="is-required"></span>Residencia Fiscal</span>
                                                        <select class="form-control" id="fiscalRecidenceRemitente" name="fiscalRecidenceRemitente" onchange="">
                                                            <option value="-1">Seleccione</option>
                                                            <option value="1">México - (M.X)</option>
                                                            <option value="2">Estados Unidos - (U.S)</option>
                                                        </select>
                                                    </div>
													<!--Fin-->
													<!--ControlComboBox para el Numero de Identificación Fiscal-->
                                                    <div class="col-lg-4" id="divNumFiscalRemitente">
                                                        <span class="form-text text-muted"><span class="is-required"></span>Número de Identificación Fiscal</span>
                                                        <input type="number" class="form-control" placeholder="" id="numIdentFiscalRemitente" name="numIdentFiscalRemitente" value="">
                                                    </div>
													<!--Fin-->
                                                </div>

                                                <div class="kt-separator kt-separator--border-dashed kt-separator--space-md" style="border-color:#000000"></div>

												<!--Destino ↓↓-->
                                                <div class="form-group row form-group-marginless">
                                                    <!--<label class="col-lg-1 col-form-label">RFC:</label>-->
                                                    <div class="col-lg-4">
                                                        <span class="form-text text-muted">Fecha de recolección</span>
                                                        <input type="date" class="form-control" placeholder="" name="recdate" value="">
                                                    </div>
                                                    <!--<label class="col-lg-1 col-form-label">Nombre:</label>-->
                                                    <div class="col-lg-4 col-xs-5">
                                                        <span class="form-text text-muted">Nombre corto</span>
                                                        <div class="input-group">
                                                            <div class="input-group-prepend"><span class="input-group-text" id="Span1"><a href="javascritp:;" class="kt-link" data-toggle="modal" data-target="#kt_customers_modal" onclick="getCustomers();"><i class="fa fa-search  kt-font-primary"></i></a></span></div>
                                                            <input type="text" class="form-control" name="shortname" placeholder="" value="" onblur="getSubCustomer(this.value)">
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-4 col-xs-5">
                                                        <span class="form-text text-muted">Nombre de la Empresa o persona</span>
                                                        <input type="text" class="form-control" placeholder="" name="name" value="">
                                                    </div>
                                                    <!--<label class="col-lg-1 col-form-label">Correo:</label>-->
                                                    <div class="col-lg-4">
                                                    </div>
                                                </div>

                                                <div class="form-group row form-group-marginless">
                                                    <!--<label class="col-lg-1 col-form-label">Calle:</label>-->
                                                    <div class="col-lg-4">
                                                        <span class="form-text text-muted">Calle</span>
                                                        <input type="text" class="form-control" placeholder="" name="address" value="">
                                                    </div>
                                                    <!--<label class="col-lg-1 col-form-label">Colonia:</label>-->
                                                    <div class="col-lg-4">
                                                        <span class="form-text text-muted">Colonia</span>
                                                        <div class="kt-input-icon">
                                                            <input type="text" class="form-control" placeholder="" name="neighborhood" value="">
                                                            <span class="kt-input-icon__icon kt-input-icon__icon--right"><span><i class="la la-map-marker"></i></span></span>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-4">
                                                    </div>
                                                </div>
                                                <div class="form-group row form-group-marginless">
                                                    <!--<label class="col-lg-1 col-form-label">Estado:</label>-->
                                                    <div class="col-lg-4">
                                                        <span class="form-text text-muted">Estado</span>
                                                        <input type="text" class="form-control" placeholder="" name="state" value="">
                                                        <!--<select class="form-control kt-input select2" id="state" name="state">
												            </select>
                                                            <input type="hidden" name="statename" id="statename">-->
                                                    </div>
                                                    <!--<label class="col-lg-1 col-form-label">Municipio:</label>-->
                                                    <div class="col-lg-4">
                                                        <span class="form-text text-muted">Ciudad</span>
                                                        <input type="text" class="form-control" placeholder="" name="city" value="">
                                                        <!--<select class="form-control kt-input select2" id="city" name="city">
												            </select>
                                                            <input type="hidden" name="cityname" id="cityname">-->
                                                    </div>
                                                    <!--<label class="col-lg-1 col-form-label">CP:</label>-->
                                                    <div class="col-lg-4">
                                                    </div>
                                                </div>
                                                <div class="form-group row form-group-marginless">
                                                    <!--<label class="col-lg-1 col-form-label">Cond Pago:</label>-->
                                                    <div class="col-lg-4">
                                                        <span class="form-text text-muted">Persona de contacto</span>
                                                        <input type="text" class="form-control form-control-danger" placeholder="" name="contact" value="">
                                                    </div>
                                                    <!--<label class="col-lg-1 col-form-label">Ord Compra:</label>-->
                                                    <div class="col-lg-4">
                                                        <span class="form-text text-muted">Teléfono</span>
                                                        <input type="text" class="form-control form-control-danger" placeholder="" name="phone" value="">
                                                    </div>
                                                    <!--<label class="col-lg-1 col-form-label">Pedimento:</label>-->
                                                    <div class="col-lg-4">
                                                    </div>
                                                </div>
                                                <!--Destino-->
                                                <!--Cambios en el modulo potosinos ♣RequerimientoDeIntegracion v3 -DIC-10-2021-->
                                                <div class="form-group row form-group-marginless">
                                                    <!--Input para el RFC-->
                                                    <div class="col-lg-4">
                                                        <span class="form-text text-muted" id="rfcValidationDestino"><span class="is-required"></span>RFC</span>
                                                        <input type="text" class="form-control" placeholder="" id="rfcDestino" name="rfcDestino" value="">
                                                    </div>
                                                    <!--Fin-->
                                                    <!--ControlComboBox para Residencia Fiscal-->
                                                    <div class="col-lg-4">
                                                        <span class="form-text text-muted"><span class="is-required"></span>Residencia Fiscal</span>
                                                        <select class="form-control" id="fiscalRecidenceDestino" name="fiscalRecidenceDestino" onchange="">
                                                            <option value="-1">Seleccione</option>
                                                            <option value="1">México - (M.X)</option>
                                                            <option value="2">Estados Unidos - (U.S)</option>
                                                        </select>
                                                    </div>
                                                    <!--Fin-->
                                                    <!--ControlComboBox para el Numero de Identificación Fiscal-->
                                                    <div class="col-lg-4" id="divNumFiscalDestino">
                                                        <span class="form-text text-muted"><span class="is-required"></span>Número de Identificación Fiscal</span>
                                                        <input type="number" class="form-control" placeholder="" id="numIdentFiscalDestino" name="numIdentFiscalDestino" value="">
                                                    </div>
                                                    <!--Fin-->
                                                </div>
												<!--Fin Destino ↑↑-->

                                                <div class="kt-separator kt-separator--border-dashed kt-separator--space-md" style="border-color: #000000"></div>

                                                   
                                                <div class="form-group row form-group-marginless">
													<!--<label class="col-lg-1 col-xs-3 col-form-label"><a href="" class="btn btn-bold btn-sm btn-label-brand kt-link" data-toggle="modal" data-target="#kt_customers_modal" onclick="getCustomers();">Cliente</a><div class="kt-space-5"></div></label>-->
													<div class="col-lg-4 col-xs-5">
                                                        <span class="form-text text-muted">Paquete o Tarima:</span>
                                                        <select class="form-control" name="cmbpacktype" id="cmbpacktype" >
														<option value="-1">Seleccione</option>
                                                        <option value="1">Paquete</option>
														<option value="2">Tarima</option>
													</select>
													</div>
													<!--<label class="col-lg-1 col-xs-3 col-form-label">Serie:</label>-->
													<div class="col-lg-8 col-xs-6">
														 <span class="form-text text-muted">Contenido</span>
														<input type="text" class="form-control" placeholder="" name="content" value="">													
													</div>
												</div>
                                                <div class="form-group row form-group-marginless">
													<!--<label class="col-lg-1 col-xs-3 col-form-label"><a href="" class="btn btn-bold btn-sm btn-label-brand kt-link" data-toggle="modal" data-target="#kt_customers_modal" onclick="getCustomers();">Cliente</a><div class="kt-space-5"></div></label>-->
													<div class="col-lg-2">
														<span class="form-text text-muted">Peso KG</span>
                                                        <input type="number" step=".01" class="form-control" placeholder="" name="weight" value="">
													</div>
                                                   
													<!--<label class="col-lg-1 col-xs-3 col-form-label">Serie:</label>-->
													<div class="col-lg-2 col-xs-3">
														<span class="form-text text-muted">Ancho</span>
                                                        <input type="number" class="form-control" placeholder="" name="width" value="">																												
													</div>
                                                    <!--<label class="col-lg-1 col-xs-3 col-form-label">Folio:</label>-->
													<div class="col-lg-2">
														<span class="form-text text-muted">Largo</span>
                                                        <input type="number" class="form-control" placeholder="" name="length" value="">			
													</div>
                                                    <div class="col-lg-2">
														<span class="form-text text-muted">Alto</span>
                                                        <input type="number" class="form-control" placeholder="" name="height" value="">			
													</div>
												</div>
                                                <div class="form-group row form-group-marginless">
													<!--<label class="col-lg-1 col-xs-3 col-form-label"><a href="" class="btn btn-bold btn-sm btn-label-brand kt-link" data-toggle="modal" data-target="#kt_customers_modal" onclick="getCustomers();">Cliente</a><div class="kt-space-5"></div></label>-->
													<div class="col-lg-4 col-xs-5">
                                                        <span class="form-text text-muted">Flete asegurado</span>
                                                        <select class="form-control" name="cmbinsured" id="cmbinsured" >
														<option value="-1">Seleccione</option>
                                                        <option value="1">SI</option>
														<option value="2">NO</option>
													</select>
													</div>
													<!--<label class="col-lg-1 col-xs-3 col-form-label">Serie:</label>-->
													<div class="col-lg-4 col-xs-6">
														 <span class="form-text text-muted">Valor factura</span>
														<input type="number" class="form-control" placeholder="" name="invoicevalue" id="invoicevalue" value="">													
													</div>
                                                    <div class="col-lg-4 col-xs-6">
														 <span class="form-text text-muted">Inf. Adicional</span>
														<input type="text" class="form-control" placeholder="" name="extrainfo" id="extrainfo" value="">													
													</div>
												</div>

												<div class="kt-separator kt-separator--border-dashed kt-separator--space-lg" style="border-color:transparent"></div>
                                                <!--Cambios en el modulo potosinos ♣RequerimientoDeIntegracion (Datos Complementarios de la Mercancía) v3 -DIC-10-2021-->
                                                <h3 class="kt-portlet__head-title">Mercancia Complementaria
                                                </h3>
                                                <br />
                                                <div class="form-group row form-group-marginless" id="divDetalleMercancia">
                                                    <div id="kt_repeater_1">
                                                        <div class="form-group form-group-last row" id="kt_repeater_1">
                                                            <div data-repeater-list="" class="col-lg-12">
                                                                <div data-repeater-item class="form-group row align-items-center" id="removeLinePadre">
                                                                    <div class="col-lg-3" hidden="hidden" id="removeLineHijo">
                                                                        <input type="text" class="form-control kt-input" id="ServiceLineId" name="ServiceLineId" />
                                                                    </div>
                                                                    <div class="col-md-2 col-xs-6" id="removeLineHijo">
                                                                        <div class="kt-form__group--inline">
                                                                            <div class="kt-form__control">
                                                                                <span class="form-text text-muted">Clave Producto</span><input type="number" class="form-control " placeholder="" name="lnCodeProd" id="lnCodeProd" value="">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-4" id="removeLineHijo">
                                                                        <div class="kt-form__group--inline">
                                                                            <div class="kt-form__control">
                                                                                <span class="form-text text-muted">Descripción</span><input type="text" class="form-control " placeholder="" name="lnDescription" id="lnDescription" value="">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-2 col-xs-6" id="removeLineHijo">
                                                                        <div class="kt-form__group--inline">
                                                                            <div class="kt-form__control">
                                                                                <span class="form-text text-muted ">Cantidad</span><input type="number" class="form-control ln-leavep" placeholder="" name="lnQty" id="lnQty" value="">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-2 col-xs-6" id="removeLineHijo">
                                                                        <div class="kt-form__group--inline">
                                                                            <div class="kt-form__control">
                                                                                <span class="form-text text-muted">Clave Unidad</span><input type="text" class="form-control " placeholder="" name="lnCodeUnit" id="lnCodeUnit" value="">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-2 col-xs-6" id="removeLineHijo">
                                                                        <div class="kt-form__group--inline">
                                                                            <div class="kt-form__control">
                                                                                <span class="form-text text-muted">Peso</span><input type="number" class="form-control " placeholder="" name="lnWeight" id="lnWeight" value="">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-2 col-xs-6" id="removeLineHijo">
                                                                        <div class="kt-form__group--inline">
                                                                            <div class="kt-form__control">
                                                                                <span class="form-text text-muted ">Fracción Arancelaria</span><input type="number" class="form-control" placeholder="" name="lnTfraction" id="lnTfraction" value="">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-2 col-xs-6" id="removeLineHijo">
                                                                        <div class="kt-form__group--inline">
                                                                            <div class="kt-form__control">
                                                                                <span class="form-text text-muted ">UUID</span><input type="number" class="form-control" placeholder="" name="lnUUID" id="lnUUID" value="">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-1 col-xs-6" id="removeLineHijo">
                                                                        <a href="javascript:;" id="btnlnRemoveServicesRepeater" name="btnRemoveLn" data-repeater-delete="" class="btn-sm btn btn-label-danger btn-bold">
                                                                            <i class="la la-trash-o"></i>
                                                                        </a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group form-group-last row">
                                                            <div class="col-lg-4">
                                                                <a href="javascript:;" id="btnlnServicesRepeater" data-repeater-create="" class="btn btn-bold btn-sm btn-label-brand">
                                                                    <i class="la la-plus"></i>Agregar
                                                                </a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
												<div class="kt-separator kt-separator--border-dashed kt-separator--space-lg"></div>
                                                </div>
											
											<div class="kt-portlet__foot">
												<div class="kt-form__actions">
													<div class="row">
                                                        <label class="col-lg-1 col-xs-2 col-form-label">
															     <a href="" id="dwpdf" class="kt-link" style="display:none;color:#cc0000"><i class="kt-menu__link-icon fa fa-file-pdf fa-3x"></i> Imprimir</a>
                                                        </label>
														<div class="col-lg-3 d-xs-none"></div>
														<div class="col-lg-7 col-xs-7">
															<button type="reset" class="btn btn-brand" onclick="saveGuide(); return false;">Guardar</button>
															<button onclick="location.href = 'Default.aspx?actn=minv'; return false"; type="reset" class="btn btn-secondary">Limpiar</button>
														</div>
													</div>
												</div>
											</div>
											</div>

											<%--Mercancia Complementaria--%>

										</form>
										<!--end::Form-->
                                </div>
                            </div>
                            </div>
                          <!-- end:: Make Invoice -->

						<!-- begin:: Make Invoice -->
                            <div class="row" id="makeinvoiceAlmex" <%if (pageType != "minvalmx") { Response.Write("style=display:none"); } else { Response.Write("style=min-height:100%;min-width:100%;"); } %>>
                                <div class="col-xl-12 order-lg-2 order-xl-1">
                                    <div class="kt-portlet kt-portlet--height-fluid kt-portlet--mobile" style="min-height:100%;min-width:100%">
                                        <div class="kt-portlet__head kt-portlet__head--lg kt-portlet__head--noborder kt-portlet__head--break-sm" style="min-height:100%;min-width:100%">
                                            <div class="kt-portlet__body" style="min-height:100%;min-width:100%">
                                                <iframe id="frame" src="<%--http://166.62.93.54/ProconecttApi/Configuracion/GetParameters?ClienteRFC=1&EmpresaRFC=2&Cuenta=3--%>" style="min-height: 100%; min-width: 100%"></iframe>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                          <!-- end:: Make InvoiceAlmex -->
                          
                          <!-- begin:: Invoices -->
                          <div class="row" id="sinvrow" <%if(pageType!="sinv"){Response.Write("style=display:none");} %>> 
                            <div class="col-xl-12 order-lg-2 order-xl-1">
									<div class="kt-portlet kt-portlet--height-fluid kt-portlet--mobile ">
										<div class="kt-portlet__head kt-portlet__head--lg kt-portlet__head--noborder kt-portlet__head--break-sm">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">
													Facturas
												</h3>
											</div>
											<div class="kt-portlet__head-toolbar">
												<div class="dropdown dropdown-inline">
													<button type="button" class="btn btn-clean btn-sm btn-icon btn-icon-md" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
														<i class="flaticon-more-1"></i>
													</button>
													<div class="dropdown-menu dropdown-menu-right dropdown-menu-md dropdown-menu-fit">

														<!--begin::Nav-->
														<ul class="kt-nav">
															<li class="kt-nav__head">
																Exportar
																<span data-toggle="kt-tooltip" data-placement="right" title="Exportar...">
																	<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1" class="kt-svg-icon kt-svg-icon--brand kt-svg-icon--md1">
																		<g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
																			<rect x="0" y="0" width="24" height="24" />
																			<circle fill="#000000" opacity="0.3" cx="12" cy="12" r="10" />
																			<rect fill="#000000" x="11" y="10" width="2" height="7" rx="1" />
																			<rect fill="#000000" x="11" y="7" width="2" height="2" rx="1" />
																		</g>
																	</svg> </span>
															</li>
															<li class="kt-nav__separator"></li>
															<li class="kt-nav__item">
																<a href="MediaUploader/Reporte Facturas.xlsx" download="Reporte Facturas.xlsx" class="kt-nav__link">
																	<i class="kt-nav__link-icon flaticon2-telegram-logo"></i>
																	<span class="kt-nav__link-text">Exportar a Excel</span>
																</a>
															</li>
														</ul>

														<!--end::Nav-->
													</div>
												</div>
											</div>
										</div>
										<div class="kt-portlet__body">

											<!--begin: Datatable -->

                                            <!--begin: Search Form -->
									            <form class="kt-form kt-form--fit kt-margin-b-20" id="formsinv">
										            <div class="row kt-margin-b-20">
											            <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
												            <label>Fecha:</label>
												            <div class="input-daterange input-group" id="kt_si_datepicker">
													            <input type="text" class="form-control kt-input" name="start" placeholder="Desde" data-col-index="5" />
													            <div class="input-group-append">
														            <span class="input-group-text"><i class="la la-ellipsis-h"></i></span>
													            </div>
													            <input type="text" class="form-control kt-input" name="end" placeholder="Hasta" data-col-index="5" />
                                                                <input type="hidden" name="dsStartDate" id="Hidden1">
                                                                <input type="hidden" name ="dsEndDate" id="Hidden2">
												            </div>
											            </div>
                                                        <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
												            <label>Folio:</label>
												            <input class="form-control kt-input" type"text" name="code">
											            </div>
											            <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
												            <label>Estatus:</label>
												            <select class="form-control kt-input" data-col-index="6" name="cmbInvStatus">
													            <option value="">Seleccione</option>
                                                                <option value="0">Creada</option>
                                                                <option value="4">Pagada</option>
                                                                <option value="5">Cancelada</option>
												            </select>
											            </div>
											            <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
												            <label>Tipo:</label>
												            <select class="form-control kt-input" data-col-index="7" name="cmbInvType">
													            <option value="">Seleccione</option>
                                                                <option value="I">INGRESO</option>
                                                                <option value="E">EGRESO</option>
                                                                <option value="P">Pago</option>
												            </select>
											            </div>
                                                        <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
												            <label>Cliente</label>
												            <input class="form-control kt-input" type"text" name="customer">
											            </div>
                                                        <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
												            <label>RFC</label>
												            <input class="form-control kt-input" type"text" name="rfc">
											            </div>
                                                        <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
												            <label>Complemento de Pago:</label>
												            <select class="form-control kt-input" data-col-index="6" name="cmbInvComplement">
													            <option value="">Seleccione</option>
                                                                <option value="4">Con Complemento</option>
                                                                <option value="0">Sin Complemento</option>
												            </select>
											            </div>
                                                        <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
                                                        <div class="kt-space-20"></div>
                                                            <button class="btn btn-primary btn-brand--icon" data-toggle="kt-tooltip" title="Buscar" id="Button1" onclick="getSaleInvoices(); return false;"> <!---->
													            <span>
														            <i class="la la-search"></i>
														            <span>Buscar</span>
													            </span>
												            </button>
                                                            &nbsp;&nbsp;
												            <button class="btn btn-secondary btn-secondary--icon kt_reset" onclick="javascript:;" id="kt_reset2">
													            <span>
														            <i class="la la-close"></i>
														            <span>Limpiar</span>
													            </span>
												            </button>
                                                            <input type="hidden" id="sID" name="sID" value="">
											            </div>
										            </div>
									            </form>

                                                <table class="table table-hover table-checkable order-column dataTable no-footer" id="saleinvoicestable" role="grid" aria-describedby="sample_1_info">
                                                    <thead bgcolor="#f6f7fd">
                                                        <tr role="row">
                                                            <th class="sorting_asc" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-sort="ascending" aria-label=" ID : activate to sort column descending" style="width: 35px;"> 
                                                                ID 
                                                            </th>
                                                            <th class="sorting_asc" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-sort="ascending" aria-label=" ID : activate to sort column descending" style="width: 35px;"> 
                                                                Folio 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Username : activate to sort column ascending" style="width: 206px;"> 
                                                                Nombre 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-sort="ascending" aria-label=" ID : activate to sort column descending" style="width: 35px;"> 
                                                                Tipo 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Tipo : activate to sort column ascending" style="width: 150px;"> 
                                                                Dirección
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 80px;"> 
                                                                Fecha 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 80px;"> 
                                                                Vence 
                                                            </th>
                                                             <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 50px;"> 
                                                                Total
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 86px;"> 
                                                                Estatus 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 90px;"> 
                                                                Acción 
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>     

											<!--end: Datatable -->


                                            <div class="kt-separator kt-separator--border-dashed kt-separator--space-md"></div>
                                        
                                        <table class="table table-hover table-checkable order-column dataTable no-footer" id="tbl_invoice_lines" role="grid" aria-describedby="sample_1_info">
                                                    <thead bgcolor="#f6f7fd">
                                                        <tr role="row">
                                                            <th class="sorting_asc" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-sort="ascending" aria-label=" ID : activate to sort column descending" style="width: 75px;"> 
                                                                Codigo 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Username : activate to sort column ascending" style="width: 60px;"> 
                                                                Cantidad 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Tipo : activate to sort column ascending" style="width: 150px;"> 
                                                                Concepto
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 50px;"> 
                                                                Precio 
                                                            </th>
                                                             <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 50px;"> 
                                                                Total 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 60px;"> 
                                                                Unidad 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 60px;"> 
                                                                Cve SAT 
                                                            </th>
                                                             <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 60px;"> 
                                                                Cve Unidad 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 60px;"> 
                                                                Tasa 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 60px;"> 
                                                                Descto 
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>

										</div>
									</div>
								</div>
                                </div> 
                          <!-- end:: Invoices -->                     
                          <!-- begin:: Receivable Accounts -->
                          <div class="row" id="cxcrow" <%if(pageType!="cxc"){Response.Write("style=display:none");} %>> 
                            <div class="col-xl-12 order-lg-2 order-xl-1">
									<div class="kt-portlet kt-portlet--height-fluid kt-portlet--mobile ">
										<div class="kt-portlet__head kt-portlet__head--lg kt-portlet__head--noborder kt-portlet__head--break-sm">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">
													Cuentas por cobrar
												</h3>
											</div>
											<div class="kt-portlet__head-toolbar">
												<div class="dropdown dropdown-inline">
													<button type="button" class="btn btn-clean btn-sm btn-icon btn-icon-md" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
														<i class="flaticon-more-1"></i>
													</button>
													<div class="dropdown-menu dropdown-menu-right dropdown-menu-md dropdown-menu-fit">

														<!--begin::Nav-->
														<ul class="kt-nav">
															<li class="kt-nav__head">
																Exportar
																<span data-toggle="kt-tooltip" data-placement="right" title="Exportar...">
																	<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1" class="kt-svg-icon kt-svg-icon--brand kt-svg-icon--md1">
																		<g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
																			<rect x="0" y="0" width="24" height="24" />
																			<circle fill="#000000" opacity="0.3" cx="12" cy="12" r="10" />
																			<rect fill="#000000" x="11" y="10" width="2" height="7" rx="1" />
																			<rect fill="#000000" x="11" y="7" width="2" height="2" rx="1" />
																		</g>
																	</svg> </span>
															</li>
															<li class="kt-nav__separator"></li>
															<li class="kt-nav__item">
																<a href="" class="kt-nav__link">
																	<i class="kt-nav__link-icon flaticon2-telegram-logo"></i>
																	<span class="kt-nav__link-text">Exportar a Excel</span>
																</a>
															</li>
														</ul>
														<!--end::Nav-->
													</div>
												</div>
											</div>
										</div>
										<div class="kt-portlet__body">
											<!--begin: Datatable -->
                                            <!--begin: Search Form -->
									            <form class="kt-form kt-form--fit kt-margin-b-20" id="frmcxc">
										            <div class="row kt-margin-b-20">
											            <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
												            <label>Fecha:</label>
												            <div class="input-daterange input-group" id="kt_recact_datepicker">
													            <input type="text" class="form-control kt-input" name="start" placeholder="Desde" data-col-index="5" />
													            <div class="input-group-append">
														            <span class="input-group-text"><i class="la la-ellipsis-h"></i></span>
													            </div>
													            <input type="text" class="form-control kt-input" name="end" placeholder="Hasta" data-col-index="5" />
												            </div>
											            </div>
                                                        <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
												            <label>Folio:</label>
												            <input class="form-control kt-input" type"text" name="code">
											            </div>
											            <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
												            <label>Estatus:</label>
												            <select class="form-control kt-input" data-col-index="6" name="cmbInvStatus">
													            <option value="">Seleccione</option>
                                                                <option value="0">Creada</option>
                                                                <option value="4">Pagada</option>
                                                                <option value="5">Cancelada</option>
												            </select>
											            </div>
											            <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
												            <label>Tipo:</label>
												            <select class="form-control kt-input" data-col-index="7" name="cmbInvType">
													            <option value="">Seleccione</option>
                                                                <option value="I">INGRESO</option>
                                                                <option value="E">EGRESO</option>
                                                                <option value="P">Pago</option>
												            </select>
											            </div>
                                                        <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
												            <label>Cliente</label>
												            <input class="form-control kt-input" type"text" name="customer">
											            </div>
                                                        <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
												            <label>RFC</label>
												            <input class="form-control kt-input" type"text" name="rfc">
											            </div>
                                                        <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
												            <label>Complemento de Pago:</label>
												            <select class="form-control kt-input" data-col-index="6" name="cmbInvComplement">
													            <option value="">Seleccione</option>
                                                                <option value="4">Con Complemento</option>
                                                                <option value="0">Sin Complemento</option>
												            </select>
											            </div>
                                                        <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
                                                        <div class="kt-space-20"></div>
                                                            <button class="btn btn-primary btn-brand--icon" data-toggle="kt-tooltip" title="Buscar" id="Button2" onclick="getSaleInvoices(); return false;"> <!---->
													            <span>
														            <i class="la la-search"></i>
														            <span>Buscar</span>
													            </span>
												            </button>
                                                            &nbsp;&nbsp;
												            <button class="btn btn-secondary btn-secondary--icon kt_reset" onclick="javascript:;" id="Button3">
													            <span>
														            <i class="la la-close"></i>
														            <span>Limpiar</span>
													            </span>
												            </button>
                                                            <input type="hidden" id="Hidden5" name="sID" value="">
											            </div>
										            </div>
									            </form>

                                                <table class="table table-hover table-checkable order-column dataTable no-footer" id="tblreceivableaccounts" role="grid" aria-describedby="sample_1_info">
                                                    <thead bgcolor="#f6f7fd">
                                                        <tr role="row">
                                                            <th class="sorting_asc" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-sort="ascending" aria-label=" ID : activate to sort column descending" style="width: 35px;"> 
                                                                ID 
                                                            </th>
                                                            <th class="sorting_asc" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-sort="ascending" aria-label=" ID : activate to sort column descending" style="width: 35px;"> 
                                                                Folio 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Username : activate to sort column ascending" style="width: 206px;"> 
                                                                Nombre 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-sort="ascending" aria-label=" ID : activate to sort column descending" style="width: 35px;"> 
                                                                Tipo 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Tipo : activate to sort column ascending" style="width: 150px;"> 
                                                                Dirección
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 80px;"> 
                                                                Fecha 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 80px;"> 
                                                                Vence 
                                                            </th>
                                                             <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 50px;"> 
                                                                Total
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 86px;"> 
                                                                Estatus 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 90px;"> 
                                                                Acción 
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>     

											<!--end: Datatable -->


                                            <div class="kt-separator kt-separator--border-dashed kt-separator--space-md"></div>
                                        
                                        <table class="table table-hover table-checkable order-column dataTable no-footer" id="tblreceivableaccountstotal" role="grid" aria-describedby="sample_1_info">
                                                    <thead bgcolor="#f6f7fd">
                                                        <tr role="row">
                                                            <th class="sorting_asc" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-sort="ascending" aria-label=" ID : activate to sort column descending" style="width: 75px;"> 
                                                                Moneda 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Username : activate to sort column ascending" style="width: 60px;"> 
                                                                Documentos 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Tipo : activate to sort column ascending" style="width: 150px;"> 
                                                                Monto
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 50px;"> 
                                                                Cobrado 
                                                            </th>
                                                             <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 50px;"> 
                                                                Pendiente 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 60px;"> 
                                                                Moneda 
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>

										</div>
									</div>
								</div>
                                </div> 
                          <!-- end:: Receivable Accounts-->
                          
                             <!-- begin:: Cobranza -->
                          <div class="row" id="cobrow" <%if(pageType!="cob"){Response.Write("style=display:none");} %>> 
                            <div class="col-xl-12 order-lg-2 order-xl-1">
									<div class="kt-portlet kt-portlet--height-fluid kt-portlet--mobile ">
										<div class="kt-portlet__head kt-portlet__head--lg kt-portlet__head--noborder kt-portlet__head--break-sm">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">
													Cobranza
												</h3>
											</div>
											<div class="kt-portlet__head-toolbar">
												<div class="dropdown dropdown-inline">
													<button type="button" class="btn btn-clean btn-sm btn-icon btn-icon-md" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
														<i class="flaticon-more-1"></i>
													</button>
													<div class="dropdown-menu dropdown-menu-right dropdown-menu-md dropdown-menu-fit">

														<!--begin::Nav-->
														<ul class="kt-nav">
															<li class="kt-nav__head">
																Exportar
																<span data-toggle="kt-tooltip" data-placement="right" title="Exportar...">
																	<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1" class="kt-svg-icon kt-svg-icon--brand kt-svg-icon--md1">
																		<g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
																			<rect x="0" y="0" width="24" height="24" />
																			<circle fill="#000000" opacity="0.3" cx="12" cy="12" r="10" />
																			<rect fill="#000000" x="11" y="10" width="2" height="7" rx="1" />
																			<rect fill="#000000" x="11" y="7" width="2" height="2" rx="1" />
																		</g>
																	</svg> </span>
															</li>
															<li class="kt-nav__separator"></li>
															<li class="kt-nav__item">
																<a href="" class="kt-nav__link">
																	<i class="kt-nav__link-icon flaticon2-telegram-logo"></i>
																	<span class="kt-nav__link-text">Exportar a Excel</span>
																</a>
															</li>
														</ul>

														<!--end::Nav-->
													</div>
												</div>
											</div>
										</div>
										<div class="kt-portlet__body">

											<!--begin: Datatable -->

                                            <!--begin: Search Form -->
									            <form class="kt-form kt-form--fit kt-margin-b-20" id="formsp">
										            <div class="row kt-margin-b-20">
											            <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
												            <label>Fecha:</label>
												            <div class="input-daterange input-group" id="kt_sp_datepicker">
													            <input type="text" class="form-control kt-input" name="start" placeholder="Desde" data-col-index="5" />
													            <div class="input-group-append">
														            <span class="input-group-text"><i class="la la-ellipsis-h"></i></span>
													            </div>
													            <input type="text" class="form-control kt-input" name="end" placeholder="Hasta" data-col-index="5" />
												            </div>
											            </div>
                                                        <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
												            <label>Folio:</label>
												            <input class="form-control kt-input" type"text" name="code">
											            </div>
											            <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
												            <label>Estatus:</label>
												            <select class="form-control kt-input" data-col-index="6" name="cmbInvStatus">
													            <option value="">Seleccione</option>
                                                                <option value="0">Creada</option>
                                                                <option value="4">Pagada</option>
                                                                <option value="5">Cancelada</option>
												            </select>
											            </div>
											            <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
												            <label>Tipo:</label>
												            <select class="form-control kt-input" data-col-index="7" name="cmbInvType">
													            <option value="">Seleccione</option>
                                                                <option value="I">INGRESO</option>
                                                                <option value="E">EGRESO</option>
                                                                <option value="P">Pago</option>
												            </select>
											            </div>
                                                        <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
												            <label>Cliente</label>
												            <input class="form-control kt-input" type"text" name="customer">
											            </div>
                                                        <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
												            <label>RFC</label>
												            <input class="form-control kt-input" type"text" name="rfc">
											            </div>
                                                        <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
												            <label>Complemento de Pago:</label>
												            <select class="form-control kt-input" data-col-index="6" name="cmbInvComplement">
													            <option value="">Seleccione</option>
                                                                <option value="4">Con Complemento</option>
                                                                <option value="0">Sin Complemento</option>
												            </select>
											            </div>
                                                        <div class="col-lg-3 kt-margin-b-10-tablet-and-mobile">
                                                        <div class="kt-space-20"></div>
                                                            <button class="btn btn-primary btn-brand--icon" data-toggle="kt-tooltip" title="Buscar" id="Button5" onclick="getSalePayments(); return false;"> <!---->
													            <span>
														            <i class="la la-search"></i>
														            <span>Buscar</span>
													            </span>
												            </button>
                                                            &nbsp;&nbsp;
												            <button class="btn btn-secondary btn-secondary--icon kt_reset" onclick="javascript:;" id="Button6">
													            <span>
														            <i class="la la-close"></i>
														            <span>Limpiar</span>
													            </span>
												            </button>
                                                            <input type="hidden" id="Hidden3" name="sID" value="">
											            </div>
										            </div>
									            </form>

                                                <table class="table table-hover table-checkable order-column dataTable no-footer" id="salepaymentstable" role="grid" aria-describedby="sample_1_info">
                                                    <thead bgcolor="#f6f7fd">
                                                        <tr role="row">
                                                            <th class="sorting_asc" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-sort="ascending" aria-label=" ID : activate to sort column descending" style="width: 35px;"> 
                                                                ID 
                                                            </th>
                                                            <th class="sorting_asc" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-sort="ascending" aria-label=" ID : activate to sort column descending" style="width: 35px;"> 
                                                                Folio 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Username : activate to sort column ascending" style="width: 206px;"> 
                                                                Nombre 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-sort="ascending" aria-label=" ID : activate to sort column descending" style="width: 35px;"> 
                                                                Tipo 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Tipo : activate to sort column ascending" style="width: 150px;"> 
                                                                
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 80px;"> 
                                                                Fecha 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 80px;"> 
                                                                Referencia 
                                                            </th>
                                                             <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 50px;"> 
                                                                Total
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 86px;"> 
                                                                T.C. 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 90px;"> 
                                                                Acción 
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>     

											<!--end: Datatable -->


                                            <div class="kt-separator kt-separator--border-dashed kt-separator--space-md"></div>
                                        
                                        <table class="table table-hover table-checkable order-column dataTable no-footer" id="tblsalepaymentstotal" role="grid" aria-describedby="sample_1_info">
                                                    <thead bgcolor="#f6f7fd">
                                                        <tr role="row">
                                                            <th class="sorting_asc" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-sort="ascending" aria-label=" ID : activate to sort column descending" style="width: 75px;"> 
                                                                No. Pagos 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Username : activate to sort column ascending" style="width: 60px;"> 
                                                                Subtotal 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Tipo : activate to sort column ascending" style="width: 150px;"> 
                                                                Impuesto
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 50px;"> 
                                                                Total 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 60px;"> 
                                                                Moneda 
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>

										</div>
									</div>
								</div>
                                </div> 
                          <!-- end:: Receivable Accounts-->
                            
						<!-- end:: Content -->
					</div>

					<!-- begin:: Footer -->
					<div class="kt-footer  kt-grid__item kt-grid kt-grid--desktop kt-grid--ver-desktop" id="kt_footer">
						<div class="kt-container  kt-container--fluid ">
							<div class="kt-footer__copyright">
								<% Response.Write(DateTime.Today.Year.ToString());%>&nbsp;&copy;&nbsp;<a href="<% if (HttpContext.Current.Request.Url.Host.ToLower() == "invoice.evoctus.com") { Response.Write("http://evoctus.co"); } else { Response.Write("https://inf.com.mx"); } %>" target="_blank" class="kt-link"><% if (HttpContext.Current.Request.Url.Host.ToLower() == "invoice.evoctus.com") { Response.Write("Evoctus"); } else { Response.Write("InfnIT Solutions"); } %></a>
							</div>
							<div class="kt-footer__menu">
								<a href="" target="_blank" class="kt-footer__menu-link kt-link">Acerca</a>
								<a href="" target="_blank" class="kt-footer__menu-link kt-link">Contacto</a>
							</div>
						</div>
					</div>

					<!-- end:: Footer -->
				</div>
			</div>
		</div>

		<!-- end:: Page -->

		<!-- begin::Quick Panel -->
        <!--
		<div id="kt_quick_panel" class="kt-quick-panel">
			<a href="#" class="kt-quick-panel__close" id="kt_quick_panel_close_btn"><i class="flaticon2-delete"></i></a>
			<div class="kt-quick-panel__nav">
				<ul class="nav nav-tabs nav-tabs-line nav-tabs-bold nav-tabs-line-3x nav-tabs-line-brand  kt-notification-item-padding-x" role="tablist">
					<li class="nav-item active">
						<a class="nav-link active" data-toggle="tab" href="#kt_quick_panel_tab_notifications" role="tab">Notifications</a>
					</li>
					<li class="nav-item">
						<a class="nav-link" data-toggle="tab" href="#kt_quick_panel_tab_logs" role="tab">Audit Logs</a>
					</li>
					<li class="nav-item">
						<a class="nav-link" data-toggle="tab" href="#kt_quick_panel_tab_settings" role="tab">Settings</a>
					</li>
				</ul>
			</div>
			<div class="kt-quick-panel__content">
				<div class="tab-content">
					<div class="tab-pane fade show kt-scroll active" id="kt_quick_panel_tab_notifications" role="tabpanel">
						<div class="kt-notification">
							<a href="#" class="kt-notification__item">
								<div class="kt-notification__item-icon">
									<i class="flaticon2-line-chart kt-font-success"></i>
								</div>
								<div class="kt-notification__item-details">
									<div class="kt-notification__item-title">
										New order has been received
									</div>
									<div class="kt-notification__item-time">
										2 hrs ago
									</div>
								</div>
							</a>
							<a href="#" class="kt-notification__item">
								<div class="kt-notification__item-icon">
									<i class="flaticon2-box-1 kt-font-brand"></i>
								</div>
								<div class="kt-notification__item-details">
									<div class="kt-notification__item-title">
										New customer is registered
									</div>
									<div class="kt-notification__item-time">
										3 hrs ago
									</div>
								</div>
							</a>
							<a href="#" class="kt-notification__item">
								<div class="kt-notification__item-icon">
									<i class="flaticon2-chart2 kt-font-danger"></i>
								</div>
								<div class="kt-notification__item-details">
									<div class="kt-notification__item-title">
										Application has been approved
									</div>
									<div class="kt-notification__item-time">
										3 hrs ago
									</div>
								</div>
							</a>
							<a href="#" class="kt-notification__item">
								<div class="kt-notification__item-icon">
									<i class="flaticon2-image-file kt-font-warning"></i>
								</div>
								<div class="kt-notification__item-details">
									<div class="kt-notification__item-title">
										New file has been uploaded
									</div>
									<div class="kt-notification__item-time">
										5 hrs ago
									</div>
								</div>
							</a>
							<a href="#" class="kt-notification__item">
								<div class="kt-notification__item-icon">
									<i class="flaticon2-drop kt-font-info"></i>
								</div>
								<div class="kt-notification__item-details">
									<div class="kt-notification__item-title">
										New user feedback received
									</div>
									<div class="kt-notification__item-time">
										8 hrs ago
									</div>
								</div>
							</a>
							<a href="#" class="kt-notification__item">
								<div class="kt-notification__item-icon">
									<i class="flaticon2-pie-chart-2 kt-font-success"></i>
								</div>
								<div class="kt-notification__item-details">
									<div class="kt-notification__item-title">
										System reboot has been successfully completed
									</div>
									<div class="kt-notification__item-time">
										12 hrs ago
									</div>
								</div>
							</a>
							<a href="#" class="kt-notification__item">
								<div class="kt-notification__item-icon">
									<i class="flaticon2-favourite kt-font-danger"></i>
								</div>
								<div class="kt-notification__item-details">
									<div class="kt-notification__item-title">
										New order has been placed
									</div>
									<div class="kt-notification__item-time">
										15 hrs ago
									</div>
								</div>
							</a>
							<a href="#" class="kt-notification__item kt-notification__item--read">
								<div class="kt-notification__item-icon">
									<i class="flaticon2-safe kt-font-primary"></i>
								</div>
								<div class="kt-notification__item-details">
									<div class="kt-notification__item-title">
										Company meeting canceled
									</div>
									<div class="kt-notification__item-time">
										19 hrs ago
									</div>
								</div>
							</a>
							<a href="#" class="kt-notification__item">
								<div class="kt-notification__item-icon">
									<i class="flaticon2-psd kt-font-success"></i>
								</div>
								<div class="kt-notification__item-details">
									<div class="kt-notification__item-title">
										New report has been received
									</div>
									<div class="kt-notification__item-time">
										23 hrs ago
									</div>
								</div>
							</a>
							<a href="#" class="kt-notification__item">
								<div class="kt-notification__item-icon">
									<i class="flaticon-download-1 kt-font-danger"></i>
								</div>
								<div class="kt-notification__item-details">
									<div class="kt-notification__item-title">
										Finance report has been generated
									</div>
									<div class="kt-notification__item-time">
										25 hrs ago
									</div>
								</div>
							</a>
							<a href="#" class="kt-notification__item">
								<div class="kt-notification__item-icon">
									<i class="flaticon-security kt-font-warning"></i>
								</div>
								<div class="kt-notification__item-details">
									<div class="kt-notification__item-title">
										New customer comment recieved
									</div>
									<div class="kt-notification__item-time">
										2 days ago
									</div>
								</div>
							</a>
							<a href="#" class="kt-notification__item">
								<div class="kt-notification__item-icon">
									<i class="flaticon2-pie-chart kt-font-warning"></i>
								</div>
								<div class="kt-notification__item-details">
									<div class="kt-notification__item-title">
										New customer is registered
									</div>
									<div class="kt-notification__item-time">
										3 days ago
									</div>
								</div>
							</a>
						</div>
					</div>
					<div class="tab-pane fade kt-scroll" id="kt_quick_panel_tab_logs" role="tabpanel">
						<div class="kt-notification-v2">
							<a href="#" class="kt-notification-v2__item">
								<div class="kt-notification-v2__item-icon">
									<i class="flaticon-bell kt-font-brand"></i>
								</div>
								<div class="kt-notification-v2__itek-wrapper">
									<div class="kt-notification-v2__item-title">
										5 new user generated report
									</div>
									<div class="kt-notification-v2__item-desc">
										Reports based on sales
									</div>
								</div>
							</a>
							<a href="#" class="kt-notification-v2__item">
								<div class="kt-notification-v2__item-icon">
									<i class="flaticon2-box kt-font-danger"></i>
								</div>
								<div class="kt-notification-v2__itek-wrapper">
									<div class="kt-notification-v2__item-title">
										2 new items submited
									</div>
									<div class="kt-notification-v2__item-desc">
										by Grog John
									</div>
								</div>
							</a>
							<a href="#" class="kt-notification-v2__item">
								<div class="kt-notification-v2__item-icon">
									<i class="flaticon-psd kt-font-brand"></i>
								</div>
								<div class="kt-notification-v2__itek-wrapper">
									<div class="kt-notification-v2__item-title">
										79 PSD files generated
									</div>
									<div class="kt-notification-v2__item-desc">
										Reports based on sales
									</div>
								</div>
							</a>
							<a href="#" class="kt-notification-v2__item">
								<div class="kt-notification-v2__item-icon">
									<i class="flaticon2-supermarket kt-font-warning"></i>
								</div>
								<div class="kt-notification-v2__itek-wrapper">
									<div class="kt-notification-v2__item-title">
										$2900 worth producucts sold
									</div>
									<div class="kt-notification-v2__item-desc">
										Total 234 items
									</div>
								</div>
							</a>
							<a href="#" class="kt-notification-v2__item">
								<div class="kt-notification-v2__item-icon">
									<i class="flaticon-paper-plane-1 kt-font-success"></i>
								</div>
								<div class="kt-notification-v2__itek-wrapper">
									<div class="kt-notification-v2__item-title">
										4.5h-avarage response time
									</div>
									<div class="kt-notification-v2__item-desc">
										Fostest is Barry
									</div>
								</div>
							</a>
							<a href="#" class="kt-notification-v2__item">
								<div class="kt-notification-v2__item-icon">
									<i class="flaticon2-information kt-font-danger"></i>
								</div>
								<div class="kt-notification-v2__itek-wrapper">
									<div class="kt-notification-v2__item-title">
										Database server is down
									</div>
									<div class="kt-notification-v2__item-desc">
										10 mins ago
									</div>
								</div>
							</a>
							<a href="#" class="kt-notification-v2__item">
								<div class="kt-notification-v2__item-icon">
									<i class="flaticon2-mail-1 kt-font-brand"></i>
								</div>
								<div class="kt-notification-v2__itek-wrapper">
									<div class="kt-notification-v2__item-title">
										System report has been generated
									</div>
									<div class="kt-notification-v2__item-desc">
										Fostest is Barry
									</div>
								</div>
							</a>
							<a href="#" class="kt-notification-v2__item">
								<div class="kt-notification-v2__item-icon">
									<i class="flaticon2-hangouts-logo kt-font-warning"></i>
								</div>
								<div class="kt-notification-v2__itek-wrapper">
									<div class="kt-notification-v2__item-title">
										4.5h-avarage response time
									</div>
									<div class="kt-notification-v2__item-desc">
										Fostest is Barry
									</div>
								</div>
							</a>
						</div>
					</div>
					<div class="tab-pane kt-quick-panel__content-padding-x fade kt-scroll" id="kt_quick_panel_tab_settings" role="tabpanel">
						<form class="kt-form">
							<div class="kt-heading kt-heading--sm kt-heading--space-sm">Customer Care</div>
							<div class="form-group form-group-xs row">
								<label class="col-8 col-form-label">Enable Notifications:</label>
								<div class="col-4 kt-align-right">
									<span class="kt-switch kt-switch--success kt-switch--sm">
										<label>
											<input type="checkbox" checked="checked" name="quick_panel_notifications_1">
											<span></span>
										</label>
									</span>
								</div>
							</div>
							<div class="form-group form-group-xs row">
								<label class="col-8 col-form-label">Enable Case Tracking:</label>
								<div class="col-4 kt-align-right">
									<span class="kt-switch kt-switch--success kt-switch--sm">
										<label>
											<input type="checkbox" name="quick_panel_notifications_2">
											<span></span>
										</label>
									</span>
								</div>
							</div>
							<div class="form-group form-group-last form-group-xs row">
								<label class="col-8 col-form-label">Support Portal:</label>
								<div class="col-4 kt-align-right">
									<span class="kt-switch kt-switch--success kt-switch--sm">
										<label>
											<input type="checkbox" checked="checked" name="quick_panel_notifications_2">
											<span></span>
										</label>
									</span>
								</div>
							</div>
							<div class="kt-separator kt-separator--space-md kt-separator--border-dashed"></div>
							<div class="kt-heading kt-heading--sm kt-heading--space-sm">Reports</div>
							<div class="form-group form-group-xs row">
								<label class="col-8 col-form-label">Generate Reports:</label>
								<div class="col-4 kt-align-right">
									<span class="kt-switch kt-switch--sm kt-switch--danger">
										<label>
											<input type="checkbox" checked="checked" name="quick_panel_notifications_3">
											<span></span>
										</label>
									</span>
								</div>
							</div>
							<div class="form-group form-group-xs row">
								<label class="col-8 col-form-label">Enable Report Export:</label>
								<div class="col-4 kt-align-right">
									<span class="kt-switch kt-switch--sm kt-switch--danger">
										<label>
											<input type="checkbox" name="quick_panel_notifications_3">
											<span></span>
										</label>
									</span>
								</div>
							</div>
							<div class="form-group form-group-last form-group-xs row">
								<label class="col-8 col-form-label">Allow Data Collection:</label>
								<div class="col-4 kt-align-right">
									<span class="kt-switch kt-switch--sm kt-switch--danger">
										<label>
											<input type="checkbox" checked="checked" name="quick_panel_notifications_4">
											<span></span>
										</label>
									</span>
								</div>
							</div>
							<div class="kt-separator kt-separator--space-md kt-separator--border-dashed"></div>
							<div class="kt-heading kt-heading--sm kt-heading--space-sm">Memebers</div>
							<div class="form-group form-group-xs row">
								<label class="col-8 col-form-label">Enable Member singup:</label>
								<div class="col-4 kt-align-right">
									<span class="kt-switch kt-switch--sm kt-switch--brand">
										<label>
											<input type="checkbox" checked="checked" name="quick_panel_notifications_5">
											<span></span>
										</label>
									</span>
								</div>
							</div>
							<div class="form-group form-group-xs row">
								<label class="col-8 col-form-label">Allow User Feedbacks:</label>
								<div class="col-4 kt-align-right">
									<span class="kt-switch kt-switch--sm kt-switch--brand">
										<label>
											<input type="checkbox" name="quick_panel_notifications_5">
											<span></span>
										</label>
									</span>
								</div>
							</div>
							<div class="form-group form-group-last form-group-xs row">
								<label class="col-8 col-form-label">Enable Customer Portal:</label>
								<div class="col-4 kt-align-right">
									<span class="kt-switch kt-switch--sm kt-switch--brand">
										<label>
											<input type="checkbox" checked="checked" name="quick_panel_notifications_6">
											<span></span>
										</label>
									</span>
								</div>
							</div>
						</form>
					</div>
				</div>
			</div>
		</div>
        -->
		<!-- end::Quick Panel -->

		<!-- begin::Scrolltop -->
		<div id="kt_scrolltop" class="kt-scrolltop">
			<i class="fa fa-arrow-up"></i>
		</div>

		<!-- end::Scrolltop -->

        <!--begin::Modal Customers-->
	    <div class="modal fade" id="kt_customers_modal" role="dialog" aria-labelledby="" aria-hidden="true">
								<div class="modal-dialog modal-xl" role="document">
									<div class="modal-content">
                                    <div class="kt-chat">
										<div class="modal-header">
											<h5 class="modal-title" id="">Clientes</h5>
											<button type="button" class="close" data-dismiss="modal" aria-label="Close">
												<span aria-hidden="true" class="la la-remove"></span>
											</button>
										</div>
										<form class="kt-form kt-form--fit kt-form--label-right">
											<div class="modal-body">
                                                <div class="kt-portlet__body">
                                                <div class="row table-responsive">
												    <div class="col-md-12">
                                                        <table class="table table-hover table-checkable order-column dataTable no-footer" id="tbl_customers" role="grid" aria-describedby="sample_1_info">
                                                    <thead bgcolor="#f6f7fd">
                                                        <tr role="row">
                                                            <th class="sorting_asc" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-sort="ascending" aria-label=" ID : activate to sort column descending" style="width: 35px;"> 
                                                                ID 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Username : activate to sort column ascending" style="width: 66px;"> 
                                                                Nombre Corto 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-sort="ascending" aria-label=" ID : activate to sort column descending" style="width: 135px;"> 
                                                                Nombre 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Tipo : activate to sort column ascending" style="width: 150px;"> 
                                                                Dirección
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 80px;"> 
                                                                Ciudad 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 80px;"> 
                                                                Estado 
                                                            </th>
                                                             <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 50px;"> 
                                                                CP
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 90px;"> 
                                                                Acción 
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>
												    </div>
												</div>
                                                </div>
											</div>
											<div class="modal-footer">
												<button type="button" class="btn btn-brand" data-dismiss="modal" id="btncustclose">Cerrar</button>
												<!--<button type="button" class="btn btn-secondary">Aceptar</button>-->
											</div>
										</form>
                                        </div><!--END KTCHAT-->
									</div>
								</div>
							</div>
		<!--end::Modal Customers-->

        <!--begin::Modal SaleInvoicePayment-->
	    <div class="modal fade" id="saleinvpay_modal" role="dialog" aria-labelledby="" aria-hidden="true">
								<div class="modal-dialog modal-xl" role="document">
									<div class="modal-content">
                                    <div class="kt-chat">
										<div class="modal-header">
											<h5 class="modal-title" id="H1">Cobranza</h5>
											<button type="button" class="close" data-dismiss="modal" aria-label="Close">
												<span aria-hidden="true" class="la la-remove"></span>
											</button>
										</div>
										<form id="salepayment_form" class="kt-form kt-form--fit kt-form--label-right">
											<div class="modal-body">
                                                <div class="kt-portlet__body">
                                                <div class="kt-form__content">
											        <div class="kt-alert m-alert--icon alert alert-danger kt-hidden" role="alert" id="kt_form_sp_msg">
												        <div class="kt-alert__icon">
													        <i class="la la-warning"></i>
												        </div>
												        <div class="kt-alert__text">
												        </div>
												        <div class="kt-alert__close">
													        <button type="button" class="close" data-close="alert" aria-label="Close">
													        </button>
												        </div>
											        </div>
										        </div>
                                                <div class="row kt-margin-l-5">
                                                    <h6>Cliente: &nbsp;<span id="spcustid" name="spcustid"></span>&nbsp; - &nbsp;<span id="spcustname"></span></h6>
                                                    <input type="hidden" id="spcustomerid" name="spcustomerid" value="">
                                                    <input type="hidden" id="spserie" name="spserie" value="">
                                                    <input type="hidden" id="spfolio" name="spfolio" value="">
                                                </div>
                                                <hr>
                                                <div class="form-group row form-group-marginless kt-margin-r-5">
														<div class="col-lg-4">
                                                            <span class="form-text text-muted">Forma pago</span>
															<select class="form-control kt-input select2" id="sppay_method" name="sppay_method">
												            </select>
														</div>
														<div class="col-lg-4">
															<span class="form-text text-muted">Banco</span>
															<select class="form-control kt-input select2" id="spbank" name="spbank">
												            </select>
														</div>
														<div class="col-lg-4">
															 <span class="form-text text-muted">Cuenta</span>
															<select class="form-control kt-input select2" id="spaccount" name="spaccount">
												             </select>
														</div>
												</div>
                                                <div class="form-group row form-group-marginless kt-margin-r-5">
														<div class="col-lg-4">
                                                            <span class="form-text text-muted">Monto cobro</span>
															<input type="number" class="form-control form-control-danger" readonly="readonly" placeholder="" id="sppay_total" name="sppay_total" value="">
												            <%--</select>--%>
														</div>
														<div class="col-lg-4">
															<span class="form-text text-muted">Moneda</span>
															<select class="form-control kt-input select2" id="spcurrency" name="spcurrency">
												            </select>
														</div>
														<div class="col-lg-4">
															 <span class="form-text text-muted">Tipo de cambio</span>
															<input type="number" class="form-control form-control-danger" placeholder="" name="spexchange_rate" value="1">
														</div>

												</div>
                                                <div class="form-group row form-group-marginless kt-margin-r-5">
														<div class="col-lg-4">
                                                            <span class="form-text text-muted">Referencia</span>
															<input type="text" class="form-control form-control-danger" placeholder="" name="spreference" value="">
														</div>
														<div class="col-lg-4">
                                                            <span class="form-text text-muted">Correo</span>
															<div class="input-group">
															<input type="text" class="form-control fieldvalidate" id="spemail" name="spemail" placeholder="">
                                                            <div class="input-group-append"><span class="input-group-text" id="Span2"><a href="javascript:;" class="kt-link" onclick="sendSPMail();"><i class="fa fa-paper-plane kt-font-primary"></i></a></span></div>
															<!--<span class="kt-input-icon__icon kt-input-icon__icon--right"><span><a href="javascritp:;" onclick="sendInvMail();"><i class="fa fa-paper-plane fa-success"></i></a></span></span>-->
														</div>
														</div>
														<div class="col-lg-4">
															
														</div>

												</div>
                                                <div class="row table-responsive">
                                                        <table class="table table-hover table-checkable order-column dataTable no-footer range" id="tblsaleinvpay" role="grid" aria-describedby="sample_1_info">
                                                    <thead bgcolor="#f6f7fd">
                                                        <tr role="row">
                                                            <th class="sorting_asc" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-sort="ascending" aria-label=" ID : activate to sort column descending" style="width: 35px;"> 
                                                                Folio 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Username : activate to sort column ascending" style="width: 66px;"> 
                                                                Tipo 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-sort="ascending" aria-label=" ID : activate to sort column descending" style="width: 135px;"> 
                                                                Total 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Tipo : activate to sort column ascending" style="width: 150px;"> 
                                                                Cobrado
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 80px;"> 
                                                                Balance 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 80px;"> 
                                                                Cobro 
                                                            </th>
                                                             <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 50px;"> 
                                                                Moneda
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 86px;"> 
                                                                Estatus 
                                                            </th>
                                                            <th class="sorting" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label=" Email : activate to sort column ascending" style="width: 90px;"> 
                                                                Acción 
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>
												</div>
                                                </div>
											</div>
											<div class="modal-footer">
                                                <label class="col-lg-1 col-xs-2 col-form-label">
															     <a href="" id="dwppdf" style="display:none" class="kt-link" ><i class="kt-menu__link-icon fa fa-file-pdf fa-2x"></i> PDF</a>
                                                             </label>
                                                             <label class="col-lg-1 col-xs-2 col-form-label">
                                                                 <a href="" id="dwpxml" style="display:none" class="kt-link" ><i class="kt-menu__link-icon fa fa-file-code fa-2x"></i> XML</a>
                                                             </label>
												<button type="button" class="btn btn-brand" data-dismiss="modal" id="Button4">Cerrar</button>
												<button type="button" onclick="AddPayment();" class="btn btn-secondary">Cobrar</button>
											</div>
										</form>
                                        </div><!--END KTCHAT-->
									</div>
								</div>
							</div>
		<!--end::Modal SaleInvoicePayment-->

	   <!--Begin:: Modal Actions-->

       <div class="modal fade" id="InvActions" role="dialog" data-backdrop="true" data-keyboard="true">
			<div class="modal-dialog modal-dialog-centered" role="document">
				<div class="modal-content">
					<div class="kt-chat">
						<div class="kt-portlet kt-portlet--last">
							<div class="kt-portlet__head">
								<div class="kt-chat__head ">
									<div class="kt-chat__left">
										<div class="kt-chat__label">
											<span class="kt-chat__title">Acciones</span>
											<span class="kt-chat__status">
												<!--<span class="kt-badge kt-badge--dot kt-badge--success"></span> Active-->
											</span>
										</div>
									</div>
									<div class="kt-chat__right">
										<button type="button" class="btn btn-clean btn-sm btn-icon" data-dismiss="modal" id="actionDismiss">
											<i class="flaticon2-cross"></i>
										</button>
									</div>
								</div>
							</div>
                            <form id="payDate">                              
							    <div class="kt-portlet__body">
								    <div class="kt-scroll kt-scroll--pull" data-height="210" data-mobile-height="125">
									    <div class="form-group row">
													    <label class="col-form-label col-lg-3 col-sm-12">Fecha de Pago</label>
													    <div class="col-lg-9 col-md-9 col-sm-12">
														    <div class="input-group date">
															    <input type="text" class="form-control" name="kt_datepicker_2_modal" readonly placeholder="Seleccione Fecha" id="kt_datepicker_2_modal" />
                                                                <input type="hidden"  name="pinvID" id="pinvID" value=""/>
															    <div class="input-group-append">
																    <span class="input-group-text">
																	    <i class="la la-calendar-check-o"></i>
																    </span>
															    </div>
														    </div>
													    </div>
												    </div>
								    </div>
							    </div>
							    <div class="kt-portlet__foot">
								    <div class="kt-chat__input">
									    <div class="kt-chat__toolbar">
										    <div class="kt_chat__tools">
										    </div>
										    <div class="kt_chat__actions">
											    <button type="button" class="btn btn-brand btn-md  btn-font-sm btn-upper btn-bold kt-chat__reply" onclick="updatePay(); return false;">Actualizar</button>
										    </div>
									    </div>
								    </div>
							    </div>
                        </form>
						</div>
					</div>
				</div>
			</div>
		</div>
       
       <!--End:: Modal Actions-->

       <!--Begin:: Modal SAT CODE-->

       <div class="modal fade" id="SatCode" role="dialog" aria-labelledby="" aria-hidden="true">
								<div class="modal-dialog modal-dialog-centered" role="document">
									<div class="modal-content">
										<div class="modal-header">
											<h5 class="modal-title" id="H2">Catálogo de Productos</h5>
											<button type="button" id="satcodemodalclose" class="close" data-dismiss="modal" aria-label="Close">
												<span aria-hidden="true" class="la la-remove"></span>
											</button>
										</div>
										<form class="kt-form kt-form--fit kt-form--label-right">
											<div class="modal-body">
												<div class="form-group row kt-margin-t-20">
													<label class="col-form-label col-lg-12">Productos</label>
													<div class="col-lg-12">
                                                        <input type="hidden" id="clkControl" />
														<select class="form-control kt-select" id="kt_select2_1" name="lnsatcode2">
														</select>
													</div>
												</div>
											</div>
											<div class="modal-footer">
												<!--<button type="button" class="btn btn-brand" data-dismiss="modal">Cerrar</button>-->
												<button type="button" id="btnSatCode" class="btn btn-brand" data-dismiss="modal" onclick="setCode(); return false;">Aceptar</button>
											</div>
										</form>
									</div>
								</div>
							</div>
       
       <!--End:: Modal SAT CODE-->

		<!--Begin:: Chat-->
		<div class="modal fade- modal-sticky-bottom-right" id="kt_chat_modal" role="dialog" data-backdrop="false">
			<div class="modal-dialog" role="document">
				<div class="modal-content">
					<div class="kt-chat">
						<div class="kt-portlet kt-portlet--last">
							<div class="kt-portlet__head">
								<div class="kt-chat__head ">
									<div class="kt-chat__left">
										<div class="kt-chat__label">
                                            <div class="kt-space-20"></div>
											<span class="kt-chat__title">Datos de la Factura</span>
											<span class="kt-chat__status">
												<!--<span class="kt-badge kt-badge--dot kt-badge--success"></span> Active-->
											</span>
										</div>
                                        <div class="kt-space-10"></div>
                                        <div class="kt-chat__label">
                                        <b>Proveedor:</b> <span id="supNameDet"></span>
                                        </div>
                                        <div class="kt-chat__label"><b>Orden de Compra:</b> <span id="poDet"></span>
                                        </div>
                                        <div class="kt-chat__label"><b>Documento:</b> <span id="typeDet"></span> 
                                        </div>
                                        <div class="kt-chat__label"><b>UUID:</b> <span id="uuidDet"></span>
                                        </div>
                                        <div class="kt-space-20"></div>
									</div>
									<div class="kt-chat__right">
										<div class="dropdown dropdown-inline">
											<button type="button" class="btn btn-clean btn-md btn-icon" onclick="getInvHistory();" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
												<i class="flaticon-calendar-with-a-clock-time-tools"></i>
											</button>
											<div class="dropdown-menu dropdown-menu-fit dropdown-menu-right dropdown-menu-xl">
                                                
												<!--begin::Nav-->
												<ul class="kt-nav" id="lstHistory">
													<li class="kt-nav__head">
														Historial
														<i class="flaticon2-information" data-toggle="kt-tooltip" data-placement="right" title="Historial de la Factura..."></i>
													</li>
													<li class="kt-nav__separator"></li>
                                                    <li class="kt-nav__item">
                                                        <div class="kt-widget2">
                                                                <div class="kt-widget2__item kt-widget2__item--danger">
                                                                    <div class="kt-widget2__checkbox">
																        <label>
																	        <span></span>
																        </label>
															        </div>
															        <div class="kt-widget2__info">
																        <a href="#" class="kt-widget2__title">
																	        Factura Registrada (Comentario de la factura)
																        </a>
																        <a href="#" class="kt-widget2__username">
																	        Por el Usuario Encargado el 22-May-2020 14:43pm
																        </a>
															        </div>
															        <div class="kt-widget2__actions">
																        
															        </div>
														        </div>
                                                            </div>
                                                    </li>
													<li class="kt-nav__item">
														<div class="kt-widget2">
                                                                <div class="kt-widget2__item kt-widget2__item--info">
                                                                    <div class="kt-widget2__checkbox">
																        <label>
																	        <span></span>
																        </label>
															        </div>
															        <div class="kt-widget2__info">
																        <a href="#" class="kt-widget2__title">
																	        Factura Autorizada 
																        </a>
																        <a href="#" class="kt-widget2__username">
																	        Por el Usuario Administrador el 22-May-2020 16:22pm
																        </a>
															        </div>
															        <div class="kt-widget2__actions">
															        </div>
														        </div>
                                                            </div>
													</li>
													<li class="kt-nav__item">
														<div class="kt-widget2">
                                                                <div class="kt-widget2__item kt-widget2__item--success">
                                                                    <div class="kt-widget2__checkbox">
																        <label">
																	        <span></span>
																        </label>
															        </div>
															        <div class="kt-widget2__info">
																        <a href="#" class="kt-widget2__title">
																	        Factura Pagada 
																        </a>
																        <a href="#" class="kt-widget2__username">
																	        Por el Usuario Administrador el 23-May-2020 17:49pm
																        </a>
															        </div>
															        <div class="kt-widget2__actions">
															        </div>
														        </div>
                                                            </div>
													</li>
													
													<li class="kt-nav__separator"></li>
													<li class="kt-nav__foot">
														
														<a class="btn btn-clean btn-bold btn-sm" href="javascript:;" >Cerrar</a>
													</li>
												</ul>

												<!--end::Nav-->
											</div>
										</div>
										<button type="button" class="btn btn-clean btn-sm btn-icon" data-dismiss="modal">
											<i class="flaticon2-cross"></i>
										</button>
									</div>
								</div>
							</div>
							<div class="kt-portlet__body">
								<div id="chatscroll" class="kt-scroll kt-scroll--pull" data-height="410" data-mobile-height="225">
									<div class="kt-chat__messages kt-chat__messages--solid">
                                        <!--
										<div class="kt-chat__message kt-chat__message--right kt-chat__message--brand">
											<div class="kt-chat__user">
												<div class="kt-badge kt-badge--md kt-badge--brand">U</div>
												<a href="#" class="kt-chat__username">Usuario</a>
												<span class="kt-chat__datetime">4 Horas</span>
											</div>
											<div class="kt-chat__text">
												Hola, no veo la fecha de pago de la factura
											</div>
										</div>
										<div class="kt-chat__message kt-chat__message--success">
											<div class="kt-chat__user">
												<div class="kt-badge kt-badge--md kt-badge--brand">N</div>
                                                <a href="#" class="kt-chat__username">Encargado</a>
												<span class="kt-chat__datetime">2 Horas</span>
       										</div>
                                            <div class="kt-chat__text">
												Ya establecimos la fecha, saludos
											</div>
										</div>
                                        -->
									</div>
								</div>
							</div>
							<div class="kt-portlet__foot">
								<div class="kt-chat__input">
									<div class="kt-chat__editor">
										<textarea placeholder="Escribe tu mensaje..." style="height: 50px"></textarea>
									</div>
									<div class="kt-chat__toolbar">
										<div class="kt_chat__tools">
											<a href="#"><i class="flaticon2-link"></i></a>
											<a href="#"><i class="flaticon2-photograph"></i></a>
											<a href="#"><i class="flaticon2-photo-camera"></i></a>
										</div>
										<div class="kt_chat__actions">
											<button type="button" class="btn btn-brand btn-md  btn-font-sm btn-upper btn-bold kt-chat__reply">Responder</button>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
		<!--ENd:: Chat-->

		<!-- begin::Global Config(global config for global JS sciprts) -->
		<script>

            //$("#loadPageAlmex").load("https://www.google.com/index.htm");

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

        <% Response.Write(scripts); %>

		<!-- end::Global Config -->

		<!--begin::Global Theme Bundle(used by all pages) -->
		<script src="assets/plugins/global/plugins.bundle.js" type="text/javascript"></script>
		<script src="assets/js/scripts.bundle.js" type="text/javascript"></script>

		<!--end::Global Theme Bundle -->

		<!--begin::Page Vendors(used by this page) -->
		<script src="assets/plugins/custom/fullcalendar/fullcalendar.bundle.js" type="text/javascript"></script>
        <script src="assets/plugins/custom/datatables/datatables.bundle.js" type="text/javascript"></script>
        <script src="assets/plugins/custom/uppy/uppy.bundle.js" type="text/javascript"></script>

		<!--end::Page Vendors -->

		<!--begin::Page Scripts(used by this page) -->
		<script src="assets/js/pages/dashboard.js?9" type="text/javascript"></script>
        <script src="assets/js/pages/crud/forms/widgets/bootstrap-datepicker.js" type="text/javascript"></script>
        <script src="assets/js/pages/components/extended/sweetalert2.js" type="text/javascript"></script>
        <script src="assets/js/pages/components/extended/bootstrap-notify.js" type="text/javascript"></script>
        <script src="assets/js/pages/components/extended/blockui.js" type="text/javascript"></script>
        <script src="assets/js/pages/components/utils/session-timeout.js" type="text/javascript"></script>

		<!--end::Page Scripts -->
	</body>

	<!-- end::Body -->
</html>