<?php
require_once "../model/deleteBeer_JSON.php";

$id = $_GET['id'];
$beer = deleteBeer($id);
return $beer;
?>