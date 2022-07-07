<?php

$pdo = new PDO("mysql:dbname=medturno;host=localhost","root","");

if(isset($_GET['accion']))
{
    $accion = $_GET['accion'];
}

switch($accion)
{
    case 'agregar':
        //... busca idPrestador asociado al usuario....
        $buscar = $pdo->prepare("SELECT idprestador FROM usuario WHERE id = ".$_POST['paciente']);
        $buscar->execute();
        $idResult = $buscar->fetchAll(PDO::FETCH_ASSOC);

        // busca el numero siguiente de turno...
        $fecha = explode(" ", $_POST['start']);
        $busqueda = "$fecha[0]%";
        $buscar = $pdo->prepare("SELECT SUM((SELECT COUNT(*) FROM turnos WHERE idDoctor = ".$_POST['profesional']." AND start LIKE '".$busqueda."') + 1) AS sigue");
        $buscar->execute();
        $turnoProximo = $buscar->fetchAll(PDO::FETCH_ASSOC);

        if(isset($_POST['color']))
        {
            $estado = 0;
            if($_POST['color'] == '#52af52'){ $estado = 8; }
            if($_POST['color'] == '#ff0000'){ $estado = 5; }
            if($_POST['color'] == '#ffff0070'){ $estado = 6; }
            if($_POST['color'] == '#5b5be2'){ $estado = 2; }
            if($_POST['color'] == '#e03197'){ $estado = 1; }
            if($_POST['color'] == '#e67e7e'){ $estado = 7; }
        } 

        $query = $pdo->prepare("insert into turnos(title,idDoctor,idUsuario,idPrestador,descripcion,color,textColor,start,end,estado)
        values(:title,:profesional,:paciente,:prestador,:descripcion,:color,:textColor,:start,:end,:estado)");
        $tituloCompuesto = 'Su Turno: '.$turnoProximo[0]['sigue'].' - '.$_POST['title'];
        $res = $query->execute(array(
            "title" => $tituloCompuesto,
            "profesional" => $_POST['profesional'],
            "paciente" => $_POST['paciente'],
            "prestador" => $idResult[0]['idprestador'],
            "descripcion" => $_POST['descripcion'],
            "color" => $_POST['color'],
            "textColor" => $_POST['textColor'],
            "start" => $_POST['start'],
            "end" => $_POST['end'],
            "estado" => $estado
        ));
        echo json_encode($res);
        break;

    case 'modificar':
        //...
        if(isset($_POST['color']))
        {
            $estado = 0;
            if($_POST['color'] == '#52af52'){ $estado = 8; }
            if($_POST['color'] == '#ff0000'){ $estado = 5; }
            if($_POST['color'] == '#ffff0070'){ $estado = 6; }
            if($_POST['color'] == '#5b5be2'){ $estado = 2; }
            if($_POST['color'] == '#e03197'){ $estado = 1; }
            if($_POST['color'] == '#e67e7e'){ $estado = 7; }
            
            if($_POST['color'] == '#30E8BF'){ $estado = 4; }
            if($_POST['color'] == '#f5af19'){ $estado = 3; }
        } 
        
        $query = $pdo->prepare("UPDATE turnos SET title = :title, descripcion = :descripcion, color = :color, textColor = :textColor, start = :start, end = :end, estado = :estado WHERE id = :id");
        $res = $query->execute(array(
            "id" => $_POST['id'],
            "title" => $_POST['title'],
            "descripcion" => $_POST['descripcion'],
            "color" => $_POST['color'],
            "textColor" => $_POST['textColor'],
            "start" => $_POST['start'],
            "end" => $_POST['end'],
            "estado" => $estado
        ));
        echo json_encode($res);
        break;

    case 'eliminar':
        //...
        $query = $pdo->prepare("UPDATE turnos SET estado = '0', color = '#c31432' WHERE id = :id");
        $res = $query->execute(array("id" => $_POST['id']));
        echo json_encode($res);
        break;

    case 'leer':
        $query = $pdo->prepare("SELECT * From turnos WHERE estado NOT IN(0, 3, 4)");
        $query->execute();
        $res = $query->fetchAll(PDO::FETCH_ASSOC);

        echo json_encode($res);
        break;
    
    default:
        $res = def($accion, $pdo);        

        echo json_encode($res);
        break;
}

function def($accion, $con)
{
    
    if(is_numeric($accion))
    {
        $query = $con->prepare("SELECT * FROM turnos t JOIN doctor d ON d.id = t.idDoctor WHERE d.id = ".$accion." AND t.estado NOT IN('0')");         
    }
    else
    {
        $query = $con->prepare("SELECT * FROM turnos WHERE color = '#".$accion."'");
    }

    $query->execute();
    $res = $query->fetchAll(PDO::FETCH_ASSOC);
    return $res;
    
}



?>