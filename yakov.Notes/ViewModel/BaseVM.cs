using CommunityToolkit.Mvvm.ComponentModel;

namespace yakov.Notes.ViewModel
{
    public class BaseVM : ObservableObject
    {
        public virtual Task OnNavigatingTo(object parameter) => Task.CompletedTask;
    }
}
