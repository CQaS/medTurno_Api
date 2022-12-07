<?php

include_once('dbConfig.php');

class admin extends dbConfig
{

        private function initSession()
        {
                
                try
                {
                        $SQL = "SELECT password FROM usuario WHERE mail = 'hashLog@mail.com'";
                        $query = $this->connect()->prepare($SQL);
                        $query->execute();
                        $res = $query->fetchAll(PDO::FETCH_ASSOC);

                        return $res;
                }
                catch (Exception $e) 
                {
			die('Error '.$e->getMessage());
		} 
                finally
                {
			$result = null;
		}                 
        }
        
        function get_initSession()
        {
                return $this->initSession();
        }

        function listaProfesionales()
        {
                try
                {
                        $query = $this->connect()->prepare("SELECT d.id, d.nombre, e.tipo From doctor d JOIN especialidad e ON d.idEspecialidad = e.id WHERE d.estado = '1'");
                        $query->execute();
                        $res = $query->fetchAll(PDO::FETCH_ASSOC);

                        return $res;    
                }
                catch (Exception $e) 
                {
                        die('Error '.$e->getMessage());
                } 
                finally
                {
                        $result = null;
                }  
                
        }

        function get_listaProfesionales()
        {
                return $this->listaProfesionales();
        }

        function listaPacientes()
        {
                try
                {
                        $query = $this->connect()->prepare("SELECT u.id, u.nombre, p.nombre AS oSocial From usuario u JOIN prestador p ON u.idprestador = p.id WHERE u.estado = '1' AND u.rol NOT IN('1') AND u.rol NOT IN('2')");
                        $query->execute();
                        $res = $query->fetchAll(PDO::FETCH_ASSOC);

                        return $res;
                }
                catch (Exception $e) 
                {
                        die('Error '.$e->getMessage());
                } 
                finally
                {
                        $result = null;
                }  
                
        }

        function get_listaPacientes()
        {
                return $this->listaPacientes();  
        }
}







?>