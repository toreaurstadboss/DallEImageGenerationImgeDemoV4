using DallEImageGenerationImageDemoV4.Resources;
using System.ComponentModel.DataAnnotations;

namespace DallEImageGenerationImageDemoV4.Models
{

    public enum ImageQuality
    {
        [DisplayAttribute(Name = "High image quality")]
        High,

        [DisplayAttribute(Name = "StandardImgQuality", ResourceType = typeof(EnumResources))]
        Standard,
    }

    public enum ImageSize
    {
        [Display(Name = "W1024xH1024")]
        W1024xH1024,

        [Display(Name = "W1024xH1792")]
        W1024xH1792,

        [Display(Name = "W1024xH1792")]
        W1792H1024
    }

    public enum ImageStyle
    {
        [DisplayAttribute(Name = "VividImgStyle", ResourceType = typeof(EnumResources))]
        Vivid,

        [DisplayAttribute(Name = "NaturalImgStyle", ResourceType = typeof(EnumResources))]
        Natural
    }


}
