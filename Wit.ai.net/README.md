# Wit.ai.net

A .Net client for Wit.ai HTTP API

## Features

* Supports .NET 4.5+
* Easy installation using [NuGet](http://nuget.org/packages/Wit.ai.net) 

## Code Samples

### Simple Message example
```csharp
WitClient client = new WitClient(Properties.Settings.Default.WitToken);
Message message = client.GetMessage("hello");client.DownloadData(request).SaveAs(path);
```

### Conversation example
```csharp
public void example()
{
    WitConversation client = new WitConversation(Properties.Settings.Default.WitToken, conversationId, doMerge, doSay, doAction, doStop);
    Task<bool> t = client.SendMessageAsync("hello");
    t.Wait();
    // do something
}

public object doMerge(string conversationId, object context, object entities, double confidence)
{
    // do something
    return context;
}

public void doSay(string conversationId, object context, string msg, double confidence)
{
    // do something
    // Console.WriteLine(msg);
}

public object doAction(string conversationId, object context, string action, double confidence)
{
    // do something
    return context;
}

public object doStop(string conversationId, object context)
{
    // do something
    return context;
}
```