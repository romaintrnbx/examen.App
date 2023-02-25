<?php
include 'db_JSON.php';

function addBeer($name, $desc, $alcool, $IBU, $EBC, $style, $glass, $fermentation, $formats, $create, $brewery_name) {
    $conn = getConnection();

    $stmt = $conn->prepare('SELECT id FROM beer_styles WHERE name = :style');
    $stmt->bindParam(':style', $style);
    $stmt->execute();
    $style_id = $stmt->fetchColumn();

    $stmt = $conn->prepare('SELECT id FROM beer_glasses WHERE name = :glass');
    $stmt->bindParam(':glass', $glass);
    $stmt->execute();
    $glass_id = $stmt->fetchColumn();

    $stmt = $conn->prepare('SELECT id FROM beer_fermentation WHERE name = :fermentation');
    $stmt->bindParam(':fermentation', $fermentation);
    $stmt->execute();
    $fermentation_id = $stmt->fetchColumn();

    $stmt = $conn->prepare('INSERT INTO beers (name, description, alcool, ibu, ebc, style_id, type_verre_id, fermentation_id, created_at) VALUES (:name, :desc, :alcool, :IBU, :EBC, :style_id, :glass_id, :fermentation_id, :create)');
    $stmt->bindParam(':name', $name);
    $stmt->bindParam(':desc', $desc);
    $stmt->bindParam(':alcool', $alcool);
    $stmt->bindParam(':IBU', $IBU);
    $stmt->bindParam(':EBC', $EBC);
    $stmt->bindParam(':style_id', $style_id);
    $stmt->bindParam(':glass_id', $glass_id);
    $stmt->bindParam(':fermentation_id', $fermentation_id);
    $stmt->bindParam(':create', $create);
    $stmt->execute();

    $beer_id = $conn->lastInsertId();

    $stmt = $conn->prepare('INSERT IGNORE INTO brewery (name) VALUES (:name)');
    $stmt->bindParam(':name', $brewery_name);
    $stmt->execute();

    $stmt = $conn->prepare('select id from brewery WHERE name = :name');
    $stmt->bindParam(':name', $brewery_name);
    $stmt->execute();
    $brewery_id = $stmt->fetchColumn();

    $stmt = $conn->prepare('INSERT INTO beer_brewery (beer_id,brewery_id) VALUES (:beer,:brewery)');
    $stmt->bindParam(':beer', $beer_id);
    $stmt->bindParam(':brewery', $brewery_id);
    $stmt->execute();

	$formats_arr = explode(",", $formats);
	foreach($formats_arr as $format) {
		$format_id = intval($format);
		$stmt = $conn->prepare('INSERT INTO beer_format (beer_id,format_id) VALUES (:beer,:format)');
		$stmt->bindParam(':beer', $beer_id);
		$stmt->bindParam(':format', $format_id);
		$stmt->execute();
	}
    return true;
}

?>