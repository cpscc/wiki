# python 2 example transaction
import httplib, json, base64

endpoint = "api.cornerstone.cc"
path = "/v1/transactions"
user = "sandbox_3xSOjtxSvICXVOKYqbwI"
key = "key_RdutJGqI50YIwjehGtHBOe1Uu"

data = json.dumps({  
	"amount": "14",
	"customer": {
		"firstname": "bob",
		"lastname": "parr",
		"email": "bparr@cstonemail.com"
	},
	"card": {
		"number": "4444333322221111",
		"expmonth": "12",
		"expyear": "24",
		"cvv": "123"
	}
})

auth = base64.b64encode(user + ':' + key)
headers = {
	"Authorization": "Basic %s" % auth,
	"Content-Type": "application/json"
}
conn = httplib.HTTPSConnection(endpoint)
conn.request("POST", path, data, headers)

resp = conn.getresponse()
print resp.read()
