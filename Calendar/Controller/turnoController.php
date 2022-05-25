<?php

include_once('Models/lista.php');

class turnoController extends admin
{
    public function turno()
    {
        foreach (parent::get_initSession() as $init)
        {
            if(empty($init) || $init['password'] == "")
            {
                header('location: http://localhost:5000/');
            }
        }
        require_once('views/inicio.php');
    }
}
?>