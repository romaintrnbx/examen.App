<?php
require_once "../model/beer_JSON.php";

$id = $_GET['id'];
$beer = deleteBeer($id);
return $beer;
?>