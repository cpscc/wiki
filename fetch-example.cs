using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.IO;
/*
*  Cornerstone REST API Example.
*
*  This example is to provide a means of fetching transactions
*  given parameters passed via the GET request method.
*
*  06/13/2016
*/
namespace CornerStoneAPI
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Initializing..");
            // Define URL and Credentials
            string url   = "https://bridgewell.cornerstone.cc/v1/transactions?";
            string param = "range=06/11/2016-06/13/2016&show_test=true";
            string user  = "client_id";
            string key   = "client_key";
            string responseString = "";

            Console.WriteLine("Creating Request..");
            // Create Request and set Proper Credentials etc.
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url+param);
            request.Method = "GET";
            request.Accept = "application/json";
            SetBasicAuth(request, user, key);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Console.WriteLine("Receiving..");

            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                responseString = reader.ReadToEnd();
            }

            // Write Response
            //Console.WriteLine(responseString); //Uncomment this line to see output via console.
            Console.WriteLine("Request Finished..");

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
