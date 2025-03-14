﻿using DallEImageGenerationImageDemoV4.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Resources;

namespace DallEImageGenerationImageDemoV4.Utility
{
  
    public static class EnumHelper
    {
      
        public static RenderFragment GenerateEnumDropDown<TEnum>(
            object receiver,
            TEnum selectedValue,
            Action<TEnum> valueChanged) 
            where TEnum : Enum
        {
            Expression<Func<TEnum>> onValueExpression = () => selectedValue;
            var onValueChanged = EventCallback.Factory.Create<TEnum>(receiver, valueChanged);
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
                builder.AddAttribute(2, "ValueChanged", onValueChanged);
                builder.AddAttribute(3, "ValueExpression", onValueExpression);
                builder.AddAttribute(4, "class", "form-select");  // Adding Bootstrap class for styling
                builder.AddAttribute(5, "ChildContent", (RenderFragment)(childBuilder =>
                {
                    foreach (var value in Enum.GetValues(typeof(TEnum)))
                    {
                        childBuilder.OpenElement(6, "option");
                        childBuilder.AddAttribute(7, "value", value?.ToString());
                        childBuilder.AddContent(8, GetEnumOptionDisplayText(value)?.ToString()?.Replace("_", " ")); // Ensure the display text is clean
                        childBuilder.CloseElement();
                    }
                }));
                builder.CloseComponent();
            };
        }

        /// <summary>
        /// Retrieves the display text of an enum alternative 
        /// </summary>
        private static string? GetEnumOptionDisplayText<T>(T value)
        {
            string? result = value!.ToString()!; 

            var displayAttribute = value
                .GetType()
                .GetField(value!.ToString()!)
                ?.GetCustomAttributes(typeof(DisplayAttribute), false)?
                .OfType<DisplayAttribute>()
                .FirstOrDefault();
            if (displayAttribute != null)
            {
                if (displayAttribute.ResourceType != null && !string.IsNullOrWhiteSpace(displayAttribute.Name))
                {
                    result = new ResourceManager(displayAttribute.ResourceType).GetString(displayAttribute!.Name!);                    
                }
                else if (!string.IsNullOrWhiteSpace(displayAttribute.Name))
                {
                    result = displayAttribute.Name;
                }           
            }
            return result;          
        }


    }
}
