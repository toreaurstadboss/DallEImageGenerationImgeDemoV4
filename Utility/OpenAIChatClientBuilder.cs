using Azure.AI.OpenAI;
using OpenAI.Chat;
using System.ClientModel;

namespace DallEImageGenerationImageDemoV4.Utility
{

    /// <summary>
    /// Creates AzureOpenAIClient or ChatClient (default ai model (LLM) is set to "gpt-4")
    /// </summary>
    public class OpenAIChatClientBuilder(IConfiguration configuration)
    {

        private string? _endpoint = null;
        private ApiKeyCredential? _key = null;
        private readonly IConfiguration _configuration = configuration;

        /// <summary>
        /// Set the endpoint for Open AI Chat GPT-4 chat client. Defaults to config setting 'ChatGpt4:Endpoint' inside the appsettings.json file
        /// </summary>
        public OpenAIChatClientBuilder WithEndpoint(string? endpoint = null)
        {

            _endpoint = endpoint ?? _configuration["OpenAI:ChatGpt4:Endpoint"];
            return this;
        }

        /// <summary>
        /// Set the key for Open AI Chat GPT-4 chat client. Defaults to config setting 'ChatGpt4:ApiKey' inside the appsettings.json file
        /// </summary>
        public OpenAIChatClientBuilder WithApiKey(string? key = null)
        {
            string? keyToUse = key ?? _configuration["OpenAI:ChatGpt4:ApiKey"];
            if (!string.IsNullOrWhiteSpace(keyToUse))
            {
                _key = new ApiKeyCredential(keyToUse!);
            }
            return this;
        }

        /// <summary>
        /// In case the derived AzureOpenAIClient is to be used, use this Build method to get a specific AzureOpenAIClient
        /// </summary>
        /// <returns></returns>
        public AzureOpenAIClient? BuildAzureOpenAIClient() => !string.IsNullOrWhiteSpace(_endpoint) && _key != null ? new AzureOpenAIClient(new Uri(_endpoint), _key) : null;

        /// <summary>
        /// Returns the ChatClient that is set up to use OpenAI Default ai model (LLM) will be set 'gpt-4'.
        /// </summary>
        /// <returns></returns>
        public ChatClient? Build(string aiModel = "gpt-4") => BuildAzureOpenAIClient()?.GetChatClient(aiModel);

    }
}