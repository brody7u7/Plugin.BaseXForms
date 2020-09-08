using Plugin.BaseXForms.Content.Resources;
using Plugin.BaseXForms.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.BaseXForms.Extensions
{
    public enum AlertType
    {
        Information,
        Warning,
        Error
    }

    public static class ViewModelBaseExtensions
    {
        public static async Task DisplayAlert(this ViewModelBase vm, AlertType alertType, string message)
        {
            string title;
            switch (alertType)
            {
                case AlertType.Error: title = Labels.Error; break;
                case AlertType.Warning: title = Labels.Warning; break;
                case AlertType.Information: title = Labels.Information; break;
                default: title = Labels.Information; break;
            }
            await DisplayAlert(vm, title, message);
        }

        public static async Task DisplayAlert(this ViewModelBase vm, string title, string message)
        {
            await vm.Context.DisplayAlert(title, message, Labels.OK);
        }
    }
}
