using OpenAI.Images;

namespace DallEImageGenerationDemo.Utility
{
    public static class DallEImageExtensions
    {

        /// <summary>
        /// Generates an image from an description in <paramref name="imagedescription"/>
        /// This uses OpenAI DALL-e-3 AI 
        /// </summary>
        /// <param name="imageClient"></param>
        /// <param name="imagedescription"></param>
        /// <param name="options">Send in options for the image generation. If no options are sent, a 512x512 natural image in response format bytes will be returned</param>
        /// <returns></returns>
        public static async Task<GeneratedImage> GenerateDallEImageAsync(this ImageClient imageClient,
            string imagedescription, ImageGenerationOptions? options = null)
        {
            options = options ?? new ImageGenerationOptions
            {
                Quality = GeneratedImageQuality.High,
                Size = GeneratedImageSize.W1024xH1024,
                Style = GeneratedImageStyle.Vivid,
            };
            options.ResponseFormat = GeneratedImageFormat.Bytes;
            return await imageClient.GenerateImageAsync(imagedescription, options);
        }

        /// <summary>
        /// Generates an image from an description in <paramref name="imagedescription"/>
        /// This uses OpenAI DALL-e-3 AI. Base-64 string is extracted from the bytes in the image for easy display of 
        /// image inside a web application (e.g. Blazor WASM)
        /// </summary>
        /// <param name="imageClient"></param>
        /// <param name="imagedescription"></param>
        /// <param name="options">Send in options for the image generation. If no options are sent, a 512x512 natural image in response format bytes will be returned</param>
        /// <returns></returns>
        public static async Task<string> GenerateDallEImageB64StringAsync(this ImageClient imageClient,
            string imagedescription, ImageGenerationOptions? options = null)
        {
            GeneratedImage generatedImage = await GenerateDallEImageAsync(imageClient, imagedescription, options);
            string preamble = "data:image/png;base64,";
            return $"{preamble}{Convert.ToBase64String(generatedImage.ImageBytes.ToArray())}";
        }

    }
}
