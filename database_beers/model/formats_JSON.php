<?php
include 'db_JSON.php';

function getFormats() {
	$conn = getConnection();
	$stmt = $conn->prepare('SELECT * FROM formats');
	$stmt->execute();
	return $stmt->fetchAll();
}

?>