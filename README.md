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

### Classes

`DiscordResponse`: Returned by every function in the library

<table>

<tr>
<td>Type</td>
<td>Property</td>
<td>Description</td>
<td>Example</td>
</tr>

<tr>
<td>int</td>
<td>status</td>
<td>HTTP Status code</td>
<td>200, 301, 404</td>
</tr>

<tr>
<td>string</td>
<td>json</td>
<td>Returned JSON</td>
<td>{id:"231244"...}</td>
</tr>

<tr>
<td>string</td>
<td>reason</td>
<td>HTTP Reason Phrase</td>
<td>Unavailable For Legal Reasons</td>
</tr>

<tr>
<td>bool</td>
<td>reason</td>
<td>True if we have a successful response</td>
<td>true, false</td>
</tr>

</table>

----------------

### Enums:

`libdiscord.STATUS`: Contains a list of the valid statuses (online, idle, dnd, invisible)

----------------

### Functions:

`libdiscord.GetInfo(token)`: Gets information regarding an account.

#### Example:
```c#
string token = "xxxxxxxxxxxxxxxxxxxxxxxxxxx";

Console.WriteLine( libdiscord.GetInfo(token).json );
```
Formatted Output:
```json
{
    "id":"123456789123456789",
    "username":"Awesome Username",
    "avatar":"3218dc3db615c97a4b1afd4382689c5e",
    "discriminator":"1337",
    "public_flags":256,
    "flags":304,
    "locale":"en-US",
    "nsfw_allowed":false,
    "mfa_enabled":true,
    "email":"user@example.com",
    "verified":true,
    "phone":"+15417543010"
}
```
----------------
`libdiscord.GetMessages(token, channelid)`: Gets the last 50 messages from that channel

#### Example:
```c#
string token = "xxxxxxxxxxxxxxxxxxxxxxxxxxx";

Console.WriteLine( libdiscord.GetMessages(token, 810770364327824865).status );
```
Formatted Output:
```json
200
```
----------------
`libdiscord.SendMessage(token, channelid, msg, tts=false)`: Sends a message on the specified channel

#### Example:
```c#
string token = "xxxxxxxxxxxxxxxxxxxxxxxxxxx";

// No TTS
Console.WriteLine( libdiscord.SendMessages(token, 810770364327824865, "this is my message" ).reason );

// or with TTS
Console.WriteLine( libdiscord.SendMessages(token, 810770364327824865, "this is my message", true ).reason );
```
Formatted Output:
```json
Unauthorized
```
----------------
`libdiscord.SendFriendRequest(token, user)`: Gets the last 50 messages from that channel

#### Example:
```c#
string token = "xxxxxxxxxxxxxxxxxxxxxxxxxxx";

Console.WriteLine( libdiscord.SendFriendRequest(token, "Awesome Username#0000" ).success );
```
Formatted Output:
```json
True
```
----------------
`libdiscord.JoinServer(token, invite)`: Joins a server with the specified invite

#### Example:
```c#
string token = "xxxxxxxxxxxxxxxxxxxxxxxxxxx";

Console.WriteLine( libdiscord.JoinServer(token, "https://discord.gg/Gzh1TvX4" ).status );
```
Formatted Output:
```json
301
```
----------------
`libdiscord.ChangeNickName(token, serverid, nick)`: Changes the account's nickname on the specified server

#### Example:
```c#
string token = "xxxxxxxxxxxxxxxxxxxxxxxxxxx";

Console.WriteLine( libdiscord.ChangeNickName(token, 761434776463465469, "this is my new nick" ).json );
```
Formatted Output:
```json
{
	"nick": "this is my new nick"
}
```
----------------
`libdiscord.ChangeStatus(token, status)`: Changes the account's status (online, idle, dnd, invisible)

#### Example:
```c#
string token = "xxxxxxxxxxxxxxxxxxxxxxxxxxx";

// See the ENUMs section for details
Console.WriteLine( libdiscord.ChangeStatus(token, libdiscord.STATUS.online ).reason );
```
Formatted Output:
```json
OK
```
----------------
`libdiscord.ChangeStatusMessage(token, msg)`: Changes the account's status message

#### Example:
```c#
string token = "xxxxxxxxxxxxxxxxxxxxxxxxxxx";

Console.WriteLine( libdiscord.ChangeStatusMessage(token, "this is my custom status" ).success );
```
Formatted Output:
```json
True
```
----------------
`libdiscord.ChangeSetting(token, content)`: Changes settings for the account

#### Example:
```c#
string token = "xxxxxxxxxxxxxxxxxxxxxxxxxxx";

var settings = new Dictionary<string, string> {
	"developer_mode": "true"
};

Console.WriteLine( libdiscord.ChangeSetting(token, settings).json );
```
Formatted Output:
```json
{"locale": "en-US", "show_current_game": true, "restricted_guilds": [], "default_guilds_restricted": false, "inline_attachment_media": true, "inline_embed_media": true, ...
```
----------------
`libdiscord.DeleteMessage(token, channelid, messageid)`: Deletes a specific message from the specified channel

#### Example:
```c#
string token = "xxxxxxxxxxxxxxxxxxxxxxxxxxx";

Console.WriteLine( libdiscord.DeleteMessage(token, 810770364327824865, 216379612442636839).reason );
```
Formatted Output:
```json
Unauthorized
```
----------------

### JSON Response Docs - https://discord.com/developers/docs/intro

----------------

**Important**
-------------
Please keep in mind that I do NOT work for Discord, and this is my attempt at making a library that interfaces with their API, this is all just based on my findings with the API.