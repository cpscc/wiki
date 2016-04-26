
# ACH Vault / Fetch

    GET https://api.cornerstone.cc/v1/ach
    GET https://api.cornerstone.cc/v1/ach/<token>

## Creating a client

    POST https://api.cornerstone.cc/v1/ach

### Parameters


Note: except in rare cases, only Cornerstone has the need to create client IDs and keys, and we will provide them for you.

### Examples

    POST https://api.cornerstone.cc/v1/clients

```yaml
partnername: Joe's Trucking
pagename: null
```

```json
HTTP/1.1 200 OK

{
	"partnername": "Joe's Trucking",
	"pagename": "null",
	"id": "client_ioRIovisEjMQ1C28fy35",
	"key": "key_GyiW9JbucJ9VmTOAwLpTxdn85"
}
```
