<?php

$host = "localhost";
$db_name = "database_beers";
$db_user = "root";
$db_password = "root";
$options = array(PDO::MYSQL_ATTR_INIT_COMMAND => 'SET NAMES utf8');

function getConnection() {
    global $host, $db_name, $db_user, $db_password, $options;
    try {
        $conn = new PDO("mysql:host=$host;dbname=$db_name; charset=utf8", $db_user, $db_password, $options);
        $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    }
	catch(PDOException $e) {
        echo "Connection failed: " . $e->getMessage();
    }

    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    $conn->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_ASSOC);
    return $conn;
}

?>