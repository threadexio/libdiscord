using System.Collections.Generic;
using System.Text.Json;
using System.Net.Http;
using System.Text;
using System.Web;
using System;

public class libdiscord
{
	// Create a public HttpClient so you can
	// access it from within the project
	public static readonly HttpClient client = new HttpClient();

	public static string POST(string url, Dictionary<string, string> content, string token)
    {

		// Create the request
		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

		if (content != null)
		{
			// Make the directory into json
			var json = JsonSerializer.Serialize(content);

			// Add the content to the request
			client.DefaultRequestHeaders.Accept.Add(
				new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
			);
			request.Content = new StringContent(json, Encoding.UTF8, "application/json");
		}

		// Add the 'authorisation' header
		request.Headers.Add("authorization", token);

		// Send the request and return the result as a string
		var response = client.SendAsync(request).Result;
		return response.ToString();
	}

	// Function to send message and return the response
	public static string SendMsg(string token, string channelID, string msg)
	{
		string url = $"https://discordapp.com/api/v6/channels/{channelID}/messages";
		
		// Create the request content
		var content = new Dictionary<string, string>{
			{ "content" , msg },
			{ "tts" , "false"}
		};
		
		return POST(url, content, token);
	}

	public static string SendFriendRequest(string token, string user)
	{
		string url = "https://discord.com/api/v6/users/@me/relationships";

		string[] nametag = user.Split('#');

		var content = new Dictionary<string, string>
		{
			{ "username", nametag[0] },
			{ "discriminator", nametag[1] }
		};

		return POST(url, content, token);
    }

	public static string JoinServer(string token, string invite)
    {
		string inviteCode = invite.Replace("https://discord.gg/", "");

		string url = $"https://discord.com/api/v6/invites/{inviteCode}?inputValue={HttpUtility.UrlEncode(invite)}";

		return libdiscord.POST(url, null, token);
	}
}