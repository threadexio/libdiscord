# libdiscord
A library to automate some things in Discord

## How To use
On Visual Studio 2019:
**Project -> Add Project Refrence... -> Browse** and find `libdiscord.dll`

And you're done...
To access the library's functions use `libdiscord.<the function>(<parameters>)`

Also you don't need to add `using libdiscord;`

## Documentation
----------------
#### libdiscord.SendMsg

Sends a message to `channelID` and returns the response

##### Parameters:
`token`: the token of the account used to send messages

`channelID`: the ID of the channel where messages are sent

`msg`: the message to send

##### Example:
```Console.Write("Message: ");
string input = Console.ReadLine();
string response = libdiscord.SendMsg("<example token>", "<example id>", input);
Console.WriteLine(response);
```
----------------
#### libdiscord.SendFriendRequest

Sends a friend request to the specified user

##### Parameters:
`token`: the token of the account used to send the friend request

`user`: the name and tag of the user where the request is sent

##### Example:
```Console.Write("User: ");
string input = Console.ReadLine();
string response = libdiscord.SendFriendRequest("<example token>", input);
Console.WriteLine(response);
```
----------------
#### libdiscord.JoinServer

Tells the account to join a server with an invite

##### Parameters:
`token`: the token of the account which will join the server

`invite`: the invite link from the server

##### Example:
```Console.Write("Invite: ");
string input = Console.ReadLine();
string response = libdiscord.JoinServer("<example token>", input);
Console.WriteLine(response);
```

# Misc functions

----------------
#### libdiscord.POST

Sends a post request to the specified url

##### Parameters:
`url`: the url where the request will be sent

`content`: request payload

__Tip__: You can use `null` so you don't send a payload

`token`: the token of the account to perform the action on

##### Example: Turning on developer mode
```string url = "https://discord.com/api/v6/users/@me/settings";

var contents = new Dictionary<string, string>{
  { "developer_mode", "true" }
};

string response = libdiscord.POST(url, contents, token);
Console.WriteLine(response);
```
