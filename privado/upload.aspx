<%@ Page Language="VB" AutoEventWireup="false" CodeFile="upload.aspx.vb" Inherits="privado_upload" %>

<!doctype html>
<html lang="en">

<head>
	<meta charset="utf-8"/>
	<title> BIO * NATURAL Online</title>
	
	<link rel="stylesheet" href="../css/layout.css" type="text/css" media="screen" />
	<link rel="stylesheet" href="../css/sexyalertbox.css" type="text/css" media="screen" />
	
    <!--[if lt IE 9]>
	<link rel="stylesheet" href="css/ie.css" type="text/css" media="screen" />
	<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
	<![endif]-->
	<script src="../js/jquery-1.5.2.min.js" type="text/javascript"></script>
	<script src="../js/hideshow.js" type="text/javascript"></script>
	<script src="../js/jquery.tablesorter.min.js" type="text/javascript"></script>
	<script type="text/javascript" src="../js/jquery.equalHeight.js"></script>
    <script type="text/javascript" src="../js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="../js/sexyalertbox.v1.2.jquery.js"></script>
    <script type="text/javascript" src="../js/jquery-ui-1.8.24.custom.min.js"></script>
    <script type="text/javascript" src="../js/jquery.fileupload.js"></script>
    <script type="text/javascript"  src="../js/jquery.iframe-transport.js"></script>

    <link href="../css/jquery-ui-1.8.24.custom.css" rel="stylesheet" type="text/css" />
    <style>
        .progress { position:relative; width:400px; border: 1px solid #ddd; padding: 1px; border-radius: 3px; }
        .bar { background-color: #B4F5B4; width:0%; height:20px; border-radius: 3px; }
        .percent { position:absolute; display:inline-block; top:3px; left:48%; }
    </style>

<!--SCRIPT PARA CARGAR IMAGENES AL SERVIDOR -->
<script type="text/javascript" language="javascript">
    (function () {

        $('#fileupload').fileupload({
            replaceFileInput: false,
            dateType: 'json',
            url: '<%=ResolveUrl("guardaimagen.ashx") %>',
            done: function (e, data) {
                $.each(data.result, function (index, file) {
                    $('<p/>').text(file).appendTo('body');
                });
            }
        });
    });

</script>
</head>
<body>
<form id="formulario" runat="server" >
	<header id="header">
		<hgroup>
			<h1 class="site_title"><a href="menu.aspx">BIO * NATURAL Online</a></h1>
			<h2 class="section_title">BIO * NATURAL</h2><div class="btn_view_site"><!--<a href="http://www.medialoot.com">View Site</a>--></div>
		</hgroup>
	</header> <!-- end of header bar -->
	
	<section id="secondary_bar">
		<div class="user">
			<p>Usuario Conectado: ELDER</p>
			<!-- <a class="logout_user" href="#" title="Logout">Logout</a> -->
		</div>
		<div class="breadcrumbs_container">
			<article class="breadcrumbs"><a href="menu.aspx">BIO * NATURAL Online</a> <div class="breadcrumb_divider"></div> <a class="current">Web</a></article>
		</div>
	</section><!-- end of secondary bar -->
	
	<aside id="sidebar" class="column">
	<h3>PACIENTES</h3>
		<ul class="toggle">
			<li class="icn_new_article"><a href="pacienteconsulta.html">Consulta medica</a></li>
			<li class="icn_edit_article"><a href="paciente.html">Mantenimiento de pacientes</a></li>
			<li class="icn_categories"><a href="pacientehistorial.html">Historial</a></li>
			<li class="icn_tags"><a href="pacientecita.html">Programar cita</a></li>
		</ul>
        <h3>MEDICAMENTOS</h3>
		<ul class="toggle">
			<li class="icn_new_article"><a href="medicina.html">Mantenimiento de medicinas</a></li>
			<li class="icn_edit_article"><a href="medicinaaplicacion.html">Mantenimiento de aplicacion de medicamentos</a></li>
		</ul>
        <h3>TRATAMIENTOS</h3>
		<ul class="toggle">
			<li class="icn_new_article"><a href="tratamiento.html">Mantenimiento de tratamientos</a></li>
			<li class="icn_edit_article"><a href="sintoma.html">Mantenimiento de sintomas</a></li>
            <li class="icn_edit_article"><a href="partedelcuerpo.html">Mantenimiento de partes del cuerpo</a></li>
		</ul>
		<h3>DATOS GENERALES</h3>
		<ul class="toggle">
			<li class="icn_folder"><a href="empresa.html">Mantenimiento de empresa</a></li>
			<li class="icn_folder"><a href="empleado.html">Mantenimiento de empleados</a></li>
			<li class="icn_folder"><a href="empleadopuesto.html">Mantenimiento de puestos de trabajo</a></li>
		</ul>
		<h3>USUARIOS</h3>
		<ul class="toggle">
			<li class="icn_add_user"><a href="usuario.html">Mantenimiento de usuarios</a></li>
			<li class="icn_view_users"><a href="usuarioperfil.html">Mantenimiento de perfiles</a></li>
			<li class="icn_profile"><a href="usuariorol.html">Mantenimiento de roles</a></li>
		
		
		</ul>
		
		<footer>
			<hr />
			<p><strong>Copyright &copy; 2012 Control medico web 1.0</strong></p>
			
		</footer>
	</aside><!-- end of sidebar -->
	
	<section id="main" class="column">
				
                		<article class="module width_full">
			            <header>
                            <h3>BUSQUEDA</h3><input type="hidden" id="hidpaciente" value="0" />
                        </header>
                        <div class="module_content">
                            <input type="text" id="txtbusqueda" class="texto" style="width:25%;" />
                            <input type="button" id="btncita" class="youtube" value="Crear cita"/>
                            <input type="button" id="btnbuscar" class="youtube" value="Editar info"/>
                        </div>
                        </article>

		<article class="module width_full">
			<header><h3>PACIENTES</h3></header>
				<div class="module_content">
               
               <input type="file" id="fileupload" name="file"/>
                        
                        </div>
				</div>
			<footer>
					<div class="submit_linkizquierda">
					<input type="button" id="btnguardar" class="youtube" value="Guardar"/>
                        <input type="button" id="btnactualizar" class="youtube" value="Actualizar" style="display:none;"/>
                        <input type="button" id="btneliminar" class="youtube" value="Eliminar" style="display:none;" />
                        <input type="button" id="btncancelar" class="youtube" value="Cancelar" style="display:none;"/>
                        </div>


			</footer>
		</article><!-- end of post new article -->
		
		<!-- end of styles article -->
		<div class="spacer"></div>
	</section>
</form>
</body>
</html>