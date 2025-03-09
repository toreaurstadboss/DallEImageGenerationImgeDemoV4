using OpenAI.Chat;
using System.ClientModel;

namespace OpenAIDemo
{
    public static class OpenAIChatClientExtensions
    {

        /// <summary>
        /// Provides a stream result from the Chatclient service using AzureAI services.
        /// </summary>
        /// <param name="chatClient">ChatClient instance</param>
        /// <param name="message">The message to send and communicate to the ai-model</param>
        /// <param name="systemMessage">Set the system message to instruct the chat response. Defaults to 'You are an helpful, wonderful AI assistant'.</param>
        /// <returns>Streamed chat reply / result. Consume using 'await foreach'</returns>
        public static async IAsyncEnumerable<string?> GetStreamedReplyStringAsync(this ChatClient chatClient, string message, string? systemMessage = null)
        {
            await foreach (var update in GetStreamedReplyInnerAsync(chatClient, message, systemMessage))
            {
                foreach (var textReply in update.ContentUpdate.Select(cu => cu.Text))
                {
                    yield return textReply;
                }
            }
        }

        private static AsyncCollectionResult<StreamingChatCompletionUpdate> GetStreamedReplyInnerAsync(this ChatClient chatClient, string message, string? systemMessage = null) =>
            chatClient.CompleteChatStreamingAsync(
                [new SystemChatMessage(systemMessage ?? "You are an helpful, wonderful AI assistant"), new UserChatMessage(message)]);


    }
}