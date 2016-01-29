<%@ Page Language="VB" AutoEventWireup="false" CodeFile="tratamiento.aspx.vb" Inherits="privado_tratamiento" %>

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
    <script src="../js/jquery-1.8.2.min.js" type="text/javascript"></script>
    
    <script type="text/javascript" src="../js/jquery-ui-1.8.24.custom.min.js"></script>
        <script src="source/jquery.fancybox.pack.js" type="text/javascript"></script>

    <script type="text/javascript" src="../js/map.hilight.js"></script>
	<script src="../js/hideshow.js" type="text/javascript"></script>
	<script src="../js/jquery.tablesorter.min.js" type="text/javascript"></script>
	<script type="text/javascript" src="../js/jquery.equalHeight.js"></script>
    <script type="text/javascript" src="../js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="../js/sexyalertbox.v1.2.jquery.js"></script>
    <script type="text/javascript" src="../js/sexy-tooltips.v1.1.jquery.js"></script>
    <script type="text/javascript" src="../js/acordion.js"></script>
    <script src="../js/vallenato.js" type="text/javascript" charset="utf-8"></script>

    <link rel="stylesheet" type="text/css" href="../css/jquery.multiselect.css" />

    <link rel="stylesheet" type="text/css" href="../css/jquery-ui-1.8.24.custom.css" />
	<script type="text/javascript">

	    var idtratamiento = '';

	    $(function () {

	        $("#txtbusquedatratamiento").autocomplete({
	            source: function (request, response) {
	                $.ajax({
	                    url: "../WSConsultamedica.asmx/ConsultaTratamiento",
	                    data: "{ 'busqueda': '" + request.term + "',opcion:3 }",
	                    dataType: "json",
	                    type: "POST",
	                    contentType: "application/json; charset=utf-8",
	                    dataFilter: function (data) { return data; },
	                    success: function (data) {
	                        response($.map(data.d, function (item) {
	                            return {
	                                value: item.idtratamiento
	                                
	                            }
	                        }))
	                    },
	                    error: function (XMLHttpRequest, textStatus, errorThrown) {
	                        alert(textStatus);
	                    }
	                });
	            },
	            minLength: 2,
	            select: seleccionaTratamiento
	        });

	        $("#txttratamientopadre").autocomplete({
	            source: function (request, response) {
	                $.ajax({
	                    url: "../WSConsultamedica.asmx/ConsultaTratamiento",
	                    data: "{ 'busqueda': '" + request.term + "',opcion:3 }",
	                    dataType: "json",
	                    type: "POST",
	                    contentType: "application/json; charset=utf-8",
	                    dataFilter: function (data) { return data; },
	                    success: function (data) {
	                        response($.map(data.d, function (item) {
	                            return {
	                                value: item.idtratamiento

	                            }
	                        }))
	                    },
	                    error: function (XMLHttpRequest, textStatus, errorThrown) {
	                        alert(textStatus);
	                    }
	                });
	            },
	            minLength: 2,
	            select: seleccionaTratamientoPadre
	        });

	    });

	    function seleccionaTratamientoPadre(event, ui) {
	        document.getElementById('htratamientopadre').value = ui.item.value;
	        setTimeout("document.getElementById('txtcantidadarebajar').focus()", 100);

	    }

	    function seleccionaTratamiento(event, ui) {
	        idtratamiento = ui.item.value;
	        $('#btnbuscar').click();
	        
	    }

	    //FUNCION UTILIZADA PARA OCULTAR BOTON ACTUALIZAR,ELIMINAR,CANCELAR Y PARA MOSTRAR EL BOTON GUARDAR, LIMPIAR FORMULARIO Y FOCO A UN TEXTO
	    function inicializa() {
	        idtratamiento = '';
	        document.getElementById('btnguardar').style.display = '';
	        document.getElementById('btnactualizar').style.display = 'none';
	        document.getElementById('btneliminar').style.display = 'none';
	        document.getElementById('btncancelar').style.display = 'none';
	        document.getElementById("formulario").reset();
	    }

	    function cargaTipoTratamiento() {
	        $.ajax({
	        	type: "POST",
	        	url: "../WSTratamiento.asmx/cargaTiposTratamientos",
	        	data: "{}",
	        	contentType: "application/json; charset=utf-8",
	        	dataType: "json",
	        	success: cargatiposi,
	        	error: cargatipono
	        });
	    }

	    function cargatiposi(msg) {

	        $("#droptipotratamiento").html("");
	        $("#droptipotratamiento").append($("<option></option>").attr("value", "0").text("Seleccione tipo")); 
            $.each(msg.d, function () {
	            $("#droptipotratamiento").append($("<option></option>").attr("value", this.idtipotratamiento).text(this.descripcion));
	        });
	    }

	    function cargatipono(msg) {
	        alert('Error: ' + msg.responseText);
	    }

	    $(document).ready(function () {

	        cargaTipoTratamiento();


	        //CARGA DROP DE BUSQUEDA DE tratamientoS

	        //	        $.ajax({
	        //	            type: "POST",
	        //	            url: "../WSTratamiento.asmx/TratamientoDatos",
	        //	            data: "{}",
	        //	            contentType: "application/json; charset=utf-8",
	        //	            dataType: "json",
	        //	            success: datossi,
	        //	            error: datosno
	        //	        });

	        //	        function datossi(msg) {

	        //	            $("#dropbusqueda").html("");
	        //	            $.each(msg.d, function () {
	        //	                $("#dropbusqueda").append($("<option></option>").attr("value", this.idtratamiento).text(this.Descripcion))
	        //	            });
	        //	        }

	        //	        function datosno(msg) {
	        //	            alert('Error: ' + msg.responseText);
	        //	        }

	        //	        $.ajax({
	        //	            type: "POST",
	        //	            url: "../WSPuesto.asmx/PuestoDatos",
	        //	            data: "{}",
	        //	            contentType: "application/json; charset=utf-8",
	        //	            dataType: "json",
	        //	            success: tratamientosi,
	        //	            error: tratamientono
	        //	        });

	        //	        function tratamientosi(msg) {

	        //	            $("#droppuesto").html("");
	        //	            $.each(msg.d, function () {
	        //	                $("#droppuesto").append($("<option></option>").attr("value", this.idpuesto).text(this.nombre))
	        //	            });
	        //	        }

	        //	        function tratamientono(msg) {
	        //	            alert('Error: ' + msg.responseText);
	        //	        }

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



	        //BOTON GUARDAR tratamiento
	        $('#btnguardar').click(function () {
	            var Otros = document.getElementById('txtotros').value;
	            var codigo = document.getElementById('txtcodigo').value;
	            var droptipotratamiento = document.getElementById('droptipotratamiento');
	            var descripcion = document.getElementById('txtdescripcion').value;
	            var existencia = document.getElementById('txtexistencia');
	            var tratamientopadre = document.getElementById('htratamientopadre');
	            var existenciaarebajar = document.getElementById('txtcantidadarebajar');

	            if (!existencia.value) existencia.value = 0;

	            if (existenciaarebajar.value == "") {
	                existenciaarebajar.value = 0;
	            }


	            if (!codigo) {
	                Sexy.error("<h1>Control Medico Web</h1><p>No ha seleccionado un tratamiento en la busqueda</p>");
	            } else if (!descripcion) {
	                Sexy.error("<h1>Control Medico Web</h1><p>Ingrese una descripcion para el  tratamiento</p>");
	            } else if (!Otros) {
	                Sexy.error("<h1>Control Medico Web</h1><p>Ingrese las observaciones</p>");
	            } else if (droptipotratamiento.value == "0" || droptipotratamiento.selectedIndex == 0) {
	                Sexy.error("<h1>Control Medico Web</h1><p>No ha seleccionado un tipo de tratamiento</p>");
	            }
	            else {
	                $.ajax({
	                    type: "POST",
	                    url: "../WSTratamiento.asmx/TratamientoGuardar",
	                    data: '{idtratamiento: "' + codigo + '", Descripcion: "' + descripcion + '", Otros: "' + Otros + '",idtipotratamiento:' + droptipotratamiento.value + ',existencia:' + existencia.value + ', tratamientopadre: "' + tratamientopadre.value + '", existenciaarebajar: "' + existenciaarebajar.value + '"}',
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
	                if (mensaje.substring(0, 5) == "ERROR")
	                    Sexy.error("<h1>Control Medico Web</h1><br/><p>" + mensaje + "</p>");
	                else {
	                    Sexy.info("<h1>Control Medico Web</h1><br/><p>" + mensaje + "</p>");
	                    $("txtbusquedatratamiento").append($("<option></option>").attr("value", this.idtratamiento).text(this.idtratamiento));
	                    document.getElementById('formulario').reset();
	                }
	            });
	        }
	        function guardarno(msg) {
	            $.each(msg.d, function () {
	                Sexy.error("<h1>Control Medico Web</h1><p>" + msg.responseText + "</p>");
	            });
	        }

	        //	        //BOTON BUSCAR EMPRESA
	        $('#btnbuscar').click(function () {

	            //var idtratamiento = document.getElementById('txtbusquedatratamiento').value;
	            if (!idtratamiento) {
	                Sexy.error("<h1>Control Medico Web</h1><p>No ha seleccionado un tratamiento</p>");
	            }
	            else {
	                $.ajax({
	                    type: "POST",
	                    url: "../WSTratamiento.asmx/TratamientoBuscar",
	                    data: '{idtratamiento: "' + idtratamiento + '"}',
	                    contentType: "application/json; charset=utf-8",
	                    dataType: "json",
	                    success: buscarsi,
	                    error: buscarno
	                });
	            }
	        });

	        function buscarsi(msg) {
	            $.each(msg.d, function () {
	                document.getElementById('txtdescripcion').value = this.Descripcion;
	                document.getElementById('txtotros').value = this.Otros;
	                document.getElementById('txtexistencia').value = this.existencia;
	                document.getElementById('txtcodigo').value = this.idtratamiento;
	                document.getElementById('droptipotratamiento').value = this.idtipotratamiento;
	                document.getElementById('htratamientopadre').value = this.tratamientopadre;
	                document.getElementById('txttratamientopadre').value = this.tratamientopadre;
	                document.getElementById('txtcantidadarebajar').value = this.existenciaarebajar;

	                idtratamiento = this.idtratamiento;
	                document.getElementById('btnactualizar').style.display = '';
	                document.getElementById('btneliminar').style.display = '';
	                document.getElementById('btncancelar').style.display = '';
	                document.getElementById('btnguardar').style.display = 'none';
	            });
	        }
	        function buscarno(msg) {
	            Sexy.error("<h1>Control Medico Web</h1><br/><p>" + msg.responseText + "</p>");
	        }

	        //BOTON ACTUALIZAR LA EMPRESA SELECCIONADA
	        $('#btnactualizar').click(function () {
	            //var txttratamiento = document.getElementById('txtbusquedatratamiento').value;
	            var Descripcion = document.getElementById('txtdescripcion');
	            var Otros = document.getElementById('txtotros');
	            var codigo = document.getElementById('txtcodigo');
	            var droptipotratamiento = document.getElementById('droptipotratamiento');
	            var existencia = document.getElementById('txtexistencia');

	            var tratamientopadre = document.getElementById('htratamientopadre');
	            var existenciaarebajar = document.getElementById('txtcantidadarebajar');

	            if (!existencia.value) existencia.value = 0;

	            if (existenciaarebajar.value == "") {
	                existenciaarebajar.value = 0;
	            }

	            if (!idtratamiento) {
	                Sexy.error("<h1>Control Medico Web</h1><p>No ha seleccionado un tratamiento en la busqueda</p>");
	            } else if (!codigo) {
	                Sexy.error("<h1>Control Medico Web</h1><p>Ingrese un codigo para el  tratamiento</p>");
	            } else if (!Otros) {
	                Sexy.error("<h1>Control Medico Web</h1><p>Ingrese las observaciones</p>");
	            } else if (droptipotratamiento.value == "0" || droptipotratamiento.selectedIndex == 0) {
	                Sexy.error("<h1>Control Medico Web</h1><p>No ha seleccionado un tipo de tratamiento</p>");
	            } else {
	                $.ajax({
	                    type: "POST",
	                    url: "../WSTratamiento.asmx/TratamientoActualizar",
	                    data: '{Otros: "' + Otros.value + '", Descripcion: "' + Descripcion.value + '", idtratamiento: "' + idtratamiento + '", idtipotratamiento: ' + droptipotratamiento.value + ',existencia: ' + existencia.value + ', tratamientopadre: "' + tratamientopadre.value + '", existenciaarebajar: "' + existenciaarebajar.value + '"}',
	                    contentType: "application/json; charset=utf-8",
	                    dataType: "json",
	                    success: actualizasi,
	                    error: actualizano
	                });
	            }
	        });

	        function actualizasi(msg) {
	            $.each(msg.d, function () {
	                var mensaje = this.mensaje;
	                if (mensaje.substring(0, 5) == "ERROR")
	                    Sexy.error("<h1>Control Medico Web</h1><br/><p>" + mensaje + "</p>");
	                else {
	                    Sexy.info("<h1>Control Medico Web</h1><br/><p>" + mensaje + "</p>");
	                    //var posicion = document.getElementById('txtbusquedatratamiento').selectedIndex;
	                    //document.getElementById('txtbusquedatratamiento').options[posicion].text = document.getElementById('txtdescripcion').value;
	                    document.getElementById("formulario").reset();
	                    inicializa();
	                    document.getElementById('txtdescripcion').focus();
	                }

	            });

	        }

	        function actualizano(msg) {
	            Sexy.error("<h1>Control Medico Web</h1><p>" + msg.responseText + "</p>");
	        }

	        //	        //BOTON ELIMINAR LA EMPRESA SELECCIONADA
	        $('#btneliminar').click(function () {
	            if (!idtratamiento) {
	                Sexy.error("<h1>Control Medico Web</h1><p>No ha seleccionado un tratamiento</p>");
	            } else {
	                Sexy.confirm('<h1>Advertencia</h1><p>¿Deseas Eliminar el registro seleccionado?</p><p>Pulsa "Ok" para continuar, o pulsa "Cancelar" para salir.</p>', { onComplete:
	                        function (returnvalue) {
	                            if (returnvalue) {
	                                //var idtratamiento = document.getElementById('txtbusquedatratamiento').value;

	                                $.ajax({
	                                    type: "POST",
	                                    url: "../WSTratamiento.asmx/TratamientoEliminar",
	                                    data: '{idtratamiento: "' + idtratamiento + '"}',
	                                    contentType: "application/json; charset=utf-8",
	                                    dataType: "json",
	                                    success: eliminasi,
	                                    error: eliminano
	                                });
	                            }
	                        }
	                });
	            }
	        });

	        function eliminasi(msg) {
	            $.each(msg.d, function () {
	                var mensaje = this.mensaje;
	                if (mensaje.substring(0, 5) == "ERROR")
	                    Sexy.error("<h1>Control Medico Web</h1><br/><p>" + mensaje + "</p>");
	                else {
	                    Sexy.info("<h1>Control Medico Web</h1><br/><p> Dato eliminado correctamente </p>");
	                    //var posicion = document.getElementById('txtbusquedatratamiento').selectedIndex;
	                    //document.getElementById('txtbusquedatratamiento').remove(posicion);
	                    inicializa();
	                }
	            });
	        }
	        function eliminano(msg) {

	            Sexy.error("<h1>Control Medico Web</h1><p>" + msg.responseText + "</p>");
	        }

	        //	        //BOTON CANCELAR OPERACION ACTUALIZAR O ELIMINAR
	        $('#btncancelar').click(function () {
	            inicializa();
	        });

	    });

	    function borrahtratamientopadre() {
	        document.getElementById('htratamientopadre').value = "";

        }

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
			<header><h3>DATOS tratamiento</h3></header>
				<div class="module_content">
						
                        <fieldset style="width:48%; float:left; margin-right: 3%;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>Descripcion</label>
							<input type="text" id="txtdescripcion" style="width:92%;"/>
						</fieldset>
						
                        <fieldset style="width:48%; float:left;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>CODIGO</label>
							<input type="text" id="txtcodigo" style="width:92%;"/>
						</fieldset>

                        <fieldset style="width:98.5%; float:left;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>Otros</label>
						<textarea cols="1"  id="txtotros" rows="15" style="width:96%;"></textarea>
						</fieldset>
                        
                        <fieldset style="width:48%; float:left; margin-right: 3%;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>Tipo</label>
							<select id="droptipotratamiento" style="width:92%;">
                            
                            </select>
						</fieldset>

                        <fieldset style="width:48%; float:left;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>Existencia</label>
							<input type="text" id="txtexistencia" style="width:92%;"/>

						</fieldset>
                        
                        <fieldset style="width:48%; float:left; margin-right: 3%;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>Tratamiento Padre</label>
							<input type="text" id="txttratamientopadre" onkeydown="borrahtratamientopadre()"; style="width:92%;"/>
                            <input type="hidden" id="htratamientopadre" />
						</fieldset>

                        <fieldset style="width:48%; float:left;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>Cantidad a rebajar</label>
							<input type="text" id="txtcantidadarebajar" style="width:92%;"/>

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
					Busqueda <input type="text" id="txtbusquedatratamiento" class="texto" />
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
