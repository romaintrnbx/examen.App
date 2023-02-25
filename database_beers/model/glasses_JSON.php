<?php
include 'db_JSON.php';

function getGlasses() {
	$conn = getConnection();
	$stmt = $conn->prepare('SELECT * FROM beer_glasses ORDER BY name ASC');
	$stmt->execute();
	return $stmt->fetchAll();
}

?>