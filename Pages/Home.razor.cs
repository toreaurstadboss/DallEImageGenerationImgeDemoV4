using DallEImageGenerationDemo.Components.Pages;
using DallEImageGenerationDemo.Utility;
using DallEImageGenerationImageDemoV4.Models;
using Microsoft.AspNetCore.Components;
using OpenAI.Images;
using System.Data;

namespace DallEImageGenerationImageDemoV4.Pages;

public partial class Home : ComponentBase
{

    private HomeModel homeModel = new HomeModel();

    [Inject]
    public required IConfiguration Config { get; set; }

    public required ImageClient imageClient;

    private string ImageData { get; set; } = string.Empty;

    protected override void OnInitialized()
    {
        imageClient = imageClient ?? new ImageClient(modelName, Config["OpenAI:ApiKey"]);
    }

    private const string modelName = "dall-e-3";

    protected async Task HandleValidSubmit()
    {
        string generatedImageBase64 = await imageClient.GenerateDalleImageBass64JsonAsync(homeModel.Description!,
            new ImageGenerationOptions
            {
                Quality = MapQuality(homeModel.Quality),
                Style = MapStyle(homeModel.Style),
                Size = MapSize(homeModel.Size)                
            });
        ImageData = generatedImageBase64;
    }

    private GeneratedImageSize MapSize(ImageSize size) => size switch
    {
        ImageSize.W1024xH1792 => GeneratedImageSize.W1024xH1792,
        ImageSize.W1792H1024 => GeneratedImageSize.W1792xH1024,
        _ => GeneratedImageSize.W1024xH1024,
    };

    private GeneratedImageStyle MapStyle(ImageStyle style) => style switch
    {
        ImageStyle.Vivid => GeneratedImageStyle.Vivid,
        _ => GeneratedImageStyle.Natural
    };

    private GeneratedImageQuality MapQuality(ImageQuality quality) => quality switch
    {
        ImageQuality.High => GeneratedImageQuality.High,
        _ => GeneratedImageQuality.Standard
    };

}
