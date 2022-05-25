<?php

class dbConfig
{	
	protected function connect()
    {
		try 
        {
			$connect = new PDO('mysql:host=localhost;dbname=medturno;charset=utf8;','root','');
			$connect->setAttribute(PDO::ATTR_ERRMODE,PDO::ERRMODE_EXCEPTION);
			return $connect;	
		} 
        catch (Exception $e) 
        {
			die('Error db(connect) '.$e->getMessage());
		}
	}
}
?>