<?php
require_once "../model/glasses_JSON.php";
$glasses = getGlasses();
$glasses = json_encode($glasses);
echo $glasses;
?>