using Azure;
using Azure.Storage.Queues;
using Newtonsoft.Json;

namespace AQS_Common;

public class Queue
{
    // local
    private const string AzureStorageConnectionString = "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;";
    private const string QueueName = "test-local-queue";

    private readonly QueueClient _queue;

    public Queue()
    {
        _queue = new QueueClient(AzureStorageConnectionString, QueueName);
    }

    public async Task Init()
    {
        await _queue.CreateIfNotExistsAsync();
    }

    public async Task SendMessage(string message)
    {
        try
        {
            await _queue.SendMessageAsync(message);
        }
        catch (RequestFailedException ex)
        {
            Console.WriteLine(ex.ErrorCode);
            throw;
        }
    }

    public async Task SendJsonMessage<T>(T json)
    {
        var message = JsonConvert.SerializeObject(json);
        await SendMessage(message);
        Console.WriteLine($"Sent JSON: {message}");
    }

    public async Task<string?> ReadAndDeleteOneMessage()
    {
        try
        {
            var message = (await _queue.ReceiveMessageAsync())?.Value;

            if (message != null) await _queue.DeleteMessageAsync(message.MessageId, message.PopReceipt);

            return message?.MessageText;
        }
        catch (RequestFailedException ex)
        {
            Console.WriteLine(ex.ErrorCode);
            throw;
        }
    }

    public async Task<T?> ReadMessage<T>()
    {
        var message = await ReadAndDeleteOneMessage();
        Console.WriteLine(message == null ? "No new messages" : $"Read: {message}");
        return TryParseJson<T>(message);
    }

    private static T? TryParseJson<T>(string? json = "")
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(json!);
        }
        catch
        {
            // ignored
        }

        return default;
    }
}