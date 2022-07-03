var medSelec= 'leer';

window.onload=function()
{

    if(sessionStorage.getItem("seleccionado")!=null)
    {
        medSelec = sessionStorage.getItem("seleccionado");
        sessionStorage.clear();

        //alert(seleccionado);
    }
}

$(document).ready(
    function(){

    $('#calendarioweb').fullCalendar(
        {
            header:
            {
                left:'prev,today,Miboton',
                center:'title',
                right:'month,basicWeek,basicDay,agendaWeek,agendaDay,next'
            },
            /* customButtons:
            {
                Miboton:
                {
                    text:'Ejemplo1',
                    click:function()
                    {
                        alerta();
                    }
                },
            }, */
            eventLimit: true,
            dayClick:function(date, jsEvent, view)
            {
                var btnModificar = document.getElementById('btnModificar');
                btnModificar.style.display = "none";
                var btnEliminar = document.getElementById('btnEliminar');
                btnEliminar.style.display = "none";

                $('#txtFecha').val(date.format());
                var abm = document.getElementById('abm');
                abm.style.display = "block";
                abm.style.paddingRight = "17px";
                abm.className="modal fade show"; //show muestra el modal

                //$(this).css('background-color','#f77f50');

                //$("#calendarioModal").modal();
                
            },

            //consulta los turnos en la BD...
            events:traerTurnos(),

            /* eventSources:
            [{
                events:
                [{
                    id:1,
                    title:'Un nuevo evento',
                    descripcion:"Evento descriptivo!",
                    start:'2022-01-03',
                    color:"Blue",
                    textColor:"yellow"
                },
                {
                    title  : 'Otro evento',
                    start  : '2022-01-05',
                    end    : '2022-01-07'
                },
                {
                    title  : 'Otro evento mas',
                    start  : '2022-01-08T12:30:00',
                    allDay : false // will make the time show
                }],
                color:"black",
                textColor:"yellow"
            }], */
            eventClick:function(calEvent, jsEvent, view)
            {
                var btnAgregar = document.getElementById('btnAgregar');
                btnAgregar.style.display = "none";

                $('#tituloEvento').html(calEvent.title);
                $('#txtId').html(calEvent.id);
                $('#txtTitulo').val(calEvent.title);
                $('#txtPaciente').val(calEvent.idUsuario);
                $('#txtProfesional').val(calEvent.idDoctor);
                $('#txtDesc').val(calEvent.descripcion);
                $('#txtColor').val(calEvent.color);

                //formatear fecha...
                fechaHora = calEvent.start._i.split(" ");
                $('#txtFecha').val(fechaHora[0]);
                $('#txtHora').val(fechaHora[1]);                

                var locModal = document.getElementById('abm');
                locModal.style.display = "block";
                locModal.style.paddingRight = "17px";
                locModal.className="modal fade show";               
            },
            editable:true,
            eventDrop:function(calEvent)    
            {
                $('#txtId').html(calEvent.id);
                $('#txtTitulo').val(calEvent.title);
                $('#txtProfesional').val(calEvent.idDoctor);
                $('#txtDesc').val(calEvent.descripcion);
                $('#txtColor').val(calEvent.color);

                //formatear fecha...
                var fechaHora = calEvent.start.format().split(" ");
                $('#txtFecha').val(fechaHora[0]);
                $('#txtHora').val(fechaHora[1]);

                leerDatos();
                enviarInfo('modificar',NuevoEvento);
            }
        }
    );
}
);



function selectMed() 
{
      var listmed = document.getElementById("listmed");
      Select = listmed.options[listmed.selectedIndex].value;
      sessionStorage.setItem("seleccionado", Select);
      location.href = './?controller=turno&action=turno';
}

function selectTipo() 
{
      var listtipo = document.getElementById("listtipo");
      Select = listtipo.options[listtipo.selectedIndex].value;
      sessionStorage.setItem("seleccionado", Select);
      location.href = './?controller=turno&action=turno';
}

function traerTurnos()
{
    return './turnos.php?accion=' + medSelec;
}

function alerta()
{
    alert('ACA VAMOS de nuevo23');
}

var NuevoEvento;

$('#btnAgregar').click(function()
{
    var error = validateForm();
    if(!error)
    {
        leerDatos();
        enviarInfo('agregar',NuevoEvento);
    }   

});

$('#btnEliminar').click(function()
{
    swal({
        title : "MedTurno informa!",
        text : "Se eliminara definitivamente, seguro?",
        icon: "warning",
        buttons : true
    })
    .then((borrar) => 
    {
        if (borrar) 
        {
            leerDatos();
            enviarInfo('eliminar',NuevoEvento);
        }
    });

});

$('#btnModificar').click(function()
{
    var error = validateForm();
    if(!error)
    {
        leerDatos();
        enviarInfo('modificar',NuevoEvento);
    }

});

function leerDatos()
{
    NuevoEvento = 
    {
        id : $('#txtId').text(),
        title  : $('#txtTitulo').val(),
        profesional : $('#txtProfesional').val(),
        paciente : $('#txtPaciente').val(),
        start  : $('#txtFecha').val()+" "+$('#txtHora').val(),
        color : $('#txtColor').val(),
        descripcion  : $('#txtDesc').val(),
        textColor  : '#FFFFFF',
        end  : $('#txtFecha').val()+" "+$('#txtHora').val()
    };

    limpiarForm();
}

function enviarInfo(accion, objEvento)
{
    $.ajax({
        type : 'POST',
        url : 'turnos.php?accion='+accion,
        data : objEvento,
        success : function(msg)
        {
            if(msg)
            {
                $('#calendarioweb').fullCalendar('refetchEvents');
                var abm = document.getElementById('abm');
                abm.style.display = "";
                //alert('Turno agregado con Exito');
                if(accion == 'eliminar')
                {
                    swal({ text: "Se dio de baja con Exito!", });
                }
                if(accion == 'modificar')
                {
                    swal({ text: "Se modifico con Exito!", });
                }
                if(accion == 'agregar')
                {
                    swal({ text: "Se agrego con Exito!", });
                }
            }   
            else
            {
                alert(msg);
            }
        },
        error : function()
        {
            alert("ERROR: Ponte en contacto con Admin!");
        }
    });
}

$('#closeModal').click(function()
{
    var btnModificar = document.getElementById('btnModificar');
    btnModificar.style.display = "inline-block";
    var btnAgregar = document.getElementById('btnAgregar');
    btnAgregar.style.display = "inline-block";
    var btnEliminar = document.getElementById('btnEliminar');
    btnEliminar.style.display = "inline-block";

    limpiarForm();

    var abm = document.getElementById('abm');
    abm.style.display = "";
});

$('#closeModal2').click(function()
{
    var btnModificar = document.getElementById('btnModificar');
    btnModificar.style.display = "inline-block";
    var btnAgregar = document.getElementById('btnAgregar');
    btnAgregar.style.display = "inline-block";
    var btnEliminar = document.getElementById('btnEliminar');
    btnEliminar.style.display = "inline-block";    
    
    limpiarForm();

    var abm = document.getElementById('abm');
    abm.style.display = "";
});

$('.clockpicker').clockpicker();

function limpiarForm()
{
    $('#txtId').html('..');
    $('#tituloEvento').html('Nuevo Evento');
    $('#txtTitulo').val('');
    $('#txtProfesional').val('-1');
    $('#txtPaciente').val('-1');
    $('#txtDesc').val('');
    $('#txtColor').val('-1');
    $('#txtFecha').val('');
    $('#txtHora').val('00:00');
}

function validateForm()
{
    var error = false;
    var rango = /^(?!\s)[A-Za-z0-9\s]+$/;
    
    var title  = $('#txtTitulo').val()
    var descripcion  = $('#txtDesc').val()
    
    if(title == "")
    {
        error = true;
        alert('Ingresa un Titulo');
    }
    else if(!rango.test(title))
    {
        error = true;
        alert('Ingresa un Titulo sin signos(.,?!$%&/)');
    }

    if(descripcion == "")
    {
        error = true;
        alert('Ingresa una descripcion');
    }
    else if(!rango.test(descripcion))
    {
        error = true;
        alert('Ingresa una descripcion sin signos(.,?!$%&/)');
    }

    return error;       
}  
 