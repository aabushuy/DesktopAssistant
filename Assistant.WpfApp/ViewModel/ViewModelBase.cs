using System.ComponentModel;

namespace Assistant.WpfApp.ViewModel;
 public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void RaisePropertyChanged(string p) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
}