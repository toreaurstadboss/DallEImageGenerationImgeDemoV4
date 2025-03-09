using DallEImageGenerationImageDemoV4.Models;
using System.ComponentModel.DataAnnotations;

namespace DallEImageGenerationDemo.Components.Pages;

public class HomeModel
{

    [Required(ErrorMessage = "Provide a description")]
    public string? Description { get; set; } = default;

    [Required(ErrorMessage = "Select image quality")]
    public ImageQuality Quality { get; set; }

    [Required(ErrorMessage = "Selected image size")]
    public ImageSize Size { get; set; }

    [Required(ErrorMessage = "Selected image style")]
    public ImageStyle Style { get; set; }

}
