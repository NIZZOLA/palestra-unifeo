using Azure.AI.OpenAI;
using Azure;
using static System.Environment;
using System.Threading.Tasks.Dataflow;
using ConsoleAzureOpenAi;

OpenAIClient client = new(new Uri(OpenAiCredentials.Endpoint), new AzureKeyCredential(OpenAiCredentials.Key));

IList<ChatMessage> messages = new List<ChatMessage>();

messages.Add(new ChatMessage(ChatRole.System, PromptConstants.TravelPrompt));  
string question = string.Empty;
do
{
    Console.Write("Digite sua pergunta:");
    question = Console.ReadLine();
    if (question != string.Empty)
    {
        messages.Add(new ChatMessage(ChatRole.User, question));

        var chatCompletionsOptions = new ChatCompletionsOptions(messages);

        Response<ChatCompletions> response = client.GetChatCompletions(
            deploymentOrModelName: OpenAiCredentials.DeploymentName,
            chatCompletionsOptions);

        Console.WriteLine(response.Value.Choices[0].Message.Content);
        messages.Add(new ChatMessage(ChatRole.Assistant, response.Value.Choices[0].Message.Content));
    }
} while (!String.IsNullOrEmpty(question));
