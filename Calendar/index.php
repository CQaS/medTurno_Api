<?php
//http://localhost/medTurno/?controller=turno&action=turno

  //chekea si estan seteados ./?controller= ?? &action= ?? ...
  if(isset($_GET['controller']) && isset($_GET['action']))
  {
      $controller = $_GET['controller'];
      $action = $_GET['action'];

      //chekea no si existe el archivo de nombre $controller...
      if(!file_exists('controller/'.$controller.'Controller.php'))
      {
        header('location: http://localhost:5000/');
      }    
  }
  else
  {
    header('location: http://localhost:5000/');
  }

  require_once('controller/'.$controller.'Controller.php');

  $objController = ucfirst($controller).'Controller';

  //chekea si existe la clase...
  if(class_exists($objController))
  {
    $nombreController = new $objController();

    //chekea si existe el metodo de nombre $action dentro de la Class $objController...
      if(method_exists($nombreController, $action))
      {
          $nombreController->$action();
      }
      else
      {
        header('location: http://localhost:5000/');
      } 
  }
  else
  {
    header('location: http://localhost:5000/');
  }
 

?>
