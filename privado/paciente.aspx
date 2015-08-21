<%@ Page Language="VB" AutoEventWireup="false" CodeFile="paciente.aspx.vb" Inherits="privado_paciente" %>

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

        <style type="text/css">
    
    table {
    *border-collapse: collapse; /* IE7 and lower */
    border-spacing: 0;
    width: 100%;    
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

.textoind
{
    height:20px;
    width:80%;
    -webkit-border-radius: 5px;
-moz-border-radius: 5px;
border-radius: 5px;
border: 1px solid #BBBBBB;
height: 20px;
color: #666666;
float:left;
-webkit-box-shadow: inset 0 2px 2px #ccc, 0 1px 0 #fff;
-moz-box-shadow: inset 0 2px 2px #ccc, 0 1px 0 #fff;
box-shadow: inset 0 2px 2px #ccc, 0 1px 0 #fff;
padding-left: 5px;
background-position: 10px 6px;
margin: 0 10px;
line-height: 20px;
display:block;
    }
    
    .textoind:focus
{
     background:#F2F5A9;
    
    }
</style>

    <script type="text/javascript">
        var codigopaciente = "";
        var filassintomas = 0;
        var filasindicaciones = 0;
        var filasmedicamentos = 0;

        function cargarNacionalidades() {
            $.ajax({
                type: "POST",
                url: "../WSPuesto.asmx/NacionalidadDatos",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: datossinacionalidad,
                error: datosno
            });
        }

        function datossinacionalidad(msg) {
            $("#txtnacionalidad").html("");
            $.each(msg.d, function () {
                $("#txtnacionalidad").append($("<option></option>").attr("value", this.idpuesto).text(this.nombre))
            });
            document.getElementById('txtnacionalidad').value = 2;
        }

        function datosno(msg) {
            alert('Error: ' + msg.responseText);
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

        webcam.set_api_url('subirfoto.aspx');
        webcam.set_swf_url('webcam.swf');
        webcam.set_quality(50); // JPEG quality (1 - 100)
        webcam.set_shutter_sound(true); // play shutter click sound
        webcam.shutter_url = 'shutter.mp3';
        webcam.set_hook("onLoad", null);
        webcam.set_hook("onComplete", null);
        webcam.set_hook("onError", null);
        //document.write(webcam.get_html(240, 320));

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

        function camGrabar() {
            webcam.reset();
            webcam.freeze();
            document.getElementById('btngrabarfoto').style.display = 'none';
            document.getElementById('btncancelarfoto').style.display = '';
            document.getElementById('btnenviarfoto').style.display = '';
        }
        function camCancelar() {
            webcam.reset();
            document.getElementById('btngrabarfoto').style.display = '';
            document.getElementById('btncancelarfoto').style.display = 'none';
            document.getElementById('btnenviarfoto').style.display = 'none';
        }
        function camEnviar() {
            webcam.upload("subirfoto.aspx");
            webcam.reset();
            document.getElementById('btngrabarfoto').style.display = '';
            document.getElementById('btncancelarfoto').style.display = 'none';
            document.getElementById('btnenviarfoto').style.display = 'none';
            setTimeout("guardarutaimagen();", 1000);
            //guardarutaimagen();
            //alert('Si hubieramos configurado un script (PHP, lo que sea) podríamos subir la imagen en JPEG a nuestro servidor.');
        }

        function guardarutaimagen() {

            var codigopaciente = document.getElementById('hidpaciente').value;
            if (!codigopaciente)
                mensajeadvertencia("No se ha buscado aun algun paciente");
                //Sexy.error("<h1>Control Medico Web</h1><br/><p> No se ha buscado algun paciente </p>");
            else {
                $.ajax({
                    type: "POST",
                    url: "paciente.aspx/actualizarfoto",
                    data: '{idpaciente: ' + codigopaciente + '}',
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    dataType: "json",
                    success: actualizaimagen,
                    error: actualizaimagenno
                });
            }
        }

        function actualizaimagen(msg) {
            webcam.freeze();
            setTimeout("close_modal();", 1);
            //codigopaciente = ""; MOYO
            //document.getElementById("hidpaciente").value="";

            //if (confirm("¿Desea mostrar la ficha del paciente?")) {
                // Respuesta afirmativa...
                mostrarFicha();
            //}

        }

        function actualizaimagenno(msg) { Sexy.error("<h1>Control Medico Web</h1><br/><p> "+ msg.responseText + " </p>"); }

        function despacharpacientes() {
   
            $.ajax({
                type: "POST",
                url: "../WSConsultamedica.asmx/DespacharRecetas",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: despacharsi,
                error: despacharno
            });
        }

        function despacharsi(msg) {
            $("#divpacientes").html("");
            $("#detallereceta").html("");

            $.each(msg.d, function () {
                console.log(this.nombrepaciente);

                var div = document.createElement('div');
                div.id = this.idpaciente + '|' + this.idconsulta;
                div.innerHTML = this.nombrepaciente;
                div.className = "styleface"
                $("#divpacientes").append(div);
                //mensajecorrecto("Paciente despachado correctamente");
            });
        }

        function despacharno(msg) { console.log(msg.responseText); }

        
        $(function () {

            $("#txtfechanacimiento, #txtproximaconsultadespachocompleto, #txtproximaconsultadespacho").datepicker({
                numberOfMonths: 1,
                dateFormat: 'dd/mm/yy',
                showAnim: 'slide',
                firstDay: 0
            });
        });


        function calculaanionacimiento(edad) {
            var fechaactual = document.getElementById('hfechaactual');

            var arreglofecha = fechaactual.value.split("/");

            var anio = arreglofecha[2];
            var mes = arreglofecha[1];
            var dia = arreglofecha[0];

            var anionuevo = parseInt(anio) - parseInt(edad);

            document.getElementById('txtfechanacimiento').value = arreglofecha[0] + '/' + arreglofecha[1] + '/' + anionuevo;

        }

        function proximaConsulta() {

            $.ajax({
                type: "POST",
                url: "../WSPaciente.asmx/proximaConsulta",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    document.getElementById('txtproximaconsultadespachocompleto').value = msg.d;
                },
                error: fechano
            });
        }

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

            document.getElementById('txtfechanacimiento').value = msg.d;
            document.getElementById('hfechaactual').value = msg.d;

        }

        function fechano(msg) {
            mensajeerror(msg.responseText);
        }

        $(document).ready(function () {

            fechaactual();
            cargarNacionalidades();
            proximaConsulta();
            $("#btnagregarobservacion").click(function () {
                //moyo
                control = document.getElementById('hobservaciones').value;
                var correlativo = control.substring(7, control.length);
                var objeto = control.substring(0, 7);
                var destino = '';
                if (objeto == "sintobs")
                    destino = 'obssintoma' + correlativo
                else if (objeto == "indiobs")
                    destino = 'obsindicacion' + correlativo
                else if (objeto == "mediobs")
                    destino = 'obsmedicamento' + correlativo

                var observacion = document.getElementById('txtobservacion');

                document.getElementById(destino).innerHTML = observacion.value;
                observacion.value = "";

                cerrarmodalSencillo("divagregarobservaciones");

            });

            $("#btnguardarreceta").click(function () {
                var n = 1;
                var arregloMedicina = new Array();
                var numeromedicamentos = 0;
                var numerodeerrores = 0;
                $('#detallereceta tr').each(function (i) {

                    var cantidad = document.getElementById('txtcantidad' + n);
                    var existencia = document.getElementById('tdexistencia' + n);
                    /*ESTA FUNCION ES PARA DETERMINAR SI SE ESTA VENDIENDO MAS DE LO QUE SE POSEE EN EXISTENCIA*/
                    if (existencia.innerHTML > 0) {
                        if (cantidad.value > existencia.innerHTML) {
                            numerodeerrores++;
                        }
                    }
                    var idmedicina = document.getElementById('tdidmedicina' + n);
                    if (!idmedicina.innerHTML) {
                        console.log("sin datos");
                    }
                    else {
                        numeromedicamentos++;
                        arregloMedicina.push({ idmedicina: idmedicina.innerHTML, cantidad: cantidad.value });
                        n++;
                    }
                });

                console.log(arregloMedicina);

                if (numerodeerrores > 0) {
                    mensajeadvertencia("Existe por lo menos una cantidad de medicina para despachar invalida");
                    return;
                }

                var datos = document.getElementById('idpacienteactivo').value;

                var arreglo = datos.split("|");
                var idpaciente = arreglo[0];
                var idreceta = arreglo[1];

                if (arregloMedicina.length < 1) { mensajeadvertencia("No ha seleccionado tratamientos, medicina o sintomas"); return; }

                var proximaconsulta = formatofecha(document.getElementById('txtproximaconsultadespacho').value);

                $.ajax({
                    type: "POST",
                    url: "../WSConsultamedica.asmx/ConfirmarReceta",
                    data: "{'arregloMedicina':" + JSON.stringify(arregloMedicina) + ",'idpaciente':" + idpaciente + ",'idreceta':" + idreceta + ",'numeromedicamentos':" + numeromedicamentos + ",'Varproximaconsulta':'" + proximaconsulta + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: confirmarecetasi,
                    error: confirmarecetano
                });
            });

            function confirmarecetasi(msg) {
                var mensaje = msg.d;
                if (mensaje.substring(0, 5) == "ERROR")
                    Sexy.error("<h1>Control Medico Web</h1><br/><p>" + mensaje + "</p>");
                else {
                    $("#detallereceta").html("");
                    var idpacienteactivo = document.getElementById('idpacienteactivo');
                    document.getElementById(idpacienteactivo.value).style.display = 'none';
                    idpacienteactivo.value = "";
                    var pacienteactivo = document.getElementById('pacienteactivo');
                    pacienteactivo.innerHTML = "";
                    abremodal(mensaje);
                }

            }

            $("#btnguardarrecetacompleta").click(function () {
                var pacienteactivo = document.getElementById('hidpacienteactivodespachocompleto');
                var usuario = 1;

                var totalmedicinas = 0;
                var totalsintomas = 0;
                var totaltratamientos = 0;

                if (!pacienteactivo.value) {
                    mensajeadvertencia("No ha seleccionado un paciente");
                }
                else {

                    var idSintoma = '';
                    var idIndicacion = '';
                    var idMedicamento = '';
                    var observaciones = '';

                    /*MEDICINA*/
                    var arregloMedicina = new Array();
                    $('#detallemedicamentosagregados tr').each(function (index) {
                        $(this).children("td").each(function (index2) {
                            switch (index2) {
                                case 0:
                                    observaciones = $(this).text();
                                    break;
                                case 2:
                                    idMedicamento = $(this).text();
                                    break;
                            }
                        });
                        arregloMedicina.push({ observaciones: observaciones, idmedicina: idMedicamento });
                        totalmedicinas++;
                    });
                    console.log("total medicinas " + totalmedicinas);
                    /*TRATAMIENTO*/
                    var arregloTratamiento = new Array();

                    $('#detalleindicacionesagregados tr').each(function (index) {
                        $(this).children("td").each(function (index2) {
                            switch (index2) {
                                case 0:
                                    observaciones = $(this).text();
                                    break;
                                case 2:
                                    idIndicacion = $(this).text();
                                    break;

                            }
                        });
                        arregloTratamiento.push({ observaciones: observaciones, idtratamiento: idIndicacion });
                        totaltratamientos++;
                    });


                    /*SINTOMAS*/
                    var arregloSintoma = new Array();

                    $('#detallesintomasagregados tr').each(function (index) {
                        $(this).children("td").each(function (index2) {
                            switch (index2) {
                                case 0:
                                    observaciones = $(this).text();
                                    break;
                                case 2:
                                    idSintoma = $(this).text();
                                    break;

                            }
                        });
                        arregloSintoma.push({ observaciones: observaciones, idsintoma: idSintoma });
                        totalsintomas++;

                    });

                    var totaldetodo = parseInt(totalmedicinas) + parseInt(totalsintomas) + parseInt(totaltratamientos);

                    console.log(arregloSintoma);
                    console.log(arregloTratamiento);
                    console.log(arregloMedicina);

                    if (totaldetodo < 1) {
                        mensajeadvertencia("No ha ingresado datos a la consulta");
                        return;
                    }

                    var proximaconsultatexto = document.getElementById('txtproximaconsultadespachocompleto');
                    if (!proximaconsultatexto.value) { proximaconsultatexto.focus(); mensajeadvertencia("Ingrese una fecha"); return; }
                    var fecha = proximaconsultatexto.value;
                    var proximaconsulta = formatofecha(fecha);


                    $.ajax({
                        type: "POST",
                        url: "../WSConsultamedica.asmx/RecetaGuardarCompleta",
                        data: "{'arregloSintoma':" + JSON.stringify(arregloSintoma) + ",'arregloMedicina':" + JSON.stringify(arregloMedicina) + ",'arregloTratamiento':" + JSON.stringify(arregloTratamiento) + ",'usuario':" + usuario + ",'idpaciente':" + pacienteactivo.value + ",'Varproximaconsulta':" + proximaconsulta + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: guardarecetasi,
                        error: guardarecetano
                    });
                }
            });

            function guardarecetasi(msg) {
                var pacienteactivo = document.getElementById('hidpacienteactivodespachocompleto');
                var mensaje = msg.d;
                if (mensaje.substring(0, 5) == "ERROR") {
                    mensajeadvertencia(mensaje);
                }
                else {

                    agradecimiento();

                    mensajecorrecto("Datos creados correctamente");

                    abremodal(mensaje);

                    $("#detalleindicacionesagregados").html("");
                    $("#detallemedicamentosagregados").html("");
                    $("#detallesintomasagregados").html("");
                    pacienteactivo.value = "";
                    document.getElementById('codigopaciente').innerHTML = "";
                    document.getElementById('nombre').innerHTML = "";
                    creapacientesenespera();
                }
            }

            function guardarecetano(msg) {
                mensajeerror(msg.responseText);
            }

            function confirmarecetano(msg) { }
            /*EJECUCION DEL EVENTO CLICK DE LOS PACIENTES QUE ESTAN EN ESPERA PERO QUE EL DOCTOR NO LO REGISTRO EN SU TABLET*/
            $(".stylefacecompleto").live("click", function () {
                var codigopaciente = $(this).attr('id');
                var nombrepaciente = $(this).attr('value');
                document.getElementById('hidpacienteactivodespachocompleto').value = codigopaciente;
                document.getElementById('codigopaciente').innerHTML = codigopaciente;
                document.getElementById('nombre').innerHTML = nombrepaciente;
                document.getElementById('txtnombrefactura').value = nombrepaciente;
                document.getElementById('txtbusquedasintoma').focus();
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

            function muestrarecetasi(msg) {
                $("#detallereceta").html("");
                var cantidad = 0;
                var n = 1;
                var tds = "";
                //style="display:none;"
                var fecha = document.getElementById('txtproximaconsultadespacho');
                $.each(msg.d, function () {
                    tds = tds + '<tr>';
                    tds = tds + '<td id="tdidtratamiento' + n + '" class="sorting_1">' + this.idtratamiento + '</td>';

                    var textoactivo = "";
                    if (this.idmedicina == "") { textoactivo = "disabled='disabled'"; cantidad = 0; }
                    else { cantidad = 1; textoactivo = ""; }
                    fecha.value = this.fecha;
                    tds = tds + '<td id="tdidmedicina' + n + '" class="sorting_1">' + this.idmedicina + '</td>';
                    //tds = tds + '<td id="tdmedicina' + n + '" class="sorting_1">' + this.medicina + '</td>';
                    tds = tds + '<td id="tdexistencia' + n + '" class="sorting_1">' + this.existencia + '</td>';
                    tds = tds + '<td> <input type="text" class="text" style="width:50px;" id="txtcantidad' + n + '" ' + textoactivo + ' value="' + cantidad + '"/> </td>';
                    tds = tds + '</tr>';
                    n++;

                });
                n--;
                document.getElementById('hcontador').value = n;

                $("#detallereceta").append(tds);
                document.getElementById('txtcantidad1').focus();
            }

            function muestrarecetano(msg) {
                Sexy.error("<h1>Control Medico Web</h1><br/><p>" + msg.responseText + "</p>");
            }


            /*CARGAR DROP DE LOS MEDICOS */

            $.ajax({
                type: "POST",
                url: "../WSPaciente.asmx/MedicoDatos",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: medicosi,
                error: medicono
            });

            function medicosi(msg) {

                $("#dropmedico").html("");
                $.each(msg.d, function () {
                    $("#dropmedico").append($("<option></option>").attr("value", this.iddoctor).text(this.nombre))
                });
            }

            function medicono(msg) {
                mensajeerror('Error: ' + msg.responseText);
            }

            //FUNCION UTILIZADA PARA OCULTAR BOTON ACTUALIZAR,ELIMINAR,CANCELAR Y PARA MOSTRAR EL BOTON GUARDAR, LIMPIAR FORMULARIO Y FOCO A UN TEXTO
            function inicializa() {
                document.getElementById('btnguardar').style.display = '';
                document.getElementById('btnactualizar').style.display = 'none';
                document.getElementById('btneliminar').style.display = 'none';
                document.getElementById('btncancelar').style.display = 'none';
                document.getElementById("formulario").reset();
                document.getElementById('txtnombre').focus();
            }

            function formatofecha(fecha) {
                var arreglofecha = fecha.split("/");
                var anio = arreglofecha[2];
                var mes = arreglofecha[1];
                var dia = arreglofecha[0];
                return (anio + mes + dia);
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

            //BOTON BUSCAR EMPRESA
            $('#btnbuscar').click(function () {

                if (codigopaciente == "")
                    mensajeadvertencia("No se ha buscado algun paciente");
                else {

                    $.ajax({
                        type: "POST",
                        url: "../WSPaciente.asmx/PacienteBuscar",
                        data: '{idpaciente: "' + codigopaciente + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: buscarsi,
                        error: buscarno
                    });
                }
            });

            function buscarsi(msg) {
                $.each(msg.d, function () {
                    document.getElementById('txtnombre').value = this.nombre;
                    document.getElementById('txtapellido').value = this.apellido;
                    document.getElementById('txtfechanacimiento').value = this.fechanacimiento;
                    document.getElementById('txtnacionalidad').value = this.nacionalidad;
                    document.getElementById('txtocupacion').value = this.ocupacion;
                    document.getElementById('txtdireccion').value = this.direccion;
                    document.getElementById('txttelefono').value = this.telefono;
                    document.getElementById('txtocupacion').value = this.ocupacion;
                    document.getElementById('txtcorreo').value = this.correo;
                    document.getElementById('txtnhijos').value = this.nhijos;
                    document.getElementById('txtoperaciones').value = this.operaciones;
                    document.getElementById('txtalergias').value = this.alergias;
                    document.getElementById('txtdatosextras').value = this.datosextras;
                    document.getElementById('dropgenero').value = this.genero;
                    document.getElementById('droprecomendado').value = this.recomendado;
                    document.getElementById('txtnacionalidad').value = this.nacionalidad;
                    document.getElementById('dropestado').value = this.estado;
                    document.getElementById('btnactualizar').style.display = '';
                    document.getElementById('btneliminar').style.display = '';
                    document.getElementById('btncancelar').style.display = '';
                    document.getElementById('btnguardar').style.display = 'none';
                });
                document.getElementById('txtnombre').focus();
            }
            function buscarno(msg) {
                mensajeerror(msg.responseText);
            }

            //BOTON GUARDAR EMPLEADO
            $('#btnguardar').click(function () {

                var apellido = document.getElementById('txtapellido');
                var nombre = document.getElementById('txtnombre');
                var estado = document.getElementById('dropestado').options[document.getElementById('dropestado').selectedIndex].text;
                var direccion = document.getElementById('txtdireccion');
                var telefono = document.getElementById('txttelefono');
                var edad = document.getElementById('txtedad');
                var fechanacimiento = document.getElementById('txtfechanacimiento');
                varfechanacimiento = formatofecha(fechanacimiento.value);
                var ocupacion = document.getElementById('txtocupacion');
                var nacionalidad = document.getElementById('txtnacionalidad');
                var alergias = document.getElementById('txtalergias');
                //nhijos.value = $.trim(nhijos.value);
                var datosextras = document.getElementById('txtdatosextras');
                var correo = document.getElementById('txtcorreo');
                if (!correo.value) correo.value = 0;
                var nhijos = document.getElementById('txtnhijos');
                if (!nhijos.value) nhijos.value = 0;
                var operaciones = document.getElementById('txtoperaciones');
                if (!operaciones.value) operaciones.value = 0;
                var recomendado = document.getElementById('droprecomendado').options[document.getElementById('droprecomendado').selectedIndex].text;
                var genero = document.getElementById('dropgenero').options[document.getElementById('dropgenero').selectedIndex].text;
                var foto = ""; //document.getElementById('txtfoto').value

                var idempresa = 1;

                if (!nombre) {
                    mensajeadvertencia("Ingrese los nombres del paciente");
                    nombre.focus();
                } else if (!apellido) {
                    mensajeadvertencia("Ingrese los apellidos del paciente");
                    apellido.focus();
                } else if (!edad.value) {
                    mensajeadvertencia("Ingrese los apellidos del paciente");
                    edad.focus();
                } else if (!apellido) {
                    mensajeadvertencia("Ingrese los apellidos del paciente");
                    fechanacimiento.focus();
                } else {
                    $.ajax({
                        type: "POST",
                        url: "../WSPaciente.asmx/PacienteGuardar",
                        data: '{nombre: "' + nombre.value + '",apellido:"' + apellido.value + '",estado:"' + estado + '",direccion:"' + direccion.value + '",telefono:"' + telefono.value + '",edad:"' + edad.value + '",fechanacimiento:"' + varfechanacimiento + '",ocupacion:"' + ocupacion.value + '",nacionalidad:"' + nacionalidad.value + '",datosextras:"' + datosextras.value + '",correo:"' + correo.value + '",nhijos:"' + nhijos.value + '",operaciones:"' + operaciones.value + '",recomendado:"' + recomendado + '",genero:"' + genero + '",foto:"' + foto + '",idempresa:"' + idempresa + '",alergias:"' + alergias.value + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) { /*GUARDAR PACIENTE*/
                            $.each(msg.d, function () {
                                var mensaje = this.mensaje;
                                var idpaciente = this.idpaciente;
                                codigopaciente = this.idpaciente;
                                console.log(this.idpaciente);

                                if (mensaje.substring(0, 5) == "ERROR")
                                    mensajeadvertencia(mensaje);
                                else {
                                    console.log("variable " + idpaciente);
                                    document.getElementById('hidpaciente').value = idpaciente;
                                    document.getElementById('txtbusqueda').value = nombre.value + ' ' + apellido.value;

                                    crearCita();

                                    //BORRADO DE DATOS
                                    document.getElementById('txtapellido').value = "";
                                    document.getElementById('txtnombre').value = "";
                                    document.getElementById('txtdireccion').value = "";
                                    document.getElementById('txttelefono').value = "";
                                    document.getElementById('txtedad').value = "";
                                    document.getElementById('txtfechanacimiento').value = "";
                                    document.getElementById('txtocupacion').value = "";
                                    document.getElementById('txtnacionalidad').value = "";
                                    document.getElementById('txtalergias').value = "";

                                    document.getElementById('txtdatosextras').value = "";
                                    document.getElementById('txtcorreo').value = "";
                                    document.getElementById('txtnhijos').value = "";
                                    document.getElementById('txtoperaciones').value = "";




                                    Sexy.confirm('<h1>Informacion</h1><p>El Paciente se ha creado correctamente. ¿Desea Agregar foto al paciente?</p><p>Pulsa "Ok" para continuar, o pulsa "Cancelar" para salir.</p>', { onComplete:
                        function (returnvalue) {
                            if (returnvalue) {
                                establecefoto();
                            }
                            else {

                                mostrarFicha();

                            }


                        }
                                    });
                                }
                            });
                        }, /*GUARDAR PACIENTE*/
                        error: guardarno
                    });
                }
            });

            function guardarsi(msg) {
                $.each(msg.d, function () {
                    var mensaje = this.mensaje;
                    var idpaciente = this.idpaciente;

                    if (mensaje.substring(0, 5) == "ERROR")
                        mensajeadvertencia(mensaje);
                    else {
                        document.getElementById('formulario').reset();
                        document.getElementById('hidpaciente').value = idpaciente;
                        Sexy.confirm('<h1>Informacion</h1><p>El Paciente se ha creado correctamente. ¿Desea Agregar foto al paciente que se ha creado?</p><p>Pulsa "Ok" para continuar, o pulsa "Cancelar" para salir.</p>', { onComplete:
                        function (returnvalue) {
                            if (returnvalue) {
                                establecefoto();
                            }
                        }
                        });
                    }
                });
            }
            function guardarno(msg) {
                $.each(msg.d, function () {
                    mensajeerror(msg.responseText);
                });
            }

            $(function () {
                $("#txtocupacion").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: "../WSPuesto.asmx/ProfesionDatosFiltrado",
                            data: "{ 'nombre': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataFilter: function (data) { return data; },
                            success: function (data) {
                                response($.map(data.d, function (item) {
                                    return {
                                        value: item.nombre.toUpperCase(),
                                        codigo: item.idpuesto
                                    }
                                }))
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                alert(textStatus);
                            }
                        });
                    },
                    minLength: 2
                });
            });


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
                // recupera la informacion del producto seleccionado  
                var paciente = ui.item.value;
                codigopaciente = ui.item.codigo;
                mensajecorrectopermanente("Codigo " + ui.item.codigo + " " + paciente);
                document.getElementById('hidpaciente').value = codigopaciente;
                //document.getElementById('hidpaciente').value = codigopaciente;
            }

            //optTipoBusqueda
            $('.optTipoBusqueda').click(function () {
                var busqueda = document.getElementById('txtbusqueda');
                busqueda.value = "";
                busqueda.focus();

            });

            //BOTON ACTUALIZAR LA EMPRESA SELECCIONADA
            $('#btnactualizar').click(function () {
                var apellido = document.getElementById('txtapellido').value;
                var nombre = document.getElementById('txtnombre').value;
                var estado = document.getElementById('dropestado').options[document.getElementById('dropestado').selectedIndex].text;
                var direccion = document.getElementById('txtdireccion').value;
                var telefono = document.getElementById('txttelefono').value;
                var edad = document.getElementById('txtedad').value;
                var fechanacimiento = document.getElementById('txtfechanacimiento').value;
                fechanacimiento = formatofecha(fechanacimiento);
                var ocupacion = document.getElementById('txtocupacion').value;
                var nacionalidad = document.getElementById('txtnacionalidad');
                var genero = document.getElementById('dropgenero');
                var recomendado = document.getElementById('droprecomendado');
                console.log(genero.selectedIndex);

                if (nacionalidad.selectedIndex == -1) { mensajeadvertencia("Seleccione una nacionalidad"); nacionalidad.focus(); return; }
                if (recomendado.selectedIndex == -1) { mensajeadvertencia("Seleccione quien lo recomendo"); recomendado.focus(); return; }
                if (genero.selectedIndex == -1) { mensajeadvertencia("Seleccione un genero para el paciente"); genero.focus(); return; }

                var datosextras = document.getElementById('txtdatosextras').value;
                var correo = document.getElementById('txtcorreo').value;
                var nhijos = document.getElementById('txtnhijos').value;
                var operaciones = document.getElementById('txtoperaciones').value;
                recomendado = recomendado.options[recomendado.selectedIndex].text;
                genero = genero.options[genero.selectedIndex].text;

                var foto = ""; //document.getElementById('txtfoto').value
                var idempresa = 1;
                var idpaciente = document.getElementById('hidpaciente').value;

                if (codigopaciente == "") {
                    mensajeadvertencia("No ha seleccionado un paciente");
                } else if (!nombre) {
                    mensajeadvertencia("Ingrese el nombre del paciente");
                    document.getElementById('txtnombre').focus();
                } else if (!apellido) {
                    mensajeadvertencia("Ingrese apellidos del paciente");
                    document.getElementById('txtapellido').focus();

                } else {

                    $.ajax({
                        type: "POST",
                        url: "../WSPaciente.asmx/PacienteActualizar",
                        data: '{idpaciente:"' + codigopaciente + '",nombre: "' + nombre + '",apellido:"' + apellido + '",estado:"' + estado + '",direccion:"' + direccion + '",telefono:"' + telefono + '",edad:"' + edad + '",fechanacimiento:"' + fechanacimiento + '",ocupacion:"' + ocupacion + '",nacionalidad:"' + nacionalidad.value + '",datosextras:"' + datosextras + '",correo:"' + correo + '",nhijos:"' + nhijos + '",operaciones:"' + operaciones + '",recomendado:"' + recomendado + '",genero:"' + genero + '",foto:"' + foto + '",idempresa:"' + idempresa + '"}',
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
                        mensajeadvertencia(mensaje);
                    else {
                        mensajecorrecto(mensaje);
                        document.getElementById("formulario").reset();
                        document.getElementById('txtnombre').focus();
                        inicializa();
                    }
                });
            }

            function actualizano(msg) {
                $.each(msg.d, function () {
                    mensajeerror(msg.responseText);
                });
            }

            //BOTON ELIMINAR LA EMPRESA SELECCIONADA
            $('#btneliminar').click(function () {
                if (codigopaciente == "")
                    mensajeadvertencia("No ha seleccionado un paciente");
                else {
                    Sexy.confirm('<h1>Advertencia</h1><p>¿Deseas Eliminar el registro seleccionado?</p><p>Pulsa "Ok" para continuar, o pulsa "Cancelar" para salir.</p>', { onComplete:
                  function (returnvalue) {
                      if (returnvalue) {

                          $.ajax({
                              type: "POST",
                              url: "../WSPaciente.asmx/PacienteEliminar",
                              data: '{idpaciente: "' + codigopaciente + '"}',
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

                if (mensaje.substring(0, 5) == "ERROR") {
                    mensajeadvertencia(mensaje);
                }
                else {
                    mensajecorrecto("Dato eliminado correctamente");
                    document.getElementById('formulario').reset();
                    inicializa();
                }
            }

            function eliminano(msg) {
                $.each(msg.d, function () {
                    //Sexy.error("<h1>Control Medico Web</h1><p>" + msg.responseText + "</p>");
                    mensajeerror(msg.responseText);
                });
            }

            //BOTON CANCELAR OPERACION ACTUALIZAR O ELIMINAR
            $('#btncancelar').click(function () {
                inicializa();
            });

            //BOTON CANCELAR OPERACION ACTUALIZAR O ELIMINAR        
            $('#btncita').click(function () {
                crearCita();
            });

            $('#mostrarpaciente').click(function () {
                $("#divpaciente").fadeIn(750);
                $("#divbusqueda").fadeIn(750);
                document.getElementById('divdespacho').style.display = 'none';
                document.getElementById('divdespachocompleto').style.display = 'none';

            });

            $('#btnficha').click(function () {
                mostrarFicha();
            });

            $('#btndatosactualizados').click(function () {
                $.ajax({
                    type: "POST",
                    url: "../WSPaciente.asmx/mostrarUltimosDatosActualizados",
                    data: '{}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        console.log(msg.d);
                        mostrarmodal('datosactualizados');
                        document.getElementById('cp1').innerHTML = msg.d.codigopaciente;
                        document.getElementById('cp2').innerHTML = msg.d.nombrepaciente + " " + msg.d.apellidopaciente;
                        document.getElementById('cp3').innerHTML = msg.d.direccionpaciente;
                        document.getElementById('cp4').innerHTML = msg.d.fechacreacionpaciente;

                        document.getElementById('Td1').innerHTML = msg.d.codigopacientereceta;
                        document.getElementById('Td2').innerHTML = msg.d.nombrepacientereceta + " " + msg.d.apellidopacientereceta;
                        document.getElementById('Td3').innerHTML = msg.d.direccionpacientereceta;
                        document.getElementById('Td4').innerHTML = msg.d.codigoreceta;
                        
                        document.getElementById('Td6').innerHTML = msg.d.fechareceta;
                        

                    },
                    error: function (msg) { mensajeerror(msg.responseText); }
                });
            });


            $('#mostrardespacho').click(function () {
                $("#divdespacho").fadeIn(750);
                document.getElementById('divpaciente').style.display = 'none';
                document.getElementById('divbusqueda').style.display = 'none';
                document.getElementById('divdespachocompleto').style.display = 'none';
                document.getElementById('idpacienteactivo').value = '';
                document.getElementById('pacienteactivo').value = '';

                despacharpacientes();
            });

            $('#mostrardespachocompleto').click(function () {
                $("#divdespachocompleto").fadeIn(750);
                document.getElementById('divpaciente').style.display = 'none';
                document.getElementById('divbusqueda').style.display = 'none';
                document.getElementById('divdespacho').style.display = 'none';
                creapacientesenespera();
                //despacharpacientes();
            });


            //FIN DOCUMENT READY
        });


        function crearCita() {
            console.log("creando la cita");
            if (codigopaciente == "")
                mensajeadvertencia("No ha seleccionado un paciente");
            else
                $.ajax({
                    type: "POST",
                    url: "../WSPaciente.asmx/PacienteGuardarCita",
                    data: '{idpaciente: "' + codigopaciente + '",iddoctor: "' + document.getElementById('dropmedico').value + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $.each(msg.d, function () {
                            var resultado = this.mensaje;
                            if (resultado.substring(0, 5) == "ERROR")
                                mensajeadvertencia(resultado);
                            else {
                                document.getElementById('txtbusqueda').value = "";
                                mensajecorrectopermanente(resultado + ". Codigo de paciente " + codigopaciente);
                                document.getElementById('txtbusqueda').focus();
                            }
                        });
                    },
                    error: function (msg) { mensajeerror(msg.responseText); }
                });
        }

        function crearcitasi(msg) {
            $.each(msg.d, function () {
                var resultado = this.mensaje;
                if (resultado.substring(0, 5) == "ERROR")
                    mensajeadvertencia(resultado);
                else {
                    document.getElementById('txtbusqueda').value = "";
                    mensajecorrecto(resultado);
                    document.getElementById('txtbusqueda').focus();
                }
            });
        }
        function crearcitano(msg) {
            mensajeerror(msg.responseText);
        }


        //THE FUNCTIONS

        function close_modal() {

            //hide the mask
            $('#mask').fadeOut(500);

            //hide modal window(s)
            $('.modal_window').fadeOut(500);
            document.getElementById('txtbusqueda').focus();

        }
        function show_modal(modal_id) {

            //get the height and width of the page
            var window_width = $(window).width();
            var window_height = $(window).height();
            //vertical and horizontal centering of modal window(s)
            /*we will use each function so if we have more then 1 
            modal window we center them all*/
            $('.modal_window').each(function () {
                //get the height and width of the modal
                var modal_height = $(this).outerHeight();
                var modal_width = $(this).outerWidth();
                //calculate top and left offset needed for centering
                var top = (window_height - modal_height) / 2;
                var left = (window_width - modal_width) / 2;
                //apply new top and left css values 
                $(this).css({ 'top': top, 'left': left });

            });

            //set display to block and opacity to 0 so we can use fadeTo
            $('#mask').css({ 'display': 'block', opacity: 0 });

            //fade in the mask to opacity 0.8 
            $('#mask').fadeTo(500, 0.5);

            //show the modal window
            $('#' + modal_id).fadeIn(500);

        }

        function establecefoto() {
            //get the id of the modal window stored in the name of the activating element
            //alert(document.getElementById('hidpaciente').value);
            var modal_id = 'modal_window';
            if (document.getElementById('hidpaciente').value == "") {
                //Sexy.error("<h1>Control Medico Web</h1><br/><p> No se ha buscado algun paciente </p>");
                mensajeadvertencia("No se ha buscado algun paciente");
            }
            else {
                //use the function to show it
                show_modal(modal_id);
                //mostrarmodal(modal_id);
            }

        }

        function cerrarmodalSencillo(control) {
            $.fancybox.close("#" + control)
        }

        function cerrarmodal() {

            //use the function to close it
            close_modal();

            document.getElementById('btngrabarfoto').style.display = '';
            document.getElementById('btncancelarfoto').style.display = 'none';
            document.getElementById('btnenviarfoto').style.display = 'none';
        }

        function determinaTipoBusqueda() {
            var opcionBusqueda = 0;

            if ($("#optCodigo").is(':checked')) opcionBusqueda = 1;
            if ($("#optNombre").is(':checked')) opcionBusqueda = 2;
            if ($("#optApellido").is(':checked')) opcionBusqueda = 3;
            return opcionBusqueda;
        }

        function mostrarFicha() {
            //moyo
            var idpaciente = document.getElementById('hidpaciente');
            var busqueda = document.getElementById('txtbusqueda');
            if (!idpaciente.value) {
                mensajeadvertencia("Al parecer no hay un paciente seleccionado");
            } else {
            $.ajax({
                type: "POST",
                url: "../WSConsultamedica.asmx/fichaPaciente",
                data: "{'idPaciente':" + idpaciente.value + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d.substring(0, 5) == "ERROR")
                        mensajeerror(msg.d);
                    else {
                        busqueda.value = "";
                        idpaciente.value = "";
                        codigopaciente = "";
                        abremodal(msg.d);
                    }

                },
                error: function (msg) {
                    mensajeerror(msg.d);
                }
            });
            }
    }

    function creapacientesenespera() {
        var cadenabusqueda = '';
        $.ajax({
            type: "POST",
            url: "../WSConsultamedica.asmx/ConsultaCreaPacientesTodos",
            data: '{cadenabusqueda:"' + cadenabusqueda + '",usuario: "' + document.getElementById('usuario').innerHTML + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: creapacientessi,
            error: creapacientesno
        });
    }

    function creapacientessi(msg) {
        $("#divpacientesdespachocompleto").html("");
        var html = '';
        var n = 0;
        $.each(msg.d, function () {
            html += '<input type="button" value="' + this.nombrepaciente + '" id="' + this.idpaciente + '" class="stylefacecompleto"/>';
            n++;
        });
        $("#divpacientesdespachocompleto").append(html);
        document.getElementById('lblnumeropacientes').innerHTML = n;
    }

    function creapacientesno(msg) { mensajeerror(msg.responseText); }

    $(function () {
        $("#txtbusquedatratamiento").autocomplete({
            autoFocus: true,
            source: function (request, response) {
                $.ajax({
                    url: "../WSConsultamedica.asmx/ConsultaTratamiento",
                    data: "{ 'busqueda': '" + request.term + "','opcion': 1 }",
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

        $("#txtbusquedamedicamento").autocomplete({
            autoFocus: true,
            source: function (request, response) {
                $.ajax({
                    url: "../WSConsultamedica.asmx/ConsultaTratamiento",
                    data: "{ 'busqueda': '" + request.term + "','opcion': 2 }",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                value: item.idtratamiento,
                                existencia: item.existencia
                            }
                        }))
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus);
                    }
                });
            },
            minLength: 2,
            select: seleccionaMedicamento
        });

    //SINTOMAS
        $("#txtbusquedasintoma").autocomplete({
            autoFocus: true,
            source: function (request, response) {
                $.ajax({
                    url: "../WSConsultamedica.asmx/ConsultaSintoma",
                    data: "{ 'busqueda': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                value: item.nombre,
                                codigo: item.idsintoma
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


    function seleccionaTratamiento(event, ui) {
        var idtratamiento = ui.item.value;
        setTimeout('document.getElementById("txtbusquedatratamiento").value="";', 100);
        var observaciones = document.getElementById('txttratamientoobservaciones');
        var existe = existeelemento('detalleindicacionesagregados', idtratamiento);

        if (existe == false) {
            var html = '';
            html += '<tr id="filaindicacion' + filasindicaciones + '">' +
                            '<td id="obsindicacion' + filasindicaciones + '" style="display:none;">'+ observaciones.value +'</td>' +
   					        '<td><img id="indiobs' + filasindicaciones + '" src="../images/icn_new_article.png" onclick="agregarobservaciones(this.id);" style="cursor:pointer;" title="Agregar observaciones"/></td>' +
    				        '<td style="color:Green;font-weight:bolder;">' + idtratamiento + '</td>' +
    				        '<td><img id="indi' + filasindicaciones + '" onclick="eliminafilaindicaciones(this.id);" style="cursor:pointer;" src="../images/icn_trash.png" title="Eliminar"/></td>' +
				       '</tr>';
            $("#detalleindicacionesagregados").append(html);
            filasindicaciones++;
            observaciones.value = "";
        }
    }

    function seleccionaMedicamento(event, ui) {
        var existencia = ui.item.existencia;
        console.log(existencia);
        var estilo = (existencia > 0) ? 'style="color:Green;font-weight:bolder;"' : 'style="color:red;font-weight:bolder;"'; 
                
        var idtratamiento = ui.item.value;
        setTimeout('document.getElementById("txtbusquedamedicamento").value="";', 100);
        var existe = existeelemento('detallemedicamentosagregados', idtratamiento);
        var observaciones = document.getElementById('txtmedicamentoobservaciones');
        if (existe == false) {
            var html = '';
            html += '<tr id="filamedicamento' + filasmedicamentos + '">' +
                            '<td id="obsmedicamento' + filasmedicamentos + '" style="display:none;">'+observaciones.value+'</td>' +
   					        '<td><img id="mediobs' + filasmedicamentos + '" src="../images/icn_new_article.png" onclick="agregarobservaciones(this.id);" style="cursor:pointer;" title="Agregar observaciones"/></td>' +
    				        '<td '+ estilo+'>' + idtratamiento + '</td>' +
    				        '<td><img id="medi' + filasmedicamentos + '" onclick="eliminafilamedicamentos(this.id);" src="../images/icn_trash.png" style="cursor:pointer;" title="Eliminar"/></td>' +
				       '</tr>';
            $("#detallemedicamentosagregados").append(html);
            filasmedicamentos++;
            observaciones.value="";
        }
    }

    function seleccionaSintoma(event, ui) {
        var idsintoma = ui.item.codigo;
        setTimeout('document.getElementById("txtbusquedasintoma").value="";', 100);
        var existe = existeelemento('detallesintomasagregados', idsintoma);
        var observaciones = document.getElementById('txtsintomaobservaciones');
        if (existe == false) {
            var html = '';
            html += '<tr id="filasintoma' + filassintomas + '">' +
                            '<td id="obssintoma' + filassintomas + '" style="display:none;">'+ observaciones.value +'</td>' +
   					        '<td><img id="sintobs' + filassintomas + '" src="../images/icn_new_article.png" onclick="agregarobservaciones(this.id);" style="cursor:pointer;" title="Agregar observaciones"/></td>' +
    				        '<td style="display:none;">' + idsintoma + '</td>' +
                            '<td style="color:Green;font-weight:bolder;">' + ui.item.value + '</td>' +
    				        '<td><img id="indi' + filassintomas + '" onclick="eliminafilasintomas(this.id);" src="../images/icn_trash.png" style="cursor:pointer;" title="Eliminar"/></td>' +
				       '</tr>';
            $("#detallesintomasagregados").append(html);
            filassintomas++;
            observaciones.value=""
        }
    }

    function existeelemento(tabla, valor) {
        var control = false;
        $('#' + tabla + ' tr').each(function (index) {
            $(this).children("td").each(function (index2) {
                switch (index2) {
                    case 2:
                        if (valor == $(this).text()) {

                            control = true;
                            console.log("valor control " + control);
                            return false;
                            break;
                        }
                }
            });
        });
        return control;
    }

    function eliminafilasintomas(control) {
        console.log(control);
        var correlativo = control.substring(4, control.length);
        console.log("correlativo " + correlativo);
        jqRow = $("#filasintoma" + correlativo);
        jqRow.fadeOut("slow", function () {
            jqRow.remove();

        });
    }

    function eliminafilaindicaciones(control) {
        console.log(control);
        var correlativo = control.substring(4, control.length);
        console.log("correlativo " + correlativo);
        jqRow = $("#filaindicacion" + correlativo);
        jqRow.fadeOut("slow", function () {
            jqRow.remove();

        });
    }

    function eliminafilamedicamentos(control) {
        console.log(control);
        var correlativo = control.substring(4, control.length);
        console.log("correlativo " + correlativo);
        jqRow = $("#filamedicamento" + correlativo);
        jqRow.fadeOut("slow", function () {
            jqRow.remove();

        });
    }

    function agregarobservaciones(control) {
        console.log(control);
        mostrarmodal("divagregarobservaciones");
        var correlativo = control.substring(7, control.length);
        var objeto = control.substring(0, 7);
        document.getElementById('hobservaciones').value = control;
        console.log(objeto);
        console.log(correlativo);
        var origen = '';
        if (objeto == "sintobs")
            origen = 'obssintoma' + correlativo
        else if (objeto == "indiobs")
            origen = 'obsindicacion' + correlativo
        else if (objeto == "mediobs")
            origen = 'obsmedicamento' + correlativo

        var observacion = document.getElementById('txtobservacion');
        observacion.value = "";
        observacion.value = document.getElementById(origen).innerHTML;
        observacion.focus();
    }

    function recorte (myString)
    {
        return myString.replace(/^\s+/g,'').replace(/\s+$/g,'')
    }

    
    function agradecimiento() {
        var check=document.getElementById('chkfactura');
        if (check.checked == true) {
            var nombreCliente = document.getElementById('txtnombrefactura');
            var texto = nombreCliente.value + ", muchas gracias por su visita, esperamos que regrese pronto";
            var url = 'http://translate.google.com/translate_tts?tl=es&q=' + texto;
            window.frames['marcooculto'].location.replace(url);
            check.checked = false;
            nombreCliente.value = "";
          //  console.log("no mas vos");
        }
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
                <a id="mostrarpaciente" class="current" href="#">Paciente</a> 
                <div class="breadcrumb_divider"></div> 
                <a id="mostrardespacho"  href="#">Despachar</a>
                <div class="breadcrumb_divider"></div> 
                <a id="mostrardespachocompleto"  href="#">Recetar</a>
            </article>
		</div>
	</section><!-- end of secondary bar -->
	
	<aside id="sidebar" class="column">
	
            <h3>PACIENTES</h3>
		<ul class="toggle">
            <li class="icn_new_article"><a href="pacienteconsulta.aspx">Consulta</a></li>
			<li class="icn_edit_article"><a href="#" style="color:#04B404;text-shadow: 1px 1px white; font-weight:bolder;">Paciente</a></li>
			<li class="icn_categories"><a  href="pacientehistorial.aspx">Historial</a></li>
		</ul>
        <h3>MEDICAMENTOS</h3>
		<ul class="toggle">
            <li class="icn_folder"><a href="ingresomedicina.aspx">Compra de medicamento</a></li>
            
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
        </p>       
                        </div>
                        <br />
                        <div style="width:90%">
                            <input type="text" id="txtbusqueda" class="texto" style="width:30%;" />
                            <input type="button" id="btncita" class="youtube" value="Crear cita"/>
                            <select id="dropmedico" >
                                
                            </select>
                            
                            <input type="button" id="btnbuscar" class="youtube" value="Editar info"/>
                            <input type="button" id="btnfoto" onclick="establecefoto();" class="youtube" value="Agregar foto"/>
                            <input type="button" id="btnficha" class="youtube" value="Ficha"/>
                            <input type="button" id="btndatosactualizados" class="youtube" value="Ultimos Datos"/>

                        </div>
                        </div>
                        </article>

		<article id="divpaciente" class="module width_full" >
			<header><h3>PACIENTES</h3></header>
				<div class="module_content" style="height:300px;" >

                <!-- PRIMER DIV -->
					<div style="width:48%;float:left">
                        
                        <div style="width:100%">
                            <label class="label">NOMBRES</label>
                            <input id="txtnombre" type="text" class="textopaciente" />
                        </div>

                        <div style="width:100%">
                            <label class="label">APELLIDOS</label>
                            <input id="txtapellido" type="text" class="textopaciente" />
                        </div>

                        <div style="width:100%">
                            <label class="label">ESTADO CIVIL</label>
                            <select id="dropestado" class="droppaciente">
								<option>Soltero</option>
								<option>Casado</option>
							</select>
                        </div>

                        <div style="width:100%">
                            <label class="label">TELEFONO</label>
                            <input type="text" id="txttelefono" class="textopaciente"/>
                        </div>

                        <div style="width:100%">
                            <label class="label">DIRECCION</label>
                            <input type="text" id="txtdireccion" class="textopaciente"/>
                        </div>

                        <div style="width:100%">
                            <label class="label">EDAD</label>
                            <input type="text" id="txtedad" onblur="calculaanionacimiento(this.value);" class="textopaciente"/>
                        </div>

                        <div style="width:100%">
                            <label class="label">FECHA DE NACIMIENTO</label>
                            <input type="text" id="txtfechanacimiento"  class="textopaciente"/>
                            <input type="hidden" id="hfechaactual" value="" />
                        </div>
                        
                        <div style="width:100%">
                            <label class="label">AREA DE TRABAJO</label>
                            <input type="text" id="txtocupacion" class="textopaciente"/>                          
                        </div>

                   </div>
                <!--FIN PRIMER DIV-->

                <!--SEGUNDO DIV-->
                <div style="width:48%;float:left">
                        
                        <div style="width:100%">
                            <label class="label">NACIONALIDAD</label>
                            <select id="txtnacionalidad" class="droppaciente"></select>
                        </div>

                        <div style="width:100%">
                            <label class="label">CORREO</label>
                            <input type="text" id="txtcorreo" class="textopaciente"/>
                        </div>

                        <div style="width:100%">
                            <label class="label">HIJOS</label>
                            <input type="text" id="txtnhijos" class="textopaciente"/>
                        </div>

                        <div style="width:100%">
                            <label class="label">OPERACIONES</label>
                            <input type="text" id="txtoperaciones" class="textopaciente"/>
                        </div>

                        <div style="width:100%">
                            <label class="label">ALERGIAS</label>
                            <input type="text" id="txtalergias" class="textopaciente"/>
                        </div>

                        <div style="width:100%">
                            <label class="label">RECOMENDADO POR:</label>
                            <select id="droprecomendado" class="droppaciente">
								<option>Radio</option>
								<option>Television</option>
                                <option>Familiares</option>
								<option>Testimonio</option>
							    <option>Pagina web</option>
							</select>
                        </div>

                        <div style="width:100%">
                            <label class="label">GENERO:</label>
							<select id="dropgenero" class="droppaciente">
								<option>M</option>
								<option>F</option>
							</select>
                        </div>

                        <div style="width:100%">
                            <label class="label">OBSERVACIONES:</label>
							<textarea id="txtdatosextras" rows="2" cols="55" style="width:90%; margin-left:10px;"></textarea>
                        </div>

                </div>
                <!--FIN SEGUNDO DIV-->

                        
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
		

        <!--DIV PARA EL DESPACHO-->

        <article id="divdespacho" class="module width_full" style="display:none;">
			<header><h3>DESPACHAR</h3></header>
				<div class="module_content" style="height: 400px;">
                <!-- PRIMER FILA -->
						
                        <fieldset id="divpacientes" style="padding:10px 10px 10px 10px; width:30%; min-height:300px; float:left;">
                        <legend>PACIENTES PENDIENTES DE DESPACHO</legend>

                        </fieldset>

                        <div id="divreceta" style="padding: 5px  0px 0px 0px; width:65%; margin-left:5px; height:100%; float:left;">
                        <span style="font-size:12px; font-weight:800;"> PACIENTE ACTUAL: </span>
                        
                        <input type="hidden" id="hcontador" value="0" />
                        
                        <input type="hidden" id="idpacienteactivo" value="" />
                        <h3 id="pacienteactivo" style="font-size:13px; color:Green; font-weight:800;"></h3>
                        
                        <span style="font-size:12px; font-weight:800; float:left"> PROXIMA CONSULTA: </span>
                        <input type="text" id="txtproximaconsultadespacho" style="width:40%;" class="textopaciente" />
                        <br />
                        
                        <article class="module width_full">
			            <header><h3 style="font-size:medium; font-weight:bolder; margin-left:10px;" class="tabs_involved">Receta</h3></header>
			            <div class="tab_container">
                        <table id="tablareceta" class="tablesorter" cellspacing="0" >
                        <thead>
                        <tr>
                            <th class="header">TRATAMIENTO</th>
                            <th class="header">MEDICINA</th>
                            <th class="header">EXISTENCIA</th>
                            <th class="header">CANTIDAD</th>
                        </tr>
                        </thead>
                        <tbody id="detallereceta">
                        
                        </tbody>
                            
                        </table>
                        </div></article>

                        </div>
                        </div>

                <footer>
					<div class="submit_linkizquierda">
					    
                        <input type="button" id="btnguardarreceta" class="youtube" value="Guardar"/>
                        
                    </div>
                </footer>

        </article>

        <!--FIN DIV PARA EL DESPACHO-->

		<!--INICIO DEL DIV PARA QUE LOS ADMINISTRADORES INGRESEN TODA LA INFORMACION, CUANDO EL DOCTOR NO LA HAYA INGRESADO DESDE SU TABLE-->

        <article id="divdespachocompleto" class="module width_full" style="display:none;">
			<header><h3>DESPACHO COMPLETO</h3></header>
				<div class="module_content" style="height:450px;">

                <!--PANEL DE SINTOMAS-->
			    <article id="articlesintomas" class="module width_quarter">
                <header><h3>SINTOMAS</h3></header>
                    <p id="sintomaactual" class="first-p"></p>      
                    <table>
                        <thead>
                        <tr>
                            <th>OBSERVACION</th>
                            <th>SINTOMA</th>
                        </tr>
                        </thead>
                        <tbody>
                        <tr>
                            <th><input type="text" id="txtsintomaobservaciones" class="textoind"/></th>
                            <th><input type="text" id="txtbusquedasintoma" class="textoind"/></th>
                        </tr>
                        </tbody>
                        
                    </table>                 
                    <div class="tab_container" style="
			          height: 150px;
			          overflow-x:hidden;
                      overflow-y: scroll;">
                        
                <table class="tablesorter" cellspacing="0"> 
			        <thead> 
				        <tr> 
   					        <th class="header">+</th> 
    				        <th class="header" style="display:none;">CODIGO</th> 
    				        <th class="header">SINTOMA</th> 
    				        <th class="header">ACCION</th> 
				        </tr> 
			        </thead> 
			        <tbody id="detallesintomasagregados"> 
				 				 
			        </tbody> 
			    </table>

            </div>
		    
                </article>
			<!--FIN PANEL DE SINTOMAS-->

            <!--PANEL DE TRATAMIENTOS -->
				<article class="module width_quarter">    
                    <header><h3>INDICACIONES</h3></header>
                    <p class="first-p"></p>
				        <table>
                        <thead>
                        <tr>
                            <th>OBSERVACION</th>
                            <th>TRATAMIENTO</th>
                        </tr>
                        </thead>
                        <tbody>
                        <tr>
                            <th><input type="text" id="txttratamientoobservaciones" class="textoind"/></th>
                            <th><input type="text" id="txtbusquedatratamiento" class="textoind"/></th>
                        </tr>
                        </tbody>
                        
                    </table>

                        <div class="tab_container" style="
			          height: 150px;
			          overflow-x:hidden;
                      overflow-y: scroll;">
                            <table class="tablesorter" cellspacing="0"> 
			                    <thead> 
				                    <tr> 
   					                    <th class="header">+</th> 
    				                    <th class="header">TRATAMIENTO</th> 
    				                    <th class="header">ACCION</th> 
				                    </tr> 
			                    </thead> 
			                    <tbody id="detalleindicacionesagregados"> 				 
				 
			                    </tbody> 
			                 </table>
                        </div>


                </article>
			<!--FIN PANEL DE TRATAMIENTOS-->

            <!--PANEL DE MEDICINA-->
			    <article class="module width_quarter">				    
                    <header><h3>MEDICAMENTOS</h3></header>
                    <p class="first-p"></p>
				   <table>
                        <thead>
                        <tr>
                            <th>OBSERVACION</th>
                            <th>MEDICAMENTO</th>
                        </tr>
                        </thead>
                        <tbody>
                        <tr>
                            <th><input type="text" id="txtmedicamentoobservaciones" class="textoind"/></th>
                            <th><input type="text" id="txtbusquedamedicamento" class="textoind"/></th>
                        </tr>
                        </tbody>
                        
                    </table>


                        <div class="tab_container" style="
			          height: 150px;
			          overflow-x:hidden;
                      overflow-y: scroll;">                    
                            <table class="tablesorter" cellspacing="0"> 
			                    <thead> 
				                    <tr> 
   					                    <th class="header">+</th> 
    				                    <th class="header">TRATAMIENTO</th> 
    				                    <th class="header">ACCION</th> 
				                    </tr> 
			                    </thead> 
			                    <tbody id="detallemedicamentosagregados"> 
				 
				 
			                    </tbody> 
			               </table>
                        </div>

                </article>
			<!--FIN PANEL DE MEDICINA-->

                        <fieldset id="divpacientesdespachocompleto" style="width:26%; margin-top:18px; min-height:120px; overflow-x:hidden;
                        overflow-y:scroll;margin-right: 0; margin-left:3%;float:left;">
                        <legend>PACIENTES PARA DESPACHO COMPLETO</legend>

                        </fieldset>

                            <!--div datos del paciente que esta siendo atendido-->
        <article class="module width_3_quarter">
		<header><h3>PACIENTE ACTUAL</h3></header>
        <input type="hidden" id="hidpacienteactivodespachocompleto" value="" />

			<table class="tablesorter" cellspacing="0"> 
			<tbody> 

            	<tr> 
   					<td><strong>CODIGO</strong></td> 
    				<td id="codigopaciente" style="color:green; font-weight:bolder;"></td> 
    				<td colspan="2"><strong>FECHA REGRESO</strong></td>  
                    <td colspan="2" style="color:green; font-weight:bolder;"> <input type="text" class="textopaciente" id="txtproximaconsultadespachocompleto"/> </td> 
				</tr> 
				<tr> 
   					<td><strong>NOMBRE</strong></td> 
    				<td id="nombre" colspan="5" style="color:green; font-weight:bolder;"></td> 
    				
				</tr> 

                <tr> 
   					<td><strong>FACTURA</strong></td> 
    				<td><input type="checkbox" id="chkfactura"/></td> 
    				<td colspan="3"><strong>A NOMBRE DE</strong></td> 
    				<td><input type="text" maxlength="50" name="q" id="txtnombrefactura" class="textopaciente"/> </td> 
                    
				</tr> 
               
			</tbody> 
			</table>


	    <footer>
				<div class="submit_linkizquierda">Pacientes <label id="lblnumeropacientes"> 0</label></div>
                <div class="submit_link">
                    <input type="button" class="youtube" id="btnguardarrecetacompleta" value="Guardar" /> 
                    <!--<input type="button" class="youtube" id="btnhistorial" value="Historial completo" />-->
                 </div>
		</footer>

		</article>

                        </div>

                <footer>
					<div class="submit_linkizquierda">
					</div>
                </footer>

        </article>

        <!--FIN DEL DIV PARA QUE LOS ADMINISTRADORES INGRESEN TODA LA INFORMACION, CUANDO EL DOCTOR NO LA HAYA INGRESADO DESDE SU TABLE-->
        
        <article class="module width_full" id="divagregarobservaciones" style="display:none;margin-left:0px; width:400px;">
			            <header><h3 style="font-size:medium; font-weight:bolder; margin-left:10px;" class="tabs_involved">OBSERVACIONES</h3></header>
			            <div class="tab_container">
                        <input type="hidden" id="hobservaciones" value=""/>
                        <label for="txtobservacione">INGRESE LA OBSERVACION</label><input type="text" id="txtobservacion" class="texto" />
                        <br />
                        <br />
                        </div>
                        <footer>
                        <div class="submit_link">
                            <input type="button" class="youtube" id="btnagregarobservacion" value="Agregar" />                     
                        </div>
                        </footer>
    </article>

        <!-- end of styles article -->
		<div class="spacer"></div>
	</section>

    <!-- MODAL PARA SUBIR LA FOTO -->

        <div id='mask' class='close_modal'></div>

	<div id="modal_window" class="modal_window">
    <table width="80%">
    <tr><th>TOME LA FOTO QUE DESEA AGREGAR AL PACIENTE SELECCIONADO</th></tr>
    <tr>
    <td>
    <script type="text/javascript">
            document.write(webcam.get_html(240, 240));        
   </script>
   <br /><br />
   </td></tr>
    <tr>
    <td>  
    <button onclick="camGrabar(); return false;" class="youtube" id='btngrabarfoto'>TOMAR FOTO</button>
    <button onclick="camCancelar(); return false;" class="youtube" id='btncancelarfoto' style='display:none'>REEMPLAZAR FOTO</button>
    <button onclick="camEnviar(); return false" class="youtube" id='btnenviarfoto' style='display:none'>CARGAR FOTO</button>    
    <button onclick="cerrarmodal(); return false" class="youtube" id="btnterminar">CERRAR</button>
    </td>
    </tr>
    </table>
    </div>
    <iframe id="marcooculto" name="marcooculto" src="" width="0" height="0" ></iframe>

                <article class="module width_full" style="width:800px; margin-top:0px; margin-left:0px;display:none;" id="datosactualizados">    
                    <header><h3>ULTIMOS DATOS INGRESADOS</h3></header>



                        <div class="tab_container">
                            <table class="tablesorter" cellspacing="0"> 
			                    <thead> 
				                    <tr> 
   					                    <th class="header">CODIGO PACIENTE</th> 
    				                    <th class="header" colspan="2">NOMBRE PACIENTE</th> 
    				                    <th class="header" colspan="2">DIRECCION</th> 
                                        <th class="header">FECHA CREACION</th> 

				                    </tr> 
			                    </thead> 
			                    <tbody id="Tbody1"> 				 
				 
			                    <tr>
                                    <td id="cp1" style="color:Green;font-weight:bolder;"></td>
                                    <td id="cp2" colspan="2" style="color:Green;font-weight:bolder;"></td>
                                    <td id="cp3" colspan="2" style="color:Green;font-weight:bolder;"></td>
                                    <td id="cp4" style="color:Green;font-weight:bolder;"></td>
                                </tr>

                                    <tr><th class="header" colspan="6">ULTIMA RECETA</th></tr>
                                    <tr> 
                                        <th class="header">CODIGO PACIENTE</th> 
   					                    <th class="header" colspan="2">NOMBRE PACIENTE</th> 
    				                    <th class="header">DIRECCION</th>
                                        <th class="header">RECETA</th> 
    				                    <th class="header">FECHA RECETA</th> 

				                    </tr> 
			                    <tr>
                                    <td id="Td1" style="color:Green;font-weight:bolder;"></td>
                                    <td id="Td2" colspan="2" style="color:Green;font-weight:bolder;"></td>
                                    <td id="Td3" style="color:Green;font-weight:bolder;"></td>
                                    <td id="Td4" style="color:Green;font-weight:bolder;"></td>
                                    
                                    <td id="Td6" style="color:Green;font-weight:bolder;"></td>
                                </tr>
                                </tbody>
			                 </table>
                        </div>

                </article>


</form>
</body>


</html>