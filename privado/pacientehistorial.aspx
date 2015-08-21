<%@ Page Language="VB" AutoEventWireup="false" CodeFile="pacientehistorial.aspx.vb" Inherits="privado_pacientehistorial" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!doctype html>
<html lang="en">

<head>
	<meta charset="utf-8"/>
	<title> BIO * NATURAL Online</title>
	
	<link rel="stylesheet" href="../css/layout.css" type="text/css" media="screen" />

        <link href="../css/notifications.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery-ui-1.8.24.custom.css" rel="stylesheet" type="text/css" />

	<link rel="stylesheet" href="../css/sexyalertbox.css" type="text/css" media="screen" />
    <link href="source/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    
    <!--[if lt IE 9]>
	<link rel="stylesheet" href="css/ie.css" type="text/css" media="screen" />
	<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
	<![endif]-->
	<!--<script src="../js/jquery-1.5.2.min.js" type="text/javascript"></script>-->
    <script src="../js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="source/jquery.fancybox.pack.js" type="text/javascript"></script>
	<script src="../js/hideshow.js" type="text/javascript"></script>
	<script src="../js/jquery.tablesorter.min.js" type="text/javascript"></script>
	<script type="text/javascript" src="../js/jquery.equalHeight.js"></script>
    <script type="text/javascript" src="../js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="../js/sexyalertbox.v1.2.jquery.js"></script>
    <script type="text/javascript" src="../js/jquery-ui-1.8.24.custom.min.js"></script>
    <link href="../css/jquery-ui-1.8.24.custom.css" rel="stylesheet" type="text/css" />
    <script src="../js/webcam.js" type="text/javascript"></script>
        <script src="../js/mylibs/jquery.notifications.js" type="text/javascript"></script>

       
    <script type="text/javascript">
        var fechaGeneral = '';

        $(function () {
            $("#txtbusqueda").autocomplete({
                autoFocus: true,
                source: function (request, response) {
                    $.ajax({
                        url: "../WSPaciente.asmx/PacienteBuscarAutocomplete",
                        data: "{ 'busquedapaciente': '" + request.term + "','tipoBusqueda':'" + determinaTipoBusqueda() + "' }",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item.nombre.toUpperCase(),
                                    codigo: item.idpaciente
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                minLength: 2,
                select: seleccionaPaciente
            });
        });


        function seleccionaPaciente(event, ui) {
            var codigopaciente = ui.item.codigo;
            creapacientesporcodigo(codigopaciente);
        }

    function determinaTipoBusqueda() {
            var opcionBusqueda = 0;

            if ($("#optCodigo").is(':checked')) opcionBusqueda = 1;
            if ($("#optNombre").is(':checked')) opcionBusqueda = 2;
            if ($("#optApellido").is(':checked')) opcionBusqueda = 3;
            return opcionBusqueda;
    }

    function formatofecha(fecha,separador) {
        var arreglofecha = fecha.split("/");
        var anio = arreglofecha[2];
        var mes = arreglofecha[1];
        var dia = arreglofecha[0];
        return (anio +separador+ mes +separador+ dia);
    }

        function mostrarmodal(control) {
            $.fancybox.open("#" + control);
        }

                function mensajecorrecto(mensaje) {
            $.jGrowl(mensaje, { theme: 'success' });
        }

        function mensajeerror(mensaje) {
            $.jGrowl(mensaje, { theme: 'error' });
        }

        function mensajeadvertencia(mensaje) {
            $.jGrowl(mensaje, { theme: 'warning' });
        }

        function mensajeerrorpermanente(mensaje) {
            $.jGrowl(mensaje, { theme: 'error', sticky: true });
        }

        function mensajecorrectopermanente(mensaje) {
            $.jGrowl(mensaje, { theme: 'success', sticky: true });
        }

        $(function () {
            $("#txtfechapordia").datepicker({
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy',
                showAnim: 'slide',
                firstDay: 0,
                onSelect: function (dateText) {
                    var fecha = formatofecha(document.getElementById('txtfechapordia').value,".");
                    console.log(fecha);
                    creapacientespordia(fecha);
                }
            });
        });

        function fechaactual() {

            $.ajax({
                type: "POST",
                url: "../WSPaciente.asmx/FechaActual",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: fechasi,
                error: fechano
            });
        }

        function fechasi(msg) {

            fechaGeneral = msg.d;
            console.log(msg.d);
        }

        function fechano(msg) {
            mensajeerror(msg.responseText);
        }

        $(document).ready(function () {

            fechaactual();

            //optTipoBusqueda
            $('.optTipoBusqueda').click(function () {
                var busqueda = document.getElementById('txtbusqueda');
                var fecha = document.getElementById('txtfechapordia');
                busqueda.style.display = '';
                fecha.style.display = 'none'
                busqueda.value = "";
                busqueda.focus();
            });

            $('.optTipoBusquedaFecha').click(function () {
                var busqueda = document.getElementById('txtbusqueda');
                var fecha = document.getElementById('txtfechapordia');
                busqueda.style.display = 'none';
                fecha.style.display = ''
                fecha.value = fechaGeneral;
                fecha.focus();
            });

            /*EJECUCION DEL EVENTO CLICK DE LOS PACIENTES QUE ESTAN EN ESPERA PERO QUE EL DOCTOR NO LO REGISTRO EN SU TABLET*/
            $(".stylefacecompleto").live("click", function () {
                var datos = $(this).attr('id');
                var arregloDatos = datos.split("|");
                var idpaciente = arregloDatos[0];
                var idreceta = arregloDatos[1];
                $.ajax({
                    type: "POST",
                    url: "../WSConsultamedica.asmx/reimprimirReceta",
                    data: "{'idpaciente':" + idpaciente + ",'idreceta':" + idreceta + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var mensaje = msg.d;
                        if (mensaje.substring(0, 5) == "ERROR")
                            Sexy.error("<h1>Control Medico Web</h1><br/><p>" + mensaje + "</p>");
                        else {

                            abremodal(mensaje);
                        }
                    },
                    error: function (msg) { mensajeerror(msg.responseText); }
                });

            });


            /*EJECUCION DEL EVENTO CLICK DE LOS PACIENTES QUE ESTAN EN ESPERA*/
            $(".styleface").live("click", function () {
                var valor = $(this).attr('id');
                var arreglo = valor.split("|"); //posicion 0 es la receta posicion 1 es el paciente
                document.getElementById('pacienteactivo').innerHTML = document.getElementById(valor).innerHTML;
                document.getElementById('idpacienteactivo').value = valor;

                $.ajax({
                    type: "POST",
                    url: "../WSConsultamedica.asmx/PacienteReceta",
                    data: '{idreceta: ' + arreglo[1] + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: muestrarecetasi,
                    error: muestrarecetano
                });
            });



            //FUNCION UTILIZADA PARA OCULTAR BOTON ACTUALIZAR,ELIMINAR,CANCELAR Y PARA MOSTRAR EL BOTON GUARDAR, LIMPIAR FORMULARIO Y FOCO A UN TEXTO
            function inicializa() {
                document.getElementById('btnguardar').style.display = '';
                document.getElementById('btnactualizar').style.display = 'none';
                document.getElementById('btneliminar').style.display = 'none';
                document.getElementById('btncancelar').style.display = 'none';
                document.getElementById("formulario").reset();
                document.getElementById('txtnombre').focus();
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


            //FIN DOCUMENT READY
        });

        function creapacientespordia(fecha) {
            var div ='divpacientespordia';
            $.ajax({
                type: "POST",
                url: "../WSConsultamedica.asmx/ConsultaCreaPacientesAtendidos",
                data: '{usuario: "' + document.getElementById('usuario').innerHTML + '",fecha: "' + fecha + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $("#" + div).html("");
                    var html = '';
                    $.each(msg.d, function () {
                        html += '<tr>' +
                       '<td></td>' +
                       '<td><input type="button" value="' + this.nombrepaciente + '" id="' + this.idpaciente + '|'+this.idconsulta+ '" class="stylefacecompleto"/></td>' +
                        '</tr>';
                    });
                    $("#" + div).append(html);
                },
                error: function (msg) {  mensajeerror(msg.responseText); }
            });
        }

        function creapacientesporcodigo(idpaciente) {
            var div = 'divpacientespordia';
            $.ajax({
                type: "POST",
                url: "../WSConsultamedica.asmx/ConsultaCreaPacientesPorCodigo",
                data: '{usuario: "' + document.getElementById('usuario').innerHTML + '",idpaciente: "' + idpaciente + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $("#" + div).html("");
                    var html = '';
                    $.each(msg.d, function () {
                        html += '<tr>' +
                       '<td></td>' +
                       '<td><input type="button" value="' + this.nombrepaciente + '" id="' + this.idpaciente + '|' + this.idconsulta + '" class="stylefacecompleto"/></td>' +
                        '</tr>';
                    });
                    $("#" + div).append(html);
                },
                error: function (msg) { mensajeerror(msg.responseText); }
            });
        }

        function abremodal(ruta) {
            $.fancybox.open({
                href: ruta,
                type: 'iframe',
                padding: 5
            });
        }

</script>

</head>
<body>
<form id="formulario" runat="server" >

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true">
</asp:ToolkitScriptManager>
	<header id="header">
		<hgroup>
			<h1 class="site_title"><a href="menu.aspx">BIO * NATURAL Online</a></h1>
			<h2 class="section_title">BIO * NATURAL</h2><div class="btn_view_site"><!--<a href="http://www.medialoot.com">View Site</a>--></div>
		</hgroup>
	</header> <!-- end of header bar -->
	
	<section id="secondary_bar">
		<div class="user">
			<p>Usuario Conectado: <asp:Label Text="" style="text-transform:uppercase;color:#04B404;text-shadow: 1px 1px white; font-weight:bolder;" ID="usuario" runat="server"></asp:Label></p>
			<!-- <a class="logout_user" href="#" title="Logout">Logout</a> -->
		</div>
		<div class="breadcrumbs_container">
			<article class="breadcrumbs">
                <a id="mostrarpaciente" class="current" href="menu.aspx">Menu</a> 
                <div class="breadcrumb_divider"></div> 
                <a id="mostrardespacho"  href="#">Historial</a>

            </article>
		</div>
	</section><!-- end of secondary bar -->
	
	<aside id="sidebar" class="column">
	
                <h3>PACIENTES</h3>
		<ul class="toggle">
            <li class="icn_new_article"><a href="pacienteconsulta.aspx">Consulta</a></li>
			<li class="icn_edit_article"><a href="paciente.aspx">Paciente</a></li>
			<li class="icn_categories"><a  href="#" style="color:#04B404;text-shadow: 1px 1px white; font-weight:bolder;">Historial</a></li>
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
					
        <!--REIMPRESION DE LAS RECETAS GENERADAS EL DIA ACTUAL-->					
                <article id="divbusqueda" class="module width_full">
			        <header>
                         <h3>BUSQUEDA</h3><asp:HiddenField ID="hidpaciente" Value="" runat="server" />
                    </header>
                    <div class="module_content">
                        <div style="width:90%">
                            <p>
                          <label>Tipo de Busqueda</label>            
                          <input type = "radio"
                                 name = "radSize"
                                 id = "optCodigo"
                                 value = "small"
                                 checked = "checked"
                                 class="optTipoBusqueda" />
                          <label for = "sizeSmall">Codigo</label>
          
                          <input type = "radio"
                                 name = "radSize"
                                 id = "optNombre"
                                 value = "medium" class="optTipoBusqueda" />
                          <label for = "sizeMed">Nombre</label>
 
                          <input type = "radio"
                                 name = "radSize"
                                 id = "optApellido"
                                 value = "large" class="optTipoBusqueda" />
                          <label for = "sizeLarge" >Apellido</label>

                          <input type = "radio"
                                 name = "radSize"
                                 id = "optFecha"
                                 value = "large" class="optTipoBusquedaFecha" />
                          <label for = "sizeLarge" >Fecha</label>

                            </p>       
                        </div>
                        <br />
                        <div style="width:90%">
                        <input type="hidden" id="hidpaciente" value="0"/>
                            <input type="text" id="txtbusqueda" class="texto" style="width:45%;" />
                            <input type="text" id="txtfechapordia" class="texto" style="width:45%;display:none;" />
                        </div>
                     </div>
                     
                     <!--LISTADO DE PACIENTES ESPERANDO-->
                     <div class="tab_container" style="
			          height: 250px;
			          overflow-x:hidden;
                      overflow-y: scroll;">
                            <table class="sorter" cellspacing="0">

			                    <tbody id="divpacientespordia"> 				 
				                     
                                </tbody> 
			                 </table>
                        </div>

                  </article>

                <footer>
					<div class="submit_linkizquierda">				                          
                    </div>
                </footer>

        <!-- end of styles article -->
		<div class="spacer"></div>
	</section>


</form>
</body>


</html>