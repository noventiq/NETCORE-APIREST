// See https://aka.ms/new-console-template for more information
using Websocket.Client;

Console.WriteLine("Hello, World!");
string streaming_API_Key = "your_api_key";
Console.CursorVisible = false;

try
{
    var exitEvent = new ManualResetEvent(false);
    var url = new Uri("wss://localhost:7078/ws");

    using (var client = new WebsocketClient(url))
    {
        client.ReconnectTimeout = TimeSpan.FromSeconds(30);
        client.ReconnectionHappened.Subscribe(info =>
        {
            Console.WriteLine("Reconnection happened, type: " + info.Type);
        });
        client.MessageReceived.Subscribe(msg =>
        {
            Console.WriteLine("Message received: " + msg);
            if (msg.ToString().ToLower() == "connected")
            {
                string data = "{\"userKey\":\"" + streaming_API_Key + "\", \"symbol\":\"EURUSD,GBPUSD,USDJPY\"}";
                client.Send(data);
            }
        });
        client.Start();
        //Task.Run(() => client.Send("{ message }"));
        exitEvent.WaitOne();
    }
}
catch (Exception ex)
{
    Console.WriteLine("ERROR: " + ex.ToString());
}
Console.ReadKey();