using System.Collections.Generic;
using System.Data;
using Prism.Mvvm;

namespace Metaphrast.Wpf.ViewModels
{
    public class Model
    {
        public string Name { get; set; }
        public List<string> Values { get; set; } = new();
    }

    public class MainWindowViewModel : BindableBase
    {
        public DataTable Data { get; set; } = new();

        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

            Data.Columns.Add("Key");
            Data.Columns.Add("*English*");
            Data.Columns.Add("German");
            Data.Columns.Add("Italia");
            Data.Columns.Add("France");

            Data.Rows.Add(new object[] { "England", "England", "England", "Inghilterra", "Angleterre" });
        }
    }
}
