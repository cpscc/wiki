package main

import ("bytes"; "encoding/xml"; "io"; "net/http"; "os"; "time")

type Memo struct {
	/* add your own custom properties here */
}
type Shipping struct {
	FirstName string `xml:"customer>shipping>firstname,omitempty"`
	LastName  string `xml:"customer>shipping>lastname,omitempty"`
	Address   string `xml:"customer>shipping>address,omitempty"`
	State     string `xml:"customer>shipping>state,omitempty"`
	Zip       string `xml:"customer>shipping>zip,omitempty"`
	Country   string `xml:"customer>shipping>country,omitempty"`
	Phone     string `xml:"customer>shipping>phone,omitempty"`
}
type Customer struct {
	FirstName string `xml:"customer>firstname,omitempty"`
	LastName  string `xml:"customer>lastname,omitempty"`
	Email     string `xml:"customer>email,omitempty"`
	Address   string `xml:"customer>address,omitempty"`
	State     string `xml:"customer>state,omitempty"`
	Zip       string `xml:"customer>zip,omitempty"`
	Country   string `xml:"customer>country,omitempty"`
	Phone     string `xml:"customer>phone,omitempty"`
	Comment   string `xml:"customer>comment,omitempty"`
	Ip        string `xml:"customer>ip,omitempty"`
	Agent     string `xml:"customer>agent,omitempty"`
	Shipping
}
type Card struct {
	Number   string `xml:"card>number,omitempty"`
	Expmonth string `xml:"card>expmonth,omitempty"`
	Expyear  string `xml:"card>expyear,omitempty"`
	Cvv      string `xml:"card>cvv,omitempty"`
}
type Check struct {
	Aba     string `xml:"check>aba,omitempty"`
	Account string `xml:"check>acount,omitempty"`
	Type    string `xml:"check>type,omitempty"`
}
type Request struct {
	XMLName   xml.Name `xml:"request"`
	RequestId string   `xml:"request_id,omitempty"`
	Amount    int      `xml:"amount,omitempty"`
	Merchant  string   `xml:"merchant,omitempty"`
	Recurring string   `xml:"recurring,omitempty"`
	StartDate string   `xml:"start-date,omitempty"`
	Memo      `xml:"memo,omitempty"`
	Vault     bool `xml:"vault,omitempty"`
	Customer  `xml:"customer,omitempty"`
	Card      `xml:"card,omitempty"`
	Check     `xml:"check,omitempty"`
	Token     string `xml:"token,omitempty"`
}

func main() {
	url := "https://api.cornerstone.cc/v1/transactions"
	user := "sandbox_3xSOjtxSvICXVOKYqbwI"
	key := "key_RdutJGqI50YIwjehGtHBOe1Uu"

	r := &Request{Amount: 15}
	r.Customer = Customer{FirstName: "Robert", LastName: "Parr", Email: "robertp@example.com"}
	r.Card = Card{Number: "4444333322221111", Expmonth: "12", Expyear: "23"}
	b := new(bytes.Buffer)
	xml.NewEncoder(b).Encode(r)

	client := &http.Client{Timeout: time.Second * 30}
	req, _ := http.NewRequest("POST", url, b)
	req.Header.Add("Content-Type", "application/xml")
	req.SetBasicAuth(user, key)
	resp, _ := client.Do(req)

	io.Copy(os.Stdout, resp.Body)
}
