## Verifying Responses

You will want to verify that a visit to your webhook url is not a spoof, but truly from us. To do this, we authenticate with you using a key that we share, which is combined with other data (a payload send time, a random number used only once (or "nonce"), and the payload itself) and hashed using the `md5` algorithm. This is called an HMAC, or Hash-Based Message Authentication Code. Your language should include the tools to do this out of the box.

Our HMACs are made up of a timestamp (Unix-style), nonce, and we use md5 as the algorithm. You would just parse the Authorization header to get the timestamp and nonce, and signature, and create a new signature to compare with the one sent.

```php
$ts = time();
$nonce = rand();
$hmac = hash_hmac($algo = 'md5', $body, $key); // json $body assumed

// Authorization: hmac ts=$ts nonce=$nonce algorithm=$algo signature=$hmac
```

Here is a concrete example, given the following body you would have this Authorization header:

```
Authorization: hmac ts=1620932376 nonce=42295527 algorithm=md5 signature=d2f22bd1d91b072a3ecfefa164c04a58
```

```json
{
  "id": "101blxH28k6JN8",
  "status": "approved",
  "message": "APPROVED 000007                 ",
  "memo": {
    "InvoiceNumber": "",
    "CustomerNumber": "",
    "Frequency": ""
  },
  "amount": "7.00",
  "customer": {
    "firstname": "Robert",
    "lastname": "Parr",
    "email": "receipts@zick.io",
    "address": "55 Traction Avenue",
    "company": "",
    "city": "Metroville",
    "state": "CA",
    "zip": "94111"
  },
  "payment": {
    "card": {
      "number": "4****1111",
      "expmonth": "11",
      "expyear": "23",
      "cvv": "***"
    },
    "check": ""
  }
}
```
