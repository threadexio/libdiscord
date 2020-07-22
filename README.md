# libdiscord
A library to automate some things in Discord

### NOTE:
At this point and time there's only one function to send messages in the library, I will be updating it

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
