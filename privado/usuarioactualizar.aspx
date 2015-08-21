<%@ Page Language="VB" AutoEventWireup="false" CodeFile="usuarioactualizar.aspx.vb" Inherits="privado_usuarioactualizar" %>

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
	<script type="text/javascript">
	    var idusuario = '';
	        //FUNCION PARA MOSTRAR USUARIOS EN EL DROP DOWN
	    $(document).ready(function () {

	        idusuario = document.getElementById('usuario').innerHTML;

	        //When page loads...
	        $(".tab_content").hide(); //Hide all content
	        $("ul.tabs li:first").addClass("active").show(); //Activate first tab
	        $(".tab_content:first").show(); //Show first tab content

	        //On Click Event
	        $("ul.tabs li").click(function () {

	            $("ul.tabs li").removeClass("active"); //Remove any "active" class
	            $(this).addClass("active"); //Add "active" class to selected tab
	            $(".tab_content").hide(); //Hide all tab content

	            var activeTab = $(this).find("a").attr("href"); //Find the href attribute value to identify the active tab + content
	            $(activeTab).fadeIn(); //Fade in the active ID content
	            return false;
	        });

	        //	        //BOTON GUARDAR USUARIOS
	        $('#btnguardar').click(function () {
	            var passwordactual = document.getElementById('txtpasswordactual');
	            var passwordnueva = document.getElementById('txtpasswordnueva');
	            var passwordconfirmacion = document.getElementById('txtpasswordconfirmacion');

	            if (!passwordactual.value)
	                Sexy.error("<h1>Control Medico Web</h1><br/><p> Ingrese su contraseña actual </p>");
	            else if (!passwordnueva.value)
	                Sexy.error("<h1>Control Medico Web</h1><br/><p> Ingrese su nueva contraseña </p>");
	            else if (!passwordconfirmacion.value)
	                Sexy.error("<h1>Control Medico Web</h1><br/><p> Ingrese nuevamente su nueva contraseña </p>");
	            else if (passwordactual.value == passwordnueva.value)
	                Sexy.error("<h1>Control Medico Web</h1><br/><p> La contraseña no puede ser igual a la anterior </p>");
	            else if (passwordnueva.value != passwordconfirmacion.value)
	                Sexy.error("<h1>Control Medico Web</h1><br/><p> La contraseña de confirmación no coincide </p>");
	            else {
	                $.ajax({
	                    type: "POST",
	                    url: "../WSUsuario.asmx/UsuarioCP",
	                    data: '{idusuario: "' + idusuario + '", passwordactual: "' + passwordactual.value + '",passwordnueva: "' + passwordnueva.value + '"}',
	                    contentType: "application/json; charset=utf-8",
	                    dataType: "json",
	                    success: guardarsi,
	                    error: guardarno
	                });
	            }
	        });

	        function guardarsi(msg) {

	            var mensaje = msg.d;
	            if (mensaje.substring(0, 5) == "ERROR")
	                Sexy.error("<h1>Control Medico Web</h1><br/><p>" + mensaje + "</p>");
	            else {
	                Sexy.info("<h1>Control Medico Web</h1><br/><p>" + mensaje + "</p>");
	                document.getElementById('formulario').reset();
	            }
	        }

	        function guardarno(msg) {
	            Sexy.error("<h1>Control Medico Web</h1><p>" + msg.responseText + "</p>");
	        }
	    });

</script>


</head>


<body>
<form id="formulario" >
	<header id="header">
		<hgroup>
			<h1 class="site_title"><a href="menu.aspx">BIO * NATURAL Online</a></h1>
			<h2 class="section_title">BIO * NATURAL</h2><div class="btn_view_site"></div>
		</hgroup>
	</header> <!-- end of header bar -->
	
	<section id="secondary_bar">
		<div class="user">
			<p>Usuario Conectado: <asp:Label Text="" style="text-transform:uppercase;color:#04B404;text-shadow: 1px 1px white; font-weight:bolder;" ID="usuario" runat="server"></asp:Label></p>
            <!-- <a class="logout_user" href="#" title="Logout">Logout</a> -->
		</div>
		<div class="breadcrumbs_container">
			<article class="breadcrumbs"><a href="menu.aspx">BIO * NATURAL Online</a> <div class="breadcrumb_divider"></div> <a class="current">Web</a></article>
		</div>
	</section><!-- end of secondary bar -->
	
	<aside id="sidebar" class="column">
		
                                    <h3>PACIENTES</h3>
		<ul class="toggle">
            <li class="icn_new_article"><a href="pacienteconsulta.aspx">Consulta</a></li>
			<li class="icn_edit_article"><a href="paciente.aspx">Paciente</a></li>
			<li class="icn_categories"><a  href="pacientehistorial.aspx">Historial</a></li>
		</ul>
        <h3>MEDICAMENTOS</h3>
		<ul class="toggle">
            <li class="icn_folder"><a href="ingresomedicina.aspx" >Compra de medicamento</a></li>
            
		</ul>
		<h3>DATOS DE CONSULTA</h3>
		<ul class="toggle">
			<li class="icn_folder"><a href="tratamiento.aspx">Tratamientos</a></li>
			<li class="icn_folder"><a href="sintoma.aspx">Sintomas</a></li>
            <li class="icn_folder"><a href="partedelcuerpo.aspx">Partes del cuerpo</a></li>
            
		</ul>
        <h3>DATOS GENERALES</h3>
		<ul class="toggle">
		    <!--<li class="icn_folder"><a href="empresa.aspx">Clinica</a></li>-->
		    <li class="icn_folder"><a href="empleado.aspx">Empleados</a></li>
            <li class="icn_folder"><a href="empleadopuesto.aspx">Cargos</a></li>
            <li class="icn_folder"><a href="profesion.aspx">Profesiones</a></li>
            <li class="icn_folder"><a href="nacionalidad.aspx">Nacionalidades</a></li>
		
        </ul>
		<h3>USUARIOS</h3>
		<ul class="toggle">
			<li class="icn_add_user"><a  href="usuario.aspx">Usuarios</a></li>
<!--			<li class="icn_view_users"><a href="usuarioperfil.aspx">Perfiles</a></li>
			<li class="icn_profile"><a href="usuariorol.aspx">Roles</a></li>-->
		    <li class="icn_profile"><a style="color:#04B404;text-shadow: 1px 1px white; font-weight:bolder;" href="usuarioactualizar.aspx">Cambiar Contrasena</a></li>
		<li class="icn_profile"><a href="logout.aspx">Cerrar sesion</a></li>
		</ul>
		
		<footer>
			<hr />
			<p><strong>Copyright &copy; 2012 Control medico web 1.0</strong></p>
			
		</footer>

	</aside><!-- end of sidebar -->
	
	<section id="main" class="column">
		
				
				
		<article class="module width_full">
			<header><h3>DATOS USUARIOS</h3></header>
				<div class="module_content">
						
                        <fieldset style="width:48%; float:left; margin-right: 3%;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>Contraseña actual</label>
							<input type="password" id="txtpasswordactual" class="textopaciente" style="width:92%;"/>
						</fieldset>

						<fieldset style="width:48%; float:left;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>Nueva Contraseña</label>
							<input type="password" id="txtpasswordnueva" class="textopaciente" style="width:92%;"/>
						</fieldset>
						
                        <fieldset style="width:48%; float:left; margin-right: 3%;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>Confirme contraseña</label>
                                <input type="password" id="txtpasswordconfirmacion" class="textopaciente" style="width:92%;"/>
                            </fieldset>
                         <div class="clear"></div>
				</div>
			<footer>
					<div class="submit_linkizquierda">
					    <input type="button" id="btnguardar" class="youtube" value="Guardar"/>
				    </div>
               
                <div class="submit_link">

               	</div>
			</footer>
		</article><!-- end of post new article -->
		
		<!-- end of styles article -->
		<div class="spacer"></div>
	</section>

</form>
</body>

</html>
