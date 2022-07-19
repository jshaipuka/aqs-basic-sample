using System.Globalization;
using AQS_Common;

var queue = new Queue();
await queue.Init();

foreach (var _ in Enumerable.Range(1, 1000))
{
    var message = await queue.ReadMessage<Message>();
    Console.WriteLine($"Message payload timestamp: {message?.Timestamp.ToString(CultureInfo.InvariantCulture) ?? string.Empty}");

    await Task.Delay(2000);
}