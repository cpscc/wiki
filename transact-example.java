import java.io.*;
import java.net.*;

class CornerstoneTransactionExample {
	public static void main(String[] args) {
		String url = "https://api.cornerstone.cc/v1/transactions";
		String user = "sandbox_3xSOjtxSvICXVOKYqbwI";
		String key = "key_RdutJGqI50YIwjehGtHBOe1Uu";
		String result = "";
		
		// you will likely want to use something like jaxb or xstream
		// to create xml and possibly write it to the stream
		String request = 
		"<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
		"<request>" +
			"<amount>15</amount>" +
			"<card>" +
				"<number>4444333322221111</number>" +
				"<expmonth>12</expmonth>" +
				"<expyear>23</expyear>" +
			"</card>" +
			"<customer>" +
				"<firstname>Robert</firstname>" +
				"<lastname>Parr</lastname>" +
				"<email>robertp@example.com</email>" +
			"</customer>" +
		"</request>";

		try {				
			HttpURLConnection con = (HttpURLConnection) (new URL(url)).openConnection();
			
			byte[] message = (user+":"+key).getBytes("UTF-8");
			String encoded = javax.xml.bind.DatatypeConverter.printBase64Binary(message);
						
			con.setRequestProperty("User-Agent", user);
			con.setRequestProperty("Authorization", "Basic " + encoded);
			con.setRequestProperty("Accept", "application/xml");
			con.setRequestProperty("Content-Type", "application/xml");

			con.setRequestMethod("POST");
			con.setDoOutput(true);
			
			DataOutputStream out = new DataOutputStream(con.getOutputStream());
			out.writeBytes(request);
			out.flush();
			out.close();
			
			InputStream stream = con.getResponseCode() < 400 ? con.getInputStream() : con.getErrorStream();
						
			BufferedReader buf = new BufferedReader(new InputStreamReader(stream));
			
			String line;
			while ((line = buf.readLine()) != null) {
				result += line + "\n";
			}

			System.out.println(result);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
}
