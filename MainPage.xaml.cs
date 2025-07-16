using System.Collections.ObjectModel;

namespace IndicatorViewDataTemplateBug;

public class MyModel
{
    public DateOnly Date { get; set; }
    public int DayOfYear { get; set; }
}

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        var today = DateTime.Today;
        var result = Enumerable.Range(-30, 60).Select(i => DateOnly.FromDateTime(today.AddDays(i))).ToArray();
        foreach (var date in result)
            Dates.Add(new MyModel { Date = date, DayOfYear = date.DayOfYear });
        BindingContext = this;
    }
    public ObservableCollection<MyModel> Dates { get; } = new();
}

