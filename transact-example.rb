#!/usr/bin/env ruby
require "net/https"
require "uri"
data = {  
		"amount" => "14",
		"customer[firstname]" => "bob",
		"customer[lastname]" => "parr",
		"customer[email]" => "bparr@cstonemail.com",
		"card[number]" => "4444333322221111",
		"card[expmonth]" => "12",
		"card[expyear]" => "24",
		"card[cvv]" => "123"
	   }

usr  = "client_8rHPjmG9YWqKmSiPQkrd"
pwd  = "key_qj9KUZJKIUcJSMIL3EHU16tmz"

uri  = URI.parse("https://api.cornerstone.cc/v1/transactions/")
http = Net::HTTP.new(uri.host, uri.port)

http.use_ssl = true
http.verify_mode = OpenSSL::SSL::VERIFY_NONE

request = Net::HTTP::Post.new(uri.request_uri)
request.basic_auth(usr, pwd)
request.set_form_data(data)

response = http.request(request)

print response.code
print response.body
