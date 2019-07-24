Merchant Quarry API
=============================

Introduction:

- [Endpoints](#endpoints)
- [Parameters](#parameters)
- [Authenication](#authentication)
- [HTTP Verbs](#http-verbs)
- [Errors](#errors)
- [Raw HTTP and cURL Examples](#raw-http-and-curl-examples)
- [Language-Specific Examples](#language-specific-examples)
- [Transaction IDs](#transaction-ids)

Usage:

- [API Version](#api-version)
- [Transactions](#transactions)
- [Fetch Transactions](#fetch-transactions)
- [Refund Transactions](#refund-transactions)
- [Unlinked Credit (not always available)](#unlinked-credit)
- [Update Schedules](#update-schedule)
- [Payment Information Vault (to tokenize payment information)](#payment-information-vault)
- [Merchant Application Status](#merchant-applications-status)
- [Tenants (Customers)](#tenants)

# Introduction 

The Cornerstone API is a [REST](http://en.wikipedia.org/wiki/REST)-like API secured with TLS/HTTPS and accepts request parameters as HTTP GET and POST fields ([x-www-form-urlencoded](https://www.w3.org/TR/html401/interact/forms.html#h-17.13.4.1), [example](http://php.net/manual/en/function.http-build-query.php#refsect1-function.http-build-query-examples)), and returns a [JSON](http://en.wikipedia.org/wiki/JSON) response. This API attempts to greatly simplify the integration process over many other payment APIs.

<!--
Inspiration for API:
[Apache CouchDB](http://en.wikipedia.org/wiki/CouchDB#Accessing_data_via_HTTP),
[Google Fusion Tables](https://developers.google.com/fusiontables/docs/v1/using),
[Github API](http://developer.github.com/).
-->

## Endpoints

The main endpoint for the API is

    https://api.cornerstone.cc/v1

In the event of a network fault, it is also possible to try the alternative endpoint. (This endpoint routes into the API from a different IP address, registrar, DNS provider, and SSL authority.)

    https://cornerstone2.cc/api/v1

<!--
There is also a legacy sandbox endpoint, which will be completely removed within the next couple of years. Please note that you will need separate credentials to access this endpoint. It also also not recommended to send any live or confidential information, only testing data. 

    http://api.cornerstone.cc:8080/v1
-->


## Authentication

Authentication is provided by [HTTP Basic Authentication](http://tools.ietf.org/html/rfc2617#section-2), using an API client id, and client key, which Cornerstone provides. These credentials are *not* sent in the body of the request, but instead in a header. For exact details on how to use Basic Authentication, you will need to refer to the documentation of your HTTP library. In the end, no matter what tools or library you use to get there, basic auth ends up sending a header that looks something like the following:

    Authorization: Basic QWxhZGRpbjpPcGVuU2VzYW1l

If your library does not include an implementation of basic auth, Wikipedia has a helpful section explaining [how to manually create the header](https://en.wikipedia.org/wiki/Basic_access_authentication#Client_side) (it's about 3 steps).

With the [curl](http://curl.haxx.se/) utility, it looks something line this:

    $ curl -i -u <client_id>:<client_key> <resource_url>
    
[Curl](http://curl.haxx.se/) is a great utility you can use for debugging outside of your language, in order to narrow down any issues outside of possible complications created by your language or library. This way you can ensure a request works by itself, and work backwards to exactly what is causing problems in an integration.

## Parameters

For GET requests, all parameters not included in the resource path can be accessed as an HTTP query string parameter:

    $ curl -i -u client_id:client_key "https://api.cornerstone.cc/v1/transactions?range=2012/01/01-2012/01/15&show_test"

Other requests are placed in the body of the request as POST parameters:

    $ curl -i -u client_id:client_key -X POST https://api.cornerstone.cc/v1/transactions -d \
      "amount=15&card[number]=4444333322221111&card[expmonth]=12&card[expyear]=23\
       &customer[firstname]=Robert&customer[lastname]=Parr&customer[email]=robertp@example.com"

## HTTP Verbs

- **GET** - Retrieve a resource
- **POST** - Create a new resource
- **PATCH** - Partially update an existing resource
- **PUT** - Replace a resource with new data
- **COPY** - Duplicate a resource, with any changes included as parameters
- **DELETE** - Delete a resource

## Errors

Here are example errors the API will respond with:

- `401` `auth_error`: Thrown in two circumstances. When client ID and key are missing or unmatched, and when the client doesn't have permissions to perform certain API actions.
- `400` `bad_request`: When arguments are missing or do not match a specific pattern.
- `404` `not_found`: If a resource referred to by its identifier is missing, or a client lacks permission to view it.
- `500` `internal`: Any internal problem. Please [contact us](mailto:development@cornerstone.cc) right away if you experience one of these. They are very rare and can be dangerous if left in the wild.
- `501` `not_implemented`: Rarely, a particular feature may not be available based on the settings and backend gateway your account is configured to use.

All errors return as a Json object, and contain at least two properties: `error` and `reason`. Often a `try` property is also given, to hint at possible next steps. When applicable, a `user_message` is also sent, which is always a safe message to display directly to users.

### Example Errors

```json
{
	"error": "auth_error",
	"reason": "Could not find a matching key and ID",
	"try": "Double-checking your credentials"
}
```

```json
{
	"error": "not_found",
	"reason": "No application found by ID: xyz"
}
```

```json
{
	"error": "not_found",
	"reason": "No resource URI: xyz"
}
```

## Raw HTTP and cURL Examples

For debugging, it's important to be able to create a valid, working example to compare with. We will provide this example using the shell [cURL](https://curl.haxx.se/) utility, and the raw request this will ultimately create (which you will be recreating in your own language and environment). using cURL. A basic transaction process can be perfomed with cURL using a shell command similar to below (this is a working example):

```sh
curl -iv "https://api.cornerstone.cc/v1/transactions" \
    -X POST \
    -u client_4tdGWGOXLooRVH9zlSBF:key_NzoDnXbAKLZ6FlM2diNMy6iFz \
    -d "amount=7" \
    -d "customer[email]=angusm@example.com" \
    -d "customer[firstname]=Angus" \
    -d "customer[lastname]=MacGyver" \
    -d "card[number]=4111111111111111" \
    -d "card[expmonth]=12" \
    -d "card[expyear]=23" \
    -d "card[cvv]=1114"
```

This generates a raw HTTP request that looking something like

```http
POST /v1/transactions HTTP/1.1
```
```yaml
Host: api.cornerstone.cc
Authorization: Basic Y2xpZW50XzR0ZEdXR09YTG9vUlZIOXpsU0JGOmtleV9Oem9EblhiQUtMWjZGbE0yZGlOTXk2aUZ6
User-Agent: curl/7.49.1
Accept: */*
Content-Length: 177
Content-Type: application/x-www-form-urlencoded
```
```x-www-form-urlencoded
customer[email]=angusm@example.com&customer[firstname]=Angus&customer[lastname]=MacGyver&card[number]=4111111111111111&card[expmonth]=12&card[expyear]=23&card[cvv]=1114
```

And the response from the Cornerstone API will return something along the lines of 

```http
HTTP/1.1 200 OK
```
```yaml
Date: Thu, 08 Dec 2016 17:35:16 GMT
Server: cornerstone-httpd-0.9
X-Quarry: v1 895cb85d493fe399b685d3e256268786f5735a4b onyx us.central
Expires: Mon, 26 Jul 1997 05:00:00 GMT
Cache-Control: no-cache, must-revalidate
Pragma: no-cache
Content-Length: 296
Connection: close
Content-Type: application/json
```
```json
{
    "approved": [
        {
            "id": 823201,
            "merchant": "gearbox",
            "reason": "approved",
            "amount": "7",
            "frequency": "once",
            "startdate": false,
            "test": true,
            "token": "visa.1111.1223.YWM5MGM0MDk4ZjNlNDg1ZDkwODFiNWEwNTk2ZWVjMjY=",
            "cvv_match": "P",
            "avs_match": "Y"
        }
    ]
}
```

## Language-Specific Examples

To get you started, we also have a few very simple transaction examples to get you started:

- [C Sharp](https://github.com/cpscc/wiki/blob/master/transact-example.cs) (or [Using Newtonsoft.Json](https://github.com/cpscc/wiki/blob/master/transact-example-json.cs))
- [Java](https://github.com/cpscc/wiki/blob/master/transact-example.java)
- [PHP](https://github.com/cpscc/wiki/blob/master/transact-example.php)
- [Python 2](https://github.com/cpscc/wiki/blob/master/transact-example-python2.py)
- [Python 3](https://github.com/cpscc/wiki/blob/master/transact-example-python3.py)
- [Ruby](https://github.com/cpscc/wiki/blob/master/transact-example.rb)

If your language is not here, let us know, as we may be working on an example for that language.


## Transaction IDs

There are two ID formats, depending on how your API client is configured behind the scenes. There is a numeric `id` and a `v2id` that also contains letters. They are used interchangeably in our system, and a transaction always ends up having a numeric ID under the hood, but you can also use the `v2id` in it’s place for lookup.


# API Version

Use this to do a quick sanity check to make sure you can connect to our API (this is a simple GET request with no parameters and no need for authentication).

    GET https://api.cornerstone.cc/v1/

```json
HTTP/1.1 200 OK

{
	"cornerstone": "Howdy there!",
	"version": "1.0",
	"documentation": "https://github.com/cpscc/wiki/blob/master/official-cornerstone-api.md"
}
```


# Transactions

    POST https://api.cornerstone.cc/v1/transactions
    GET https://api.cornerstone.cc/v1/transactions
    GET https://api.cornerstone.cc/v1/transactions/<transaction_id>

## Create (charge) a transaction

    POST https://api.cornerstone.cc/v1/transactions

### Testing Credentials
Card | Securenet | Sage
---- | ----- | -----
number | 4444333322221111 | 4111111111111111
expmo | 12 | 12
expyr | 24 | 24
cvv | 123 | 123

eCheck | Securenet | Sage
---- | ----- | -----
account | 9999999999 | 056008849
routing | 031100393 | 12345678901234

### Parameters

Name | Usage
---- | -----
request_id | (optional, recommended) **Numeric** A unique id string (max. length 255) sent along with the transaction request. If the transaction request is re-sent, the `request_id` will be checked for uniqueness, and if it is found, a `request_id_conflict` error will be sent with a `400` response code. Please see the notes and examples on this below.
customer[ip] | (optional, recommened) IP address of the customer connected to your integration. This is used for risk checking and rate limiting. Only include if it makes sense for your situation. For a call center, for instance, this will not be necessary, but for a web checkout it  reduces risk and improves security.
customer[agent] | (optional, recommened) [User agent](https://en.wikipedia.org/wiki/User_agent) (web browser/OS) of the client connected to your integration. This is used for risk checking and rate limiting. Only include if it makes sense for your situation. For a call center, for instance, this will not be necessary, but for a web checkout it reduces risk and improves security.
amount | **String** Amount in US dollars. We try to determine what you mean automatically, so `13`, `13.00`, `$13`, and `13 dollars` all register as $13.00 USD.
merchant | (optional) **String** If the transaction is being charged to another merchant or a sub-account, it is specified here as a string.
recurring | (optional) **String** Allows you to specify a recurring cycle. Values available: `once` (default), `weekly`, `monthly`, `quarterly`, or `yearly`. Without `startdate` set, all monthly transactions made on the 13th will fall on the 13th of the next month, and so on.
start-date | (optional) **String** Used to schedule a transaction in the future. Must be formatted: `mm/dd/yyyy`, e.g. `12/31/1999`. If the day of the month is above 30 (as it is in our example), it is silently shifted down to 30.
memo | (optional) **String** Any string of text for reporting.
memo[] | (optional) **Array** Instead of a string, the memo can be split into an array , for example `memo[shoe_size]=12&memo[shoe_color]=red`.
vault | **Boolean** Vault the payment info -- this results in a 0 transaction record, where no authorization or capture has been made on for the payment
customer[] | **Array** Customer address and shipping information. Whether or not these fields are required are based on your AVS settings, however usually all of the fields are optional, but you will receive better rates when these fields are included and the address validates. Contains: <br> `customer[firstname]` <br> `customer[lastname]` <br> `customer[email]` <br> `customer[address]` <br> `customer[state]` <br> `customer[zip]` <br> `customer[country]` <br> `customer[phone]` <br> `customer[comment]` <br> `customer[shipping][firstname]` <br> `customer[shipping][lastname]` <br> `customer[shipping][address]` <br> `customer[shipping][state]` <br> `customer[shipping][zip]` <br> `customer[shipping][country]` <br> `customer[shipping][phone]` <br> `customer[ip]` <br> `customer[agent]`
card[] | **Array** Contains: `card[number]`, `card[expmonth]`, `card[expyear]` and `card[cvv]`
check[] | **Array** Only required if `card[]` or `token` is missing, contains: `check[aba]`, `check[account]` and `check[type]`. `type` can be one of `savings`, `checking`, `bsave`or `bcheck`.
token | **String** Only required if `card[]` or `check[]` is missing. A valid token, as returned by a transaction/vault process. For example, `check.1234.1234.NxDUy`.

For more details, see "Parameter Details" below.

### Examples

Approved CC transaction:

    POST https://api.cornerstone.cc/v1/transactions

```yaml
request_id: abc123
amount: 15
customer[firstname]: Bob
customer[lastname]: Parr
customer[email]: robertp@example.com
card[number]: 4444333322221111
card[expmonth]: 12
card[expyear]: 21
card[cvv]: 123
```

```json
HTTP/1.1 200 OK

{
    "approved": [
        {
            "id": 8984,
            "merchant": "Test Ministry",
            "reason": "Accepted",
            "amount": "15",
            "frequency": "once",
            "startdate": false,
            "test": false,
            "token": "visa.1111.1225.NTAwODc3Ni03MjI=",
            "cvv_match": "M",
            "avs_match": "Y"
        }
    ]
}
```

`request_id` conflict:

    POST https://api.cornerstone.cc/v1/transactions

```yaml
request_id: abc123
amount: 15
customer[firstname]: Bob
customer[lastname]: Parr
customer[email]: robertp@example.com
card[number]: 4444333322221111
card[expmonth]: 12
card[expyear]: 21
card[cvv]: 123
```

```json
HTTP/1.1 400 Bad Request

{
    "error": "bad_request",
    "reason": "request_id already exists: abc123",
    "request_id_conflict": "abc123",
    "transaction_ids": [
        "8984"
    ]
}
```

Declined transaction:

    POST https://api.cornerstone.cc/v1/transactions

```yaml
amount: 15
customer[firstname]: Bob
customer[lastname]: Parr
customer[email]: robertp@example.com
card[number]: 1234123412341234
card[expmonth]: 12
card[expyear]: 21
card[cvv]: 123
```

```json
HTTP/1.1 200 OK

{
    "declined": [
        {
            "reason": "CARD TYPE COULD NOT BE IDENTIFIED"
        }
    ]
}
```

Approved EFT Transaction:


    POST https://api.cornerstone.cc/v1/transactions

```yaml
amount: 15
customer[firstname]: bob
customer[lastname]: parr
customer[email]: robertp@insuricare.com
check[aba]: 031100393
check[account]: 9999999999
check[type]: checking
```

```json
HTTP/1.1 200 OK

{
    "approved": [
        {
            "amount": 15,
            "frequency": "once",
            "id": 1234
        }
    ]
}
```

EFT not enabled:

    POST https://api.cornerstone.cc/v1/transactions

```yaml
amount: 15
customer[firstname]: bob
customer[lastname]: parr
customer[email]: robertp@insuricare.com
check[aba]: 031100393
check[account]: 9999999999
check[type]: checking
```

```json
HTTP/1.1 200 OK

{
    "declined": [
        {
            "reason": "MERCHANT IS NOT ENABLED FOR ACH TRANSACTIONS"
        }
    ]
}
```

No fields given:

    POST https://api.cornerstone.cc/v1/transactions

```json
HTTP/1.1 400 Bad Request

{
    "error": "bad_request",
    "reason": "Missing required field: amount"
}
```

Missing fields:

    POST https://api.cornerstone.cc/v1/transactions

```yaml
amount: 15
customer[firstname]: bob
customer[lastname]: parr
customer[email]: robertp@insuricare.com
```

```json
HTTP/1.1 400 Bad Request

{
    "error": "bad_request",
    "reason": "Missing required fields: a card or check is required."
}
```

### Parameter Details

#### card[]

(required if `check` or `token` is missing)

* `card[number]` Credit card number. Must contain 15-16 digits.
* `card[expmonth]` Credit card expiration month. Must contain 2 digits between 1 and 12.
* `card[expyear]` Credit card expiration year. Must contain 2 digits, later than the current year.
* `card[cvv]` Card security code (CVV/CVC). If present, must contain 3-4 digits. (optional based on your settings)

#### check[] - EFT / E-check

(required if `card` or `token` is missing)

* `check[aba]` 9-digit bank routing number.
* `check[account]` Bank account number.
* `check[type]` Bank account type. Can be one of: `savings`, `checking`, `bsave` (business savings) or `bcheck` (business checking).

#### token

(required if `card` or `token` is missing)

This will contain the token, as returned by a previous transaction POST or GET. A valid token is in the following formats: `<type>.<number>.<expiration>.<id>`, for instance `visa.1111.1221.NTAwMzc1MC0yMw==`, `check.1234.1234.NxDUy`, and so on. Valid prefixes are `check`, `amex`, `visa`, `discover`, `mastercard`, and `master` (legacy). There are also the special purpose tokens, `papercheck` and `cash`.


#### customer[]

The following parameters are required and must contain at least two charactars: `customer[firstname]`, `customer[lastname]`.

The following parameters make up the billing address and may or may not be required depending on the setting for the merchant.

* `customer[email]`
* `customer[address]`: Address line 1
* `customer[company]`: Address line 2
* `customer[city]`:
* `customer[state]`
* `customer[zip]`
* `customer[country]` (optional)
* `customer[phone]` (optional)


#### customer[comment]

`customer[comment]` is used for notes or comments from the customer. This is different than the memo.

#### memo[]
The memo parameter can be used to send various information.
i.e. if a merchant wanted to post an invoice number they would pass it as `memo[Invoice]` with the attached value.


### Request ID

The `request_id` parameter is provided to allow re-sending a transaction request in the case of network faults or 
timeouts, without the risk of a duplicate transaction processing. A `request_id` must be unique within our system (they are *global* across the Quarry API),
and it is recommended that you use the full 255 characters of space to avoid any "false positives" with the conflicts. An example of one way you may construct a `request_id`
(although the format is completely up to you):

    <ORGANIZATION NAME>.<TIMESTAMP>.<HASH OF REQUEST>.<RANDOM DATA>
    
So, for example, given your organization is called "Orient Missions", you could have a `request_id` that looks like the following:

    ORIENT MISSIONS.1454961474.21002edf4b6627c85aece64f669b63a905819b43.R|yw*ZOmqaZ42(%z^n8= ... (truncated)

Or, it is possible to simply used an entirely random string:

    tg7cVcPg[u5a+AX/rG33uGEGzff6LdPyoFbOeTn+%d2+pBGy0E@mdEtf]%zidZzV7di6_3|mM3MJ!jlwn7-4NMCA ... (truncated)


### AVS and CVV response

#### `avs_match` (address verification response code)

The Address Verification System (AVS) helps identify suspicious activity, by verifying the address. If an address is present in a request, Cornerstone submits the address to financial institutions (issuing banks), who will verify the address against their records, and return an AVS response code. The AVS codes we support are as follows:

Code | Description
---- | -------
" " | (Blank response) Service Not Supported
0 | AVS data not provided
A | Street address matches, Zip Code does not
B | Postal code not verified due to incompatible formats
C | Street address and postal code not verified due to incompatible formats
D | Street address and postal code match *(international)*
E | Error: AVS data is invalid
G | Non-U.S. issuing bank does not support AVS *(international)*
I | Address information not verified by issuer *(international)*
M | Customer Name, Billing Address and Zip match *(international)*
N | Neither street address nor Zip code match
P | Street address not verified due to incompatible format *(international)*
R | Retry: issuer's system unavailable or timed-out
S | U.S. issuing bank does not support AVS
T | Street address does not match, but 9-digit Zip code matches
U | Address information is unavailable
W | 9-digit Zip matches, street address does not
X | Street address and 9-digit Zip match
Y | Street address and 5-digit Zip match
Z | 5-digit Zip matches, street address does not

#### `cvv_match` (card security response code)

The credit card identification code, or "Card Code" is a three- or four-digit security code printed on the back (or, in the case of American Express, front) of credit cards. This can be used to verify that a customer is in posession of the card being transacted. Cornerstone verifies this number with the issuing bank, and returns a CVV match code. The supported codes are as follows: 

Code | Description
---- | -------
0 | CVV/CID not provided
M | Match
N | No match
P | Not processed
S | Data not present
U | Issuer unable to process request
Y | Card Code Matches (Amex Only)


## Update-Schedule

    PATCH https://api.cornerstone.cc/v1/transactions
    
Update scheduled transaction information to be processed at a future date.

To update the transaction you must refer to a certain transaction ID.  These ID's are returned when a schedule is made or you can fetch the transaction ID's using the [Fetch Transactions](#fetch-transactions) functionality.  If card of check information is passed it will return the updated token related to the transaction.  You made do a simple update without any card or check information such as adjusting the amount, token, cycle, startdate, or nextdate of a transaction.

To cancel a schedule, send the value `once` in the `cycle` field.

### Parameters

Name | Usage
---- | -----
id | The ID of the transaction you would like to update. If the transaction cannot be found, or you do not have access, a "not_found" error will be returned.
amount | Amount in US dollars. We try to determine what you mean automatically, so `13`, `13.00`, `$13`, and `13 dollars` all register as $13.00 USD.
cycle | (optional) Allows you to specify a recurring cycle. Values available: `once` (send this to cancel the transaction), `weekly`, `monthly`, `quarterly`, or `yearly`.
nextdate | (optional) Used to schedule the next date a transaction will occur. Must be formatted: `mm/dd/yyyy`
startdate | (optional) Used to schedule first occurence of transaction if none have occured. Must be formatted: `mm/dd/yyyy`
token | (optional) Used to update the payment token for a transaction.  Payment tokens are returned when a recurring transaction is created or future payment scheduled.
card[] | (optional) Used to update card information for a transaction.
check[] | (optional) Used to update check information for a transaction.
memo[] | (optional) Used to update Memo field for Transaction.

#### card[] - Credit Card
* `card[number]` Credit card number. Must contain 15-16 digits.
* `card[expmonth]` Credit card expiration month. Must contain 2 digits between 1 and 12.
* `card[expyear]` Credit card expiration year. Must contain 2 digits, later than the current year.
* `card[cvv]` Card security code (CVV/CVC). Must contain 3-4 digits.

#### check[] - EFT / E-check
* `check[aba]` 9-digit bank routing number.
* `check[account]` Bank account number.
* `check[type]` Bank account type. Can be one of: `savings`, `checking`, `bsave` (business savings) or `bcheck` (business checking).


## Payment Information Vault

    POST https://api.cornerstone.cc/v1/transactions

Store payment information securely in a vault without making a charge or authorization to the payment method, creating a `token` id. This is also often referred to as "tokenizing" a card or other payment information.

To use the vault, add the “vault” field to a normal transaction request. When “vault” is present, the amount is no longer required, and if included, will be ignored. As long as the payment information is valid, you will receive an “approved” or “accepted” status.

### Examples 

Valid Vaulted Payment:

    POST https://api.cornerstone.cc/v1/transactions

```yaml
vault: 1
customer[email]: angusm@example.com
customer[firstname]: Angus
customer[lastname]: MacGyver
card[number]: 370000000000002
card[expmonth]: 12
card[expyear]: 23
card[cvv]: 1114
```

```json
{
    "approved": [
        {
            "id": 822997,
            "merchant": "gearbox",
            "reason": "approved",
            "amount": 0,
            "frequency": "once",
            "startdate": false,
            "test": false,
            "token": "amex.0002.1223.YTNkOGY2YThkMGE5NGFmYWEyMDRiMTVjZjJmNGNiYzE=",
            "cvv_match": null,
            "avs_match": null
        }
    ]
}
```



## Fetch-Transactions

    GET https://api.cornerstone.cc/v1/transactions

Fetch a list of transactions, according to a given filter.

### Parameters

Name | Usage
----:| -----
range          | Filters by date or date range. Format: `12/31/1999` or `01/31/1999-12/31/1999` (optional). If left empty, this turns into today's date. <!--To get all transactions, enter `*`. Note that this is slow and it's recommended not to use in a production environment, and instead a range should be used.-->
amount         | Dollar amount. We try to determine what you mean automatically, so `13`, `13.00`, `$13`, and `13 dollars` all register as $13.00 USD. (optional)
firstname      | Filter by customer's first name (optional)
lastname       | Or customer's last name (optional)
email 	       | Or customer's email (optional)
customerid     | Or customer ID (optional) 
payment_type[] | Can be a combination of `amex`, `discover`, `visa`, `mastercard`, and `check` (optional)
merchant       | Filter by sub-merchant name (optional)
failed         | Display declined transactions instead of approved transactions (optional)
scheduled      | Display scheduled transactions instead of approved or declined (optional)
show_test      | Display test transactions (optional)
trans_id       | Fetch a transaction by ID (optional)
request_id     | Fetch a transaction by `request_id` (optional
custom[]<br>memo[] | Filter by any custom (as sent in **`memo[]`** in a transaction POST) fields that may be present on the transaction (optional)
include_gid    | Include the transaction ID from the backend gateway with the response (optional)
page_size      | Request a certain number of transactions (default: 10)
page           | Which page number you would like to view (default: 1)
page_offset    | Offset the starting transaction. Increment to paginate through transactions. (default: 0)


## Refund Transactions

    DELETE https://api.cornerstone.cc/v1/transactions?id=<id>

Refund a transaction, passing the TransID as the parameter `id`.

### Parameters

Name | Usage
----:| -----
id | Cornerstone Transaction ID 
amount | (not required) Amount, if different than the original authorization amount of the transaction.


### Examples

Approved Refund
```json
HTTP/1.1 200 OK

{
	"approved": {
		"refundedTrans": "219677",
		"refundRefID": 219749,
		"amount": -50
	}
}
```

Declined Refund
```json
HTTP/1.1 200 OK

{
        "declined": {
                "reason": "CREDIT CANNOT BE COMPLETED ON A VOID TRANSACTION"
        }
}
```

Errors
```json
HTTP/1.1 404 Bad Request

{
        "error": "parameter_not_found",
        "reason": "Transaction ID."
}
```

```json
HTTP/1.1 404 Not Found

{
        "error": "transaction_not_found",
        "reason": "No transaction by that ID found."
}
```

```json
HTTP/1.1 403 Forbidden

{
        "error": "auth_error",
        "reason": "You are not authorized to refund this transaction."
}
```



## Unlinked Credit

    DELETE https://api.cornerstone.cc/v1/transactions?amount=<amount>&card[number]=<card-number>&card[expmonth]=<expiration-month>&card[expyear]=<expiration-year>

**NOTE: This is not available for most gateways, and the feature requires special paperwork to be enabled.
If you need unlinked credit please contact us to request it.** Unlinked credit is exactly the same as a refund, but the transaction id is replaced with a card object.

### Parameters

Name | Usage
----:| -----
amount | (required) Credit amount
card[number] | (required) Credit card number
card[expmonth] | (required) Credit card expiration month
card[expyear] | (required) Credit card expiration year
merchant | (not required) If you're API client is enabled for multiple pages, the name of the page the credit is running against.


### Examples

Approved Credit
```json
HTTP/1.1 200 OK

{
	"approved": {
		"refundRefID": 219749,
		"amount": -50
	}
}
```

Errors

```json
HTTP/1.1 501 Not Implemented

{
        "error": "not_implemented",
        "reason": "Unlinked credit not supported with gateway"
}
```

```json
HTTP/1.1 404 Bad Request

{
        "error": "parameter_not_found",
        "reason": "card[number]"
}
```




# Merchant Applications Status

    GET https://api.cornerstone.cc/v1/applications/<application_id>

These are applications that are fed into Cornerstone's internal Sales program for approval as merchants. For Cornerstone partners, this allows them to check on applications that have been submitted through them.

## Examples

Fetching an approved application:

    GET https://api.cornerstone.cc/v1/applications/rVNw9CUcerzJ6rfwlIE0

```json
HTTP/1.1 200 OK

{
	"id": "rVNw9CUcerzJ6rfwlIE0",
	"partner": "Realm of Creativity",
	"dba": "Joe's Trucking",
	"approved": true,
	"merchant_id": "12345",
	"gateway_id": "9001",
	"gateway_key": "xyz123"
}
```

An application not approved:

    GET https://api.cornerstone.cc/v1/applications/LQr7oUAYJZLQsh27i1kP

```json
HTTP/1.1 200 OK

{
	"id": "GV5ScI689yJ07ezSi3kd4HIPi",
	"partner": "Realm of Creativity",
	"dba": "Laurence Chinese Palace",
	"approved": false
}
```

A pending application:

    GET https://api.cornerstone.cc/v1/applications/GV5ScI689yJ07ezSi3kd4HIPi

```json
HTTP/1.1 200 OK

{
	"id": "LQr7oUAYJZLQsh27i1kP",
	"partner": "Realm of Creativity",
	"dba": "James Smith Pizzeria",
	"pending": true
}
```

Finally, if an application by the ID does not exist, or if your client id doesn't have permission to view an application, you will receive a not found error:

    GET https://api.cornerstone.cc/v1/applications/xyz

```json
HTTP/1.1 404 Not Found

{
	"error": "not_found",
	"reason": "No application found by ID: xyz"
}
```
# Tenants

## Create a Tenant

    POST https://api.cornerstone.cc/v1/tenants
    
### Parameters

Name | Description
---- | -----------
merchant | (required) Name of the page or site the client has access to.
login | (required) Email to be associated the user account.
password | (required) Password associated with the account.
firstname | (optional) First name of the user.
lastname | (optional) Last name of the user.

```json
HTTP/1.1 200 OK

{
        "success": true,
        "tenant_id": 757928
}
```
## Fetch a Tenant

    GET https://api.cornerstone.cc/v1/tenants
    
### Parameters

Name | Description
---- | -----------
id | (required) Customer ID used to fetch customer account.

```json
HTTP/1.1 200 OK

{
        "success": true,
        "tenant": {
                "id": "757928",
                "merchant": "oneitem",
                "firstname": "bruce",
                "lastname": "wayne",
                "login": "bwayne@yahoo.com",
                "password": "stuff"
        }
}
```
## Update a Tenant

    PATCH https://api.cornerstone.cc/v1/tenants
    
### Parameters

Name | Description
---- | -----------
id | (required) Customer ID used to update.
logins | (optional) Update user login email.
firstname | (optional) Update user first name.
lastname | (optional) Update user last name.
```json
HTTP/1.1 200 OK

{
        "success": true,
        "tenant": {
                "id": "757928",
                "merchant": "oneitem",
                "firstname": "spider",
                "lastname": "man",
                "login": "spiderman@aol.com",
                "password": "stuff"
        },
        "message": "ID 757928 updated"
}
```
## Delete a Tenant

    DELETE https://api.cornerstone.cc/v1/tenants/<customerid>

```
HTTP/1.1 200 OK

{
        "success": true,
        "tenant_id": 757928,
        "message": "ID 757928 removed."
}
```
