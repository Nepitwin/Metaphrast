using System.Data;
using Prism.Mvvm;

namespace Metaphrast.Wpf.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Metaphrast Translation App";

        public DataTable Data { get; set; } = new();

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public MainWindowViewModel()
        {

            Data.Columns.Add("Key");
            Data.Columns.Add("English");
            Data.Columns.Add("German");
            Data.Columns.Add("Italia");
            Data.Columns.Add("France");

            for(int i = 0; i < 100; i++ )
                Data.Rows.Add("England", "England", "England", "Inghilterra", "Angleterre");
        }
    }
}
