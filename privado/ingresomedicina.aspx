<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ingresomedicina.aspx.vb" Inherits="privado_compramedicina" %>
<!doctype html>
<html lang="en">

<head>
	<meta charset="utf-8"/>
	<title> BIO * NATURAL Online</title>
	
	<link rel="stylesheet" href="../css/layout.css" type="text/css" media="screen" />
    <link href="../css/notifications.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery-ui-1.8.24.custom.css" rel="stylesheet" type="text/css" />
	
    <style type="text/css">
    
    
    .youtubeSmall:hover, .youtubeSmall:focus {
   outline: none;
border: 1px solid #77BACE;
-webkit-box-shadow: inset 0 2px 2px #ccc, 0 0 10px #ADDCE6;
-moz-box-shadow: inset 0 2px 2px #ccc, 0 0 10px #ADDCE6;
box-shadow: inset 0 2px 2px #ccc, 0 0 10px #ADDCE6; 
background:#F2F5A9;
cursor:pointer;
}
 
.youtubeSmall:active {
   border: 1px solid #AAA;
   border-bottom-color: #CCC;
   border-top-color: #999;
   -webkit-box-shadow: inset 0 1px 2px #aaa;
   -moz-box-shadow:     inset 0 1px 2px #aaa;
   box-shadow:          inset 0 1px 2px #aaa;
   background: -webkit-linear-gradient(top, #E6E6E6, gainsboro);
   background:  -moz-linear-gradient(top, #E6E6E6, gainsboro);
   background:  -ms-linear-gradient(top, #E6E6E6, gainsboro);
   background:          -o-linear-gradient(top, #E6E6E6, gainsboro);
cursor:pointer;
}
    
    table {
    *border-collapse: collapse; /* IE7 and lower */
    border-spacing: 0;
    width: 95%;    
}

.bordered {
    border: solid #ccc 1px;
    -moz-border-radius: 6px;
    -webkit-border-radius: 6px;
    border-radius: 6px;
    -webkit-box-shadow: 0 1px 1px #ccc; 
    -moz-box-shadow: 0 1px 1px #ccc; 
    box-shadow: 0 1px 1px #ccc;     
}

.bordered tr:hover {
    background: #fbf8e9;
    -o-transition: all 0.1s ease-in-out;
    -webkit-transition: all 0.1s ease-in-out;
    -moz-transition: all 0.1s ease-in-out;
    -ms-transition: all 0.1s ease-in-out;
    transition: all 0.1s ease-in-out;     
}    
    
.bordered td, .bordered th {
    border-left: 1px solid #ccc;
    border-top: 1px solid #ccc;
    padding: 2px;
    text-align: left;    
}

.bordered th {
    background-color: #dce9f9;
    background-image: -webkit-gradient(linear, left top, left bottom, from(#ebf3fc), to(#dce9f9));
    background-image: -webkit-linear-gradient(top, #ebf3fc, #dce9f9);
    background-image:    -moz-linear-gradient(top, #ebf3fc, #dce9f9);
    background-image:     -ms-linear-gradient(top, #ebf3fc, #dce9f9);
    background-image:      -o-linear-gradient(top, #ebf3fc, #dce9f9);
    background-image:         linear-gradient(top, #ebf3fc, #dce9f9);
    -webkit-box-shadow: 0 1px 0 rgba(255,255,255,.8) inset; 
    -moz-box-shadow:0 1px 0 rgba(255,255,255,.8) inset;  
    box-shadow: 0 1px 0 rgba(255,255,255,.8) inset;        
    border-top: none;
    text-shadow: 0 1px 0 rgba(255,255,255,.5); 
}

.bordered td:first-child, .bordered th:first-child {
    border-left: none;
}

.bordered th:first-child {
    -moz-border-radius: 6px 0 0 0;
    -webkit-border-radius: 6px 0 0 0;
    border-radius: 6px 0 0 0;
}

.bordered th:last-child {
    -moz-border-radius: 0 6px 0 0;
    -webkit-border-radius: 0 6px 0 0;
    border-radius: 0 6px 0 0;
}

.bordered th:only-child{
    -moz-border-radius: 6px 6px 0 0;
    -webkit-border-radius: 6px 6px 0 0;
    border-radius: 6px 6px 0 0;
}

.bordered tr:last-child td:first-child {
    -moz-border-radius: 0 0 0 6px;
    -webkit-border-radius: 0 0 0 6px;
    border-radius: 0 0 0 6px;
}

.bordered tr:last-child td:last-child {
    -moz-border-radius: 0 0 6px 0;
    -webkit-border-radius: 0 0 6px 0;
    border-radius: 0 0 6px 0;
}
</style>

    <link href="source/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    
    <!--[if lt IE 9]>
	<link rel="stylesheet" href="css/ie.css" type="text/css" media="screen" />
	<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
	<![endif]-->
	<!--<script src="../js/jquery-1.5.2.min.js" type="text/javascript"></script>-->
    <script src="../js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="source/jquery.fancybox.pack.js" type="text/javascript"></script>
	<script src="../js/jquery-ui-1.8.24.custom.min.js" type="text/javascript"></script>
    <!--[if lt IE 9]>
	<link rel="stylesheet" href="css/ie.css" type="text/css" media="screen" />
	<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
	<![endif]-->

    <script src="../js/hideshow.js" type="text/javascript"></script>
	<script src="../js/jquery.tablesorter.min.js" type="text/javascript"></script>
	<script type="text/javascript" src="../js/jquery.equalHeight.js"></script>
    <script type="text/javascript" src="../js/jquery.easing.1.3.js"></script>
    <script src="../js/mylibs/jquery.notifications.js" type="text/javascript"></script>

    <script type="text/javascript">
        var usuario="";

        function mensajecorrecto(mensaje) {
            $.jGrowl(mensaje, { theme: 'success' });
        }

        function mensajeerror(mensaje) {
            $.jGrowl(mensaje, { theme: 'error' });
        }

        function mensajeadvertencia(mensaje) {
            $.jGrowl(mensaje, { theme: 'warning' });
        }


        $(function () {

            $("#txtfecha").datepicker({
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy',
                showAnim: 'slide',
                firstDay: 0,
                onSelect: function (dateText) {
                    document.getElementById('txtfactura').focus();
                }
            });
        });

        function formatofecha(fecha, separador) {
            var arreglofecha = fecha.split("/");
            var anio = arreglofecha[2];
            var mes = arreglofecha[1];
            var dia = arreglofecha[0];
            return (anio + separador + mes + separador + dia);
        }

        function abremodal(ruta) {
            $.fancybox.open({
                href: ruta,
                type: 'iframe',
                padding: 5
            });
        }

        function mostrarmodal(control) {
            $.fancybox.open("#" + control);
        }


        /*INICIO DOCUMENT READY*/
        $(document).ready(function () {

            usuario = document.getElementById('usuario').innerHTML;
            $("#btnseleccionartratamientos").click(function () {
                mostrarmodal('divseleccionartratamientos');
            });

            $('.checks').live('click', function () {
                var correlativo = $(this).closest("tr").find(".correlativo").html();
                //var estado = $(this).attr('checked');
                var estado = $(this).is(':checked');  // true
                $.ajax({
                    type: "POST",
                    url: "../WSIngresomedicina.asmx/mostrarEnReporte",
                    data: '{estado:"' + estado + '",correlativo:'+ correlativo +'}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d.substring(0, 5) == "ERROR") {
                            mensajeerror(msg.d);
                        } else {
                            mensajecorrecto(msg.d);
                        }
                    },
                    error: function (msg) { mensajeadvertencia(msg.responseText); }
                });
            });

            $(".letras").click(function () {
                var letra = $(this).val();
                console.log(letra);
                $.ajax({
                    type: "POST",
                    url: "../WSIngresomedicina.asmx/datosProductoPorLetra",
                    data: '{letra:"' + letra + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {

                        var html = '';
                        $.each(msg.d, function () {

                            var codt = this.CodT;
                            var descripcion = this.descripcion;
                            var correlativo = this.correlativo;
                            var mostrarEnReporte = this.mostrarEnReporte;
                            var selectCheck = '';
                            if (mostrarEnReporte == true)
                                selectCheck = 'checked';
                            html += '<tr class="fila">' +
                            '<td style="display:none;" class="correlativo">' + correlativo + '</td>' +
                            '<td>' + codt + '</td>' +
                            '<td>' + descripcion + '</td>' +
                            '<td><input class="checks" type="checkbox" ' + selectCheck + ' /></td>' +
                            '</tr>';

                        });
                        $("#tableMedicamentos").html(html);
                        setTimeout("$.fancybox.update();", 1000);
                    },
                    error: function (msg) { alert(msg.responseText); }
                });
            });

            /*---------R E P O R T E   D E   M E D I C A M E N T O S-------------*/
            $("#btnreporte").click(function () {
                var tiporeporte = document.getElementById('droptiporeporte');

                if (tiporeporte.value == "0") { mensajeadvertencia("Seleccione un tipo de reporte"); tiporeporte.focus(); return; }

                var cantidad = document.getElementById('dropcantidad');
                $.ajax({
                    type: "POST",
                    url: "../WSIngresomedicina.asmx/reporteMedicina",
                    data: "{tiporeporte:'" + tiporeporte.value + "',cantidad:'" + cantidad.value + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: reportesi,
                    error: reporteno
                });
            });

            function reportesi(msg) {
                var mensaje = msg.d;
                if (mensaje.substring(0, 5) == "ERROR")
                    Sexy.error("<h1>Control Medico Web</h1><br/><p>" + mensaje + "</p>");
                else {
                    abremodal(mensaje);
                }

            }

            function reporteno(msg) { }

            /*---------FIN REPORTE DE MEDICAMENTOS------*/

            cargaproveedores();
            cargaunidaddemedida();
            cargamedicina();

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


            //BOTON CANCELAR OPERACION ACTUALIZAR O ELIMINAR
            $('#btncancelar').click(function () {
                inicializa();
            });


            $('#btnguardar').click(function () {
                /*ENCABEZADO*/
                var fecha = document.getElementById('txtfecha');
                var factura = document.getElementById('txtfactura');
                var serie = document.getElementById('txtserie');
                var dropproveedor = document.getElementById('dropproveedor');
                var total = document.getElementById('txttotal');

                if (!fecha.value) {
                    mensajeadvertencia("No ha ingresado una fecha"); fecha.focus(); return;
                }
                else if (!factura.value || isNaN(factura.value)) {
                    mensajeadvertencia("Ingrese un numero de factura valido"); factura.focus(); return;
                }
                else if (!serie.value) {
                    mensajeadvertencia("Ingrese un numero de serie valido"); serie.focus(); return;
                }
                else if (dropproveedor.selectedIndex < 1) {
                    mensajeadvertencia("Seleccione un proveedor"); dropproveedor.focus(); return;
                }
                else {

                    //DATOS DEL ENCABEZADO
                    var arregloEncabezado = new Array();
                    arregloEncabezado.push({ idcompra: 0, factura: factura.value, serie: serie.value, nitproveedor: dropproveedor.value, fecha: formatofecha(fecha.value, ''), idusuario: usuario, total: total.value });
                    console.log(arregloEncabezado);

                    //ARREGLO DEL DETALLE DE LA GUIA
                    var arregloDetalle = new Array();
                    var n = 0;
                    var idmedicina = "";
                    var idunidadmedida = 0;
                    var cantidad = 0;
                    var preciounitario = 0;
                    var subtotal = 0;

                    $('#detalle tr').each(function (index) {

                        $(this).children("td").each(function (index2) {

                            switch (index2) {
                                case 0:
                                    cantidad = $(this).text();
                                    break;
                                case 1:
                                    idmedicina = $(this).text();
                                    break;
                                case 3:
                                    idunidadmedida = $(this).text();
                                case 4:
                                    preciounitario = $(this).text();
                                    break;
                                case 5:
                                    subtotal = $(this).text();
                                    break;
                            }

                        });
                        arregloDetalle.push({ idmedicina: idmedicina, idunidadmedida: idunidadmedida, cantidad: cantidad, preciounitario: preciounitario, subtotal: subtotal });
                        n++;
                    });

                    console.log(arregloDetalle);

                    if (n > 0) {
                        /*     LLAMADA AL WEB SERVICE     */
                        $.ajax({
                            type: "POST",
                            url: "../WSIngresomedicina.asmx/GuardarIngresoMedicina",
                            data: "{'arregloEncabezado':" + JSON.stringify(arregloEncabezado) + ",'arregloDetalle':" + JSON.stringify(arregloDetalle) + "}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: guardaGuia,
                            error: guardaGuiaerror
                        });
                    } else {
                        mensajeadvertencia("No ha ingresado ningun detalle de compra");
                        dropproveedor.focus();
                    }
                }
            });

            function guardaGuia(msg) {
                if (msg.d.substring(0, 5) == "ERROR") { mensajeerror(msg.d); }
                else {
                    mensajecorrecto(msg.d);
                    $("#detalle").html("");
                    var fecha = document.getElementById('txtfecha');
                    var factura = document.getElementById('txtfactura');
                    var serie = document.getElementById('txtserie');
                    var dropproveedor = document.getElementById('dropproveedor');
                    var cantidad = document.getElementById('txtcantidad');
                    var dropunidadmedida = document.getElementById('dropunidadmedida');
                    var dropproducto = document.getElementById('dropproducto');
                    var preciounitario = document.getElementById('txtpreciounitario');
                    var total = document.getElementById('txttotal');
                    factura.value = "";
                    fecha.vaue = "";
                    serie.value = "";
                    dropproveedor.selectedIndex = 0;
                    cantidad.value = "";
                    dropunidadmedida.selectedIndex = 0;
                    dropproducto.selectedIndex = 0;
                    preciounitario.value = "";
                    total.value = 0;
                }
            }

            function guardaGuiaerror(msg) { mensajeerror(msg.responseText); }

            //BOTON CANCELAR OPERACION ACTUALIZAR O ELIMINAR
            $('#btnagregar').click(function () {
                /*ENCABEZADO*/
                var fecha = document.getElementById('txtfecha');
                var factura = document.getElementById('txtfactura');
                var serie = document.getElementById('txtserie');
                var dropproveedor = document.getElementById('dropproveedor');
                /*DETALLE*/
                var cantidad = document.getElementById('txtcantidad');
                var dropunidadmedida = document.getElementById('dropunidadmedida');
                var dropproducto = document.getElementById('dropproducto');
                var preciounitario = document.getElementById('txtpreciounitario');
                var total = document.getElementById('txttotal');
                if (!fecha.value) {
                    mensajeadvertencia("No ha ingresado una fecha"); fecha.focus(); return;
                }
                else if (!factura.value || isNaN(factura.value)) {
                    mensajeadvertencia("Ingrese un numero de factura valido"); factura.focus(); return;
                }
                else if (!serie.value) {
                    mensajeadvertencia("Ingrese un numero de serie valido"); serie.focus(); return;
                }
                else if (dropproveedor.selectedIndex < 1) {
                    mensajeadvertencia("Seleccione un proveedor"); dropproveedor.focus(); return;
                }
                else if (!cantidad.value || isNaN(cantidad.value)) {
                    mensajeadvertencia("Ingrese una cantidad valida"); cantidad.focus(); return;
                }
                else if (dropunidadmedida.selectedIndex < 1) {
                    mensajeadvertencia("Seleccione una unidad de medida"); dropunidadmedida.focus(); return;
                }
                else if (dropproducto.selectedIndex < 1) {
                    mensajeadvertencia("Seleccione un producto o medicamento"); dropproducto.focus(); return;
                }
                else if (!preciounitario.value || isNaN(preciounitario.value)) {
                    mensajeadvertencia("Ingrese un precio valido"); precio.focus(); return;
                }
                else {

                    var codigohtml = '';
                    codigohtml = codigohtml + '<tr>' +
                                        '<td>' + cantidad.value + '</td>' +
                                        '<td style="display:none;">' + dropproducto.value + '</td>' +
                                        '<td>' + dropproducto.options[dropproducto.selectedIndex].text + '</td>' +
                                        '<td style="display:none;">' + dropunidadmedida.value + '</td>' +
                                        '<td>' + preciounitario.value + '</td>' +
                                        '<td>' + preciounitario.value * cantidad.value + '</td>' +
                                    '</tr>';
                    $("#detalle").append(codigohtml);
                    total.value = parseFloat(total.value) + (parseFloat(preciounitario.value) * parseFloat(cantidad.value));
                    cantidad.value = "";
                    dropunidadmedida.selectedIndex = 0;
                    dropproducto.selectedIndex = 0;
                    preciounitario.value = "";

                }

                cantidad.focus();
            });

            /*FIN DOCUMENT READY*/
        });

	    function cargaproveedores() {

	        $.ajax({
	            type: "POST",
	            url: "../WSIngresomedicina.asmx/datosproveedor",
	            data: '{}',
	            contentType: "application/json; charset=utf-8",
	            dataType: "json",
	            success: proveedorsi,
	            error: proveedorno
	        });
        
        }
        
        function proveedorsi(msg) {
            $.each(msg.d, function () {
                var nit = this.Nit;
                var nombre = this.Nombre;
                //alert(" preveedor con nit " + nit + " y nombre " + nombre);
                $("#dropproveedor").append($("<option></option>").attr("value", nit).text(nombre))
            });

        }

        function proveedorno(msg) {

            alert(msg.responseText)

        }



        function cargaunidaddemedida() {

            $.ajax({
                type: "POST",
                url: "../WSIngresomedicina.asmx/datosunidadmedida",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: medicinasi,
                error: medicinano
            });

        }

        function medicinasi(msg) {
            $.each(msg.d, function () {
                var id = this.idunidad;
                var descripcion = this.descripcion;
                //alert(" preveedor con nit " + nit + " y nombre " + nombre);
                $("#dropunidadmedida").append($("<option></option>").attr("value", id).text(descripcion))
            });

        }

        function medicinano(msg) {

            $.jGrowl(msg.responseText, { theme: 'success' });

        }

        function cargamedicina() {

            $.ajax({
                type: "POST",
                url: "../WSIngresomedicina.asmx/datosproducto",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: productosi,
                error: productono
            });

        }

        function productosi(msg) {
            $.each(msg.d, function () {
                var id = this.idproducto;
                var descripcion = this.idproducto;
                //alert(" preveedor con nit " + nit + " y nombre " + nombre);
                $("#dropproducto").append($("<option></option>").attr("value", id).text(descripcion))
            });

        }

        function productono(msg) {

            alert(msg.responseText)

        }

        function mensaje() {
            $.jGrowl("Prueba", { theme: 'success' });
        }

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
            <li class="icn_folder"><a href="#" style="color:#04B404;text-shadow: 1px 1px white; font-weight:bolder;">Compra de medicamento</a></li>
            
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
			<header><h3>INGRESO MEDICINA</h3></header>
				<div class="module_content">

						<fieldset style="width:48%; float:left; margin-right: 3%;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>FECHA</label>
							<input type="text" id="txtfecha" style="width:92%;"/>
						</fieldset>

                        <fieldset style="width:48%; float:left;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>FACTURA</label>
							<input type="text" id="txtfactura" style="width:92%;"/>
						</fieldset>

                        <fieldset style="width:48%; float:left;margin-right: 3%;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>SERIE</label>
							<input type="text" id="txtserie" style="width:92%;"/>
						</fieldset>

						<fieldset style="width:48%; float:left;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>PROVEEDOR</label>
							<select id="dropproveedor" style="width:92%;">
                                <option value="0">Seleccione un proveedor</option>
                            </select>
						</fieldset>
                        
                        <div class="clear"></div>

                        <fieldset>
                            
                            <div style="width:7%; float:left;">
                                <label>CANTIDAD</label>
                                <input type="text" id="txtcantidad" />
                            </div>

                            <div style="width:20%;float:left;margin-left:20px;">
                                <label>UNIDAD MEDIDA</label>
                                <select id="dropunidadmedida" >
                                    <option value="0">Seleccione Unidad</option>
                                </select>
                            </div>
                           
                            <div style="width:20%;float:left;margin-left:20px;">
                                <label>PRODUCTO</label>
                                <select id="dropproducto" >
                                    <option value="0">Seleccione producto</option>
                                </select>
                            </div>
                            <div style="width:15%;float:left;margin-left:20px;">
                                <label>PRECIO UNITARIO Q.</label>
                                <input type="text" id="txtpreciounitario" />
                           </div>
                                      <div style="width:10%;float:left;margin-left:40px;">
                                      
                                <input type="button" id="btnagregar" class="youtube" value="Agregar" style="margin-top:24px;"/>
                                     
                                     </div>
                         
                         <br />
                         

                        </fieldset>
				
                <div style="width:100%;">
                            <table id="encabezado" class="bordered">
                                <thead>
                                    <tr>
                                        <td>CANTIDAD</td>
                                        <td style="display:none;">CODIGOPRODUCTO</td>
                                        <td>PRODUCTO</td>
                                        <td style="display:none;">UNIDADMEDIDA</td>
                                        <td>PRECIO</td>
                                        <td>SUBTOTAL</td>
                                    </tr>
                                    
                                </thead>
                                <tbody id="detalle">
                                
                                </tbody>
                            </table>
                         </div>

                         <br />

                        <fieldset style="width:48%; float:right;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>TOTAL</label>
							<input type="text" id="txttotal" value="0" disabled="disabled" style="width:92%;"/>
						</fieldset>
                        <br /><br /><br /><br /><br /><br />



                </div>
			<footer>
					<div class="submit_linkizquierda">
					    <input type="button" id="btnguardar" class="youtube" value="Guardar"/>
                        
                        <input type="button" id="btnactualizar" class="youtube" value="Actualizar" style="display:none;"/>
                        <input type="button" id="btneliminar" class="youtube" value="Eliminar" style="display:none;" />
                        <input type="button" id="btncancelar" class="youtube" value="Cancelar" style="display:none;"/>
                        
				</div>
                
			</footer>
		</article>
    <!-- end of post new article -->
		
        <!-- ARTICULO DEL REPORTE -->

        <article class="module width_full">
			<header><h3>REPORTE DE MEDICINA</h3></header>
				<div class="module_content" style="min-height:75px;">

						<fieldset style="width:48%; float:left; margin-right: 3%;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>TIPO DE REPORTE</label>
							<select  id="droptiporeporte" style="width:92%;">
                                <option value="0">Seleccione un tipo</option>
                                <option value="1">Con menos existencia</option>
                                <option value="2">Con mas existencia</option>
                                <option value="3">Por Nombre</option>
                            </select>
						</fieldset>
                       

                        <fieldset style="width:48%; float:left;">                         
                                <label>CANTIDAD</label>
                                <select id="dropcantidad" >
                                    <option value="5">5</option>
                                    <option value="10">10</option>
                                    <option value="15">15</option>
                                    <option value="20">20</option>
                                    <option value="25">25</option>
                                    <option value="30">30</option>
                                    <option value="35">35</option>
                                    <option value="40">40</option>
                                    <option value="50">50</option>                                
                                  <option value="100">100</option>
                                  <option value="200">200</option>
                                  <option value="300">300</option>
                                  <option value="400">400</option>
                                  <option value="500">500</option>
                                  <option value="1000">1000</option>
                                  <option value="Todos">Todos</option>

                        </select>
                        </fieldset>
				
                </div>
			<footer>
					<div class="submit_linkizquierda">
                        <input type="button" id="btnreporte" class="youtube" value="Reporte"/>
                        <input type="button" id="btnseleccionartratamientos" class="youtube" value="Seleccionar Tratamientos"/>
				    </div>
			</footer>
		</article>

        <!-- div modal para elegir que medicamentos se van a mostrar en el reporte -->
        <article id="divseleccionartratamientos" style="display:none;"   class="module width_full">
			<header><h3>REPORTE DE MEDICINA</h3></header>
				<div class="module_content" style="width:600px;">
                            <table id="Table1" class="bordered">
                                <thead>
                                    <tr>
                                        <td>CODIGO</td>
                                        <td>MEDICAMENTO</td>
                                        <td>MOSTRAR</td>
                                    </tr>
                                    
                                </thead>
                                <tbody id="tableMedicamentos">
                                
                                </tbody>
                            </table>
                </div>
			<footer>
				<div class="submit_linkizquierda" style="height:50px;">
                     <input type="button" class="youtubeSmall letras" value="1"/>
                     <input type="button" class="youtubeSmall letras" value="A"/>
                     <input type="button" class="youtubeSmall letras" value="B"/>
                     <input type="button" class="youtubeSmall letras" value="C"/>
                     <input type="button" class="youtubeSmall letras" value="D"/>
                     <input type="button" class="youtubeSmall letras" value="E"/>
                     <input type="button" class="youtubeSmall letras" value="F"/>
                     <input type="button" class="youtubeSmall letras" value="G"/>
                     <input type="button" class="youtubeSmall letras" value="H"/>
                     <input type="button" class="youtubeSmall letras" value="I"/>
                     <input type="button" class="youtubeSmall letras" value="J"/>
                     <input type="button" class="youtubeSmall letras" value="K"/>
                     <input type="button" class="youtubeSmall letras" value="L"/>
                     <input type="button" class="youtubeSmall letras" value="M"/>
                     <input type="button" class="youtubeSmall letras" value="N"/>
                     <input type="button" class="youtubeSmall letras" value="O"/>
                     <input type="button" class="youtubeSmall letras" value="P"/>
                     <input type="button" class="youtubeSmall letras" value="Q"/>
                     <input type="button" class="youtubeSmall letras" value="R"/>
                     <input type="button" class="youtubeSmall letras" value="S"/>
                     <input type="button" class="youtubeSmall letras" value="T"/>
                     <input type="button" class="youtubeSmall letras" value="U"/>
                     <input type="button" class="youtubeSmall letras" value="V"/>
                     <input type="button" class="youtubeSmall letras" value="W"/>
                     <input type="button" class="youtubeSmall letras" value="X"/>
                     <input type="button" class="youtubeSmall letras" value="Y"/>
                     <input type="button" class="youtubeSmall letras" value="Z"/>

                     
				 </div>
			</footer>
		</article>
        <!--FIN ARTICULO DEL REPORTE -->

		<!-- end of styles article -->
		<div class="spacer"></div>
	</section>

</form>
</body>

</html>