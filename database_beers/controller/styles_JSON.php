<?php
require_once "../model/styles_JSON.php";
$styles = getStyles();
$styles = json_encode($styles);
echo $styles;
?>