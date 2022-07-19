using AQS_Common;

var queue = new Queue();
await queue.Init();

foreach (var num in Enumerable.Range(1, 1000))
{
    await queue.SendJsonMessage(new Message($"Message #{num}", DateTime.Now));
    await Task.Delay(2000);
}