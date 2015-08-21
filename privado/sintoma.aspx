<%@ Page Language="VB" AutoEventWireup="false" CodeFile="sintoma.aspx.vb" Inherits="privado_sintoma" %>

<!doctype html>
<html lang="en">

<head>
	<meta charset="utf-8"/>
	<title> BIO * NATURAL Online</title>
	
	<link rel="stylesheet" href="../css/layout.css" type="text/css" media="screen" />
	<link rel="stylesheet" href="../css/sexyalertbox.css" type="text/css" media="screen" />
	    <link rel="stylesheet" type="text/css" href="../css/jquery-ui-1.8.24.custom.css" />

    <!--[if lt IE 9]>
	<link rel="stylesheet" href="css/ie.css" type="text/css" media="screen" />
	<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
	<![endif]-->
	    <script src="../js/jquery-1.8.2.min.js" type="text/javascript"></script>    
        <script type="text/javascript" src="../js/jquery-ui-1.8.24.custom.min.js"></script>
	<script src="../js/hideshow.js" type="text/javascript"></script>
	<script src="../js/jquery.tablesorter.min.js" type="text/javascript"></script>
	<script type="text/javascript" src="../js/jquery.equalHeight.js"></script>
    <script type="text/javascript" src="../js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="../js/sexyalertbox.v1.2.jquery.js"></script>
	<script type="text/javascript">
	    var idsintoma = '';



	    $(function () {
	        $("#txtbusqueda").autocomplete({
	            source: function (request, response) {
	                $.ajax({
	                    url: "../WSSintoma.asmx/ConsultaSintoma",
	                    data: "{ 'busqueda': '" + request.term + "' }",
	                    dataType: "json",
	                    type: "POST",
	                    contentType: "application/json; charset=utf-8",
	                    dataFilter: function (data) { return data; },
	                    success: function (data) {
	                        response($.map(data.d, function (item) {
	                            return {
	                                value: item.descripcion,
                                    idsintoma: item.idsintoma
	                            }
	                        }))
	                    },
	                    error: function (XMLHttpRequest, textStatus, errorThrown) {
	                        alert(textStatus);
	                    }
	                });
	            },
	            minLength: 2,
	            select: seleccionaSintoma
	        });
	    });


	    function seleccionaSintoma(event, ui) {
	        idsintoma = ui.item.idsintoma;
	        console.log(idsintoma);
	        
	    }

//    function cargaSubPartesCuerpo (idpartedelcuerpo){
//        $.ajax({
//            type: "POST",
//            url: "../WSPartedelcuerpo.asmx/muestraSubPartes",
//            data: "{idpartedelcuerpo:" + idpartedelcuerpo + "}",
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            async: false,
//            success: function (msg) {
//                $("#dropsubpartedelcuerpo").html("");
//                $("#dropsubpartedelcuerpo").append($("<option></option>").attr("value", 0).text("Seleccione una subparte"));
//                $.each(msg.d, function () {
//                    $("#dropsubpartedelcuerpo").append($("<option></option>").attr("value", this.idpartedelcuerpo).text(this.nombre));
//                });
//            },
//            error: function (msg) { alert('Error: ' + msg.responseText); }
//        });
//            }

            
                //                $("#droppartedelcuerpo").change(function () {
                //                    cargaSubPartesCuerpo($(this).val());
                //                });

                //FUNCION UTILIZADA PARA OCULTAR BOTON ACTUALIZAR,ELIMINAR,CANCELAR Y PARA MOSTRAR EL BOTON GUARDAR, LIMPIAR FORMULARIO Y FOCO A UN TEXTO
                function inicializa() {
                    document.getElementById('btnguardar').style.display = '';
                    document.getElementById('btnactualizar').style.display = 'none';
                    document.getElementById('btneliminar').style.display = 'none';
                    document.getElementById('btncancelar').style.display = 'none';
                    document.getElementById("formulario").reset();
                    document.getElementById('txtdescripcion').focus();
                    idsintoma = '';
                }

                //FUNCION PARA MOSTRAR SINTOMAS EN EL DROP DOWN
                $(document).ready(function () {
                    //                    $.ajax({
                    //                        type: "POST",
                    //                        url: "../WSSintoma.asmx/SintomaDatos",
                    //                        data: "{}",
                    //                        contentType: "application/json; charset=utf-8",
                    //                        dataType: "json",
                    //                        success: datossi,
                    //                        error: datosno
                    //                    });

                    cargaSubPartesCuerpo();
                    inicializa();
                    //                });

                    //                function datossi(msg) {
                    //                    $("#dropbusqueda").html("");
                    //                    $.each(msg.d, function () {
                    //                        $("#dropbusqueda").append($("<option></option>").attr("value", this.idsintoma).text(this.descripcion))
                    //                    });
                    //                }

                    //                function datosno(msg) {
                    //                    Sexy.error("<h1>Control Medico Web</h1><p>" + msg.responseText + "</p>");
                    //                }


                    /*$.ajax({
                    type: "POST",
                    url: "../WSPartedelcuerpo.asmx/PartedelcuerpoDatos",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: partedelcuerposi,
                    error: partedelcuerpono
                    });

                    function partedelcuerposi(msg) {

                    //	            $("#droppartedelcuerpo").html("");
                    //	            $("#droppartedelcuerpo").append($("<option></option>").attr("value", this.idpartedelcuerpo).text(this.nombre))
                    $.each(msg.d, function () {
                    $("#droppartedelcuerpo").append($("<option></option>").attr("value", this.idpartedelcuerpo).text(this.nombre))
                    });
                    }

                    function partedelcuerpono(msg) {
                    alert('Error: ' + msg.responseText);
                    }

                    */


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


                    //BOTON BUSCAR SINTOMA
                    $('#btnbuscar').click(function () {
                        //var idsintoma = document.getElementById('dropbusqueda').value;
                        //alert("hola");
                        if (!idsintoma)
                            Sexy.error("<h1>Control Medico Web</h1><br/><p> No ha realizado la busqueda </p>");
                        else {
                            $.ajax({
                                type: "POST",
                                url: "../WSSintoma.asmx/SintomaBuscar",
                                data: '{idsintoma: "' + idsintoma + '"}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: buscarsi,
                                error: buscarno
                            });
                        }
                    });

                    function buscarsi(msg) {
                        $.each(msg.d, function () {
                            document.getElementById('txtdescripcion').value = this.descripcion;
                            document.getElementById('droppartedelcuerpo').value = this.idpartedelcuerpo;
                            console.log(this.idpartedelcuerpo);
                            //cargaSubPartesCuerpo(this.idpartedelcuerpo);
                            //document.getElementById('dropsubpartedelcuerpo').value = this.idsubpartedelcuerpo;
                            document.getElementById('txtobservaciones').value = this.observaciones;
                            document.getElementById('txtprioridad').value = this.prioridad;
                            document.getElementById('btnactualizar').style.display = '';
                            document.getElementById('btneliminar').style.display = '';
                            document.getElementById('btncancelar').style.display = '';
                            document.getElementById('btnguardar').style.display = 'none';
                        });
                    }
                    function buscarno(msg) {
                        Sexy.error("<h1>Control Medico Web</h1><p>" + msg.responseText + "</p>");
                    }

                    //BOTON BUSCAR GUARDAR SINTOMA
                    $('#btnguardar').click(function () {
                        var descripcion = document.getElementById('txtdescripcion').value;
                        var idpartedelcuerpo = document.getElementById('droppartedelcuerpo');
                        var idsubpartedelcuerpo = document.getElementById('dropsubpartedelcuerpo');
                        var prioridad = document.getElementById('txtprioridad').value;
                        var femenino = document.getElementById('dropfemenino').value;
                        var masculino = document.getElementById('dropmasculino').value;
                        var observaciones = document.getElementById('txtobservaciones').value;
                        if (!descripcion)
                            Sexy.error("<h1>Control Medico Web</h1><br/><p> Ingrese la descripcion del síntoma </p>");
                        else if (!observaciones)
                            Sexy.error("<h1>Control Medico Web</h1><br/><p> Ingrese las observaciones del síntoma </p>");
                        else if (idpartedelcuerpo.value == 0 || idpartedelcuerpo.selectedIndex < 1)
                            Sexy.error("<h1>Control Medico Web</h1><br/><p> Seleccione una parte del cuerpo </p>");
                        //                    else if (idsubpartedelcuerpo.value == 0 || idsubpartedelcuerpo.selectedIndex < 1)
                        //                        Sexy.error("<h1>Control Medico Web</h1><br/><p> Seleccione una subparte del cuerpo </p>");
                        else {
                            $.ajax({
                                type: "POST",
                                url: "../WSSintoma.asmx/SintomaGuardar",
                                data: '{descripcion: "' + descripcion + '", idpartedelcuerpo: "' + idpartedelcuerpo.value + '",idsubpartedelcuerpo: "' + idsubpartedelcuerpo.value + '", prioridad: "' + prioridad + '", femenino: ' + femenino + ',masculino: ' + masculino + ',observaciones: "' + observaciones + '"}',
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
                                $("#dropbusqueda").append($("<option></option>").attr("value", this.idsintoma).text(this.descripcion));
                                //document.getElementById('formulario').reset();
                                document.getElementById('txtdescripcion').value = "";
                                document.getElementById('txtobservaciones').value="";
                                document.getElementById('txtdescripcion').focus();
                            }
                        });
                    }

                    function guardarno(msg) {
                        // alert("hola");
                        Sexy.error("<h1>Control Medico Web</h1><p>" + msg.responseText + "</p>");
                    }




                    //BOTON ELIMINAR LA SINTOMA SELECCIONADA
                    $('#btneliminar').click(function () {
                        if (!idsintoma)
                            Sexy.error("<h1>Control Medico Web</h1><br/><p> No ha seleccionado un sintoma </p>");
                        else {
                            Sexy.confirm('<h1>Advertencia</h1><p>¿Deseas Eliminar el registro seleccionado?</p><p>Pulsa "Ok" para continuar, o pulsa "Cancelar" para salir.</p>', { onComplete:
                         function (returnvalue) {
                             if (returnvalue) {
                                 //var idsintoma = document.getElementById('dropbusqueda').value;
                                 $.ajax({
                                     type: "POST",
                                     url: "../WSSintoma.asmx/SintomaEliminar",
                                     data: '{idsintoma: "' + idsintoma + '"}',
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
                            //alert("hola");
                            if (mensaje.substring(0, 5) == "ERROR")
                                Sexy.error("<h1>Control Medico Web</h1><br/><p>" + mensaje + "</p>");
                            else {
                                Sexy.info("<h1>Control Medico Web</h1><br/><p> Dato eliminado correctamente </p>");
                                //alert("hola else");
                                //var posicion = document.getElementById('dropbusqueda').selectedIndex;
                                //document.getElementById('dropbusqueda').remove(posicion);
                                inicializa();
                            }
                        });
                    }
                    function eliminano(msg) {
                        //alert("hola eliminano");
                        Sexy.error("<h1>Control Medico Web</h1><p>" + msg.responseText + "</p>");
                    }



                    //BOTON ACTUALIZAR LA EMPRESA SELECCIONADA
                    $('#btnactualizar').click(function () {
                        //var idsintoma = document.getElementById('dropbusqueda').value;
                        var descripcion = document.getElementById('txtdescripcion').value;
                        var idpartedelcuerpo = document.getElementById('droppartedelcuerpo');
                        var prioridad = document.getElementById('txtprioridad').value;
                        var observaciones = document.getElementById('txtobservaciones').value;

                        if (!idsintoma)
                            Sexy.error("<h1>Control Medico Web</h1><br/><p> No ha seleccionado o buscado un síntoma </p>");
                        else if (!descripcion)
                            Sexy.error("<h1>Control Medico Web</h1><br/><p> Ingrese la descripcion del síntoma </p>");
                        else if (!observaciones)
                            Sexy.error("<h1>Control Medico Web</h1><br/><p> Ingrese las observaciones del síntoma </p>");
                        else if (idpartedelcuerpo.value == 0 || idpartedelcuerpo.selectedIndex < 1)
                            Sexy.error("<h1>Control Medico Web</h1><br/><p> Seleccione una parte del cuerpo </p>");
                        else {
                            $.ajax({
                                type: "POST",
                                url: "../WSSintoma.asmx/SintomaActualizar",
                                data: '{descripcion: "' + descripcion + '", idpartedelcuerpo: "' + idpartedelcuerpo.value + '",  prioridad: "' + prioridad + '", idsintoma: "' + idsintoma + '", observaciones: "' + observaciones + '"}',
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
                                //var posicion = document.getElementById('dropbusqueda').selectedIndex;
                                //document.getElementById('dropbusqueda').options[posicion].text = document.getElementById('txtdescripcion').value;
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


            function cargaSubPartesCuerpo() {
                $.ajax({
                    type: "POST",
                    url: "../WSPartedelcuerpo.asmx/SubPartedelcuerpoDatos",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $("#droppartedelcuerpo").html("");
                        $("#droppartedelcuerpo").append($("<option></option>").attr("value", 0).text("Seleccione una parte"));
                        $.each(msg.d, function () {
                            $("#droppartedelcuerpo").append($("<option></option>").attr("value", this.idpartedelcuerpo).text(this.nombre));

                        });
                    },
                    error: function (msg) { alert('Error: ' + msg.responseText); }
                });
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
			<li class="icn_folder"><a href="#" style="color:#04B404;text-shadow: 1px 1px white; font-weight:bolder;">Sintomas</a></li>
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
		
				
				
		<article class="module width_full">
			<header><h3>SINTOMA</h3></header>
				<div class="module_content">

						<fieldset style="width:48%; float:left; margin-right: 3%;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>NOMBRE</label>
							<input type="text" id="txtdescripcion" style="width:92%;"/>
						</fieldset>

                        <fieldset style="width:48%; float:left;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>OBSERVACIONES</label>
							<textarea id="txtobservaciones" rows="5" cols="1" style="width:92%;"></textarea>
                        </fieldset>

                        <fieldset style="width:48%; float:left;margin-right: 3%;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>PARTE DEL CUERPO</label>
						<select id="droppartedelcuerpo" >
						<option>seleccione parte del cuerpo</option>
					    </select>
						</fieldset>
                        
                        <fieldset style="width:48%; float:left;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>PRIORIDAD</label>
							<input type="text" id="txtprioridad" style="width:92%;"/>
						</fieldset>
                        

                        <fieldset style="width:48%; display:none; float:left;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>SUBPARTE DEL CUERPO</label>
						<select id="dropsubpartedelcuerpo" >
						    <option value="0">seleccione subparte del cuerpo</option>
					    </select>
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
                    <select id="dropbusqueda" style="display:none;" >
						<option>Seleccione</option>
						
					</select>
                    <input type="text" id="txtbusqueda" class="texto" />
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
