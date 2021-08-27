using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WaspApp.Extensions
{
    public class ScreenMarckupExtension : IMarkupExtension<double>
    {
        public double FullValue { get; set; }
        public double RequiredPercentage { get; set; }
        public double AddValueToResult { get; set; } = 0;
        public double ProvideValue(IServiceProvider serviceProvider)
        {
            return (RequiredPercentage * FullValue * .01) + AddValueToResult;
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<double>).ProvideValue(serviceProvider);
        }
    }

    public class ThicknessExtension : IMarkupExtension<Thickness>
    {
        public double Left { get; set; } = 0;
        public double Right { get; set; } = 0;
        public double Up { get; set; } = 0;
        public double Bottom { get; set; } = 0;

        public Thickness ProvideValue(IServiceProvider serviceProvider)
        {
            return new Thickness(Left, Up, Right, Bottom);
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<Thickness>).ProvideValue(serviceProvider);
        }
    }

    public class MathMinMarckupExtension : IMarkupExtension<double>
    {
        public double Value1 { get; set; }
        public double Value2 { get; set; }
        public double ProvideValue(IServiceProvider serviceProvider)
        {
            return Math.Min(Value1, Value2);
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<double>).ProvideValue(serviceProvider);
        }
    }

    public class MathMaxMarckupExtension : IMarkupExtension<double>
    {
        public double Value1 { get; set; }
        public double Value2 { get; set; }
        public double ProvideValue(IServiceProvider serviceProvider)
        {
            return Math.Max(Value1, Value2);
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<double>).ProvideValue(serviceProvider);
        }
    }

    public class OnOrientationExtension : IMarkupExtension
    {
        public object Portrait { get; set; }
        public object Landscape { get; set; }

        public IValueConverter Converter { get; set; }
        public object ConverterParameter { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
			object value;
			var orientation = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Orientation;
			if (orientation == Xamarin.Essentials.DisplayOrientation.Landscape)
				value = Landscape;
			else
				value = Portrait;

            var valueProvider = serviceProvider?.GetService<IProvideValueTarget>() ?? throw new ArgumentException();

            BindableProperty bp;
            PropertyInfo pi = null;
            if (valueProvider.TargetObject is Setter setter)
            {
                bp = setter.Property;
            }
            else
            {
                bp = valueProvider.TargetProperty as BindableProperty;
                pi = valueProvider.TargetProperty as PropertyInfo;
            }
            Type propertyType = bp?.ReturnType
                  ?? pi?.PropertyType
                  ?? throw new InvalidOperationException("Cannot determine property to provide the value for.");

            if (Converter != null)
                return Converter.Convert(value, propertyType, ConverterParameter, CultureInfo.CurrentUICulture);
            else
                return value;
        }
    }
}
