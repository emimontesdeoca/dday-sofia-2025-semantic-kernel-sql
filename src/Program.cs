using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SemanticKernel;
using SemanticKernel.Plugins;

Console.WriteLine("Hello Discovery Day Sofia 2025!");

Seeder.Seed();

var endpoint = Environment.GetEnvironmentVariable("OPENAI_ENDPOINT", EnvironmentVariableTarget.User);
var apiKey = Environment.GetEnvironmentVariable("OPENAI_APIKEY", EnvironmentVariableTarget.User);
var deploymentName = "gpt-4o-mini";

var chatService = new AzureOpenAIChatCompletionService(deploymentName, endpoint!, apiKey!);

var kernelBuilder = Kernel.CreateBuilder();

// Register plugins
kernelBuilder.Plugins.AddFromType<ShoppingCartPlugin>();
kernelBuilder.Plugins.AddFromType<PaymentPlugin>();
kernelBuilder.Plugins.AddFromType<PizzaMenuPlugin>();
kernelBuilder.Plugins.AddFromType<PaymentStatusPlugin>();

var kernel = kernelBuilder.Build();

var promptSettings = new AzureOpenAIPromptExecutionSettings()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};

var systemMessage = @"You are a member of a pizza shop called PizzerIA. Follow these rules:
1. Only discuss pizza-related topics (menu, orders, payments)
2. Always ask for customer name first before handling cart/payment operations
3. Maintain separate carts for different customers
4. Verify customer name matches for payment processing
5. Provide payment IDs for future reference
6. Never answer questions unrelated to pizza orders or payments
7. Never use markdown";

var chatHistory = new ChatHistory(systemMessage);

while (true)
{
    Console.Write("Q: ");
    chatHistory.AddUserMessage(Console.ReadLine()!);

    var answer = chatService.GetStreamingChatMessageContentsAsync(chatHistory, promptSettings, kernel);

    Console.Write($"A: ");
    await foreach (var chunk in answer)
    {
        Console.Write($"{chunk.Content}");
    }

    Console.Write($"\n");
}