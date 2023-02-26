<?php
include 'db_JSON.php';

function getBeers_Name() {
	$conn = getConnection();
	$stmt = $conn->prepare('SELECT beers.id, beers.name, beers.description, alcool, ibu, ebc, beer_styles.name as style_name, beer_glasses.name as glass_name,
            beer_fermentation.name as fermentation_name, picture, created_at,
            (SELECT GROUP_CONCAT(brewery.name) FROM beer_brewery INNER JOIN Brewery ON beer_brewery.brewery_id = brewery.id WHERE beer_brewery.beer_id = beers.id) as brewery_name,
            (SELECT GROUP_CONCAT(formats.name) FROM beer_format INNER JOIN formats ON beer_format.format_id = formats.id WHERE beer_format.beer_id = beers.id) as formats
            FROM beers
            LEFT JOIN beer_styles ON beers.style_id = beer_styles.id
            LEFT JOIN beer_glasses ON beers.type_verre_id = beer_glasses.id
            LEFT JOIN beer_fermentation ON beers.fermentation_id = beer_fermentation.id
            ORDER BY beers.name ASC');
	$stmt->execute();
	return $stmt->fetchAll();
}

function getBeers_Date() {
	$conn = getConnection();
	$stmt = $conn->prepare('SELECT beers.id, beers.name, beers.description, alcool, ibu, ebc, beer_styles.name as style_name, beer_glasses.name as glass_name,
            beer_fermentation.name as fermentation_name, picture, created_at, brewery.name as brewery_name,
            (SELECT GROUP_CONCAT(formats.name) FROM beer_format INNER JOIN formats ON beer_format.format_id = formats.id WHERE beer_format.beer_id = beers.id) as formats
            FROM beers
            LEFT JOIN beer_styles ON beers.style_id = beer_styles.id
            LEFT JOIN beer_glasses ON beers.type_verre_id = beer_glasses.id
            LEFT JOIN beer_fermentation ON beers.fermentation_id = beer_fermentation.id
            LEFT JOIN beer_brewery ON beers.id = beer_brewery.beer_id LEFT JOIN brewery ON beer_brewery.brewery_id = brewery.id
            ORDER BY beers.date_add DESC');
	$stmt->execute();
	return $stmt->fetchAll();
}

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

function updateBeer($id, $name, $desc, $alcool, $IBU, $EBC, $style, $glass, $fermentation, $formats, $create, $brewery_name) {
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

	$stmt = $conn->prepare('UPDATE beers SET name = :name, description = :desc, alcool = :alcool, ibu = :IBU, ebc = :EBC, style_id = :style_id, type_verre_id = :glass_id, fermentation_id = :fermentation_id, created_at = :create WHERE id = :id');
	$stmt->bindParam(':id', $id);
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

	$stmt = $conn->prepare('INSERT IGNORE INTO brewery (name) VALUES (:name)');
	$stmt->bindParam(':name', $brewery_name);
	$stmt->execute();

	$stmt = $conn->prepare('SELECT id FROM brewery WHERE name = :name');
	$stmt->bindParam(':name', $brewery_name);
	$stmt->execute();
	$brewery_id = $stmt->fetchColumn();

	$stmt = $conn->prepare('DELETE FROM beer_brewery WHERE beer_id = :id');
	$stmt->bindParam(':id', $id);
	$stmt->execute();

	$stmt = $conn->prepare('INSERT INTO beer_brewery (beer_id, brewery_id) VALUES (:id, :brewery_id)');
	$stmt->bindParam(':id', $id);
	$stmt->bindParam(':brewery_id', $brewery_id);
	$stmt->execute();

	$stmt = $conn->prepare('DELETE FROM beer_format WHERE beer_id = :id');
	$stmt->bindParam(':id', $id);
	$stmt->execute();

	$formats_arr = explode(",", $formats);
	foreach($formats_arr as $format) {
		$format_id = intval($format);
		$stmt = $conn->prepare('INSERT INTO beer_format (beer_id, format_id) VALUES (:id, :format_id)');
		$stmt->bindParam(':id', $id);
		$stmt->bindParam(':format_id', $format_id);
		$stmt->execute();
	}
	return true;
}


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