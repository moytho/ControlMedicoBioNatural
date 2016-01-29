<%@ Page Language="VB" AutoEventWireup="false" CodeFile="partedelcuerpo.aspx.vb" Inherits="privado_partedelcuerpo" %>

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

            function cargaPartesCuerpo (){
                $.ajax({

                    type: "POST",
                    url: "../WSPartedelcuerpo.asmx/PartedelcuerpoDatos",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $("#droppartedelcuerpo").html("");
                        $("#dropbusqueda").html("");

                        $.each(msg.d, function () {
                            $("#dropbusqueda").append($("<option></option>").attr("value", this.idpartedelcuerpo).text(this.nombre));
                            $("#droppartedelcuerpo").append($("<option></option>").attr("value", this.idpartedelcuerpo).text(this.nombre));
                            
                            });
                    },
                    error: function (msg) { alert('Error: ' + msg.responseText); }
                });
            }

            function cargaSubPartesCuerpo (){
                $.ajax({
	                type: "POST",
	                url: "../WSPartedelcuerpo.asmx/SubPartedelcuerpoDatos",
	                data: "{}",
	                contentType: "application/json; charset=utf-8",
	                dataType: "json",
	                success: function (msg) {
	                    $("#dropbusqueda").html("");
	                    $("#droppartedelcuerpo").html("");
	                        $.each(msg.d, function () {
	                            $("#dropbusqueda").append($("<option></option>").attr("value", this.idsubpartedelcuerpo).text(this.nombre))
	                            $("#droppartedelcuerpo").append($("<option></option>").attr("value", this.idsubpartedelcuerpo).text(this.nombre))
                        });
	                },
	                error: function (msg) { alert('Error: ' + msg.responseText);}
	            });
            }


            $(document).ready(function () {

                //FUNCION UTILIZADA PARA OCULTAR BOTON ACTUALIZAR,ELIMINAR,CANCELAR Y PARA MOSTRAR EL BOTON GUARDAR, LIMPIAR FORMULARIO Y FOCO A UN TEXTO
                function inicializa() {
                    document.getElementById('btnguardar').style.display = '';
                    document.getElementById('btnactualizar').style.display = 'none';
                    document.getElementById('btneliminar').style.display = 'none';
                    document.getElementById('btncancelar').style.display = 'none';
                    document.getElementById("formulario").reset();
                    document.getElementById('txtnombre').focus();
                }


                //cargaPartesCuerpo();
                //cargaSubPartesCuerpo();
                cargaPartesCuerpo();
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


                //BOTON GUARDAR PARTE DEL CUERPO
                $('#btnguardar').click(function () {
                    var nombre = document.getElementById('txtnombre').value;
                    var idpartedelcuerpo = document.getElementById('droppartedelcuerpo').value;
                    var dropmasculino = document.getElementById('dropmasculino').value;
                    var dropfemenino = document.getElementById('dropfemenino').value;

                    $.ajax({
                        type: "POST",
                        url: "../WSPartedelcuerpo.asmx/PartedelcuerpoGuardar",
                        data: '{nombre: "' + nombre + '",masculino: ' + dropmasculino + ',femenino: ' + dropfemenino + ',partesuperior: ' + idpartedelcuerpo + '}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: guardarsi,
                        error: guardarno
                    });

                });

                function guardarsi(msg) {
                    $.each(msg.d, function () {
                        var mensaje = this.mensaje;
                        if (mensaje.substring(0, 5) == "ERROR")
                            Sexy.error("<h1>Control Medico Web</h1><br/><p>" + mensaje + "</p>");
                        else {
                            Sexy.info("<h1>Control Medico Web</h1><br/><p>" + mensaje + "</p>");
                            $("#dropbusqueda").append($("<option></option>").attr("value", this.idpartedelcuerpo).text(this.nombre));
                            $("#droppartedelcuerpo").append($("<option></option>").attr("value", this.idpartedelcuerpo).text(this.nombre));
                            //document.getElementById('formulario').reset();
                            document.getElementById('txtnombre').focus();
                            document.getElementById('txtnombre').value = "";

                            document.getElementById('dropmasculino').selectedIndex = 0;
                            document.getElementById('dropfemenino').selectedIndex = 0;
                        }
                    });
                }
                function guardarno(msg) {
                    $.each(msg.d, function () {
                        Sexy.error("<h1>Control Medico Web</h1><p>" + msg.responseText + "</p>");
                    });
                }



                //BOTON BUSCAR PARTE DEL CUERPO
                $('#btnbuscar').click(function () {
                    var idsubpartedelcuerpo = document.getElementById('dropbusqueda').value;
                    $.ajax({
                        type: "POST",
                        url: "../WSPartedelcuerpo.asmx/PartedelcuerpoBuscar",
                        data: '{idpartedelcuerpo: "' + idsubpartedelcuerpo + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: buscarsi,
                        error: buscarno
                    });

                });

                function buscarsi(msg) {
                    console.log(msg.d);
                    $.each(msg.d, function () {
                        document.getElementById('droppartedelcuerpo').value = this.idpartesuperior;
                        document.getElementById('txtnombre').value = this.nombre;
                        document.getElementById('btnactualizar').style.display = '';
                        document.getElementById('btneliminar').style.display = '';
                        document.getElementById('btncancelar').style.display = '';
                        document.getElementById('btnguardar').style.display = 'none';
                    });
                }
                function buscarno(msg) {
                    //.responseText
                    Sexy.error("<h1>Control Medico Web</h1><br/><p>" + msg.responseText + "</p>");
                }




                //BOTON ACTUALIZAR PARTE DEL CUERPO
                $('#btnactualizar').click(function () {
                    var idparte = document.getElementById('dropbusqueda').value;
                    
                    var nombre = document.getElementById('txtnombre').value;
                    var idpartesuperior = document.getElementById('droppartedelcuerpo').value;
                    var dropmasculino = document.getElementById('dropmasculino').value;
                    var dropfemenino = document.getElementById('dropfemenino').value; 
                    $.ajax({
                        type: "POST",
                        url: "../WSPartedelcuerpo.asmx/PartedelcuerpoActualizar",
                        data: '{idparte: "' + idparte + '",nombre: "' + nombre + '", idpartesuperior: "' + idpartesuperior + '", masculino: "' + dropmasculino + '",femenino: "' + dropfemenino + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: actualizasi,
                        error: actualizano
                    });

                });

                function actualizasi(msg) {
                    $.each(msg.d, function () {
                        var mensaje = this.mensaje;
                        if (mensaje.substring(0, 5) == "ERROR")
                            Sexy.error("<h1>Control Medico Web</h1><br/><p>" + mensaje + "</p>");
                        else {
                            Sexy.info("<h1>Control Medico Web</h1><br/><p>" + mensaje + "</p>");
                            var posicion = document.getElementById('dropbusqueda').selectedIndex;
                            document.getElementById('dropbusqueda').options[posicion].text = document.getElementById('txtnombre').value;
                            inicializa();
                        }


                    });

                }
                function actualizano(msg) {
                    //.responseText
                    $.each(msg.d, function () {
                        Sexy.error("<h1>Control Medico Web</h1><p>" + msg.responseText + "</p>");
                    });
                }



                //BOTON ELIMINAR PARTE DEL CUERPO
                $('#btneliminar').click(function () {


                    Sexy.confirm('<h1>Advertencia</h1><p>¿Deseas Eliminar el registro seleccionado?</p><p>Pulsa "Ok" para continuar, o pulsa "Cancelar" para salir.</p>', { onComplete:
                function (returnvalue) {
                    if (returnvalue) {
                        var idpartedelcuerpo = document.getElementById('dropbusqueda').value;

                        $.ajax({
                            type: "POST",
                            url: "../WSPartedelcuerpo.asmx/PartedelcuerpoEliminar",
                            data: '{idpartedelcuerpo: "' + idpartedelcuerpo + '"}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: eliminasi,
                            error: eliminano
                        });
                    }
                }
                    });
                });

                function eliminasi(msg) {
                    $.each(msg.d, function () {
                        var mensaje = this.mensaje;
                        if (mensaje.substring(0, 5) == "ERROR")
                            Sexy.error("<h1>Control Medico Web</h1><br/><p>" + mensaje + "</p>");
                        else {
                            Sexy.info("<h1>Control Medico Web</h1><br/><p> Dato eliminado correctamente </p>");
                            var posicion = document.getElementById('dropbusqueda').selectedIndex;
                            document.getElementById('dropbusqueda').remove(posicion);
                            inicializa();
                        }

                    });

                }
                function eliminano(msg) {
                    $.each(msg.d, function () {
                        Sexy.error("<h1>Control Medico Web</h1><p>" + msg.responseText + "</p>");
                    });
                }



                //BOTON CANCELAR 
                $('#btncancelar').click(function () {
                    inicializa();
                });



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
            <li class="icn_folder"><a href="#" style="color:#04B404;text-shadow: 1px 1px white; font-weight:bolder;">Partes del cuerpo</a></li>
            
		</ul>
        <h3>DATOS GENERALES</h3>
		<ul class="toggle">
		    <!--<li class="icn_folder"><a href="empresa.aspx">Clinica</a></li>-->
		    <li class="icn_folder"><a href="empleado.aspx">Empleados</a></li>
            <li class="icn_folder"><a href="empleadopuesto.aspx">Cargos</a></li>
            		    <li class="icn_folder"><a href="profesion.aspx">Profesiones</a></li>
            <li class="icn_folder"><a href="nacionalidad.aspx">Nacionalidades</a></li>
            <li class="icn_folder"><a runat="server" href="empresa.aspx">Datos Clinica</a></li>
		
        </ul>
		<h3>USUARIOS</h3>
		<ul class="toggle">
			<li class="icn_add_user"><a href="usuario.aspx">Usuarios</a></li>
<!--			<li class="icn_view_users"><a href="usuarioperfil.aspx">Perfiles</a></li>
			<li class="icn_profile"><a href="usuariorol.aspx">Roles</a></li>-->
		    <li class="icn_profile"><a href="usuarioactualizar.aspx">Cambiar Contrasena</a></li>
		<li class="icn_profile"><a href="logout.aspx">Cerrar sesion</a></li>
		</ul>
		
		<footer>
			<hr />
			<p><strong>Copyright &copy; 2012 Control medico web 1.0</strong></p>
			
		</footer>

	</aside><!-- end of sidebar -->
	
	<section id="main" class="column">
		
				
				
		<article class="module width_full">
			<header><h3>PARTES DEL CUERPO</h3></header>
				<div class="module_content">
						<fieldset style="width:48%; float:left; margin-right: 3%;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>Parte del cuerpo </label>
                            <select id="droppartedelcuerpo" >
						        <option>Seleccione</option>
					    </select>
						</fieldset>
                        <fieldset style="width:48%; float:left;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>Nombre de la subparte </label>
                            <input type="text" id="txtnombre" style="width:92%;"/>
						</fieldset>

                        <fieldset style="width:48%; float:left;margin-right: 3%;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>APLICA A UNA MUJER</label>
							<select id="dropfemenino">
                                <option value="1">SI</option>
                                <option value="2">NO</option>
                            </select>
						</fieldset>

                        <fieldset style="width:48%; float:left;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>APLICA A UN HOMBRE</label>
							<select id="dropmasculino">
                                <option value="1">SI</option>
                                <option value="2">NO</option>
                            </select>
						</fieldset>


						<div class="clear"></div>
				</div>
			<footer>
					<div class="submit_linkizquierda">
					    <input type="button" id="btnguardar" class="youtube" value="Guardar"/>
                        <input type="button" id="btnactualizar" class="youtube" value="Actualizar" style="display:none;"/>
                        <input type="button" id="btneliminar" class="youtube" value="Eliminar" style="display:none;" />
                        <input type="button" id="btncancelar" class="youtube" value="Cancelar" style="display:none;"/>
                        
				</div>
                
                <div class="submit_link">
					Busqueda
                    <select id="dropbusqueda" >
						<option>Seleccione</option>
					</select>
					<input type="button" id="btnbuscar" class="youtube" value="Buscar"/>
				</div>
			</footer>
		</article><!-- end of post new article -->
		
		<!-- end of styles article -->
		<div class="spacer"></div>
	</section>

</form>
</body>

</html>
