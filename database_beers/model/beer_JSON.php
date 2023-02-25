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

?>