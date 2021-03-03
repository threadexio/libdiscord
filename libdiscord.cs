using System.Collections.Generic;
using System.Text.Json;
using System.Net.Http;
using System.Text;
using System.Web;

public static class libdiscord
{
	public static long ApiVer = 8;

	public static readonly string _LibVer = "v1.0.0";

	public enum STATUS
	{
		online,
		idle,
		dnd,
		invisible
	}

	private enum METHODS
    {
		GET,
		PUT,
		POST,
		PATCH,
		DELETE,
		OPTIONS
	}

	public class DiscordResponse
    {
		public int status;
		public string json;
		public string reason;
		public bool success;
		
		public DiscordResponse(HttpResponseMessage r)
        {
			this.status = (int)r.StatusCode;
			this.json = r.Content.ReadAsStringAsync().Result;
			this.reason = r.ReasonPhrase;
			this.success = r.IsSuccessStatusCode;
        }
    }

	private static DiscordResponse Send(METHODS method, string path, string json, string token)
    {
		HttpClient client;
		HttpRequestMessage request;

		string url = $"https://discord.com/api/" + path;

		// This is bad but I can't think of any other way to implement
		// multi-method support
		switch(method)
        {
			case METHODS.GET:
				client = new HttpClient();
				request = new HttpRequestMessage(HttpMethod.Get, url);
				break;

			case METHODS.PUT:
				client = new HttpClient();
				request = new HttpRequestMessage(HttpMethod.Put, url);
				break;

			case METHODS.POST:
				client = new HttpClient();
				request = new HttpRequestMessage(HttpMethod.Post, url);
				break;

			case METHODS.PATCH:
				client = new HttpClient();
				request = new HttpRequestMessage(new HttpMethod("PATCH"), url);
				break;

			case METHODS.DELETE:
				client = new HttpClient();
				request = new HttpRequestMessage(HttpMethod.Delete, url);
				break;

			case METHODS.OPTIONS:
				client = new HttpClient();
				request = new HttpRequestMessage(HttpMethod.Options, url);
				break;

			default:
				client = new HttpClient();
				request = new HttpRequestMessage(HttpMethod.Post, url);
				break;
		}

		if (json != null)
		{
			client.DefaultRequestHeaders.Accept.Add(
				new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
			);
			request.Content = new StringContent(json, Encoding.UTF8, "application/json");
		}

		// Add the 'authorisation' header
		request.Headers.Add("authorization", token);

		// Send the request and return the result as response
		return new DiscordResponse(client.SendAsync(request).Result);
	}

	public static DiscordResponse GetInfo(string token)
    {
		return Send(
			METHODS.GET,
			$"v{ApiVer}/users/@me",
			null,
			token
		);
    }

	public static DiscordResponse GetMessages(string token, long channelid)
	{
		return Send(
			METHODS.GET,
			$"v{ApiVer}/channels/{channelid.ToString()}/messages",
			null,
			token
		);
    }

	public static DiscordResponse SendMessage(string token, long channelid, string msg, bool tts = false)
	{	
		var content = new Dictionary<string, string>{
			{ "content" , msg },
			{ "tts" , $"{tts.ToString().ToLower()}"}
		};
		
		return Send(
			METHODS. POST,
            $"v{ApiVer}/channels/{channelid.ToString()}/messages",
			JsonSerializer.Serialize(content),
            token
		);
	}

	public static DiscordResponse SendFriendRequest(string token, string user)
	{
		string[] nametag = user.Split('#');

		var content = new Dictionary<string, string>
		{
			{ "username", nametag[0] },
			{ "discriminator", nametag[1] }
		};

		return Send(
			METHODS.POST,
            $"v{ApiVer}/users/@me/relationships",
			JsonSerializer.Serialize(content),
            token
		);
	}

	public static DiscordResponse JoinServer(string token, string invite)
    {
		string inviteCode = invite.Replace("https://discord.gg/", "");

		return Send(
			METHODS.POST,
            $"v{ApiVer}/invites/{inviteCode}?inputValue={HttpUtility.UrlEncode(invite)}",
            null,
            token
		);
	}

	public static DiscordResponse ChangeNickName(string token, long serverid, string nick)
    {
		var content = new Dictionary<string, string>
		{
			{ "nick", nick }
		};

		return Send(
			METHODS.PATCH,
			$"v{ApiVer}/guilds/{serverid.ToString()}/members/@me/nick",
			JsonSerializer.Serialize(content),
			token
		);
	}

	public static DiscordResponse ChangeStatus(string token, STATUS status)
    {
		var content = new Dictionary<string, string>
		{
			{ "status", status.ToString() }
		};

		return Send(
			METHODS.PATCH,
			$"v{ApiVer}/users/@me/settings",
			JsonSerializer.Serialize(content),
			token
		);
	}

	public static DiscordResponse ChangeStatusMessage(string token, string msg)
    {
		var content = new Dictionary<string, Dictionary<string, string>>
		{
			{ "custom_status", new Dictionary<string, string> { { "text", msg } } }
		};

		return Send(
			METHODS.PATCH,
			$"v{ApiVer}/users/@me/settings",
			JsonSerializer.Serialize(content),
			token
		);
		
	}

	public static DiscordResponse ChangeSetting(string token, Dictionary<string, string> content)
    {
		return Send(
			METHODS.PATCH,
			$"v{ApiVer}/users/@me/settings",
			JsonSerializer.Serialize(content),
			token
		);
    }

	public static DiscordResponse DeleteMessage(string token, long channelid, long messageid)
    {
		return Send(
			METHODS.DELETE,
			$"v{ApiVer}/channels/{channelid.ToString()}/messages/{messageid.ToString()}",
			null,
			token
		);
    }
}