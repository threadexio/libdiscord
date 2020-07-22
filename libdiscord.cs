using System.Collections.Generic;
using System.Text.Json;
using System.Net.Http;
using System.Text;

public class libdiscord
{
	// Create a public HttpClient so you can
	// access it from within the project
	public static readonly HttpClient client = new HttpClient();

	// Function to send message and return the response
	public static string SendMsg(string token, string channelID, string msg)
	{

		// Create the request content
		var content = new Dictionary<string, string>{
			{ "content" , msg },
			{ "tts" , "false"}
		};
		var json = JsonSerializer.Serialize(content);

		// The url
		string url = $"https://discordapp.com/api/v6/channels/{channelID}/messages";

		// Add the content to the request
		client.DefaultRequestHeaders.Accept.Add(
			new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
		);

		// Create the request body
		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
		request.Content = new StringContent(json, Encoding.UTF8, "application/json");

		// Add the 'authorisation' header
		request.Headers.Add("authorization", token);

		// Send the request and return the result as a string
		var response = client.SendAsync(request).Result;
		return response.ToString();

	}
}