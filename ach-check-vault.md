
# ACH / eCheck

    POST https://api.cornerstone.cc/v1/ach
    GET https://api.cornerstone.cc/v1/ach/<token>

## Vaulting ACH / eCheck Information

    POST https://api.cornerstone.cc/v1/ach

### Parameters

* `merchant` Name of merchant associated with the token ** Required **
* `check[aba]` 9-digit bank routing number. ** Required **
* `check[account]` Bank account number. ** Required **
* `check[type]` Bank account type. Can be one of: `savings`, `checking`, `bsave` (business savings) or `bcheck` (business checking). ** Required **

### Examples

    POST https://api.cornerstone.cc/v1/ach/

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

    POST https://api.cornerstone.cc/v1/ach/<token>

### Parameters

* `merchant` Name of merchant associated with the token ** Required **
* `token` Token identifier for vaulted account ** Required **


### Examples

    POST https://api.cornerstone.cc/v1/ach/

```yaml
merchant: Joe's Trucking
token: 123
```

```json
{
        "merchant": "oneitem",
        "check": {
                "account": "0331100393",
                "aba": "99999999",
                "type": "checking"
        }
}
```
