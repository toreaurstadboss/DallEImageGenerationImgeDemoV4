using DallEImageGenerationImageDemoV4;
using DallEImageGenerationImageDemoV4.Utility;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OpenAI.Chat;
using OpenAI.Images;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    string imageModelName = "dall-e-3";
    return new ImageClient(imageModelName, config["OpenAI:DallE3:ApiKey"]);
});

builder.Services.AddSingleton<IOpenAiChatClientBuilderFactory, OpenAiChatClientBuilderFactory>();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

await builder.Build().RunAsync();
