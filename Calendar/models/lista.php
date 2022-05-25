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

        function listaProfecionales()
        {
                try
                {
                        $query = $this->connect()->prepare("SELECT id, nombre From doctor WHERE estado = '1'");
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

        function get_listaProfecionales()
        {
                return $this->listaProfecionales();
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