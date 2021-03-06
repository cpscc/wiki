using System;
using System.Text;
using System.Net;
using System.IO;

namespace CornerstoneTransactExample
{
	class Program
	{
		static void Main()
		{
			string url   = "https://api.cornerstone.cc/v1/transactions";
			string user  = "sandbox_3xSOjtxSvICXVOKYqbwI";
			string key   = "key_RdutJGqI50YIwjehGtHBOe1Uu";

			// you will probably want to create this xml
			// string using a library or class
			var postData =
				"<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
				"<request>" +
					"<amount>15</amount>" +
					"<card>" +
						"<number>4444333322221111</number>" +
						"<expmonth>12</expmonth>" +
						"<expyear>23</expyear>" +
						"<cvv>999</cvv>" +
					"</card>" +
					"<customer>" +
						"<firstname>Robert</firstname>" +
						"<lastname>Parr</lastname>" +
						"<email>robertp@example.com</email>" +
					"</customer>" +
				"</request>";
			var data = Encoding.ASCII.GetBytes(postData);

			string authInfo = user + ":" + key;
			authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method        = "POST";
			request.ContentType   = "application/xml";
			request.Accept        = "application/xml";
			request.ContentLength = data.Length;

			request.Headers["Authorization"] = "Basic " + authInfo;

			using (var stream = request.GetRequestStream())
			{
				stream.Write(data, 0, data.Length);
			}

			HttpWebResponse response = null;

			try {
				response = (HttpWebResponse)request.GetResponse();
			} catch (WebException e) {
				if (e.Status == WebExceptionStatus.ProtocolError) {
					response = (HttpWebResponse)e.Response;
					Console.Write("Errorcode: {0}\n", (int)response.StatusCode);
				} else {
					Console.Write("Error: {0}\n", e.Status);
				}
			} finally {
				if (response != null) {
					string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
					Console.WriteLine(responseString);
					response.Close();
				}
			}
		}
	}
}
