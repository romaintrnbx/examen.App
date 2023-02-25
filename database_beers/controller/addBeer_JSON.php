<?php
require_once "../model/addBeer_JSON.php";
// Récupération des données envoyées en POST
$data = json_decode(file_get_contents('php://input'), true);

$name = $data['name'];
$brewery_name = $data['brewery_name'];
$desc = $data['description'];
$alcool = $data['alcool'];
$IBU = $data['ibu'];
$EBC = $data['ebc'];
$style = $data['styleName'];
$glass = $data['glassName'];
$fermentation = $data['fermentationName'];
$formats = $data['formats'];
$create = $data['createdAt'];

$result = addBeer($name,$desc,$alcool,$IBU,$EBC,$style,$glass,$fermentation,$formats,$create,$brewery_name);

?>