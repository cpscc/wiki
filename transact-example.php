<?php
# Sample Code:
#
# This sample creates a simple call to the Cornerstone API to create a transaction.
# client id/key are provided by Cornestone Payment Systems.
#
# Credentials for Test Cards
# Securenet: 4444333322221111 / 12 24 / 123
# Sage:		 4111111111111111 / 12 24 / 123
#
# For the id and key include the entire string give.

$data = array(
		  "amount" => "15",
		  "customer" => array(
			  "firstname" => "bob",
			  "lastname" => "parr",
			  "email" => "bparr@cstonemail.com",
		  ),
		  "card" => array(
			  "number" => "4444333322221111",
			  "expmonth" => "12",
  			  "expyear" => "24",
  			  "cvv" => "123",
		  )
	   );

$url     = "https://api.cornerstone.cc/v1/transactions";
$id 	 = "sandbox_3xSOjtxSvICXVOKYqbwI";
$key 	 = "key_RdutJGqI50YIwjehGtHBOe1Uu";
$request = http_build_query($data);

$ch = curl_init();

    curl_setopt($ch, CURLOPT_URL, $url);
    curl_setopt($ch, CURLOPT_POST, true);
    curl_setopt($ch, CURLOPT_POSTFIELDS, $request);		
	curl_setopt($ch, CURLOPT_USERPWD, "$id:$key");
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);

    $response = curl_exec($ch);
    curl_close($ch);

    var_export($response);