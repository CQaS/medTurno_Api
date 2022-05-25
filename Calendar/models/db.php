<?php

include_once('dbConfig.php');

class admin extends dbConfig
{

        private function initSession()
        {
            $this->connect();
        }

        function get_initSession()
        {
                return $this->initSession();
        }
}

    $d = new admin();
    $d->initSession();