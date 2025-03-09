using DallEImageGenerationDemo.Components.Pages;
using DallEImageGenerationDemo.Utility;
using DallEImageGenerationImageDemoV4.Models;
using DallEImageGenerationImageDemoV4.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OpenAI.Images;
using OpenAIDemo;

namespace DallEImageGenerationImageDemoV4.Pages;

public partial class Home : ComponentBase
{

    [Inject]
    public required IConfiguration Config { get; set; }

    [Inject]
    public required IJSRuntime JSRuntime { get; set; }

    [Inject]
    public required ImageClient DallEImageClient { get; set; }

    [Inject]
    public required IOpenAiChatClientBuilderFactory OpenAIChatClientFactory { get; set; }

    private readonly HomeModel homeModel = new();

    private bool IsLoading { get; set; }

    private string ImageData { get; set; } = string.Empty;

    private const string modelName = "dall-e-3";

    protected async Task HandleGenerateText()
    {
        var openAiChatClient = OpenAIChatClientFactory.Create().Build();
        if (openAiChatClient == null)
        {
            await JSRuntime.InvokeAsync<string>("alert", "Sorry, the OpenAI Chat client did not initiate properly. Cannot generate text.");
            return;
        }

        string description = """
            You are specifying instructions for generating a DALL-e-3 image.
            Do not always choose Bergen! Also choose among smaller cities, villages and different locations in Norway.
             Just generate one image, not a montage. Only provide one suggestion. 
             The suggestion should be based from this input and randomize what to display: 
             Suggests a cozy vivid location set in Norway showing outdoor scenery in good weather at different places 
             and with nice weather aimed to attract tourists. Note - it should also display both urban,
             suburban or nature scenery with a variance of which of these three types of locations to show.
             It should also include some Norwegian animals and flowers and show people. It should pick random cities and places in Norway to display.
             Suggest places from different counties, like Larvik, Steinkjer, Oppdal and nature places such as Femunden and Glomma.
""";
            

        homeModel.Description = string.Empty; 

        await foreach (var updateContentPart in openAiChatClient.GetStreamedReplyStringAsync(description))
        {
            homeModel.Description += updateContentPart;            
            StateHasChanged();
            await Task.Delay(20);
        }
    }

    protected async Task HandleValidSubmit()
    {
        IsLoading = true;

        Console.WriteLine($"Image style: {homeModel.Style}, Image size: {homeModel.Size} Quality: {homeModel.Quality}");

        string generatedImageBase64 = await DallEImageClient.GenerateDallEImageB64StringAsync(homeModel.Description!,
            new ImageGenerationOptions
            {
                Quality = MapQuality(homeModel.Quality),
                Style = MapStyle(homeModel.Style),
                Size = MapSize(homeModel.Size)
            });
        ImageData = generatedImageBase64;

        if (!string.IsNullOrWhiteSpace(ImageData))
        {
            // Open the modal
            await JSRuntime.InvokeVoidAsync("showModal", "imageModal");
        }

        IsLoading = false;
        StateHasChanged();
    }

    private static GeneratedImageSize MapSize(ImageSize size) => size switch
    {
        ImageSize.W1024xH1792 => GeneratedImageSize.W1024xH1792,
        ImageSize.W1792H1024 => GeneratedImageSize.W1792xH1024,
        _ => GeneratedImageSize.W1024xH1024,
    };

    private static GeneratedImageStyle MapStyle(ImageStyle style) => style switch
    {
        ImageStyle.Vivid => GeneratedImageStyle.Vivid,
        _ => GeneratedImageStyle.Natural
    };

    private static GeneratedImageQuality MapQuality(ImageQuality quality) => quality switch
    {
        ImageQuality.High => GeneratedImageQuality.High,
        _ => GeneratedImageQuality.Standard
    };

}
