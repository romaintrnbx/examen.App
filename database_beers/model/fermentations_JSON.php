<?php
include 'db_JSON.php';

function getFermentations() {
	$conn = getConnection();
	$stmt = $conn->prepare('SELECT * FROM beer_fermentation ORDER BY name ASC');
	$stmt->execute();
	return $stmt->fetchAll();
}

?>