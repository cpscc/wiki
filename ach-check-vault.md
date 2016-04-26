
# ACH / eCheck

    GET https://api.cornerstone.cc/v1/ach
    GET https://api.cornerstone.cc/v1/ach/<token>

## Vaulting ACH / eCheck Information

    POST https://api.cornerstone.cc/v1/ach

### Parameters

Name | Description
---- | -----------
merchant | Name of the page or site the client has access to - **Required.**
account | Account number to be vaulted - **Required.**
routing | Routing Number to be vaulted - **Required.**

### Examples

    POST https://api.cornerstone.cc/v1/ach/

```yaml
merchant: Joe's Trucking
account: 031100393
routing: 99999999999
```

```json

```

## Fetching ACH / eCheck Information

    POST https://api.cornerstone.cc/v1/ach/<id>

### Parameters

Name | Description
---- | -----------
merchant | Name of the page or site the client has access to - **Required.**
token | Token associated with account information - **Required.**


### Examples

    POST https://api.cornerstone.cc/v1/ach/

```yaml
merchant: Joe's Trucking
token: 123
```

```json

```
