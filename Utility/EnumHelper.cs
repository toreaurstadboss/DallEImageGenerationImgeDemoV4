using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;

namespace DallEImageGenerationImageDemoV4.Utility
{
    public static class EnumHelper
    {
        public static RenderFragment GenerateEnumDropDown<TEnum>(TEnum selectedValue,
            EventCallback<TEnum> valueChanged,
            Expression<Func<TEnum>> valueExpression) where TEnum : Enum
        {
            return builder =>
            {
                // Set the selectedValue to the first enum value if it is not set
                if (EqualityComparer<TEnum>.Default.Equals(selectedValue, default))
                {
                    object? firstEnum = Enum.GetValues(typeof(TEnum)).GetValue(0);
                    if (firstEnum != null)
                    {
                        selectedValue = (TEnum)firstEnum;
                    }
                }

                builder.OpenComponent<InputSelect<TEnum>>(0);
                builder.AddAttribute(1, "Value", selectedValue);
                builder.AddAttribute(2, "ValueChanged", valueChanged);
                builder.AddAttribute(3, "ValueExpression", valueExpression);
                builder.AddAttribute(4, "class", "form-select");  // Adding Bootstrap class for styling
                builder.AddAttribute(5, "ChildContent", (RenderFragment)(childBuilder =>
                {
                    foreach (var value in Enum.GetValues(typeof(TEnum)))
                    {
                        childBuilder.OpenElement(6, "option");
                        childBuilder.AddAttribute(7, "value", value?.ToString());
                        childBuilder.AddContent(8, value?.ToString()?.Replace("_", " ")); // Ensure the display text is clean
                        childBuilder.CloseElement();
                    }
                }));
                builder.CloseComponent();
            };
        }
    }
}
