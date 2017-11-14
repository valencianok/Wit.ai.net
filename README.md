# Wit.ai.net

A .Net client for Wit.ai HTTP API

## Features

* Supports .NET 4.5+
* Supports .NET Standard 2.0
* Easy installation using [NuGet](http://nuget.org/packages/Wit.ai.net) 


## Code Samples

### Simple Message example
```csharp
WitClient client = new WitClient("YOUR_WIT_TOKEN");
Message message = client.GetMessage("hello");
```

### Conversation example
```csharp
public void example()
{
    var client = new WitConversation<DemoContext>(witToken, conversationId, null,
        doMerge, doSay, doAction, doStop);
    Task<bool> t = client.SendMessageAsync("hello");
    t.Wait();

    Assert.IsTrue(t.Result && didMerge && didStop);
}

public DemoContext doMerge(string conversationId, DemoContext context, Dictionary<string, List<Entity>> entities, double confidence)
{
    didMerge = true;
    return context;
}

public void doSay(string conversationId, DemoContext context, string msg, double confidence)
{
    Console.WriteLine(msg);
}

public DemoContext doAction(string conversationId, DemoContext context, string action, Dictionary<string, List<Entity>> entities, double confidence)
{
    return context;
}

public DemoContext doStop(string conversationId, DemoContext context)
{
    didStop = true;
    return context;
}

public class DemoContext
{
    public string someField { get; set; }
}
```
