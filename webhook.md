# Quarry Webhooks

## Setting Up Your Webhook

We need the following information from you to set up a webhook:

1. A url to POST the webhook to (for example, `https://yourdomain.com/quarry_webhook_capture.do`). This domain must have TLS enabled.
2. Specifiy what fields you need in the webhook. Sometimes a Quarry customer just need an ID and whether it approved, other times might want to collect all of the information their system needs to store for a payment or order.
3. The data type you would like to receive the payload in. Available options are `xml`, `json`, and `www` (which is [web/url encoding](https://en.wikipedia.org/wiki/Query_string), the default encoding for web forms). `json` is the most popular, but in some platforms `xml` or `www` is easier to work with.

You will also receive a key from us when the webhook is set up, so that you can authenticate the message (explained in detail later on).

## Testing Your Webhook

Once it's set up on our end, just make sure your webhook url can receive data, and then use your payment sandbox to start sending tests to your webhook. You can also mock requests to the webhook url, but make sure before you go live that it's been tested against the sandbox. The best way to test the fields you recieve is to make test transactions of varying types, as the specific data that is returned is tailored what you requested at setup.

## Verifying Responses

You will want to verify that a visit to your webhook url is not a spoof, but truly from us. To do this, we authenticate with you using a key that we share, which is combined with other data (a payload send time, a random number used only once (or "nonce"), and the payload itself) and hashed using the `md5` algorithm. This is called an [HMAC](https://en.wikipedia.org/wiki/HMAC), or Hash-Based Message Authentication Code. Your language should include the tools to handle this out of the box. The Wikipedia article on HMAC also describes this accurately.

Our HMACs are made up of a timestamp (Unix-style), nonce, and currently we allow only md5 as the algorithm. You would just parse the Authorization header to get the timestamp and nonce, and signature, and create a new signature to compare with the one sent.

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
