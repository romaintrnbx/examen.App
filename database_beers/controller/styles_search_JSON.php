<?php
require_once "../model/styles_JSON.php";

$searchText = $_GET['s'];
$styles = searchStyles($searchText);
header('Content-Type: application/json');
$styles = json_encode($styles);
echo $styles;

?>