
# API Clients

    GET https://api.cornerstone.cc/v1/clients
    GET https://api.cornerstone.cc/v1/clients/<client_id>

## Creating a client

    POST https://api.cornerstone.cc/v1/clients

### Parameters

Name | Description
---- | -----------
pagename | Name of the page or site the client has access to - **Required. If client does not have access to any pages, use the String "null"**
partnername | Name of the partner the client can access applications for - optional
id | ID to be given to the client - Optional
secret | Key to be created for the client - Optional

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

`curl -i -u client:key https://api.cornerstone.cc/v1/clients -d "pagename=Test Account"`
