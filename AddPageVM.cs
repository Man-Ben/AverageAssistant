using AverageAssistant;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

public partial class AddPageTools : ObservableObject
{
    [ObservableProperty]
    public string? className;
    [ObservableProperty]
    public string? nrLessons;

    public ObservableCollection<string> gradeSystem { get; } = new() {  "American", "Romanian", "Hungarian" };

    [ObservableProperty]
    public string? selectedGradeSystem;

    [ObservableProperty]
    bool entryVisible = false;

    public void entryVisibilityControl()
    {
        if (selectedGradeSystem == "Romanian") 
                entryVisible = true;
    }

    [RelayCommand]
    public async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}