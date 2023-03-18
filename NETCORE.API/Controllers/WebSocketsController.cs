using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCORE.Domain.Dashboard.DTO;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace NETCORE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebSocketsController : ControllerBase
    {
        private readonly ILogger<WebSocketsController> _logger;

        public WebSocketsController(ILogger<WebSocketsController> logger)
        {
            _logger = logger;
        }

        //[HttpGet("/ws")]
        //public async Task Get()
        //{
        //    if (!HttpContext.WebSockets.IsWebSocketRequest)
        //    {
        //        HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        //        return;
        //    }

        //    using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
        //    _logger.Log(LogLevel.Information, "WebSocket connection established");
        //    await Echo(webSocket);

        //}

        //private async Task Echo(WebSocket webSocket)
        //{
        //    var buffer = new byte[1024 * 4];
        //    var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        //    _logger.Log(LogLevel.Information, "Message received from Client");

        //    while (!result.CloseStatus.HasValue)
        //    {
        //        var serverMsg = Encoding.UTF8.GetBytes($"Server: Hello. You said: {Encoding.UTF8.GetString(buffer)}");
        //        await webSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);
        //        _logger.Log(LogLevel.Information, "Message sent to Client");

        //        buffer = new byte[1024 * 4];
        //        result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        //        _logger.Log(LogLevel.Information, "Message received from Client");

        //    }
        //    await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        //    _logger.Log(LogLevel.Information, "WebSocket connection closed");
        //}

        [HttpGet("/time")]
        public async Task GetTime()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using (var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync())
                {
                    bool isConnectionOpen = true;
                    var buffer = new byte[1024 * 4];
                    Task.Run(async () =>
                    {
                        WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                        while (!result.CloseStatus.HasValue)
                        {
                            string message = Encoding.UTF8.GetString(buffer).Replace("\0", "");
                            var serverMsg = Encoding.UTF8.GetBytes(message);

                            await webSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);

                            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                        }

                        if (result.CloseStatus.HasValue)
                            isConnectionOpen = false;

                    });

                    while (isConnectionOpen)
                    {
                        await webSocket.SendAsync(Encoding.ASCII.GetBytes("{ \"source\": \"Server\", \"content\": \"" + DateTime.Now + "\" }"), WebSocketMessageType.Text, true, CancellationToken.None);
                        await Task.Delay(1000);
                    }

                    if (!isConnectionOpen)
                    {
                        await webSocket.CloseAsync(WebSocketCloseStatus.Empty, "Cerrado", CancellationToken.None);
                    }
                }
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }

        [HttpGet("/dashboard")]
        public async Task GetDashboard()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using (var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync())
                {
                    bool isConnectionOpen = true;
                    var buffer = new byte[1024 * 4];
                    Task.Run(async () =>
                    {
                        WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                        while (!result.CloseStatus.HasValue)
                        {
                            //string message = Encoding.UTF8.GetString(buffer).Replace("\0", "");
                            //var serverMsg = Encoding.UTF8.GetBytes(message);

                            //await webSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);

                            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                        }

                        if (result.CloseStatus.HasValue)
                            isConnectionOpen = false;

                    });

                    while (isConnectionOpen)
                    {
                        int range = 100;
                        Random rSale = new Random();
                        Random rProfit = new Random();

                        ResponseLinechartItem item = new ResponseLinechartItem();
                        item.Label = DateTime.Now.ToString("yyyy-MM-dd");
                        item.Sale = rSale.NextDouble() * range;
                        item.Profit = rProfit.NextDouble() * range;
                        string msg = JsonSerializer.Serialize(item);

                        await webSocket.SendAsync(Encoding.ASCII.GetBytes(msg), WebSocketMessageType.Text, true, CancellationToken.None);
                        await Task.Delay(1000);
                    }

                    if (!isConnectionOpen)
                    {
                        await webSocket.CloseAsync(WebSocketCloseStatus.Empty, "Cerrado", CancellationToken.None);
                    }
                }
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }
    }
}
