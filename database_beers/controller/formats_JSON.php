<?php
require_once "../model/formats_JSON.php";
$formats = getFormats();
$formats = json_encode($formats);
echo $formats;
?>