﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="medicinaaplicacion.aspx.vb" Inherits="privado_medicinaaplicacion" %>

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



	    $(document).ready(function () {

	        //FUNCION UTILIZADA PARA OCULTAR BOTON ACTUALIZAR,ELIMINAR,CANCELAR Y PARA MOSTRAR EL BOTON GUARDAR, LIMPIAR FORMULARIO Y FOCO A UN TEXTO
	        function inicializa() {
	            document.getElementById('btnguardar').style.display = '';
	            document.getElementById('btnactualizar').style.display = 'none';
	            document.getElementById('btneliminar').style.display = 'none';
	            document.getElementById('btncancelar').style.display = 'none';
	            document.getElementById("formulario").reset();
	            document.getElementById('txtdescripcion').focus();
	        }

	        //FUNCION PARA MOSTRAR APLICACIONS EN EL DROP DOWN
	        $(document).ready(function () {
	            $.ajax({
	                type: "POST",
	                url: "../WSAplicacion.asmx/AplicacionDatos",
	                data: "{}",
	                contentType: "application/json; charset=utf-8",
	                dataType: "json",
	                success: datossi,
	                error: datosno
	            });
	        });

	        function datossi(msg) {
	            $("#dropbusqueda").html("");
	            $.each(msg.d, function () {
	                $("#dropbusqueda").append($("<option></option>").attr("value", this.idaplicacion).text(this.descripcion))
	            });
	        }

	        function datosno(msg) {
	            Sexy.error("<h1>Control Medico Web</h1><p>" + msg.responseText + "</p>");
	        }



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


	        //BOTON BUSCAR APLICACION
	        $('#btnbuscar').click(function () {
	            var idaplicacion = document.getElementById('dropbusqueda').value;
	            //alert("hola");
	            $.ajax({
	                type: "POST",
	                url: "../WSAplicacion.asmx/AplicacionBuscar",
	                data: '{idaplicacion: "' + idaplicacion + '"}',
	                contentType: "application/json; charset=utf-8",
	                dataType: "json",
	                success: buscarsi,
	                error: buscarno
	            });

	        });

	        function buscarsi(msg) {
	            $.each(msg.d, function () {
	                document.getElementById('txtdescripcion').value = this.descripcion;
	                document.getElementById('btnactualizar').style.display = '';
	                document.getElementById('btneliminar').style.display = '';
	                document.getElementById('btncancelar').style.display = '';
	                document.getElementById('btnguardar').style.display = 'none';
	            });
	        }
	        function buscarno(msg) {
	            Sexy.error("<h1>Control Medico Web</h1><p>" + msg.responseText + "</p>");
	        }


	        //BOTON GUARDAR APLICACION
	        $('#btnguardar').click(function () {
	            var descripcion = document.getElementById('txtdescripcion').value;
	            //alert("hola");

	            if (!descripcion)
	                Sexy.error("<h1>Control Medico Web</h1><br/><p> Ingrese la descripcion de la vía de aplicacion </p>");
	            else {
	                $.ajax({
	                    type: "POST",
	                    url: "../WSAplicacion.asmx/AplicacionGuardar",
	                    data: '{descripcion: "' + descripcion + '"}',
	                    contentType: "application/json; charset=utf-8",
	                    dataType: "json",
	                    success: guardarsi,
	                    error: guardarno
	                });
	            }
	        });

	        function guardarsi(msg) {
	            $.each(msg.d, function () {
	                var mensaje = this.mensaje;
	                //alert("hola");
	                if (mensaje.substring(0, 5) == "ERROR")
	                    Sexy.error("<h1>Control Medico Web</h1><br/><p>" + mensaje + "</p>");
	                else {
	                    Sexy.info("<h1>Control Medico Web</h1><br/><p>" + mensaje + "</p>");
	                    $("#dropbusqueda").append($("<option></option>").attr("value", this.idaplicacion).text(this.descripcion));
	                    document.getElementById('formulario').reset();
	                    document.getElementById('txtdescripcion').focus();
	                }
	            });
	        }

	        function guardarno(msg) {
	            //alert("hola");
	            Sexy.error("<h1>Control Medico Web</h1><p>" + msg.responseText + "</p>");
	        }



	        //BOTON ELIMINAR LA APLICACION SELECCIONADA
	        $('#btneliminar').click(function () {

	            Sexy.confirm('<h1>Advertencia</h1><p>¿Deseas Eliminar el registro seleccionado?</p><p>Pulsa "Ok" para continuar, o pulsa "Cancelar" para salir.</p>', { onComplete:
             function (returnvalue) {
                 if (returnvalue) {
                     var idaplicacion = document.getElementById('dropbusqueda').value;
                     $.ajax({
                         type: "POST",
                         url: "../WSAplicacion.asmx/AplicacionEliminar",
                         data: '{idaplicacion: "' + idaplicacion + '"}',
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
	                //alert("hola");
	                if (mensaje.substring(0, 5) == "ERROR")
	                    Sexy.error("<h1>Control Medico Web</h1><br/><p>" + mensaje + "</p>");
	                else {
	                    Sexy.info("<h1>Control Medico Web</h1><br/><p> Dato eliminado correctamente </p>");
	                    //alert("hola else");
	                    var posicion = document.getElementById('dropbusqueda').selectedIndex;
	                    document.getElementById('dropbusqueda').remove(posicion);
	                    inicializa();
	                }
	            });
	        }
	        function eliminano(msg) {
	            // alert("hola eliminano");
	            Sexy.error("<h1>Control Medico Web</h1><p>" + msg.responseText + "</p>");
	        }



	        //BOTON ACTUALIZAR APLICACION
	        $('#btnactualizar').click(function () {
	            var idaplicacion = document.getElementById('dropbusqueda').value;
	            var descripcion = document.getElementById('txtdescripcion').value;

	            $.ajax({
	                type: "POST",
	                url: "../WSAplicacion.asmx/AplicacionActualizar",
	                data: '{descripcion: "' + descripcion + '", idaplicacion: "' + idaplicacion + '"}',
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
	                    document.getElementById('dropbusqueda').options[posicion].text = document.getElementById('txtdescripcion').value;
	                    inicializa();
	                }
	            });
	            // select2.options[i].text = vector[i];

	        }
	        function actualizano(msg) {
	            Sexy.error("<h1>Control Medico Web</h1><p>" + msg.responseText + "</p>");
	        }






	        //BOTON CANCELAR OPERACION ACTUALIZAR O ELIMINAR
	        $('#btncancelar').click(function () {
	            inicializa();
	        });

	    });

       




	   

</script>


</head>


<body>
<form id="formulario">
	<header id="header">
		<hgroup>
			<h1 class="site_title"><a href="menu.aspx">BIO * NATURAL Online</a></h1>
			<h2 class="section_title">BIO * NATURAL</h2><div class="btn_view_site"></div>
		</hgroup>
	</header> <!-- end of header bar -->
	
	<section id="secondary_bar">
		<div class="user">
			<p>Usuario Conectado: <asp:Label Text="" ID="usuario" runat="server"></asp:Label></p>
			<!-- <a class="logout_user" href="#" title="Logout">Logout</a> -->
		</div>
		<div class="breadcrumbs_container">
			<article class="breadcrumbs"><a href="menu.aspx">BIO * NATURAL Online</a> <div class="breadcrumb_divider"></div> <a class="current">Web</a></article>
		</div>
	</section><!-- end of secondary bar -->
	
	<aside id="sidebar" class="column">

<!-- #INCLUDE FILE="include.html" -->

	</aside><!-- end of sidebar -->
	
	<section id="main" class="column">
		
				
				
		<article class="module width_full">
			<header><h3>APLICACION</h3></header>
				<div class="module_content">
						<fieldset style="width:48%; float:left; margin-right: 3%;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>DESCRIPCION</label>
							<input type="text" id="txtdescripcion" style="width:92%;"/>
						</fieldset>&nbsp;&nbsp;
						
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
					<input type="button" id="btnbuscar" class="youtube" 
                                     value="Buscar"/>
				</div>
			</footer>
		</article><!-- end of post new article -->
		
		<!-- end of styles article -->
		<div class="spacer"></div>
	</section>

    </form>
</body>

</html>