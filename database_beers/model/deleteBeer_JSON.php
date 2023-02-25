<?php
include 'db_JSON.php';

function deleteBeer($id){
	$conn = getConnection();
    $stmt = $conn->prepare('DELETE FROM beers WHERE id = :id;');
    $stmt->bindParam(':id', $id);
    $stmt->execute();

    $stmt = $conn->prepare('DELETE FROM beer_format WHERE beer_id = :id;');
    $stmt->bindParam(':id', $id);
    $stmt->execute();

    $stmt = $conn->prepare('DELETE FROM beer_brewery WHERE beer_id = :id;');
    $stmt->bindParam(':id', $id);
    $stmt->execute();

    return true;
}
?>