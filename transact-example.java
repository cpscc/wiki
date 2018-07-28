import java.io.*;
import java.net.*;

class CornerstoneTransactionExample {
	public static void main(String[] args) {
		String url = "https://api.cornerstone.cc/v1/transactions";
		String user = "sandbox_3xSOjtxSvICXVOKYqbwI";
		String key = "key_RdutJGqI50YIwjehGtHBOe1Uu";
		String result = "";
		
		String request = 
			"amount=15&card[number]=4444333322221111&card[expmonth]=12&card[expyear]=23" +
			"&customer[firstname]=Robert&customer[lastname]=Parr&customer[email]=robertp@example.com";

		try {				
			HttpURLConnection con = (HttpURLConnection) (new URL(url)).openConnection();
			
			byte[] message = (user+":"+key).getBytes("UTF-8");
			String encoded = javax.xml.bind.DatatypeConverter.printBase64Binary(message);
						
			con.setRequestProperty("User-Agent", user);
			con.setRequestProperty("Authorization", "Basic " + encoded);
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
