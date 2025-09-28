namespace AverageAssistant.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;


public partial class AddPageTools : ObservableObject
{
    [ObservableProperty]
    public string? className;
    [ObservableProperty]
    public string? nrLessons;

    public ObservableCollection<string> GradeSystem { get; } = ["American", "Romanian", "Hungarian"];

    [ObservableProperty]
    public string? selectedGradeSystem;

    public bool IsEntryVisible => selectedGradeSystem == "Romanian";

    public void EntryVisibilityControl()
    {
        OnPropertyChanged(nameof(IsEntryVisible));
    }

    [RelayCommand]
    public static async Task ConfirmLeave()
    {
        bool answer = await Shell.Current.DisplayAlert("Warning!", "Are you sure you want to leave the page without saving? Your data might be lost!", "Yes", "No");
        if (answer)
            await Shell.Current.GoToAsync("..");
    }

    /*[RelayCommand]
     public static async Task Add()
     {

     }*/
}