<?php
require_once "../model/beer_JSON.php";
	$beers = getBeers_Name();
	$beers = json_encode($beers);
	echo $beers;
?>