<%@ Page Language="VB" AutoEventWireup="false" CodeFile="pacienteconsulta.aspx.vb" Inherits="privado_pacienteconsulta" %>

<!doctype html>
<html lang="en">

<head>
	<meta charset="utf-8"/>
	<title> BIO * NATURAL Online</title>
	
	<link rel="stylesheet" href="../css/layout.css" type="text/css" media="screen" />
	<link rel="stylesheet" href="../css/sexyalertbox.css" type="text/css" media="screen" />
	<link rel="stylesheet" href="../js/sexy-tooltips/hulk.css" type="text/css" media="screen" />
    <link href="source/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    <link href="../css/notifications.css" rel="stylesheet" type="text/css" />    <link rel="stylesheet" type="text/css" href="../css/jquery.multiselect.css" />
    <link rel="stylesheet" type="text/css" href="../css/jquery-ui-1.8.24.custom.css" />
    <link href="../css/estiloslider.css" rel="stylesheet" type="text/css" />


    <!--[if lt IE 9]>
	<link rel="stylesheet" href="css/ie.css" type="text/css" media="screen" />
	<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>

	<![endif]-->
	<!--<script src="../js/jquery-1.5.2.min.js" type="text/javascript"></script>-->
    <script src="../js/jquery-1.8.2.min.js" type="text/javascript"></script>
    
    <script type="text/javascript" src="../js/jquery-ui-1.8.24.custom.min.js"></script>
        <script src="source/jquery.fancybox.pack.js" type="text/javascript"></script>

    <script type="text/javascript" src="../js/map.hilight.js"></script>
	<script src="../js/hideshow.js" type="text/javascript"></script>
	<script src="../js/jquery.tablesorter.min.js" type="text/javascript"></script>
	<script type="text/javascript" src="../js/jquery.equalHeight.js"></script>
    <script type="text/javascript" src="../js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="../js/sexyalertbox.v1.2.jquery.js"></script>
    <script type="text/javascript" src="../js/sexy-tooltips.v1.1.jquery.js"></script>    <script type="text/javascript" src="../js/acordion.js"></script>
    <script src="../js/vallenato.js" type="text/javascript" charset="utf-8"></script>    <script src="../js/mylibs/jquery.notifications.js" type="text/javascript"></script>

<style type="text/css">
    
.filareceta
{
    width:200px;
    float: left;
    }
        
.filareceta h3 {
    display: block;
    font-size: 1.17em;
    margin-before: 1em;
    margin-after: 1em;
    margin-start: 0;
    margin-end: 0;
    font-weight: bold; 
}
        
h3 {
    display: block;
    font-size: 1.05em;
    margin-left:10px;
    font-weight: bold;
}
</style>

    <!--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.js"></script>-->


    <script type="text/javascript" src="../js/jquery.multiselect.js"></script>
<script type="text/javascript" language="javascript">

    var filassintomas = 0;
    var filasindicaciones = 0;
    var filasmedicamentos = 0;

    $(document).ready(function () {
        var slides;


        $(".next").click(function () {
            mover('right');
        });


        $(".prev").click(function () {
            mover('left');
        });



        function mover(direction) {

            slides = $('#slider .slidesContainer > .slide');

            $('#slider .slidesContainer').css('width', slides[0].offsetWidth * slides.length);



            var direction = 'right';

            //Calcula la posiciÃ³n actual del contenedor
            position = $('#slider').scrollLeft();
            console.log("position " + position);
            //Calcula la anchura total menos el Ãºltimo slide.
            //Se usa para calcular cuando el scroll llega al final.
            totalWidth = (slides.length * slides[0].offsetWidth) - slides[0].offsetWidth

            //Se comprueba la variable direction para hacer el scroll hacia izquierda o derecha
            switch (direction) {
                case 'right': //Derecha
                    if (position + slides[0].offsetWidth > totalWidth) { //Si la siguiente posiciÃ³n se sale del contenedor, vuelve al principio.
                        $('#slider:not(:animated)').animate({ scrollLeft: 0 }, 1000);
                    } else { //Si no es el final, suma a la posiciÃ³n actual la anchura del slide.
                        $('#slider:not(:animated)').animate({ scrollLeft: position + slides[0].offsetWidth }, 1000);
                    }
                    break;

                case 'left': //Izquierda
                    if (position - slides[0].offsetWidth < 0) { //Si la siguiente posiciÃ³n se sale del contenedor, vuelve al final.
                        $('#slider:not(:animated)').animate({ scrollLeft: totalWidth }, 1000);
                    } else { //Si no es el final, resta a la posiciÃ³n actual la anchura del slide.
                        $('#slider:not(:animated)').animate({ scrollLeft: position - slides[0].offsetWidth }, 1000);
                    }
                    break;
            }

        }

    });

    function mensajecorrecto(mensaje) {
        $.jGrowl(mensaje, { theme: 'success' });
    }

    function mensajeerror(mensaje) {
        $.jGrowl(mensaje, { theme: 'error' });
    }

    function mensajeadvertencia(mensaje) {
        $.jGrowl(mensaje, { theme: 'warning' });
    }

    //PARA EL DROP DONDE APARECEN LOS SINTOMAS


    //PARA EL DROP DONDE APARECEN LOS SINTOMAS
    $(function () {

        $("#dropsintomasui").multiselect({
            click: function (event, ui) {

                if (ui.checked) {
                    var existe = existeelemento('detallesintomasagregados', ui.value);

                    if (existe == false) {

                        var html = '';
                        html += '<tr id="filasintoma' + filassintomas + '">' +
   					        '<td id="obssintoma'+ filassintomas +'" style="display:none;"></td>' +
                            '<td><img id="sintobs' + filassintomas + '" src="../images/icn_new_article.png" onclick="agregarobservaciones(this.id);" style="cursor:pointer;" title="Agregar observaciones"/></td>' +
    				        '<td style="display:none;">' + ui.value + '</td>' +
    				        '<td style="color:Green;font-weight:bolder;">' + ui.text + '</td>' +
    				        '<td><img id="sint' + filassintomas + '" src="../images/icn_trash.png" onclick="eliminafilasintomas(this.id);" style="cursor:pointer;" title="Eliminar"/></td>' +
				       '</tr>';
                        $("#detallesintomasagregados").append(html);
                        filassintomas++;
                    }
                }
                else {
                    if (existe == false) {
                        //var posicion = seleccionaoption(ui.value);
                        //document.getElementById('dropsintomas').remove(posicion);
                    }
                }
            }
        });
    });

    function seleccionaoption(val) {
        var resultado = 0;

        for (var indice = 0; indice < document.getElementById('dropsintomas').length; indice++) {

            if (document.getElementById('dropsintomas').options[indice].value == val) {
                resultado = indice;
                break;
            }
        }
        return resultado;
    }

    function leerchecks() {
        var values = $("#dropmedicinaui").val();
        
        /*MEDICINA*/
        var arregloMedicina = new Array();
        for (i = 0; i < values.length; i++) {
            arregloMedicina.push({ idmedicina: values[i] });

        }
        

        /*TRATAMIENTO*/
        var droptratamiento = document.getElementById("droptratamiento");
        var arregloTratamiento = new Array();
        for (var i = 0; i < droptratamiento.length; i++) {
            opt = droptratamiento[i];
            arregloTratamiento.push({ idtratamiento: opt.value });
        }
    }


    $(function () {
        $('.map').maphilight({
            fillColor: '008800'
        });
        $('#hilightlink').mouseover(function (e) {
            $('#square2').mouseover();
        }).mouseout(function (e) {
            $('#square2').mouseout();
        }).click(function (e) { e.preventDefault(); });
        $('#starlink').click(function (e) {
            e.preventDefault();
            var data = $('#star').data('maphilight') || {};
            data.neverOn = !data.neverOn;
            $('#star').data('maphilight', data);
        });
        $('#star,#starlink2').click(function (e) {
            e.preventDefault();
            var data = $('#star').mouseout().data('maphilight') || {};
            data.alwaysOn = !data.alwaysOn;
            $('#star').data('maphilight', data).trigger('alwaysOn.maphilight');
        });
    });

</script>
	<script type="text/javascript">
	    function consultaiterativa() {
//	        var numeropacientes = document.getElementById('lblnumeropacientes').innerHTML;

//	        $.ajax({
//	            type: "POST",
//	            url: "../WSConsultamedica.asmx/ConsultaPacientesEnespera",
//	            data: '{contador:' + numeropacientes + ',usuario:"' + document.getElementById('idusuario').innerHTML + '"}',
//	            contentType: "application/json; charset=utf-8",
//	            dataType: "json",
//	            success: consultadinamicasi,
//	            error: consultadinamicano
	        //	        });
	        creapacientesenespera();
	    }

	    function consultadinamicasi(msg) {
	        var numeropacientes = document.getElementById('lblnumeropacientes');
	        if (parseInt(msg.d) > parseInt(numeropacientes.innerHTML)) {
	            numeropacientes.innerHTML = msg.d;
	            creapacientesenespera();
	        }
	    }

	    function consultadinamicano(msg) {
	        mensajeerror(msg.responseText);
	    }

	    function creapacientesenespera() {
	        var cadenabusqueda = "";
        //ESTO FUNCIONABA PARA QUE SOLO FUERA MOSTRANDO LOS PACIENTES QUE SE LES ACABA DE ASIGNAR CITA, AHORA VA A TRAER TODOS
//	        $("#divpacientes .styleface").each(function (index) {
//	            var identificador = $(this).attr("id");
//	            cadenabusqueda = cadenabusqueda + ',' + identificador;
//	        });

	        $.ajax({
	            type: "POST",
	            url: "../WSConsultamedica.asmx/ConsultaCreaPacientes",
	            data: '{cadenabusqueda:"' + cadenabusqueda + '",usuario: "' + document.getElementById('idusuario').innerHTML + '"}',
	            contentType: "application/json; charset=utf-8",
	            dataType: "json",
	            success: creapacientessi,
	            error: creapacientesno
	        });
	    }

	    function creapacientessi(msg) {
	        $("#divpacientes").html("");
	        var html = '';
	        var n = 0;
	        $.each(msg.d, function () {

	            html += '<tr><td>'+this.correlativo+'</td><td><input type="button" value="' + this.nombrepaciente + '" id="' + this.idpaciente + '" class="styleface"/></td></tr>';
	            n++;
	        });
	        $("#divpacientes").append(html);
	        document.getElementById('lblnumeropacientes').innerHTML = n;
	    }

	    function creapacientesno(msg) { mensajeerror(msg.responseText); }

	    function determinamedico() {
	        $.ajax({
	            type: "POST",
	            url: "../WSPaciente.asmx/DeterminaMedico",
	            data: '{idusuario: "' + document.getElementById('idusuario').innerHTML + '"}',
	            contentType: "application/json; charset=utf-8",
	            dataType: "json",
	            success: medicosi,
	            error: medicono
	        });
	    }

        function medicosi(msg){}

        function medicono(msg) { }

        function mostrarmodal(control) {
            $.fancybox.open("#" + control);
        }


        //##############NUEVA FORMA DE MOSTRAR EL HISTORIAL#################
        function mostrarhistorialnuevo(valor) {
            var pacienteactivo = document.getElementById('pacienteactivo');

            if (!pacienteactivo.value)
                mensajeadvertencia("No ha seleccionado un paciente");
            else {
                $.ajax({
                    type: "POST",
                    url: "../WSConsultamedica.asmx/ConsultaHistorialNuevo",
                    data: '{idpaciente: "' + pacienteactivo.value + '",tipoconsulta: "' + valor + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: historialNuevosi,
                    error: historialno
                });
            }
        }
        function historialNuevosi(msg) {
            
            var htmlindicacion ='';
            var htmlmedicamento = '';
            var htmlsintoma = '';
            var htmlfooter = '</table></div></article>';
            var n = 0;
            $.each(msg.d, function () {
                //#### INDICACIONES ####//
                htmlindicacion = '<article class="module width_full">' +
                '<header><h3>Indicaciones</h3></header>' +
                '<div class="message_list_historial">' +
                '<table class="sorter" style="width:100%;" cellpadding="1" cellspacing="0">';
                $.each(this.indicacion, function () {
                    htmlindicacion += '<tr><td style="width:5%;"></td><td style="width:30%;">' +
                    '<strong>' + this.nombre + '</strong></td><td style="width:50%;">' + this.observaciones + '</td></tr>';

                });

                htmlindicacion += htmlfooter;

                //#### MEDICAMENTOS ####//
                htmlmedicamento = '<article class="module width_full">' +
                '<header><h3>Medicamentos</h3></header>' +
                '<div class="message_list_historial">' +
                '<table class="sorter" style="width:100%;" cellpadding="1" cellspacing="0">';
                $.each(this.medicamento, function () {
                    htmlmedicamento += '<tr><td style="width:5%;"></td><td style="width:30%;">' +
                    '<strong>' + this.nombre + '</strong></td><td style="width:50%;">' + this.observaciones + '</td></tr>';
                });

                htmlmedicamento += htmlfooter;

                //#### SINTOMAS ####//
                htmlsintoma = '<article class="module width_full">' +
                '<header><h3>Sintomas</h3></header>' +
                '<div class="message_list_historial">' +
                '<table class="sorter" style="width:100%;" cellpadding="1" cellspacing="0">';
                $.each(this.sintoma, function () {
                    htmlsintoma += '<tr><td style="width:5%;"></td><td style="width:30%;">' +
                    '<strong>' + this.nombre + '</strong></td><td style="width:50%;">' + this.observaciones + '</td></tr>';
                });

                htmlmedicamento += htmlfooter;


                jQuery('<div/>', {
                    id: 'filareceta' + n,
                    class: 'slide',
                    html: '<h3>'+ this.fecha +'</h3>'
                }).appendTo('#slidesContainer');

                $("#filareceta" + n).append(htmlsintoma);
                $("#filareceta" + n).append(htmlmedicamento);
                $("#filareceta" + n).append(htmlindicacion);
                
                mostrarmodal('historial');

                n++;

            });   

        }
        //##################################################################


        function mostrarhistorial(valor) {
            var pacienteactivo = document.getElementById('pacienteactivo');

            if (!pacienteactivo.value)
                mensajeadvertencia("No ha seleccionado un paciente");
            else {
                $.ajax({
                    type: "POST",
                    url: "../WSConsultamedica.asmx/ConsultaHistorial",
                    data: '{idpaciente: "' + pacienteactivo.value + '",tipoconsulta: "' + valor + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: historialsi,
                    error: historialno
                });
            }
        }
        function historialsi(msg) {
            $("#1fila").html("");
            $("#2fila").html("");
            $("#3fila").html("");
            
            var contenidosintoma = "";
            var encabezadosintoma="";
            var piesintoma="";
            var contenidotratamiento="";
            var encabezadotratamiento="";
            var pietratamiento = "";
            var contenidomedicamento="";
            var encabezadomedicamento = "";
            var piemedicamento = "";
            var codigohtmlsintoma = "";
            var codigohtmltratamiento = "";
            var codigohtmlmedicamento = "";
            
            var fecha = "";
            var contador = 0;
            var contadorpie = 0;
            $.each(msg.d, function () {

                /*SINTOMAS*/
                if (fecha != this.fecha) {
                    encabezadosintoma += '<article class="module width_quarter">'+
		        '<header><h3>PACIENTES EN ESPERA</h3></header>'+
                '<div class="message_list">'+
            '<div class="divsintomas" style="float:left;width:200px;">' +
                    '<table class="bordered">' +
                    '<thead>' +
                    '<tr><th>SINTOMAS '+ this.fecha +'</th></tr>' +
                    '</thead><tr>';
                    contenidosintoma = '<td>' + this.sintoma + '</td></tr>';

                    encabezadotratamiento += '<div class="divtratamiento" style="float:left;width:200px;">' +
                    '<table class="bordered">' +
                    '<thead>' +
                    '<tr><th>TRATAMIENTOS ' + this.fecha + '</th></tr>' +
                    '</thead><tr>';
                    contenidotratamiento = '<td>' + this.idtratamiento + '</td></tr>';

                    encabezadomedicamento += '<div class="divmedicina" style="float:left;width:200px;">' +
                    '<table class="bordered">' +
                    '<thead>' +
                    '<tr><th>MEDICAMENTOS ' + this.fecha + ' </th></tr>' +
                    '</thead><tr>';
                    contenidomedicamento = '<td>' + this.idmedicina + '</td></tr>';

                } else {
                    encabezadosintoma = "";
                    contenidosintoma = '<tr><td>' + this.idtratamiento + '</td></tr>';

                    encabezadotratamiento = "";
                    contenidotratamiento = '<tr><td>' + this.idtratamiento + '</td></tr>';

                    encabezadomedicamento = "";
                    contenidomedicamento = '<tr><td>' + this.idmedicina + '</td></tr>';

                    contador++;

                }
                if (contador > 0 && fecha != this.fecha) {
                    piesintoma = '</table></div>';
                    pietratamiento = '</table></div>';
                    piemedicamento = '</table></div>';

                }
                else {
                    piesintoma = "";
                    pietratamiento = "";
                    piemedicamento = "";
                }

                codigohtmlsintoma += piesintoma + encabezadosintoma + contenidosintoma;
                codigohtmltratamiento += pietratamiento + encabezadotratamiento + contenidotratamiento;
                codigohtmlmedicamento += piemedicamento + encabezadomedicamento + contenidomedicamento;
                fecha = this.fecha;

            });

            piesintoma = '</table></div></div></article>';
            codigohtmlsintoma += piesintoma;

            pietratamiento = '</table></div></div></article>';
            codigohtmltratamiento += pietratamiento;

            piemedicamento = '</table></div></div></article>';
            codigohtmlmedicamento += piemedicamento;
            
            $("#1fila").append(codigohtmlsintoma);
            $("#2fila").append(codigohtmltratamiento);
            $("#3fila").append(codigohtmlmedicamento);
            
            mostrarmodal('historial');

        }

        function historialno(msg) { }

        /*INICIO DEL DOCUMENT READY*/

        $(document).ready(function () {



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

                cerrarmodal("divagregarobservaciones");

            });

            $(".subparte").live('click', function (e) {

                var id = $(this).attr("id");

                var idparte = id.substring(5, id.length);
                var determinaaccion = id.substring(0, 4);

                var sexo = document.getElementById('genero').innerHTML;

                if (determinaaccion == "papa" || determinaaccion == "hijo") cargasintomas(idparte);

                if (determinaaccion == "hijo") {
                    $('#divsubpartes').hide();
                }
                else {
                    $.ajax({
                        type: "POST",
                        url: "../WSConsultamedica.asmx/cargaSubPartes",
                        data: '{idparte: "' + idparte + '",sexo: "' + sexo + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {

                            var html = '<h1>SUBPARTES</h1>';
                            var contador = 0;
                            var id = '';

                            $.each(msg.d, function () {

                                if (parseFloat(this.hijos) > 0) id = 'papa';
                                else id = 'hijo';

                                html = html + '<a href="#" id="' + id + contador + this.idsubparte + '" class="subparte" >' + this.nombre + '</a><br/>';
                                contador++;

                            });

                            //$('#divsubpartes').hide();
                            e.preventDefault();

                            //$('#questionMarkId').html('');

                            $('#detalledepartes').html(html);

                            $('#divsubpartes').css('position', 'absolute');
                            $('#divsubpartes').css('top', e.pageY - 100);
                            $('#divsubpartes').css('left', e.pageX);
                            $('#divsubpartes').show();

                        },
                        error: function (msg) { mensajeerror(msg.responseText); }
                    });
                }
            });

            /*BOTON HISTORIAL DEL PACIENTE*/
            $("#btnhistorial").click(function () {
                mostrarhistorial(1);
            });

            /*BOTON HISTORIAL DEL PACIENTE*/
            $("#btnultimareceta").click(function () {
                mostrarhistorialnuevo(2);
                //mostrarmodal('historial');
            });

            $("#btnultimareceta1").click(function () {
                mostrarmodal('historial');
            });


            /*BOTON GUARDAR*/

            $("#btnguardar").click(function () {
                var pacienteactivo = document.getElementById('pacienteactivo');
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

                    $.ajax({
                        type: "POST",
                        url: "../WSConsultamedica.asmx/RecetaGuardar",
                        data: "{'arregloSintoma':" + JSON.stringify(arregloSintoma) + ",'arregloMedicina':" + JSON.stringify(arregloMedicina) + ",'arregloTratamiento':" + JSON.stringify(arregloTratamiento) + ",'usuario':" + usuario + ",'idpaciente':" + pacienteactivo.value + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: guardarecetasi,
                        error: guardarecetano
                    });
                }
            });

            function guardarecetasi(msg) {
                var mensaje = msg.d;
                if (mensaje.substring(0, 5) == "ERROR") {
                    mensajeadvertencia(mensaje);
                }
                else {
                    mensajecorrecto(mensaje);
                    document.getElementById('cuerpomasculino').style.display = 'none';
                    //LIMPIAMOS LOS SELECTS JQUERYUI

                    $("#dropsintomasui").multiselect("uncheckAll");
                    //LIMPIAMOS LOS SELECT NORMALES
                    $("#droptratamiento").html("");
                    $("#detalleindicacionesagregados").html("");
                    $("#detallemedicamentosagregados").html("");
                    $("#detallesintomasagregados").html("");


                    var pacientesesperando = document.getElementById('lblnumeropacientes');
                    pacientesesperando.innerHTML = parseInt(pacientesesperando.innerHTML) - 1;
                    var pacienteactivo = document.getElementById('pacienteactivo');
                    document.getElementById('foto').src = "../privado/fotos/unknow.jpg";
                    document.getElementById(pacienteactivo.value).style.display = 'none';
                    pacienteactivo.value = "";

                    document.getElementById('nombre').innerHTML = "";
                    document.getElementById('direccion').innerHTML = "";
                    document.getElementById('telefono').innerHTML = "";
                    document.getElementById('edad').innerHTML = "";
                    document.getElementById('estadocivil').innerHTML = "";
                    document.getElementById('hijos').innerHTML = "";
                    document.getElementById('alergias').innerHTML = "";
                    document.getElementById('operaciones').innerHTML = "";
                    document.getElementById('recomendado').innerHTML = "";

                }
            }

            function guardarecetano(msg) {
                mensajeerror(msg.responseText);
            }

            consultaiterativa();

            $(function () {
                $("#txtbusquedatratamiento").autocomplete({
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
                    select: seleccionaTratamiento
                });

                $("#txtbusquedamedicamento").autocomplete({
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

            });


            function seleccionaTratamiento(event, ui) {
                var idtratamiento = ui.item.value;
                setTimeout('document.getElementById("txtbusquedatratamiento").value="";', 500);
                var existencia = ui.item.existencia;
                console.log(existencia);

                var existe = existeelemento('detalleindicacionesagregados', idtratamiento);

                if (existe == false) {
                    var html = '';
                    html += '<tr id="filaindicacion' + filasindicaciones + '">' +
                            '<td id="obsindicacion' + filasindicaciones + '" style="display:none;"></td>' +
   					        '<td><img id="indiobs' + filasindicaciones + '" src="../images/icn_new_article.png" onclick="agregarobservaciones(this.id);" style="cursor:pointer;" title="Agregar observaciones"/></td>' +
    				        '<td style="color:Green;font-weight:bolder;">' + idtratamiento + '</td>' +
    				        '<td><img id="indi' + filasindicaciones + '" onclick="eliminafilaindicaciones(this.id);" style="cursor:pointer;" src="../images/icn_trash.png" title="Eliminar"/></td>' +
				       '</tr>';
                    $("#detalleindicacionesagregados").append(html);
                    filasindicaciones++;
                }
            }

            function seleccionaMedicamento(event, ui) {
                var existencia = ui.item.existencia;
                console.log(existencia);
                var idtratamiento = ui.item.value;
                setTimeout('document.getElementById("txtbusquedamedicamento").value="";', 500);
                var existe = existeelemento('detallemedicamentosagregados', idtratamiento);
                var estilo = (existencia > 0) ? 'style="color:Green;font-weight:bolder;"' : 'style="color:red;font-weight:bolder;"'; 
                if (existe == false) {
                    var html = '';
                    html += '<tr id="filamedicamento' + filasmedicamentos + '">' +
                            '<td id="obsmedicamento' + filasmedicamentos + '" style="display:none;"></td>' +
   					        '<td><img id="mediobs' + filasmedicamentos + '" src="../images/icn_new_article.png" onclick="agregarobservaciones(this.id);" style="cursor:pointer;" title="Agregar observaciones"/></td>' +
    				        '<td '+ estilo +'>' + idtratamiento + '</td>' +
    				        '<td><img id="medi' + filasmedicamentos + '" onclick="eliminafilamedicamentos(this.id);" src="../images/icn_trash.png" style="cursor:pointer;" title="Eliminar"/></td>' +
				       '</tr>';
                    $("#detallemedicamentosagregados").append(html);
                    filasmedicamentos++;
                }
            }

            /*ANCHO DE LOS MULTISELECTS, EN ESTE CASO SOLO APLICA A DROPSINTOMASUI*/


            $('.ui-multiselect').css('width', '100%');
            var div_ancho = $("#articlesintomas .ui-multiselect").width();
            $('.ui-multiselect-menu').css('width', div_ancho);

            /*EJECUCION DEL EVENTO CLICK DE LOS PACIENTES QUE ESTAN EN ESPERA*/
            $("#btntratamiento").click(function () {

                document.getElementById('txtbusquedatratamiento').value = "";
                document.getElementById('txtbusquedatratamiento').focus();

            });

            $("#btnversintomasagregados").click(function () {
                mostrarmodal('divsintomasagregados');
            });

            $("#btnverindicacionesagregados").click(function () {
                mostrarmodal('divindicacionesagregados');
            });

            $("#btnvermedicamentosagregados").click(function () {
                mostrarmodal('divmedicamentosagregados');
            });

            $("#btnmasculino").live("click", function () {
                document.getElementById('divgenero').style.display = 'none';

                var pacienteactivo = document.getElementById('pacienteactivo').value;
                if (!pacienteactivo) {
                    mensajeadvertencia("No ha seleccionado un paciente");
                }
                else {
                    var genero = 'M';
                    $.ajax({
                        type: "POST",
                        url: "../WSPaciente.asmx/PacienteGenero",
                        data: '{idpaciente: "' + pacienteactivo + '",genero: "' + genero + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: generomsi,
                        error: generono
                    });
                }
            });

            $("#btnfemenino").live("click", function () {
                document.getElementById('divgenero').style.display = 'none';
                document.getElementById('cuerpomasculino').style.display = 'none';

                var pacienteactivo = document.getElementById('pacienteactivo').value;
                if (!pacienteactivo) {
                    mensajeadvertencia("No ha seleccionado un paciente");
                }
                else {
                    var genero = "F";
                    $.ajax({
                        type: "POST",
                        url: "../WSPaciente.asmx/PacienteGenero",
                        data: '{idpaciente: "' + pacienteactivo + '",genero: "' + genero + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: generofsi,
                        error: generono
                    });
                }
            });

            function generomsi(msg) {
                mensajecorrecto(msg.d);
                $("#cuerpomasculino").fadeIn(750);
                document.getElementById('genero').innerHTML = 'M';
            }

            function generofsi(msg) {
                mensajecorrecto(msg.d);
                document.getElementById('genero').innerHTML = 'F';
                $("#cuerpomasculino").fadeIn(750);

            }

            function generono(msg) {
                mensajeerror(msg.responseText);
            }

            /*EJECUCION DEL EVENTO CLICK DE LOS PACIENTES QUE ESTAN EN ESPERA*/
            $(".styleface").live("click", function () {

                var idpaciente = $(this).attr('id');
                document.getElementById('pacienteactivo').value = idpaciente;
                $.ajax({
                    type: "POST",
                    url: "../WSPaciente.asmx/PacienteBuscar",
                    data: '{idpaciente: "' + parseInt(idpaciente) + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: muestrapacientesi,
                    error: muestrapacienteno
                });
            });

            function muestrapacientesi(msg) {


                $("#slidesContainer").html("");
                $.each(msg.d, function () {

                    document.getElementById('nombre').innerHTML = this.nombre + " " + this.apellido;
                    document.getElementById('direccion').innerHTML = this.direccion;
                    document.getElementById('telefono').innerHTML = this.telefono;
                    document.getElementById('estadocivil').innerHTML = this.estado;
                    document.getElementById('edad').innerHTML = this.edad;
                    document.getElementById('hijos').innerHTML = this.nhijos;
                    document.getElementById('alergias').innerHTML = this.alergias;
                    document.getElementById('operaciones').innerHTML = this.operaciones;
                    document.getElementById('recomendado').innerHTML = this.recomendado;

                    var imagen = this.foto;
                    if (imagen == "") document.getElementById('foto').src = "../privado/fotos/unknow.jpg";
                    else document.getElementById('foto').src = "../privado/fotos/" + imagen;
                    if (this.genero == "unknown" || this.genero == " ") {
                        document.getElementById('genero').innerHTML = "";
                        $("#divgenero").fadeIn(250);
                        document.getElementById('cuerpomasculino').style.display = 'none';
                    }
                    else if (this.genero == "M") {
                        document.getElementById('genero').innerHTML = "M";

                        $("#cuerpomasculino").fadeIn(250);
                        document.getElementById('divgenero').style.display = 'none';
                    }
                    else if (this.genero == "F") {
                        document.getElementById('genero').innerHTML = "F";

                        $("#cuerpomasculino").fadeIn(250);
                        document.getElementById('divgenero').style.display = 'none';

                    }
                });
                document.getElementById('dropsintomas').focus();
            }

            function muestrapacienteno(msg) {
                mensajeerror(msg.responseText);
            }

            /*FIN DEL EVENTO CLICK DE LOS PACIENTES EN ESPERA*/

            setInterval("consultaiterativa()", 60000);

        });

        //NUEVA CONSULTA DE LAS SUBPARTES DE UNA PARTE DEL CUERPO, EJEMPLO CABEZA: OJOS,CABELLO,OIDOS,NARIZ,BOCA,ETC.    
        function cargaSubPartes(idparte) {
            var sexo = document.getElementById('genero').innerHTML;
	        
            $.ajax({
                type: "POST",
                url: "../WSConsultamedica.asmx/cargaSubPartes",
                data: '{idparte: "' + idparte + '",sexo:"'+ sexo +'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: subpartesi,
                error: subparteno
            });
        }

        function subpartesi(msg) {
            var html = '';
            $.each(msg.d, function () {
                html=html+'<a href="#" id="'+ this.id +'" onclick="muestrasintomas('+ this.idsubpartedelcuerpo +')>'+this.nombre+'</a><br/>';
            });

            $('#questionMarkId').hide();
            e.preventDefault();
            
            $('#questionMarkId').html('');
            
            $('#questionMarkId').html(html);
            
            $('#questionMarkId').css('position', 'absolute');
            $('#questionMarkId').css('top', e.pageY);
            $('#questionMarkId').css('left', e.pageX);
            $('#questionMarkId').show();

        }

        function subparteno(msg) { mensajeerror(msg.responseText); }

        //FIN DE NUEVA CONSULTA DE LAS SUBPARTES//


	    function cargasintomas(valor) {
	        var sexo = document.getElementById('genero').innerHTML;
	        $('.active-header').toggleClass('active-header').toggleClass('inactive-header').next().slideToggle().toggleClass('open-content');
	        $('.accordion-header:eq(0)').toggleClass('active-header').toggleClass('inactive-header');
	        $('.accordion-header:eq(0)').next().slideToggle().toggleClass('open-content');

	        $.ajax({
	            type: "POST",
	            url: "../WSConsultamedica.asmx/ConsultaDatos",
	            data: '{idsubpartedelcuerpo: "' + valor + '",sexo: "' + sexo + '"}',
	            contentType: "application/json; charset=utf-8",
	            dataType: "json",
	            success: datossi,
	            error: datosno
	        });
	    }

	    function datossi(msg) {
	        $("#dropsintomasui").html("");
	        var el = $("#dropsintomasui").multiselect();
	        $.each(msg.d, function () {

	            var valor = this.idsintoma;
	            var texto = this.descripcion;
	            var opt = $('<option />', { value: valor, text: texto });
	            opt.appendTo(el);
	        });

	        el.multiselect('refresh');
	        $('#divsubpartes').hide();
	        console.log("fin del que carga los sintomas");

	        $('.ui-multiselect').css('width', '100%');
	        var div_ancho = $("#articlesintomas .ui-multiselect").width();
	        $('.ui-multiselect-menu').css('width', div_ancho);

	    }

	    function datosno(msg) {
	        mensajeerror(msg.responseText);
        }

	  
	    function tratamientoerror(msg) {
	        mensajeerror(msg.responseText);
        }
    

	    //PARA CARGAR MEDICINA 	        
	    function cargamedicina(valor) {

	        $.ajax({
	            type: "POST",
	            url: "../WSMedicina.asmx/MedicinaDatos",
	            data: '{}',
	            contentType: "application/json; charset=utf-8",
	            dataType: "json",
	            success: medicinasi,
	            error: medicinano
	        });

	    }

	    function medicinasi(msg) {
	        $("#dropmedicinaui").html("");
	        var el = $("#dropmedicinaui").multiselect();
	        $.each(msg.d, function () {
	            var valor = this.idmedicina;
	            var texto = this.Descripcion;
	            var opt = $('<option />', { value: valor, text: texto });
	            opt.appendTo(el);
	        });

	        el.multiselect('refresh');

	    }

	    function medicinano(msg) {
	        mensajeerror(msg.responseText);
        }

        function existeelemento(tabla, valor) {
            var control = false;
            $('#' + tabla + ' tr').each(function (index) {
                $(this).children("td").each(function (index2) {
                    switch (index2) {
                        case 2:
                            if (valor == $(this).text()) {
                                
                                control = true;
                                console.log("valor control "+control);
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

            observacion.value= document.getElementById(origen).innerHTML;
            observacion.focus();
        }

        function cerrarmodal(control) {
            $.fancybox.close("#" + control)
        }

</script>


</head>


<body>
<form id="formulario" runat="server">
	<header id="header">
		<hgroup>
			<h1 class="site_title"><a href="menu.aspx">BIO * NATURAL Online</a></h1>
			<h2 class="section_title">BIO * NATURAL</h2><div class="btn_view_site"></div>          
            <input type="hidden" id="pacienteactivo" value="" />		
        </hgroup>
	</header> 
	
	<section id="secondary_bar">
		<div class="user">
			<p>Usuario Conectado: <asp:Label Text="" style="text-transform:uppercase;color:#04B404;text-shadow: 1px 1px white; font-weight:bolder;" ID="idusuario" runat="server"></asp:Label></p>
			<!-- <a class="logout_user" href="#" title="Logout">Logout</a> -->
		</div>
		<div class="breadcrumbs_container">
			<article class="breadcrumbs"><a href="menu.aspx">Regresar a menu</a> <div class="breadcrumb_divider"></div> <a class="current">Consulta</a>
            </article>
		</div>
	</section>
	
    <!--BARRA LATERAL, DONDE SE MUESTRA EL CUERPO HUMANO-->    
	<aside id="sidebar" class="column">
		
            <div id="divsubpartes" class="sexy-tooltip" style="z-index: 70000; display:none; width: 200px;">
    
    
    <div class="wiki">
        <div class="tooltip-tl" style="width: 200px;">
            <div class="tooltip-tr">
                <div class="tooltip-t"></div>
            </div>
            
        </div>
        <div class="tooltip-l" style="width: 200px;">
            <div class="tooltip-r">
                <div class="tooltip-m" id="detalledepartes">
                
                    <p>Click para mostrar sintomas relacionados con el pecho.</p>
                </div>
            </div>
        </div>
        <div class="tooltip-bl" style="width: 200px;">
            <div class="tooltip-br">
                <div class="tooltip-b">
                    <div class="tooltip-l-arrow"></div>
                </div>
            </div>
        </div>
   </div>
   </div>  

        <!--DIV QUE SE MOSTRARA CUANDO EL PACIENTE NO TENGA DEFINIDO SU GENERO, ESTO POR EL DISEÑO ANTERIOR DE BD-->
        <div id="divgenero" style="display:none; margin-left:50px;" >
            <h3>Seleccione un genero</h3>
            <br />
                <input type="button" id="btnmasculino" class="youtube" value="Masculino" />
                <input type="button" id="btnfemenino" class="youtube" value="Femenino" />
            </div>
        
    <!--FIN DEL DIV DEL CUERPO MASCULINO-->
	<div id="cuerpomasculino" style="display:none;">
    
    <img src="../images/cuerpo.png" alt="varios" width="238" height="546" border="0" usemap="#Map" class="map" />

    <map name="Map" id="Map">
        <!--CABEZA-->
        <area shape="poly" id="parte1" class="subparte" coords="126,1,113,2,104,9,101,15,101,34,104,55,117,71,128,73,142,61,148,39,149,19,139,4" href="#" />
        <!--CUELLO-->  
        <area shape="poly" id="parte2" class="subparte" coords="106,59,106,58,106,70,94,78,78,84,88,89,97,93,110,93,118,95,122,96,126,101,131,96,136,93,143,93,148,93,157,91,164,87,168,85,142,73,142,68,143,61,132,71,129,74,123,73,116,71,107,59" href="#" />
        <!--HOMBROS-->  
        <area shape="poly" id="hombr3" class="subparte" coords="47,116,47,133,43,147,42,166,39,180,35,192,32,216,29,236,27,250,34,252,42,254,44,245,53,227,59,216,63,203,69,160,75,166,82,159,91,151,80,137,73,121,70,113,73,106,96,94,89,90,77,84,66,87,59,92,52,98,49,107" href="#" />
        <area shape="poly" id="hombo3" class="subparte" coords="198,250,204,249,208,248,205,238,204,223,204,200,204,188,199,172,198,152,195,135,195,127,197,118,196,107,192,98,189,92,183,87,176,85,167,85,175,100,178,106,175,119,173,132,169,140,165,147,157,150,168,155,172,158,173,197,176,211,183,224,187,238,190,247,197,251" href="#" />
        <!--Manos-->
        <area shape="poly" id="manoi3" class="subparte" coords="17,264,16,269,13,275,9,278,12,281,16,280,14,285,13,300,13,308,18,303,19,311,23,311,29,311,31,303,34,305,37,293,42,282,44,270,43,263,41,258,41,254,27,250,23,255,18,263" href="#" />
        <area shape="poly" id="manod3" class="subparte" coords="215,276,219,278,226,279,227,274,219,270,219,264,208,248,201,250,195,249,190,247,190,257,189,263,189,275,190,281,193,291,197,301,200,302,203,309,206,309,212,306,217,302,216,279" href="#" />
        <!--TORAX-->
        <area shape="poly" id="torax4" class="subparte" coords="98,91,90,94,83,98,77,103,71,111,71,121,73,132,77,144,84,150,93,155,101,153,111,149,121,147,130,147,140,150,151,151,161,147,167,140,172,132,173,125,175,116,177,109,176,102,171,97,169,94,165,90,163,87,159,89,155,91,148,92,141,94,136,96,133,96,129,102,127,98,124,96,121,94,117,94,113,94,109,93,105,93,103,94," href="#" alt="" title=""   />
        <!--ABDOMEN-->
        <area shape="poly" id="abdom5" class="subparte" coords="126,146,123,146,121,147,115,148,110,150,106,152,101,154,97,158,94,162,89,162,86,163,82,164,79,164,77,164,74,165,77,172,82,178,83,183,83,190,83,195,82,201,81,207,81,214,80,222,87,226,94,230,101,232,108,234,115,234,123,235,131,236,137,236,143,234,150,234,155,231,160,230,163,228,163,224,163,218,163,212,163,208,163,202,163,198,163,194,163,188,163,184,165,180,165,176,165,170,167,164,165,160,161,158,158,155,155,154,151,152,147,151,144,150,141,150,137,150,133,148," alt="" title=""   />
        <!--GENITALES-->
        <area shape="poly" id="parte6" class="subparte" coords="92,233,99,244,104,256,109,266,114,276,117,283,121,283,129,275,136,265,143,257,148,248,155,241,160,235,165,228,165,220,151,227,144,229,133,229,120,229,107,228,98,225,90,223,79,220,86,229,93,234" href="#" />
        <!--CIATICO-->
        <area shape="poly" id="parte7" class="subparte" coords="82,403,80,417,80,430,80,442,82,449,87,461,91,476,96,495,114,488,115,481,117,474,114,466,115,447,115,432,111,411,111,401,115,374,107,378,106,367,106,355,94,362,91,376,83,367,78,355,82,378,83,393" href="#" />
        <area shape="poly" id="piern7" class="subparte" coords="136,412,133,425,132,435,133,447,135,458,139,485,141,497,151,499,158,501,162,477,165,457,167,439,167,422,163,404,163,387,166,365,157,373,156,357,151,361,145,357,144,382,137,384,130,378,131,393,136,411" href="#" />
        <area shape="poly" id="etrpi7" class="subparte" coords="78,354,83,367,91,377,93,363,100,359,106,353,106,367,107,378,115,374,115,359,115,342,118,321,119,305,119,290,112,271,104,255,97,240,88,231,80,221,76,233,73,249,69,266,69,286,69,304,74,335" href="#" />
        <area shape="poly" id="etrpd7" class="subparte" coords="173,329,175,308,174,287,172,269,168,253,166,244,166,229,153,243,145,255,137,265,129,275,121,284,122,313,124,326,127,339,128,356,129,370,129,378,136,383,143,384,144,366,144,355,149,362,156,356,157,373,167,363,168,352" href="#" />
        <!--PIES-->
        <area shape="poly" id="pieiz8" class="subparte" coords="139,511,139,522,139,528,136,534,138,541,146,542,166,543,175,544,175,541,172,537,158,521,157,511,157,501,146,498,140,494" href="#" />
        <area shape="poly" id="piede8" class="subparte" coords="97,529,90,537,91,543,106,544,111,544,119,540,118,521,117,508,114,500,114,489,95,496" href="#" />
    </map>

    </div>

	</aside><!-- end of sidebar -->
	
	<section id="main" class="column">
				 
			<!--PANEL DE SINTOMAS-->
			    <article id="articlesintomas" class="module width_quarter">
                <header><h3>SINTOMAS</h3></header>
                    <p id="sintomaactual" class="first-p"></p>      
                    <select  id="dropsintomasui" title="Seleccione sintomas" multiple="multiple" name="example-basic" size="1" >
                    </select>            
                    <select id="dropsintomas" multiple="multiple" class="select" style="display:none;"></select><br />
                    <div style="min-height:11px;"></div>
                    
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
                    
                      
                   <!-- <footer>
                    <div class="submit_linkizquierda">
                        <input type="button" id="btnversintomasagregados" class="youtube" value="Ver"/>
                    </div>
                    </footer>-->		    
                </article>
			<!--FIN PANEL DE SINTOMAS-->

            <!--PANEL DE TRATAMIENTOS -->
				<article class="module width_quarter">    
                    <header><h3>INDICACIONES</h3></header>
                    <p class="first-p"></p>
				        &nbsp;
				        <input type="text" id="txtbusquedatratamiento" style="width:80%;" class="texto" />	    
                        <select id="droptratamiento" multiple="multiple" style="display:none;" class="select"></select>
                        <br />
                        <br />


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

                        <!--<footer>
                          <div class="submit_linkizquierda">
                            <input type="button" id="btnverindicacionesagregados" class="youtube" value="Ver"/>
                          </div>
                        </footer>-->
                </article>
			<!--FIN PANEL DE TRATAMIENTOS-->

            <!--PANEL DE MEDICINA-->
			    <article class="module width_quarter">				    
                    <header><h3>MEDICAMENTOS</h3></header>
                    <p class="first-p"></p>
				    &nbsp;
				    <input type="text" id="txtbusquedamedicamento" style="width:80%;" class="texto" />	                        
                    <!--<select id="dropmedicinaui" title="Seleccione Tratamientos" multiple="multiple" style="display:none; height:50%;" name="example-basic" size="8" ></select>-->
                    <select id="dropmedicina" multiple="multiple" class="select" style="display:none;"></select>
			        <br/>
                    <br/>

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

                    <!--<footer>
                    <div class="submit_linkizquierda">
                        <input type="button" id="btnvermedicamentosagregados" class="youtube" value="Ver"/>
                    </div>
                    </footer>-->
                </article>
			<!--FIN PANEL DE MEDICINA-->
            
            <!--PANEL DE OBSERVACIONES-->
				<div style="display:none;">
				    
                    <p class="first-p">Escriba los aspectos detectados durante la consulta</p>                    
                    <div style="height:150px">
						<fieldset style="width:100%; float:left; margin-right: 3%;"> <!-- to make two field float next to one another, adjust values accordingly -->
							<label>Observaciones</label>
							<textarea id="txtobservaciones" rows="6" cols="1"  style="width:92%;"></textarea>
						</fieldset>
                    </div>
			    </div>
            <!--FIN PANEL DE OBSERVACIONES-->
		
		<!--End Accordion-->

        <!--FOOTER
		<footer>
			
            <div class="submit_linkizquierda">
			    		<input type="button" id="btnguardar" class="youtube" value="Guardar"/>
                        <input type="button" id="btnactualizar" class="youtube" value="Actualizar" style="display:none;"/>
                        <input type="button" id="btneliminar" class="youtube" value="Eliminar" style="display:none;" />
                        <input type="button" id="btncancelar" class="youtube" value="Cancelar" style="display:none;"/>
                        
			</div>
                
            <div class="submit_link">

			</div>

		</footer>-->
        <!--FIN FOOTER-->

        <article class="module width_quarter" >
		<header><h3>PACIENTES EN ESPERA</h3></header>

        <div class="message_list">

                <table class="tablesorter" cellspacing="0">
               <!-- <thead><tr><th>#</th><th>Paciente</th></tr></thead>-->
                <tbody id="divpacientes"></tbody>
                </table>

        </div>

            <!--<p style="padding:5px 5px 5px 15px;">Listado de los pacientes que se encuentran esperando su turno.</p>
            <div id="divpacientes" style="padding:10px 10px 10px 10px; width:100%; height:100px;">
      
            </div>-->
			<footer>
                <div class="submit_linkizquierda">
                    <input type="button" id="btnguardar" class="youtube" value="Guardar"/>
                </div>
			</footer>


		</article>

    <!--FIN izquierda del main principal -->
    
    <!--div datos del paciente que esta siendo atendido-->
        <article class="module width_3_quarter">
		<header><h3>PACIENTE ACTUAL</h3></header>

			<table class="tablesorter" cellspacing="0"> 
			<tbody> 
				<tr> 
   					<td><strong>NOMBRE</strong></td> 
    				<td id="nombre" colspan="5" style="color:green; font-weight:bolder;"></td> 
    				<td rowspan="4" colspan="2" align="center" valign="middle"><img id="foto" src="../privado/fotos/unknow.jpg" style="width:100px;" alt="Foto paciente" /></td>     				
				</tr> 
				<tr> 
    				<td><strong>ESTADO CIVIL</strong></td> 
    				<td id="estadocivil" colspan="2" style="color:green; font-weight:bolder;"></td>  
                    <td><strong>TELEFONO</strong></td> 
    				<td id="telefono" colspan="2" style="color:green; font-weight:bolder;"></td>
				</tr>
				<tr> 
   					<td><strong>OPERACIONES</strong></td> 
    				<td id="operaciones" colspan="2" style="color:green; font-weight:bolder;"></td> 				
                    <td><strong>RECOMENDADO POR</strong></td> 
    				<td id="recomendado" colspan="4" style="color:green; font-weight:bolder;"></td> 				
				</tr> 
   				<tr>
                    <td><strong>DIRECCION</strong></td> 
    				<td id="direccion" colspan="5" style="color:green; font-weight:bolder;"></td> 
				</tr>
				<tr> 
                    <td><strong>GENERO</strong></td> 
    				<td id="genero" align="left" style="color:green; font-weight:bolder;"></td>   					
                    <td><strong>EDAD</strong></td> 
    				<td id="edad" style="color:green; font-weight:bolder;"></td> 
                    <td><strong>HIJOS</strong></td> 
    				<td id="hijos" style="color:green; font-weight:bolder;"></td> 
    				<td><strong>ALERGIAS</strong></td> 
    				<td id="alergias" style="color:green; font-weight:bolder;"></td>   				 	
				</tr> 
                
			</tbody> 
			</table>


	    <footer>
				<div class="submit_linkizquierda">Pacientes <label id="lblnumeropacientes"> 0</label></div>
                <div class="submit_link">
                    <input type="button" class="youtube" id="btnultimareceta" value="Historial" /> 
                    
                 </div>
		</footer>

		</article>
        <!--FIN div datos del paciente que esta siendo atendido-->
		<div class="spacer"></div>
	</section>


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

    <article class="module width_full" id="divindicacionesagregados" style="display:none;margin-left:0px; width:500px;">
			            <header><h3 style="font-size:medium; font-weight:bolder; margin-left:10px;" class="tabs_involved">INDICACIONES</h3></header>
			            
    </article>

    <article class="module width_full" id="divmedicamentosagregados" style="display:none;margin-left:0px; width:500px;">
			            <header><h3 style="font-size:medium; font-weight:bolder; margin-left:10px;" class="tabs_involved">MEDICAMENTOS</h3></header>

    </article>

    <article class="module width_full" id="divsintomasagregados" style="display:none; margin-left:0px; width:500px;">
			            <header><h3 style="font-size:medium; font-weight:bolder; margin-left:10px;" class="tabs_involved">SINTOMAS</h3></header>
			            </article>
    
    <div id="historial" style="display:none;">

                <div class="divprev">
                      <a href="#siguiente" class="next" title="Siguiente"></a>
                </div>
                <div class="sliderContainer">


                    <div id="slider">
                        <div id="slidesContainer" class="slidesContainer">
                            <div class="slide" style="background:red;"></div>
                            <div class="slide" style="background:blue;"></div>
                            <div class="slide" style="background:yellow;"></div>
                            <div class="slide" style="background:orange;"></div>
                        </div>
                    </div>
                </div>

                <div class="divnext">
                <a href="#anterior" class="prev" title="Anterior"></a>
                </div>

    </div>

</form>
</body>

</html>
