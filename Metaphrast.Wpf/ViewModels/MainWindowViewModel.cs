using Prism.Mvvm;

namespace Metaphrast.Wpf.ViewModels
{
    internal class MainWindowViewModel : BindableBase
    {
        public static string Title
        {
            get => Resources.strings.Title;
        }
    }
}
