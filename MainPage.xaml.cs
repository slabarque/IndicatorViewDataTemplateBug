using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace IndicatorViewDataTemplateBug;

public class MyModel
{
    public DateOnly Date { get; set; }
    public int DayOfYear { get; set; }
}

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    private ObservableCollection<MyModel> dates;

    public event PropertyChangedEventHandler PropertyChanged;

    public MainPage()
    {
        InitializeComponent();
        Init = new Command(() =>
        {
            var today = DateTime.Today;
            var result = Enumerable.Range(-30, 60).Select(i => DateOnly.FromDateTime(today.AddDays(i))).ToArray();
            Dates = new ObservableCollection<MyModel>(result.Select(d=>new MyModel { Date  = d, DayOfYear = d.DayOfYear}).ToList());
        });
        BindingContext = this;
    }

    public ObservableCollection<MyModel> Dates
    {
        get => dates;
        private set { SetProperty(ref dates, value); }
    }

    public ICommand Init { get; private set; }

    bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (Object.Equals(storage, value))
            return false;

        storage = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

