namespace AverageAssistant.ViewModels;

using AverageAssistant.Messengers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Collections.Generic;

public partial class CaViewModel : ObservableObject
{
    public class GradeItem
    {
        public int Value { get; set; }
    }
    [ObservableProperty]
    public ObservableCollection<double> usersGrades = [];

    public CaViewModel()
    {
        WeakReferenceMessenger.Default.Register<GradesListMessage>(this, (r, m) =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                usersGrades.Clear();
                foreach (var grade in m.Value)
                    usersGrades.Add(grade);
            });
        });
    }

    [RelayCommand]
    public async Task GoToAddPage()
    {
        await Shell.Current.GoToAsync(nameof(AddPage));
    }
}