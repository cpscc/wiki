## Endpoints

The main endpoint for the API is

    https://api.cornerstone.cc/v1

In the event of a network fault, it is also possible to try the alternative endpoint. (This endpoint routes into the API from a different IP address, registrar, DNS provider, and SSL authority.)

    https://cornerstone2.cc/api/v1

There is also a legacy sandbox endpoint, which will be completely removed within the next couple of years. Please note that you will need separate credentials to access this endpoint. It also also not recommended to send any live or confidential information, only testing data. 

    http://api.cornerstone.cc:8080/v1


## Authentication

Authentication is provided by [HTTP Basic Authentication](http://tools.ietf.org/html/rfc2617#section-2), using a provided API client ID and client key. With the [curl](http://curl.haxx.se/) utility, it looks something line this:

    $ curl -i -u <client_id>:<client_key> <resource_url>

## Parameters

For GET requests, all parameters not included in the resource path can be accessed as an HTTP query string parameter:

    $ curl -i -u client_id:client_key "https://api.cornerstone.cc/v1/transactions?token=check.0393.OTk=&merchant=oneitem"

Other requests are placed in the body of the request as POST parameters:

    $ curl -i -u client_id:client_key -X POST https://api.cornerstone.cc/v1/transactions -d \
      "amount=15&card[number]=4444333322221111&card[expmonth]=12&card[expyear]=23\
       &customer[firstname]=Robert&customer[lastname]=Parr&customer[email]=robertp@example.com"

## HTTP Verbs

- **GET** - Retrieve a resource
- **POST** - Create a new resource

## Errors

There are three types of errors our API will give you:

- `401``auth_error`: Thrown in two circumstances. When client ID and key are missing or unmatched, and when the client doesn't have permissions to perform certain API actions.
- `400``bad_request`: When arguments are missing or do not match a specific pattern.
- `404``not_found`: If a resource referred to by its identifier is missing, or a client lacks permission to view it.
- `500``internal`: Any internal problem. Please [contact us](mailto:rm@cornerstone.cc) right away if you experience one of these. They are very rare and can be dangerous if left in the wild.

All errors return as a Json object, and contain at least two properties: `error` and `reason`. Often a `try` property is also given, to hint at possible next steps. When applicable, a `user_message` is also sent, which is always a safe message to display directly to users.

# ACH / eCheck

    POST https://api.cornerstone.cc/v1/ach
    GET https://api.cornerstone.cc/v1/ach/<token>

## Vaulting ACH / eCheck Information

    POST https://api.cornerstone.cc/v1/ach/

### Parameters

* `merchant` Name of merchant associated with the token ** Required **
* `check[aba]` 9-digit bank routing number. ** Required **
* `check[account]` Bank account number. ** Required **
* `check[type]` Bank account type. Can be one of: `savings`, `checking`, `bsave` (business savings) or `bcheck` (business checking). ** Required **

### Examples

```yaml
merchant: oneitem
check[aba]: 031100393
check[account]: 9999999999
check[type]: checking
```

```json
HTTP/1.1 200 OK

{
        "success": true,
        "token": "check.0393.OTk="
}
```

## Fetching ACH / eCheck Information

    GET https://api.cornerstone.cc/v1/ach/

### Parameters

* `merchant` Name of merchant associated with the token ** Required **
* `token` Token identifier for vaulted account ** Required **


### Examples

```yaml
merchant: oneitem
token: check.0393.OTk=
```

```json
HTTP/1.1 200 OK

{
        "merchant": "oneitem",
        "check": {
                "account": "0331100393",
                "aba": "99999999",
                "type": "checking"
        }
}
```
