<?php
include 'db_JSON.php';

function getStyles() {
	$conn = getConnection();
	$stmt = $conn->prepare('SELECT * FROM beer_styles ORDER BY name ASC');
	$stmt->execute();
	return $stmt->fetchAll();
}

function searchStyles($search) {
	$conn = getConnection();
	$stmt = $conn->prepare("SELECT * FROM beer_styles  WHERE name LIKE '%$search%' ORDER BY name ASC");
	$stmt->execute();
	$styles = $stmt->fetchAll();
	return $styles;
}
?>