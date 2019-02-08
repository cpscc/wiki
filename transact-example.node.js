var request = require('request')

var url  = "https://api.cornerstone.cc/v1/transactions",
	user = "sandbox_3xSOjtxSvICXVOKYqbwI",
	key  = "key_RdutJGqI50YIwjehGtHBOe1Uu"

var options = {
	method: 'POST',
	url: 'https://api.cornerstone.cc/v1/transactions',
	auth: {
		user: user,
		pass: key
	},
	form: {
		"amount": "15",
		"customer": {
			"firstname": "Robert",
			"lastname": "Parr",
			"email": "r.parr@example.com"
		},
		"card": {
			"number": "4444333322221111",
			"expmonth": "12",
			"expyear": "24",
			"cvv": "123"
		}
	}
}

request(options, function (error, response, body) {
	console.log(body)
})