
function menuTogle()
{
    const togle = document.querySelector('.togle');
    togle.classList.toggle('active');
    const section = document.querySelector('section');
    section.classList.toggle('active');
}

//menu tog

document.querySelector('.tog').addEventListener("click", menu);
let nav = document.querySelector('.nav');
let main = document.querySelector('.main');

function menu()
{
    nav.classList.toggle('active');
    main.classList.toggle('active');
}

function validar()
{
    let id = (id) => document.getElementById(id);

    if(id("formulario").value == 'Doctor')
    {
        let nombre = id("nombre").value.trim();
        let matricula = id("matricula").value.trim();
        let horarioatencion = id("horarioatencion").value.trim();        

        let validoNombre = validacionGenericaTxt(nombre, id("nombre"));
        let validoMatricula = validacionGenericaTxt(matricula, id("matricula"));
        let validoHora = validacionGenericaTxt(horarioatencion, id("horarioatencion"));

        if(validoNombre && validoMatricula && validoHora)
        {
            document.getElementById( 'Enviar' ).style.display = '';
        }
        else
        {
            document.getElementById( 'Enviar' ).style.display = 'none';
        }
    }
    
    if(id("formulario").value == 'Especialidad')
    {
        let tipo = id("tipo").value.trim();
        let especialidad = id("especialidad").value.trim();

        let validoTipo = validacionGenericaTxt(tipo, id("tipo"));
        let validoEspecialidad = validacionGenericaTxt(especialidad, id("especialidad"));

        if(validoTipo && validoEspecialidad)
        {
            document.getElementById( 'Enviar' ).style.display = '';
        }
        else
        {
            document.getElementById( 'Enviar' ).style.display = 'none';
        }
    }
    
    if(id("formulario").value == 'OSocial')
    {
        let nombre = id("nombre").value.trim();
        let direccion = id("direccion").value.trim();
        let telefono = id("telefono").value.trim();

        let validoNombre = validacionGenericaTxt(nombre, id("nombre"));
        let validoDireccion = validacionGenericaTxt(direccion, id("direccion"));
        let validoTelefono = validacionGenericaNum(telefono, id("telefono"));

        if(validoNombre && validoDireccion && validoTelefono)
        {
            document.getElementById( 'Enviar' ).style.display = '';
        }
        else
        {
            document.getElementById( 'Enviar' ).style.display = 'none';
        }
    }
//fecha /^[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1])$/
    if(id("formulario").value == 'Usuario')
    {
        let dni = id("dni").value.trim();
        let nombre = id("nombre").value.trim();
        let Mail = id("Mail").value.trim();
        let telefono = id("telefono").value.trim();
        let direccion_calle = id("direccion_calle").value.trim();
        let direccion_numero = id("direccion_numero").value.trim();
        let direccion_ciudad = id("direccion_ciudad").value.trim();
        let password = id("password").value.trim();
        let pregunta = id("pregunta").value.trim();
        
        let validoDni = validacionGenericaNum(dni, id("dni"));
        let validoNombre = validacionGenericaTxt(nombre, id("nombre"));
        let validoMail = validacionGenericaEmail(Mail, id("Mail"));
        let validoTelefono = validacionGenericaNum(telefono, id("telefono"));
        
        let validoPass = validacionGenericaTxt(password, id("password"));
        let validoPregunta = validacionGenericaTxt(pregunta, id("pregunta"));

        if(validoDni && validoNombre && validoMail && validoTelefono && validoPass && validoPregunta)
        {
            if(direccion_calle != null && direccion_calle != '' && direccion_numero != null && direccion_numero != '' && direccion_ciudad != null && direccion_ciudad != '')
            {
                let validoDC = validacionGenericaTxt(direccion_calle, id("direccion_calle"));
                let validoDN = validacionGenericaNum(direccion_numero, id("direccion_numero"));
                let validoDCi = validacionGenericaTxt(direccion_ciudad, id("direccion_ciudad"));

                if(validoDC && validoDN && validoDCi)
                {
                    document.getElementById( 'Enviar' ).style.display = ''; 
                }
                else
                {
                    document.getElementById( 'Enviar' ).style.display = 'none';
                }
            }
            else
            {
               document.getElementById( 'Enviar' ).style.display = ''; 
            }
            
        }
        else
        {
            document.getElementById( 'Enviar' ).style.display = 'none';
        }
    }
    
}

function validacionGenericaTxt(values, id)
{
    let valido = true;
    let pat =  /^[A-ZÑa-zñ0-9:\s.-]+$/;
    let pat2 = /^[\s]+$/;

    if(values != null && values != '')
    {
        if(values.match(pat))
        {
            id.style.border = "";
            id.style.boxShadow = "";
            id.style.borderBottom = "1px solid #9b8282";
            //document.getElementById( 'Enviar' ).style.display = '';

            if(values.match(pat2))
            {
                id.style.border = "3px solid red";
                id.style.boxShadow = "0 10px 10px rgb(243 44 44)";
                //document.getElementById( 'Enviar' ).style.display = 'none';
                valido = false;
            }
        }
        else
        {
            id.style.border = "3px solid red";
            id.style.boxShadow = "0 10px 10px rgb(243 44 44)";
            //document.getElementById( 'Enviar' ).style.display = 'none';
            valido = false;
        }
    }
    else
    {
        //document.getElementById( 'Enviar' ).style.display = 'none';
        valido = false;
    }

    return valido;
}

function validacionGenericaNum(values, id)
{
    let valido = true;
    let patNumeros =  /^[0-9]+$/;

    if(values != null && values != '')
    {
        if(values.match(patNumeros) && values > 0)
        {
            id.style.border = "";
            id.style.boxShadow = "";
            id.style.borderBottom = "1px solid #9b8282";
            //document.getElementById( 'Enviar' ).style.display = '';
        }
        else
        {
            id.style.border = "3px solid red";
            id.style.boxShadow = "0 10px 10px rgb(243 44 44)";
            //document.getElementById( 'Enviar' ).style.display = 'none';
            valido = false;
        }
    }
    else
    {
        //document.getElementById( 'Enviar' ).style.display = 'none';
        valido = false;
    }   
    
    return valido;
}

function validacionGenericaEmail(values, id)
{
    valido = true;
    let patMail = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;

    if(values != null && values != '')
    {
        if(values.match(patMail))
        {
            id.style.border = "";
            id.style.boxShadow = "";
            id.style.borderBottom = "1px solid #9b8282";
            //document.getElementById( 'Enviar' ).style.display = '';
        }
        else
        {
            id.style.border = "3px solid red";
            id.style.boxShadow = "0 10px 10px rgb(243 44 44)";
            //document.getElementById( 'Enviar' ).style.display = 'none';
            vsalido = false;
        }
    }
    else
    {
        //document.getElementById( 'Enviar' ).style.display = 'none';
        valido = false;
    }

    return valido;
}

function selectEstados() 
{
      var estados = document.getElementById("estados");
      Select = estados.options[estados.selectedIndex].value;
      location.href = '/Turno/CambiarEstados/' + Select;
}

$('#closeModal').click(function()
{
    var estadoTurno = document.getElementById('estadoTurno');
    estadoTurno.style.display = "";
});

$('#closeModal2').click(function()
{
    var estadoTurno = document.getElementById('estadoTurno');
    estadoTurno.style.display = "";
});

function selectDireccion()
{
    let dire = document.getElementById("idDireccion");
    values = dire.options[dire.selectedIndex].value;

    if(values == 0)
    {
        document.getElementById( 'direccion_calle' ).style.display = '';
        document.getElementById( 'direccion_numero' ).style.display = '';
        document.getElementById( 'direccion_ciudad' ).style.display = '';
    }

    if(values != 0)
    {
        document.getElementById( 'direccion_calle' ).style.display = 'none';
        document.getElementById( 'direccion_numero' ).style.display = 'none';
        document.getElementById( 'direccion_ciudad' ).style.display = 'none';
    }    
}