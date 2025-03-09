namespace DallEImageGenerationImageDemoV4.Utility
{
    public class OpenAiChatClientBuilderFactory : IOpenAiChatClientBuilderFactory
    {
        private readonly IConfiguration _configuration;

        public OpenAiChatClientBuilderFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public OpenAIChatClientBuilder Create()
        {
            var openAiChatClient = new OpenAIChatClientBuilder(_configuration)
                .WithApiKey()
                .WithEndpoint();

            return openAiChatClient;
        }

    }
}
