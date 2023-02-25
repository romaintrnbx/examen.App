<?php
require_once "../model/fermentations_JSON.php";
$ferm = getFermentations();
$ferm = json_encode($ferm);
echo $ferm;
?>