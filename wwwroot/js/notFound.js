var contenedor = document.getElementById('contenedor');

window.onmousemove = function(e)
{
    var x = - e.clientX/5;
    var y = - e.clienty/5;

    contenedor.style.backgroundPositionX = x + 'px';
    contenedor.style.backgroundPositionY = y + 'px';
}