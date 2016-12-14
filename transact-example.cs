using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.IO;
/*
*  Cornerstone API - Create Transaction Example.
*
*  06/13/2016
*/
namespace CornerstoneCreate
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Initializing..");
            // Define URL and Credentials
            string url   = "https://api.cornerstone.cc/v1/transactions";
            string user  = "sandbox_3xSOjtxSvICXVOKYqbwI";
            string key   = "key_RdutJGqI50YIwjehGtHBOe1Uu";
            string responseString = "";

            // Post Data
            var postData =  "amount=15&card[number]=4444333322221111&card[expmonth]=12&card[expyear]=23";
                postData += "&customer[firstname]=Robert&customer[lastname]=Parr&customer[email]=robertp@example.com";
            var data = Encoding.ASCII.GetBytes(postData);

            Console.WriteLine("Creating Request..");
            // Create Request and set Proper Credentials etc.
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method        = "POST";
            request.ContentType   = "application/x-www-form-urlencoded";
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

            // Write Response
            Console.WriteLine(responseString); //Uncomment this line to see output via console.
            Console.WriteLine("Request Finished..");
            response.Close();

            // Wait for 'Enter Key'
            Console.ReadLine();
        }

        /* Sets our basic auth credentials
        *
        *  @params
        *   WebRequest request   our webrequest object
        *   String     user      our username
        *   String     key       our key for auth
        */
        static void SetBasicAuth(WebRequest request, String user, String key)
        {
            string authInfo = user + ":" + key;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            request.Headers["Authorization"] = "Basic " + authInfo;
        }
    }
}

