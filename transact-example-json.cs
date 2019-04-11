using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
/*
*  Cornerstone API - Create Transaction Example Utilizing JSON in POST
*  requires Newtonsoft.Json for Serializing and deserializing objects.
*  Newtonsoft.Json can be found in the NuGet package manager.
*  
*  04/10/2019
*/

namespace CornerstoneCreateJson
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Initializing..");
            // Define URL and Credentials
            string url = "https://api.cornerstone.cc/v1/transactions";
            string user = "";
            string key = "";
            string responseString = "";

            // create transaction object
            Transaction transaction = TestDataCC();
            // Serialize object to json
            string postData = JsonConvert.SerializeObject(transaction);
            var data = Encoding.ASCII.GetBytes(postData);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            SetBasicAuth(request, user, key);

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            // receiving response
            Console.WriteLine("Receiving..");
            var response = (HttpWebResponse)request.GetResponse();
            responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            Console.WriteLine(responseString); //Uncomment this line to see output via console.
            Console.WriteLine("Request Finished..");
            response.Close();

            // Wait for 'Enter Key'
            Console.ReadLine();        
}

        static void SetBasicAuth(WebRequest request, String user, String key)
        {
            string authInfo = user + ":" + key;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            request.Headers["Authorization"] = "Basic " + authInfo;
        }





        /// <summary>
        /// Trasaction Data
        /// </summary>            
        /// <returns>Transaction object</returns>
        public static Transaction TestDataCC()
        {
            Transaction transaction = new Transaction
            {
                amount = "15",
                customer = new Customer
                {
                    firstname = "Robert",
                    lastname = "Parr",
                    email = "robertp@example.com"
                },
                card = new Card
                {
                    number = "4111111111111111",
                    expmonth = "12",
                    expyear = "24",
                    cvv = "123"
                }

            };

            return transaction;

        }


        /// <summary>
        /// Test Data recurring payment
        /// </summary>
        /// <returns>Transaction object</returns>
        public static Transaction TestDataCCRec()
        {
            Transaction transaction = new Transaction
            {
                amount = "15",
                recurring = "monthly",
                startdate = "04/09/2019",
                customer = new Customer
                {
                    firstname = "Robert",
                    lastname = "Parr",
                    email = "robertp@example.com"
                },
                card = new Card
                {
                    number = "4111111111111111",
                    expmonth = "12",
                    expyear = "24",
                    cvv = "123"
                }

            };
            return transaction;
        }

       
        /// <summary>
        /// Transaction class with Newtonsoft.Json JsonProperty to omit cornerstone api parameters that are null for the json request.
        /// </summary>
        public class Transaction
        {
            [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
            public string request_id { get; set; }
            public string amount { get; set; }
            public Customer customer { get; set; }

            [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
            public Card card { get; set; }

            [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
            public Check check { get; set; }

            [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
            public string merchant { get; set; }
           
            [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
            public string recurring { get; set; }
            
            [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, PropertyName = "start-date")]
            public string startdate { get; set; }
                 
            [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
            public string token { get; set; }
        }

        public class Customer
        {
            public string firstname { get; set; }
            public string lastname  { get; set; }
            public string email     { get; set; }
            public string address   { get; set; }
            public string state     { get; set; }
            public string zip       { get; set; }
            public string country   { get; set; }
            public string phone     { get; set; }
            public string comment   { get; set; }
            public string ip        { get; set; }
            public string agent     { get; set; }

        }

        public class Card
        {
            public string number { get; set; }
            public string expmonth { get; set; }
            public string expyear { get; set; }
            public string cvv { get; set; }
        }

        public class Check
        {
            public string aba { get; set; }
            public string account { get; set; }
            public string type { get; set; } 
        }
    }
}
