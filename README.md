# Simple OAuth for .NET Core
[![Build Status](https://travis-ci.org/AlexBulankou/SimpleOAuth.Net.svg?branch=master)](https://travis-ci.org/AlexBulankou/SimpleOAuth.Net)    

Example:
```
var tokens = new Tokens() { ConsumerKey = "key", ConsumerSecret = "secret",
	AccessKey = "key", AccessKeySecret = "secret" };
var request = WebRequest.Create("https://api.twitter.com/1/statuses/home_timeline.json?count=5");
request.SignRequest()
	.WithTokens(tokens)
	.InHeader();
```

Or, you can condense your <code>WithTokens</code> call into the <code>SignRequest</code>

```
request.SignRequest(tokens).InHeader();
```
